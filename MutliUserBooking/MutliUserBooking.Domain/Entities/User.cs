﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MutliUserBooking.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Email field
        /// It is should have EmailAddress
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Password field
        /// It should be MinimumLength 8 chars
        /// </summary>
        [Required]
        [PasswordPropertyText]
        [StringLength(250, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
