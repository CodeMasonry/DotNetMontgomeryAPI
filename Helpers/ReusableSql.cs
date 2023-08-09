using System.Data;
using Dapper;
using MontgomeryAPI.Data;
using MontgomeryAPI.Models;

namespace MontgomeryAPI.Helpers
{
    public class ReusableSql
    {
        private readonly DataContextDapper _dapper;
        public ReusableSql(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        public bool UpsertUser(User user)
        {
            string sql = @"
            EXEC InfoSchema.spUser_Upsert
                @FirstName = @FirstNameParameter,
                @LastName = @LastNameParameter,
                @Email = @EmailParameter,
                @Gender = @GenderParameter,
                @Active = @ActiveParameter,
                @JobTitleId = @JobTitleIdParameter,
                @SubAgency = @SubAgencyParameter,
                @Salary = @SalaryParameter,
                @UserId = @UserIdParameter";

            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("@FirstNameParameter", user.FirstName, DbType.String);
            sqlParameters.Add("@LastNameParameter", user.LastName, DbType.String);
            sqlParameters.Add("@EmailParameter", user.Email, DbType.String);
            sqlParameters.Add("@GenderParameter", user.Gender, DbType.String);
            sqlParameters.Add("@ActiveParameter", user.Active, DbType.Boolean);
            sqlParameters.Add("@JobTitleIdParameter", user.JobTitleId, DbType.Int32);
            sqlParameters.Add("@SubAgencyParameter", user.SubAgencyId, DbType.Int32);
            sqlParameters.Add("@SalaryParameter", user.Salary, DbType.Decimal);
            sqlParameters.Add("@UserIdParameter", user.UserId, DbType.Int32);

            return _dapper.ExecuteSQLWithParameters(sql, sqlParameters);
        }
    }
}