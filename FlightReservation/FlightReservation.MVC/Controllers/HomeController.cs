using Azure.Core;
using FlightReservation.MVC.DTOs;
using FlightReservation.MVC.Repositories;
using System.Security.Claims;

namespace FlightReservation.MVC.Controllers;
[Authorize]
public class HomeController(
    UserRepository userRepository,
    RouteRepository routeRepository,
    TicketRepository ticketRepository) : Controller
{
    public IActionResult Index()
    {
      string userId = User.Claims.FirstOrDefault(p=> p.Type== ClaimTypes.NameIdentifier)!.Value;
      List<string>userRoles=userRepository.GetUserRoleByUserId(Guid.Parse(userId));
        if (userRoles.Contains("Admin"))
        {
            return RedirectToAction("Index", "Admin");
        }
        ViewBag.Date = DateTime.Now;

        var response = ticketRepository.GetAll(Guid.Parse(userId ?? ""));

        RouteDto routeDto = new()
        {
            Tickets = response,
         
        };

        return Ok(response);

        return View(new List<Route>());
    }

    [HttpPost]
    public IActionResult Index(GetRoutesDto request)
    {
        string userId = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)!.Value;

        ViewBag.Departure = request.Departure;
        ViewBag.Arrival = request.Arrival;
        ViewBag.Date = request.Date;
        IEnumerable<Route> routes = routeRepository.GetRoutesByParameter(request);

        var response = ticketRepository.GetAll(Guid.Parse(userId ?? ""));

        RouteDto routeDto = new()
        {
            Routes = routes,
            Tickets = response

        };

        return View(routes);
    }

}
