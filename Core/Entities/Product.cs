using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public State? State { get; set; }
        public int StateId { get; set; }
    }
}
