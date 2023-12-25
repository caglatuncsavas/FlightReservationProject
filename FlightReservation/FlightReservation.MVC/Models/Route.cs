namespace FlightReservation.MVC.Models;

public sealed class Route
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlaneId { get; set; } 
    public Plane? Plane { get; set; } 
    public string  Departure { get; set; } = string.Empty;  //Kalkış Noktası
    public string Arrival { get; set; } = string.Empty; // Varış Noktası
    public DateTime DepartureTime { get; set; }  // Kalkış Zamanı
    public DateTime ArrivalTime { get; set; } // Varış Zamanı
}





