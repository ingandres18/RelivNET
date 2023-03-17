using Microsoft.AspNetCore.Mvc.Rendering;

namespace RelivMVC.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<SelectListItem>? Category { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? State { get; set; }
        public int StateId { get; set; }
    }
   
}
