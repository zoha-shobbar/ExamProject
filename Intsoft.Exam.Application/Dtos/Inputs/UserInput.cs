﻿using Intsoft.Exam.Application.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Intsoft.Exam.Application.Dtos.Inputs
{
    public class UserInput
    {
        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NationalCodeValidation]
        public string NationalCode { get; set; }

        [PhoneNumberValidation]
        public string PhoneNumber { get; set; }
    }
}
