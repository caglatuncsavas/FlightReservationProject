using FlightReservation.MVC.DTOs;
using FlightReservation.MVC.Repositories;
using System.Security.Claims;

namespace FlightReservation.MVC.Controllers;
[Authorize]
public class HomeController(
    UserRepository userRepository,
    RouteRepository routeRepository) : Controller
{
    public IActionResult Index()
    {
      string userId = User.Claims.FirstOrDefault(p=> p.Type== ClaimTypes.NameIdentifier)!.Value;
      List<string>userRoles=userRepository.GetUserRoleByUserId(Guid.Parse(userId));
        if (userRoles.Contains("Admin"))
        {
            return RedirectToAction("Index", "Admin");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Index(GetRoutesDto request)
    {
        IEnumerable<Route> routes = routeRepository.GetRoutesByParameter(request);

        return View(routes);
    }

}
