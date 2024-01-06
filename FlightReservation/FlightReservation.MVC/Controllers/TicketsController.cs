using FlightReservation.MVC.DTOs;
using FlightReservation.MVC.Models;
using FlightReservation.MVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace FlightReservation.MVC.Controllers;
[Route("api/[controller]/[Action]")]
[ApiController]
[Authorize]
public sealed class TicketsController(TicketRepository ticketRepository) : ControllerBase
{
    //Kullanıcının aldığı biletleri göstermek istiyoruz.Önce user bilgilerini alıyoruz.
    [HttpGet]
    public IActionResult GetAll()
    {
        string? userId=User.Claims.Where(p=>p.Type == ClaimTypes.NameIdentifier).Select(s=>s.Value).FirstOrDefault();
        
        var response = ticketRepository.GetAll(Guid.Parse(userId ?? ""));
        
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Add(AddTicketDto request)
    {

        string? userId= User.Claims.Where(p=> p.Type == ClaimTypes.NameIdentifier).Select(s=> s.Value).FirstOrDefault();
        if(userId is not null)
        {
            Ticket ticket = new()
            {
                RouteId = request.RouteId,
                SeatNumber = request.SeatNumber,
                UserId = Guid.Parse(userId)
            };
            ticketRepository.Add(ticket);
        }

     return RedirectToAction("Index", "Home");
    }

}
