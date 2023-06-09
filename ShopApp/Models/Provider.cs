using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Provider
    {
        [PrimaryKey, AutoIncrement]
      //  [Key]
      //  [ForeignKey("Product")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }
    }
}
