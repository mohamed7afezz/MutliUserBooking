using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutliUserBooking.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public User ReservedBy { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Trip ID is required.")]
        public Trip Trip { get; set; }

        [Required(ErrorMessage = "Reservation date is required.")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime CreationDate { get; set; }

        public string Notes { get; set; }
    }
}
