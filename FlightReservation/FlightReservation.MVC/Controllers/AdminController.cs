﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservation.MVC.Controllers;
[Authorize]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
