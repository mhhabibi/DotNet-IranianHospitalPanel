using Dapper;
using Domain.Entities.Entities;
//using Domain.Entities.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.ApplicationServices;
using Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebApi.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IAttributeRepository _attributeRepository;

        public int userPermission { get; set; }
        
        public AuthorizeAttribute(Permission.Permisson userPermission)
        {
            this.userPermission = (int)userPermission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null)
            {
                context.Result = new JsonResult(new ForReturn{ Message = "دسترسی ندارید !",Info=null,ResCode=401 }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                var connectionString =(IDbConnection) context.HttpContext.RequestServices
                .GetService(typeof(IDbConnection));
                using(var con = connectionString.GetConnection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    try
                    {
                        parameters.Add("personId", Int32.Parse(user.FindFirst(ClaimTypes.Name)?.Value));
                        parameters.Add("permissionId", this.userPermission);
                        var res = SqlMapper.QueryAsync<dynamic>(con, "spCheckPermission", parameters, commandType: System.Data.CommandType.StoredProcedure).Result.ToList();
                        bool hasPer = (res.First().perCount == 1) ? true : false;
                        if (!hasPer)
                            context.Result = new JsonResult(new ForReturn { Message = "به این بخش دسترسی  ندارید !", Info = null, ResCode = 403 }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                    catch (Exception)
                    {
                        context.Result = new JsonResult(new ForReturn { Message = "توکن شما منقضی شده است !", Info = null, ResCode = 403 }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }

                }

            }
        }

    }
}
