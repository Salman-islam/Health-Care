using System.ComponentModel.DataAnnotations;
namespace MediFlow.Api.Models;
public class Clinic { public int Id {get;set;} [MaxLength(120)] public string Name {get;set;}=""; }
public class AppUser {public int Id {get;set;} public int ClinicId {get;set;} [MaxLength(120)] public string Email {get;set;}=""; public string PasswordHash {get;set;}=""; public string Role {get;set;}="Receptionist"; public string FullName {get;set;}=""; }
public class Patient {public int Id {get;set;} public int ClinicId {get;set;} [Required,MaxLength(120)] public string FullName {get;set;}=""; public DateOnly DateOfBirth {get;set;} public string Phone {get;set;}=""; public string Email {get;set;}=""; }
public class Doctor {public int Id {get;set;} public int ClinicId {get;set;} [Required] public string FullName {get;set;}=""; public string Specialty {get;set;}=""; public string Email {get;set;}=""; }
public class Appointment {public int Id {get;set;} public int ClinicId {get;set;} public int PatientId {get;set;} public Patient? Patient {get;set;} public int DoctorId {get;set;} public Doctor? Doctor {get;set;} public DateTime StartsAt {get;set;} public string Status {get;set;}="Scheduled"; public string? Notes {get;set;} }
public record LoginRequest(string Email,string Password);
public record RegisterRequest(string ClinicName,string FullName,string Email,string Password);
public record AppointmentInput(int PatientId,int DoctorId,DateTime StartsAt,string Status,string? Notes);
