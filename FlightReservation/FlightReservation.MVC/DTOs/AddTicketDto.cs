namespace FlightReservation.MVC.DTOs;

public sealed record AddTicketDto(
 Guid RouteId,
 int SeatNumber);

    //Kullanıcının ıd bilsigisni cookiden alabilirim. O yüzden burada kullanıcı ıd sini istemiyorum.

