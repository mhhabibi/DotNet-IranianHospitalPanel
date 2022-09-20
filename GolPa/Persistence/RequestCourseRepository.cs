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
    public class RequestCourseRepository : IRequestCourseRepository
    {
        private readonly IDbConnection _dbConnection;
        private static Random random = new Random();
        private readonly IHttpContextAccessor _httpContext;
        public RequestCourseRepository(IDbConnection dbConnection, IHttpContextAccessor httpContext)
        {
            _dbConnection = dbConnection;
            _httpContext = httpContext;
        }

        public async Task<int> AcceptCourse(AcceptCourse entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("professorId", entity.ProfessorId);
            parameters.Add("reqId", entity.ReqId);
            parameters.Add("editUser", entity.editUser);
            parameters.Add("description", entity.Description);
            parameters.Add("finishDate", MD.PersianDateTime.Core.PersianDateTime.Parse(entity.FinishDate).ToDateTime());
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await Dapper.SqlMapper.ExecuteAsync(con, "spAccpetRequestCourse", parameters, commandType: System.Data.CommandType.StoredProcedure);
                int ress = parameters.Get<int>("forRet");
                return ress;
            }
        }

        public async Task<int> AddAttach(string fileName, IFormFile file, int reqCourseId, int regUser)
        {
            string path = ""; string root = ""; string fName = "";
            if (file != null)
            {
                root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
                path = Path.Combine(root, "ReqCourse");
                if (System.IO.File.Exists(root))
                {
                    System.IO.File.Delete(root);
                }
                var ext = file.FileName.Split(".");
                string extension = ext.Last().ToLower();
                fName = RandomString(8);
                fName += "." + extension;
                while (System.IO.File.Exists(Path.Combine(path, fName)))
                {
                    fName = RandomString(8);
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("fileName", fileName);
                parameters.Add("reqCourseId", reqCourseId);
                parameters.Add("filePath", fName);
                parameters.Add("regUser", regUser);
                using (var con = _dbConnection.GetConnection)
                {
                    int res = await Dapper.SqlMapper.ExecuteAsync(con, "spAddAttachToReqCourse", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (res == 1)
                    {
                        using (Stream fileStream = new FileStream(Path.Combine(path, fName), FileMode.Create, FileAccess.Write))
                        {
                            using (Stream stream = file.OpenReadStream())
                            {
                                await stream.CopyToAsync(fileStream);


                            }
                        }
                    }
                    return res;
                }
            }
            return 1021;
        }

        public Task<int> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RequestCourseViewModel>> GetAll(bool hasPermission, int curUser)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("hasPermission", hasPermission);
            parameters.Add("curUser", curUser);

            using (var con = _dbConnection.GetConnection)
            {
                var requestCourses = await Dapper.SqlMapper.QueryAsync<RequestCourseViewModel, ForInPer_, ForInGroup, RequestCourseViewModel>(con, "spReadAllRequestCourses",
                    (requestCourseViewModel, per, group) =>
                    {
                        requestCourseViewModel.ReqUser = per; requestCourseViewModel.ReqUser.group = group;
                        return requestCourseViewModel;
                    },
                    splitOn: "PersonId,GroupId",param:parameters
                    , commandType: System.Data.CommandType.StoredProcedure);
                return requestCourses;
            }
        }

        public async Task<int> Insert(ReqCourse entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("regUser", entity.RegUser);
            parameters.Add("departmanId", entity.DepartmanId);
            parameters.Add("courseId", entity.CourseId);
            parameters.Add("needTime", entity.NeedTime);
            parameters.Add("manCapacity", entity.ManCapacity);
            parameters.Add("womanCapacity", entity.WomanCapacity);
            parameters.Add("isInHospital", entity.IsInHospital);
            parameters.Add("isOutSide", entity.IsOutSide);
            parameters.Add("description", entity.Description);
            parameters.Add("performDate",MD.PersianDateTime.Core.PersianDateTime.Parse(entity.PerformDate).ToDateTime());
            parameters.Add("finishDate",MD.PersianDateTime.Core.PersianDateTime.Parse(entity.FinishDate).ToDateTime());
            parameters.Add("eventPlace", entity.EventPlace);
            parameters.Add("personels", JsonSerializer.Serialize(entity.Personels));
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await Dapper.SqlMapper.ExecuteAsync(con, "spCreateRequestCourse", parameters, commandType: System.Data.CommandType.StoredProcedure);
                //if (res == 0)
                int ress = parameters.Get<int>("forRet");
                return ress;
            }

        }

        public Task<int> Update(ReqCourse entity)
        {
            throw new NotImplementedException();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IEnumerable<AttachFile>> GetAllAttach(int reqCourseId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("reqCourseId", reqCourseId);
            using (var con = _dbConnection.GetConnection)
            {
                var attaches = await Dapper.SqlMapper.QueryAsync<AttachFile>(con, "spReadAttachedFiles", parameters, commandType: System.Data.CommandType.StoredProcedure);
                attaches.ToList().ForEach(x=> x.FilePath= $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/ReqCourse/" + x.FilePath);
                return attaches;
            }
        }

        public async Task<int> DeleteAttach(int fileId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("fileId", fileId);
            parameters.Add("filePath", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                using (var multi = SqlMapper.QueryMultiple(con, "spDeleteAttachFile",parameters,commandType:System.Data.CommandType.StoredProcedure))
                {
                    int res = multi.Read<int>().Single();
                    string filePath =(res!=0)? multi.Read<string>().Single().ToString() : null;

                    string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/ReqCourse");
                    if (res == 1)
                        if (System.IO.File.Exists(Path.Combine(path, filePath)))
                            System.IO.File.Delete(Path.Combine(path, filePath));

                    return res;

                }

            }
        }

        public async Task<RequestCourseViewModel> Get(int curUser, int reqId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("curUser", curUser);
            parameters.Add("reqId", reqId);

            using (var con = _dbConnection.GetConnection)
            {
                var requestCourses = await Dapper.SqlMapper.QueryAsync<RequestCourseViewModel, ForInPer_, ForInGroup, RequestCourseViewModel>(con, "spReadRequestCourse",
                    (requestCourseViewModel, per, group) =>
                    {
                        requestCourseViewModel.ReqUser = per; requestCourseViewModel.ReqUser.group = group;
                        return requestCourseViewModel;
                    },
                    splitOn: "PersonId,GroupId", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);
                if (requestCourses.Count() != 0) return requestCourses.First();
                else return null;
            }
        }

        public async Task<IEnumerable<RequestCourseViewModel>> GetAllFinishedRequestCourses(int roleId, int curUser)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("roleId", roleId);
            parameters.Add("curUser", curUser);

            using (var con = _dbConnection.GetConnection)
            {
                var requestCourses = await Dapper.SqlMapper.QueryAsync<RequestCourseViewModel, ForInPer_, ForInGroup, RequestCourseViewModel>(con, "spReadAllFinishedRequestCourses",
                    (requestCourseViewModel, per, group) =>
                    {
                        requestCourseViewModel.ReqUser = per; requestCourseViewModel.ReqUser.group = group;
                        return requestCourseViewModel;
                    },
                    splitOn: "PersonId,GroupId", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);

                if (roleId != 4)
                {
                    parameters = new DynamicParameters();
                    IEnumerable<CoursePersonels> coursePersonels = await SqlMapper.QueryAsync<CoursePersonels>(con, "spReadAllCoursePersonels", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    foreach (var rq in requestCourses)
                        rq.Personels = coursePersonels.Where(x => x.ReqId == rq.ReqId).Select(x => x).ToList();
                }
                return requestCourses;
            }
        }

        public async Task<IEnumerable<RequestCourseViewModel>> GetAllPerformingRequestCourses(int roleId, int curUser)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("roleId", roleId);
            parameters.Add("curUser", curUser);

            using (var con = _dbConnection.GetConnection)
            {
                var requestCourses = await Dapper.SqlMapper.QueryAsync<RequestCourseViewModel, ForInPer_, ForInGroup, RequestCourseViewModel>(con, "spReadAllPerformingRequestCourses",
                    (requestCourseViewModel, per, group) =>
                    {
                        requestCourseViewModel.ReqUser = per; requestCourseViewModel.ReqUser.group = group;
                        return requestCourseViewModel;
                    },
                    splitOn: "PersonId,GroupId", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);

                if (roleId != 4)
                {
                    parameters = new DynamicParameters();
                    IEnumerable<CoursePersonels> coursePersonels = await SqlMapper.QueryAsync<CoursePersonels>(con, "spReadAllCoursePersonels", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    foreach (var rq in requestCourses)
                        rq.Personels = coursePersonels.Where(x => x.ReqId == rq.ReqId).Select(x => x).ToList();
                }
                return requestCourses;
            }
        }

        public async Task<IEnumerable<RequestCourseViewModel>> GetAllNotConfirmedRequestCourses()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                var requestCourses = await Dapper.SqlMapper.QueryAsync<RequestCourseViewModel, ForInPer_, ForInGroup, RequestCourseViewModel>(con, "spReadAllNotConfirmedRequestCourses",
                    (requestCourseViewModel, per, group) =>
                    {
                        requestCourseViewModel.ReqUser = per; requestCourseViewModel.ReqUser.group = group;
                        return requestCourseViewModel;
                    },
                    splitOn: "PersonId,GroupId", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);

                parameters = new DynamicParameters();
                IEnumerable<CoursePersonels> coursePersonels = await SqlMapper.QueryAsync<CoursePersonels>(con, "spReadAllCoursePersonels", parameters, commandType: System.Data.CommandType.StoredProcedure);
                foreach (var rq in requestCourses)
                    rq.Personels = coursePersonels.Where(x => x.ReqId == rq.ReqId).Select(x => x).ToList();
                return requestCourses;
            }
        }
    }
}
