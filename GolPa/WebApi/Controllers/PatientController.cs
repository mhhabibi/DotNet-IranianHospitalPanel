using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebApi.FIlter;
using WebApi.Helper;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [Authorize(Permission.Permisson.AccessToReadPatients)]
        [HttpGet("GetPatients")]
        public IActionResult GetPatients()
        {
            var patients = _patientRepository.GetAll().Result;
            if (patients.Count() == 0) return NoContent();
            return Ok(patients);
        }

        [Authorize(Permission.Permisson.AccessToReadPatients)]
        [HttpGet("GetPatientById")]
        public IActionResult GetPatientById(long Id)
        {
            var patient = _patientRepository.GetById(Id).Result;
            if (patient == null) return NoContent();
            return Ok(patient);
        }

        [Authorize(Permission.Permisson.AccessToAddPatient)]
        [HttpPost("AddPatient")]
        public IActionResult AddPatient(Patient patient)
        {
            patient.RegUser = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            int res = _patientRepository.Insert(patient).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddPatient)]
        [HttpPut("UpdatePatient")]
        public IActionResult UpdatePatient(Patient patient)
        {
            patient.EditUser = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var res = _patientRepository.Update(patient).Result;
            return Ok(res);
        }

        [Authorize(Permission.Permisson.AccessToAddPatient)]
        [HttpDelete("DeletePatient")]
        public IActionResult DeletePatient(long Id)
        {
            int res = _patientRepository.Delete(Id).Result;
            return Ok(res);
        }

    }
}
