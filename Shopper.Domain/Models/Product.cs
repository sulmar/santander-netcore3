using System;

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
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }


}
