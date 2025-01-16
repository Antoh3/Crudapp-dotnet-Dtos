using System;
using Microsoft.EntityFrameworkCore;
using PatientApi.Models;

namespace PatientApi.Data;

public class HospitalDbContext(DbContextOptions<HospitalDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; set; }
}
