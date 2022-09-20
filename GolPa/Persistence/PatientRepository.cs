using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IDbConnection _dbConnection;
        public PatientRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("patientId", id);
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.ExecuteAsync(con, "spDeletePatient", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.QueryAsync<Patient>(con, "spReadAllPatient", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Patient> GetById(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("patientId", id);
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.QuerySingleOrDefaultAsync<Patient>(con, "spReadPatientById", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<int> Insert(Patient entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("docNumber", entity.DocNumber);
            parameters.Add("nationalCode", entity.NationalCode);
            parameters.Add("fName", entity.FName);
            parameters.Add("fName2", entity.FName2);
            parameters.Add("lName", entity.LName);
            parameters.Add("lName2", entity.LName2);
            parameters.Add("shSh", entity.ShSh);
            parameters.Add("insuranceNo", entity.InsuranceNo);
            parameters.Add("isMarried", entity.IsMarried);
            parameters.Add("telephone", entity.Telephone);
            parameters.Add("telephone2", entity.Telephone2);
            parameters.Add("mobile", entity.Mobile);
            parameters.Add("mobile2", entity.Mobile2);
            parameters.Add("description", entity.Description);
            parameters.Add("address", entity.Address);
            parameters.Add("regUser", entity.RegUser);
            parameters.Add("fatherName", entity.FatherName);
            parameters.Add("birthDate", entity.BirthDate);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            using (var con = _dbConnection.GetConnection)
            {
                int res = await SqlMapper.ExecuteAsync(con, "spCreatePatient", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return parameters.Get<int>("forRet");
            }
        }

        public async Task<int> Update(Patient entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("docNumber", entity.DocNumber);
            parameters.Add("patientId", entity.Id);
            parameters.Add("nationalCode", entity.NationalCode);
            parameters.Add("fName", entity.FName);
            parameters.Add("fName2", entity.FName2);
            parameters.Add("lName", entity.LName);
            parameters.Add("lName2", entity.LName2);
            parameters.Add("shSh", entity.ShSh);
            parameters.Add("insuranceNo", entity.InsuranceNo);
            parameters.Add("isMarried", entity.IsMarried);
            parameters.Add("telephone", entity.Telephone);
            parameters.Add("telephone2", entity.Telephone2);
            parameters.Add("mobile", entity.Mobile);
            parameters.Add("mobile2", entity.Mobile2);
            parameters.Add("description", entity.Description);
            parameters.Add("address", entity.Address);
            parameters.Add("fatherName", entity.FatherName);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("birthDate", entity.BirthDate);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            using (var con = _dbConnection.GetConnection)
            {
                int res = await SqlMapper.ExecuteAsync(con, "spUpdatePatient", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return parameters.Get<int>("forRet");

            }
        }
    }
}
