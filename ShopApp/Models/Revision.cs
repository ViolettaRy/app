using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Revision
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal RevisionTotal { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal RevenueMonth { get; set; }
        public decimal Expenses { get; set; }
        public string Month { get; set; }
    }
}
