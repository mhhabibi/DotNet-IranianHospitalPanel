using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Extra;
using ViewModels.ForIn;

namespace Services.ApplicationServices
{
    public class PersonelRepository : IPersonelRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IHttpContextAccessor _httpContext;

        private static Random random = new Random();

        public PersonelRepository(IDbConnection dbConnection, IHttpContextAccessor httpContext)
        {
            _dbConnection = dbConnection;
            _httpContext = httpContext;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personId", id);

            using (var con = _dbConnection.GetConnection)
            {
                using (var multi = SqlMapper.QueryMultiple(con, "spDeletePersonel", parameters, commandType: System.Data.CommandType.StoredProcedure))
                {
                    int res = multi.Read<int>().Single();
                    string filePath = multi.Read<string>().Single().ToString();

                    string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Personels");
                    if (res == 1)
                        if (System.IO.File.Exists(Path.Combine(path, filePath)))
                            System.IO.File.Delete(Path.Combine(path, filePath));

                    return res;

                }
            }
        }

        public async Task<UserViewModel> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personId", id);
            using (var con = _dbConnection.GetConnection)
            {
                var personals = await SqlMapper.QueryAsync<UserViewModel, ForInJob, ForInDepartman, ForInGroup, UserViewModel >(con, "spReadPersonelById",
                    (userViewModel, job, departman, group) =>
                    {
                        userViewModel.job = job; userViewModel.departman = departman;
                        userViewModel.group = group;
                        return userViewModel;
                    },
                    splitOn: "JobId,DepartmanId,GroupId", param:parameters
                    , commandType: System.Data.CommandType.StoredProcedure);

                if(personals.Count() == 1)
                {
                    personals.First().Permissions =(personals.First().IsAdmin || personals.First().RoleId==1)
                        ? SqlMapper.Query<ViewModels.Extra.PersonelPermission>(con, "spReadPersonelPermossions", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList()
                        : null;
                    personals.First().ImagePath = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/Personels/" + personals.First().ImagePath;
                    personals.First().experts = (personals.First().IsDoctor)
                        ?SqlMapper.Query<ForInExpertPersonel>(con, "spReadPersonExperts", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList()
                        : null;
                    return personals.First();
                }
                return null;
            }
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                var cources = await Dapper.SqlMapper.QueryAsync<ProfessorCourses>(con, "spReadAllProfessorCourses", parameters, commandType: System.Data.CommandType.StoredProcedure);
                var permissions = await Dapper.SqlMapper.QueryAsync<PersonelPermision>(con, "spReadAllPersonelPermissions", parameters, commandType: System.Data.CommandType.StoredProcedure);
                var experts = await Dapper.SqlMapper.QueryAsync<ForInExpertPersonel>(con, "spReadAllPersonExperts", parameters, commandType: System.Data.CommandType.StoredProcedure);
                var personels = await Dapper.SqlMapper.QueryAsync<UserViewModel, ForInJob, ForInDepartman, ForInGroup, UserViewModel>(con, "spReadAllPersonels",
                    (userViewModel, job, departman, group) =>
                    {
                        userViewModel.job = job; userViewModel.departman = departman;
                        userViewModel.group = group;
                        return userViewModel;
                    },
                    splitOn: "JobId,DepartmanId,GroupId",param:parameters
                    , commandType: System.Data.CommandType.StoredProcedure);

                foreach(var per in personels)
                {
                    per.ImagePath =(per.ImagePath != null)? $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/Personels/" + per.ImagePath
                        : $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/Personels/avatar.jpg";
                    per.Permissions = permissions.Where(ex => ex.PersonId == per.PersonId).Select(ex =>new PersonelPermission {Permission=ex.Permission,Title=ex.Title }).ToList();
                    if (per.IsProfessor == true)
                        per.courses = cources.Where(ex => ex.PersonId == per.PersonId).Select(ex => ex.CourseId).ToList();
                    if(per.IsDoctor == true)
                        per.experts = experts.Where(ex => ex.PersonId == per.PersonId).Select(ex => ex).ToList();

                }
                return personels;
            }
        }

        public async Task<int> Insert(UserPost entity)
        {
            string path = ""; string root = ""; string fileName = "";
            if (entity.Image != null)
            {
                root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
                path = Path.Combine(root, "Personels");
                if (System.IO.File.Exists(root))
                {
                    System.IO.File.Delete(root);
                }
                var ext = entity.Image.FileName.Split(".");
                string extension = ext.Last().ToLower();
                fileName = RandomString(8);
                fileName += "." + extension;
                while (System.IO.File.Exists(Path.Combine(path, fileName)))
                {
                    fileName = RandomString(8);
                }
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("fname", entity.Fname);
            parameters.Add("lname", entity.Lname);
            parameters.Add("shSh", entity.ShSh);
            parameters.Add("mobile", entity.Mobile);
            parameters.Add("telephone", entity.Telephone);
            parameters.Add("nationalCode", entity.NationalCode);
            parameters.Add("fatherName", entity.FatherName);
            parameters.Add("isDoctor", entity.IsDoctor);
            parameters.Add("isProfessor", entity.IsProfessor);
            parameters.Add("degrees", entity.Degrees);
            parameters.Add("jobId", entity.JobId);
            parameters.Add("groupId", entity.GroupId);
            parameters.Add("departmanId", entity.DepartmanId);
            parameters.Add("userName", entity.UserName);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            parameters.Add("password", passwordHash);
            parameters.Add("regUser", entity.RegUser);
            parameters.Add("doctorExpert", JsonSerializer.Serialize(entity.DoctorExpert));
            parameters.Add("permissions", JsonSerializer.Serialize(entity.Permissions));
            parameters.Add("personelCode", entity.PersonelCode);
            parameters.Add("professorCourses", JsonSerializer.Serialize(entity.ProfessorCourses));
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            entity.ImagePath = (entity.Image != null) ? fileName : "";
            parameters.Add("imagePath", entity.ImagePath);
            parameters.Add("roleId", entity.RoleId);

            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spCreatePersonel", parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (res <= 0)
                    res = parameters.Get<int>("forRet");

                if (entity.Image != null && res>0)
                {
                    
                    using (Stream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write))
                    {
                        using (Stream stream = entity.Image.OpenReadStream())
                        {
                            await stream.CopyToAsync(fileStream);


                        }
                    }
                }
                return res;
            }

        }

        public async Task<int> Update(UserPost entity)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Personels"); string root = ""; string fileName = "";
            if (entity.Image != null)
            {
                var ext = entity.Image.FileName.Split(".");
                string extension = ext.Last().ToLower();
                fileName = RandomString(8);
                fileName += "." + extension;
                while (System.IO.File.Exists(Path.Combine(path, fileName)))
                {
                    fileName = RandomString(8);
                }
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personId", entity.PersonId);
            parameters.Add("fname", entity.Fname);
            parameters.Add("lname", entity.Lname);
            parameters.Add("shSh", entity.ShSh);
            parameters.Add("mobile", entity.Mobile);
            parameters.Add("telephone", entity.Telephone);
            parameters.Add("nationalCode", entity.NationalCode);
            parameters.Add("fatherName", entity.FatherName);
            parameters.Add("isDoctor", entity.IsDoctor);
            parameters.Add("isProfessor", entity.IsProfessor);
            parameters.Add("degrees", entity.Degrees);
            parameters.Add("jobId", entity.JobId);
            parameters.Add("groupId", entity.GroupId);
            parameters.Add("departmanId", entity.DepartmanId);
            parameters.Add("userName", entity.UserName);
            parameters.Add("roleId", 1);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("personelCode", entity.PersonelCode);
            parameters.Add("doctorExpert", JsonSerializer.Serialize(entity.DoctorExpert));
            parameters.Add("professorCourses", JsonSerializer.Serialize(entity.ProfessorCourses));
            parameters.Add("permissions", JsonSerializer.Serialize(entity.Permissions));
            entity.ImagePath = (entity.Image != null) ? fileName : "";
            parameters.Add("imagePath", entity.ImagePath);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                using (var multi = SqlMapper.QueryMultiple(con, "spUpdatePersonel", parameters, commandType: System.Data.CommandType.StoredProcedure))
                {
                    int res = multi.Read<int>().Single();
                    string filePath = multi.Read<string>().Single().ToString();
                    if (res <= 0)
                        res = parameters.Get<int>("forRet");

                    if (System.IO.File.Exists(Path.Combine(path, filePath)))
                        System.IO.File.Delete(Path.Combine(path, filePath));

                    if (entity.Image != null && res > 0)
                    {
                        using (Stream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write))
                        {
                            using (Stream stream = entity.Image.OpenReadStream())
                            {
                                await stream.CopyToAsync(fileStream);
                            }
                        }
                    }
                    return res;
                }
            }
            
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IEnumerable<ProfessorCourses>> GetProfessorByCourseId(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("courseId", id);
            using (var con = _dbConnection.GetConnection)
            {
                var profs = await Dapper.SqlMapper.QueryAsync<ProfessorCourses>(con , "spReadProfessorByCourseId", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return profs;
            }
        }

        public async Task<IEnumerable<dynamic>> GetDepartmansPersonel(int applicantId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("departmanId", applicantId);
            using(var con = _dbConnection.GetConnection)
            {
                return await SqlMapper.QueryAsync<dynamic>(con,"spReadAllDepartmanPersonels",parameters,commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<dynamic>> GetDepartmanManager()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await SqlMapper.QueryAsync<dynamic>(con, "spReadAllDepartmanManager", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
