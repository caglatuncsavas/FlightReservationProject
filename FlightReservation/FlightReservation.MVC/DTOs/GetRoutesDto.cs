namespace FlightReservation.MVC.DTOs;

public sealed record GetRoutesDto(
    string Departure,
    string Arrival,
    DateTime Date);
