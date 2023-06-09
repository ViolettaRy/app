using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Profit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal RevenueMonth { get; set; }
        public decimal Salary { get; set; }
        public decimal Expenses { get; set; }
        public decimal CostTotal { get; set; }
        public string Month { get; set; }
    }
}
