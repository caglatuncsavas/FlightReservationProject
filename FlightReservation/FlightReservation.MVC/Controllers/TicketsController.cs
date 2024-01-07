using FlightReservation.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightReservation.MVC.Controllers;
[Authorize]
public class TicketsController(
    TicketRepository ticketRepository
    ) : Controller
{
    public IActionResult Index()
    {
        string userId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)!.Value;
        var response = ticketRepository.GetAll(Guid.Parse(userId ?? ""));
        return View(response);
    }
}
