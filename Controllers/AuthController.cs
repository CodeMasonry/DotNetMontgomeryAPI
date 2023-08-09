using System.Data;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MontgomeryAPI.Data;
using MontgomeryAPI.Dto;
using MontgomeryAPI.Helpers;
using MontgomeryAPI.Models;

namespace MontgomeryAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly ReusableSql _reusableSql;
        private readonly AuthHelper _authHelper;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _reusableSql = new ReusableSql(config);
            _authHelper = new AuthHelper(config);
            _mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<UserForRegistrationDto, User>();
            }));
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if(userForRegistration.Password == userForRegistration.PasswordConfirm){
                string sqlCheckUserExists = @"SELECT Email FROM InfoSchema.Auth WHERE Email = '" + userForRegistration.Email + "'";
                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);
                if(!existingUsers.Any()) //Improved performance over "if(existingUsers.Count() == 0)"
                { 

                    UserForLoginDto userForSetPassword = new UserForLoginDto(){
                        Email = userForRegistration.Email,
                        Password = userForRegistration.Password,
                    };

                    if(_authHelper.SetPassword(userForSetPassword))
                    {
                        User user = _mapper.Map<User>(userForRegistration);
                        user.Active = true;

                        if(_reusableSql.UpsertUser(user))
                        {
                            return Ok();
                        }
                        throw new Exception("Failed to add user.");
                    }
                    throw new Exception("Failed to register user."); 
                }
                throw new Exception("User with this email already exists!");
            }
            throw new Exception("Passwords do not match!");
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"EXEC 
            InfoSchema.spLoginConfirmation_Get 
            @Email = '"
            + userForLogin.Email + "'";

            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("@EmailParam", userForLogin.Email, DbType.String);

            UserForLoginConfirmationDto userForLoginConfirmation = _dapper
                .LoadDataSingleWithParameters<UserForLoginConfirmationDto>(sqlForHashAndSalt, sqlParameters);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);

            //if(passwordHash == userForLoginConfirmation.PasswordHash) // Won't work
            for(int i = 0; i < passwordHash.Length; i++){
                if(passwordHash[i] != userForLoginConfirmation.PasswordHash[i]){
                    return StatusCode(401, "Incorrect password!");
                }
            }

            string userIdSql = @"
            SELECT UserId FROM InfoSchema.Users WHERE Email = '"
            + userForLogin.Email + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string,string>{
                {"token", _authHelper.CreateToken(userId)}
            });
        }
    }
}