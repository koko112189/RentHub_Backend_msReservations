using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Exceptions.ReservationExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto reservation)
        {
            try
            {
                var _reservation = new Reservation
                {
                    SpaceId = reservation.SpaceId,
                    UserId = reservation.UserId,
                    StartDateTime = reservation.StartDateTime,
                    EndDateTime = reservation.EndDateTime
                };

                await _reservationService.CreateReservationAsync(_reservation);
                return Ok(_reservation);
            }
            catch (InvalidReservationDurationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ReservationConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return reservation == null ? NotFound() : Ok(_mapper.Map<ReservationDto>(reservation));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? spaceId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate
        )
        {
            var reservations = await _reservationService.GetReservationsAsync(userId, spaceId, startDate, endDate);
            var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationsDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
