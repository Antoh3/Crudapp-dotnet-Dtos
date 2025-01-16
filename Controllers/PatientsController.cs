using System;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Dtos;
using PatientApi.Models;
using PatientApi.Services;

namespace PatientApi.Controllers;


[ApiController]
[Route("api/[controller]")]

public class PatientsController : ControllerBase
{
    public readonly PatientService _patientService;

    public PatientsController(PatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        var patients = await _patientService.GetAllPatients();

        return Ok(patients);
    } 

    // Get a patient by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);

        if (patient == null)
        {
            return NotFound(new { message = "Patient not found" });
        }

        return Ok(patient);
    }

    [HttpPost]
    public async Task<OkObjectResult> AddPatient([FromBody] PatientDto patientDto)
    {
        await _patientService.AddPatient(patientDto);
        
        return Ok(new {message = "Patient addedd "});
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatientById(int id, [FromBody] PatientDto patient)
    {
        if (patient == null)
        {
            return BadRequest(new {message = "Patient data not found"});
        }

        var isUpdated =await _patientService.UpdatePatient(id,patient);

        if (!isUpdated)
        {
            return NotFound(new {message = "Patient not found"});
        }

        return Ok(new {message = "Patient updated succesfully"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var isDeleted =await _patientService.DeletePatientAync(id);

        if (!isDeleted)
        {
            return BadRequest(new {message = "Patient not deleted"});
        }

        return Ok(new {message = "Patient deleted successfully"});
    }
}
