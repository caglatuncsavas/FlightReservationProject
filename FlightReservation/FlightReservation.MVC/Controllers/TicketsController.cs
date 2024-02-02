using FlightReservation.MVC.Models;
using FlightReservation.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightReservation.MVC.Controllers;
[Authorize]
public class TicketsController : Controller
{
    public async Task<IActionResult> Index()
    {
        string userId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)!.Value;
        HttpClient client = new HttpClient();
        var response = await client.GetFromJsonAsync<List<Ticket>>("https://localhost:7249/api/TicketApi/GetAll?userId=" + userId);

        return View(response);
    }
}
