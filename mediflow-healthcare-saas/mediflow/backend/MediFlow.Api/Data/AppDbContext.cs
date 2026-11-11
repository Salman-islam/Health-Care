using MediFlow.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace MediFlow.Api.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options){
 public DbSet<Clinic> Clinics=>Set<Clinic>(); public DbSet<AppUser> Users=>Set<AppUser>(); public DbSet<Patient> Patients=>Set<Patient>(); public DbSet<Doctor> Doctors=>Set<Doctor>(); public DbSet<Appointment> Appointments=>Set<Appointment>();
 protected override void OnModelCreating(ModelBuilder b){b.Entity<AppUser>().HasIndex(x=>x.Email).IsUnique(); b.Entity<Appointment>().HasOne(x=>x.Patient).WithMany().HasForeignKey(x=>x.PatientId).OnDelete(DeleteBehavior.Restrict); b.Entity<Appointment>().HasOne(x=>x.Doctor).WithMany().HasForeignKey(x=>x.DoctorId).OnDelete(DeleteBehavior.Restrict); b.Entity<Appointment>().HasIndex(x=>new{x.ClinicId,x.StartsAt});}
}
