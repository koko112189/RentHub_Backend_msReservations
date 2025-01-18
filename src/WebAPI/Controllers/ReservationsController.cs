using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using CsvHelper;
using Domain.Entities;
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

        public ApplicantsController(IApplicantService applicantService, IMapper mapper)
        {
            _applicantServiceService = applicantService;
            _mapper = mapper;
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
        public async Task<IActionResult> GetAll()
        {
            var applicants = await _applicantServiceService.GetApplicantsAsync();
            //var applicantDto = _mapper.Map<IEnumerable<ApplicantDto>>(applicants);
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
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ApplicantDto>().ToList();

                    if (records == null || !records.Any())
                    {
                        return BadRequest("El archivo CSV no contiene datos válidos.");
                    }

                    foreach (var applicantDto in records)
                    {
                        var applicant = _mapper.Map<Applicant>(applicantDto);
                        await _applicantServiceService.CreateApplicantAsync(applicant);
                    }

                    return Ok($"Se han cargado {records.Count} registros correctamente.");
                }
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
    }
}
