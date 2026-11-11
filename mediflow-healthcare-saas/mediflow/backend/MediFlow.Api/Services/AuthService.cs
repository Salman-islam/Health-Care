using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediFlow.Api.Models;
using Microsoft.IdentityModel.Tokens;
namespace MediFlow.Api.Services;
public class AuthService(IConfiguration config){public string CreateToken(AppUser u){var jwt=config.GetSection("Jwt"); var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!)); var claims=new[]{new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),new Claim("clinicId",u.ClinicId.ToString()),new Claim(ClaimTypes.Role,u.Role),new Claim(ClaimTypes.Name,u.FullName)};var t=new JwtSecurityToken(jwt["Issuer"],jwt["Audience"],claims,expires:DateTime.UtcNow.AddHours(8),signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256));return new JwtSecurityTokenHandler().WriteToken(t);}}
