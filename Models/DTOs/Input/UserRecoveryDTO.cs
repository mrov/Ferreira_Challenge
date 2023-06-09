﻿using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Input
{
    public class UserRecoveryRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
