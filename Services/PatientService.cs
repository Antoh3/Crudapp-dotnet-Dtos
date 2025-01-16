using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Dtos;
using PatientApi.Models;

namespace PatientApi.Services;

public class PatientService
{

    private readonly HospitalDbContext _context;

    public PatientService(HospitalDbContext context){
        _context = context;
    }

    public async Task<List<PatientDto>> GetAllPatients(){
        return await _context.Patients
            .Select(p => new PatientDto
            {
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
            }).ToListAsync();
    }

     public async Task<Patient> GetPatientByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task AddPatient(PatientDto patientDto){
        var patient = new Patient
        {
            Name = patientDto.Name,
            Email = patientDto.Email,
            Phone = patientDto.Phone,
        };

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdatePatient(int id, PatientDto patient)
    {
        var existingPatient = await _context.Patients.FindAsync(id);

        if (existingPatient == null)
        {
            return false;
        }  

        existingPatient.Name = patient.Name;
        existingPatient.Email = patient.Email;
        existingPatient.Phone = patient.Phone;

        await _context.SaveChangesAsync();

        return true;
    } 


    public async Task<bool> DeletePatientAync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return false;
        }

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return true;
    }
}
