using System;

namespace MutliUserBooking.Application.Features.Reservations.Queries.GetReservations
{
    public class GetReservationsViewModel
    {
        public Guid Id { get; set; }
        public string ReservedBy { get; set; }
        public string CustomerName { get; set; }
        public string Trip { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Notes { get; set; }
    }
}