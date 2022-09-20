using Domain.Entities.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Services.ApplicationServices;
using Services.Interfaces;
using System;
using System.IO;
//using System.Data.Common;
using System.Text;
using WebApi.Helper;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => 
            {
                opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problems = new CustomBadRequest(context);
                        return new BadRequestObjectResult(new ForReturn{Message= problems.Message,ResCode=400,Info=null });
                    };
                }).AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                        _ => "فیلد آیدی نمیتواند خالی باشد");
                });

            services.AddCors();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options))
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccessToReadGroups", policy => policy.RequireClaim("AccessToReadGroups"));
                options.AddPolicy("AccessToCourses", policy => policy.RequireClaim("AccessToCourses"));
                options.AddPolicy("AccessToCreateCourse", policy => policy.RequireClaim("AccessToCreateCourse"));
                options.AddPolicy("AccessToCreateCourseAsDepartman", policy => policy.RequireClaim("AccessToCreateCourseAsDepartman"));
                options.AddPolicy("AccessToCreateCourseAsAdmin", policy => policy.RequireClaim("AccessToCreateCourseAsAdmin"));

                options.AddPolicy("AccessToReadCourses", policy => policy.RequireClaim("AccessToReadCourses"));
                options.AddPolicy("AccessToCreateCourseAsAdmin", policy => policy.RequireClaim("AccessToReadCoursesAsAdmin"));
                options.AddPolicy("AccessToCreateCourseAsAdmin", policy => policy.RequireClaim("AccessToReadCoursesAsDepartman"));

                options.AddPolicy("AccessToReadEducationalCalender", policy => policy.RequireClaim("AccessToReadEducationalCalender"));
                options.AddPolicy("AccessToReadEducationalCalenderAsAdmin", policy => policy.RequireClaim("AccessToReadEducationalCalenderAsAdmin"));
                options.AddPolicy("AccessToReadEducationalCalenderAsDepartman", policy => policy.RequireClaim("AccessToReadEducationalCalenderAsDepartman"));
            });

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebApi",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });
            #endregion
            
            services.AddScoped<IPersonelRepository, PersonelRepository>();
            services.AddScoped<IDbConnection, DbConnection>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IDepartmanRepository, DepartmanRepository>();
            services.AddScoped<IJobGroupRepository, JobGroupRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IExpertRepository, ExpertRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IIllnessRepository, IllnessRepository>();
            services.AddScoped<IRequestCourseRepository, RequestCourseRepository>();
            services.AddScoped<IEducationalCalendarRepository, EducationalCalendarRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            //app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                           Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                string message = string.Empty;
                if (exception is ApplicationException)
                    message = exception.Message;
                else
                    message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;

                context.Response.StatusCode = 400;
                var result = JsonConvert.SerializeObject(new { message = message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi");
            });
            #endregion
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
