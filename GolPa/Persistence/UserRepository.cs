using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ViewModels.Extra;
using WebApi.Helpers;

namespace Services.ApplicationServices
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AppSettings _appSettings;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public UserRepository(IOptions<AppSettings> appSettings, IDbConnection dbConnection)
        {
            _appSettings = appSettings.Value;
            _dbConnection = dbConnection;
        }

        public User Authenticate(string username, string password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("username", username);
            using(var con = _dbConnection.GetConnection)
            {
                User user =  SqlMapper.QuerySingle<User>(con,"spReadPersonel", parameters,commandType:System.Data.CommandType.StoredProcedure);
                if (user == null)
                    return null;
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.PersonId.ToString()),
                            new Claim(ClaimTypes.Role, user.RoleId.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    user.Token = tokenHandler.WriteToken(token);
                    return user.WithoutPassword();

                }
                else
                {
                    return null;
                }
            }
        }

        public bool CheckPermission(string permission,int personId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personId", personId);
            parameters.Add("permission", permission);
            using(var con = _dbConnection.GetConnection)
            {
                var res = SqlMapper.QuerySingleAsync(con, "spCheckPermission", parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (res == null)
                    return false;
                return true;
            }
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
            //return _users.WithoutPasswords();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
            //var user = _users.FirstOrDefault(x => x.Id == id);
            //return user.WithoutPassword();
        }
    }
}
