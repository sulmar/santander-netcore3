﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }
    }

    public enum Gender
    {
        Female,
        Male
    }
}