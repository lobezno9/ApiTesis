using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using ApiRest.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Utilities.Cryptography;
using MethodParameters.MP;
using Business.Interfaces;
using MethodParameters.VM;


namespace ApiRest.Services
{
    public class AuthService : IAuthService
    {
        IUserBO _iUserBO;
        IAppSettingBO _iAppSettingBO;
        public AuthService(IUserBO iUserBO, IAppSettingBO appSettingBO)
        {
            _iUserBO = iUserBO;
            _iAppSettingBO = appSettingBO;
        }

        public LoginOut ValidateLogin(LoginIn loginIn)
        {
            return _iUserBO.Login(loginIn);
        }

        public AuthenticationOut GenerateToken(LoginOut loginOut)
        {
            List<AppSettingVM> listAppSettingVM = _iAppSettingBO.GetAll(new AppSettingVM() { Group = "JWT" });


            DateTime dateNow = DateTime.UtcNow;
            DateTime expire = dateNow.Add(TimeSpan.FromMinutes(Convert.ToInt32(listAppSettingVM.FirstOrDefault(x => x.Key == "ExpireToken").Value)));

            #region Claims
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginOut.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(dateNow).ToUniversalTime().ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
                new Claim("ProfileId", loginOut.ProfileId.ToString()),
                new Claim("CompanyId", loginOut.CompanyId.ToString()),
                new Claim("UserId", loginOut.Id.ToString()),
                new Claim("FullName", loginOut.Firstname + " " + loginOut.LastName),
                new Claim("IsSuperAdmin", loginOut.IsSuperAdmin.ToString()),
            };
            #endregion

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(listAppSettingVM.FirstOrDefault(x => x.Key == "SigningKey").Value)), SecurityAlgorithms.HmacSha256Signature);

            //Configración del token JWT
            JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: listAppSettingVM.FirstOrDefault(x => x.Key == "Issuer").Value,
            audience: listAppSettingVM.FirstOrDefault(x => x.Key == "Audience").Value,
            claims: claims,
            notBefore: dateNow,
            expires: expire,
            signingCredentials: signingCredentials
            );

            return new AuthenticationOut()
            {
                Token = loginOut.IsActive ? new JwtSecurityTokenHandler().WriteToken(jwt) : null,
                Expire = loginOut.IsActive ? expire :DateTime.MinValue,
                IsAuthetnicated = true,
                IsActive = loginOut.IsActive,
                Result = MethodParameters.General.Result.Success
            };

        }


    }
}
