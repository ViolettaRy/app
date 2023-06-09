using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Revenue
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal RevenueTotal { get; set; }
        public DateTime Date { get; set; }
    }
}
