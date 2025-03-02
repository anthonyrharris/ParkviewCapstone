﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Pulse.EntityFramework.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.UserManagementViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        public string Role { get; set; }

        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "ADMIN", Text = "Admin" },
            new SelectListItem { Value = "PEERCOACH", Text = "Peer Coach" },
            new SelectListItem { Value = "PATIENT", Text = "Patient" }
        };

        public string AssignedPeerCoach { get; set; }

        public List<User> AssignedPatients { get; set; }

        public List<User> UnassignedPatients { get; set; }
    }
}
