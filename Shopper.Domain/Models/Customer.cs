using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }        
        public string LastName { get; set; }

        //[EmailAddress]
        //[DisplayName("Adres email")]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }

        //[Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }

    // primitive-obsession
    public class Sensor
    {
        public Voltage Voltage { get; set; }
        public Power Power { get; set; }
    }

    public class Voltage
    {
        public float Value { get; set; }
    }

    public class Power
    {
        public float Value { get; set; }
    }

 
}
