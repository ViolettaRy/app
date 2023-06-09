using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class TemplateProductVisible
    {
        public int Id { get; set; }
        public int? Article { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public string? Animal { get; set; }
        public string? ProviderName { get; set; }
        public string? ProviderAdress { get; set; }
        public string? Category { get; set; }
        public string? Mark { get; set; }
    }
}
