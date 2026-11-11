using MediFlow.Api.Data;
using MediFlow.Api.Models;
using MediFlow.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MediFlow.Api.Controllers;
[ApiController,Route("api/auth")]
public class AuthController(AppDbContext db,AuthService auth):ControllerBase{
 private readonly PasswordHasher<AppUser> hasher=new();
 [HttpPost("register")]public async Task<IActionResult> Register(RegisterRequest r){if(string.IsNullOrWhiteSpace(r.ClinicName)||string.IsNullOrWhiteSpace(r.FullName)||string.IsNullOrWhiteSpace(r.Email)||r.Password.Length<10)return BadRequest("Provide clinic, name, email, and a password of at least 10 characters.");var email=r.Email.Trim().ToLowerInvariant();if(await db.Users.AnyAsync(x=>x.Email==email))return Conflict("Email already registered");var clinic=new Clinic{Name=r.ClinicName.Trim()};db.Clinics.Add(clinic);await db.SaveChangesAsync();var user=new AppUser{ClinicId=clinic.Id,Email=email,FullName=r.FullName.Trim(),Role="Admin"};user.PasswordHash=hasher.HashPassword(user,r.Password);db.Users.Add(user);await db.SaveChangesAsync();return Ok(new{token=auth.CreateToken(user),name=user.FullName,role=user.Role});}
 [HttpPost("login")]public async Task<IActionResult> Login(LoginRequest r){var user=await db.Users.SingleOrDefaultAsync(x=>x.Email==r.Email.Trim().ToLowerInvariant());if(user==null||hasher.VerifyHashedPassword(user,user.PasswordHash,r.Password)==PasswordVerificationResult.Failed)return Unauthorized("Invalid credentials");return Ok(new{token=auth.CreateToken(user),name=user.FullName,role=user.Role});}
}
