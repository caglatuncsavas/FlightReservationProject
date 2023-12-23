using FlightReservation.MVC.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;

namespace FlightReservation.MVC.Controllers;
public class AccountController : Controller
{
    private LanguageService _localization;

    public AccountController(LanguageService localization)
    {
        _localization = localization;
    }

    public IActionResult Login()
    {
        ViewBag.message = _localization.Getkey("Welcome").Value;
        var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
        return View();
    }
    public IActionResult Register()
    {
        return View();  
    }
}
