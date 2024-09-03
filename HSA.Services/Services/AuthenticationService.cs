using HSA.Services.Shared;
using HSA.Common.Utilities;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace HSA.Services.Services
{
    public class AuthenticationService : BaseService<LoginSM, User>    
    {
        private readonly AppConfig _appConfig;
        private ILogger _logger;

        public AuthenticationService(AppConfig _appConfig, ILogger logger) : base(_appConfig) { 
            this._appConfig = _appConfig;
            _logger = logger;
        }

        public UserSM? AuthenticateUser(LoginSM loginsm, out string msg, out bool isAuthenticated)
        {
            msg = "Email or Password is incorrect";
            isAuthenticated = false;
            try
            {
                var data = uow.RepositoryAsync<User>().Queryable()
                    .First(user => user.Email == loginsm.Email);
                if (data != null ) {
                    string passwordHash = PasswordHelper.HashPassword(loginsm.Password, data.Salt);
                    if(passwordHash == data.PasswordHash)
                    {
                        msg = "User Authenticated";
                        isAuthenticated = true;
                    }
                    else
                    {
                        msg = "Password is incorrect";
                    }
                    return new UserSM().FromDataModel(data);
                }
                else
                {
                    msg = "Email is not correct";
                    return null;
                }
            }
            catch (Exception ex) {
                _logger.LogError($"CustomLog:AuthenticationService: Error Occured while Authenticate User. Exp: {ex}");
                return null;
            }
        }

        private User getUser(int Id)
        {
            try
            {
                return uow.RepositoryAsync<User>().Queryable().First(u => u.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:AuthenticationService: Error Occured while fetching User. Exp: {ex}");
                throw;
            }
        }

        public string getToken(LoginSM loginsm, UserSM userSm)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.jwt.key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userSm.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{userSm.FirstName} {userSm.LastName}" ),
                new Claim(JwtRegisteredClaimNames.Email, loginsm.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(_appConfig.jwt.Issuer,
                   _appConfig.jwt.Issuer,
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
                //return "";
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:AuthenticationService: Error Occured while Authenticate User. Exp: {ex}");
                throw;
            }
        }
    }
}
