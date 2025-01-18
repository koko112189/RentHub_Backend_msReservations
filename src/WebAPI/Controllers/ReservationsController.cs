using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reservation = await _applicantServiceService.GetApplicantByIdAsync(id);
            return reservation == null ? NotFound() : Ok(_mapper.Map<ApplicantDto>(reservation));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applicants = await _applicantServiceService.GetApplicantsAsync();
            var applicantDto = _mapper.Map<IEnumerable<ApplicantDto>>(applicants);
            return Ok(applicantDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _applicantServiceService.DeleteApplicantAsync(id);
            return NoContent();
        }
    }
}
