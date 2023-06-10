using System;
using System.ComponentModel.DataAnnotations;

namespace MutliUserBooking.Domain.Entities
{
    public class Trip
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name field
        /// It should be required
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City name is required.")]
        public string CityName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Url(ErrorMessage = "Invalid image URL.")]
        public string ImageUrl { get; set; }

        public string Content { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime CreationDate { get; set; }
    }
}
