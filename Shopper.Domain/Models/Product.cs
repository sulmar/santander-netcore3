using System;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Domain
{
    public abstract class Base
    {
        public string Dump()
        {
            return this.ToString();
        }
    }

    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }
    }

    public class Product : BaseEntity
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        public string Color { get; set; }

        [Required, Range(1, 100)]
        public decimal Price { get; set; }
    }


}
