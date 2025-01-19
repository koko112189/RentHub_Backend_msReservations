using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using CsvHelper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantService _applicantServiceService;
        private readonly IMapper _mapper;
        private readonly ICsvProcessingService _csvProcessingService;

        public ApplicantsController(IApplicantService applicantService, IMapper mapper, ICsvProcessingService csvProcessingService)
        {
            _applicantServiceService = applicantService;
            _mapper = mapper;
            _csvProcessingService = csvProcessingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicantDto applicant)
        {
            try
            {
                var _applicant = _mapper.Map<Applicant>(applicant);

                await _applicantServiceService.CreateApplicantAsync(_applicant);
                return Ok(_applicant);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    return StatusCode(409, "El documento ya existe."); 
                }

                return StatusCode(500, "Ocurrió un error inesperado al guardar el aplicante.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var applicant = await _applicantServiceService.GetApplicantByIdAsync(id);
            return applicant == null ? NotFound() : Ok(_mapper.Map<ApplicantDto>(applicant));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var applicants = await _applicantServiceService.GetApplicantsAsync();
            return Ok(applicants);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _applicantServiceService.DeleteApplicantAsync(id);
            return NoContent();
        }

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo válido.");
            }

            try
            {
                var (successfulRecords, failedRecords) = await _csvProcessingService.ProcessCsvAsync(file);

                if (failedRecords.Any())
                {
                    return Ok(new
                    {
                        Message = "Algunos registros no se pudieron guardar.",
                        SuccessfulCount = successfulRecords.Count,
                        FailedCount = failedRecords.Count,
                        SuccessfulRecords = successfulRecords,
                        FailedRecords = failedRecords.Select(f => new
                        {
                            Record = f.Record,
                            Error = f.Error
                        }).ToList()
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "Todos los registros se guardaron correctamente.",
                        SuccessfulCount = successfulRecords.Count,
                        SuccessfulRecords = successfulRecords
                    });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}
