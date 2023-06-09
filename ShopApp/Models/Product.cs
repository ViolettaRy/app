using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Article { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal PriceT { get; set; }
    public decimal CostPrice { get; set; }
    public decimal CostPriceT { get; set; }
    public int Quantity { get; set; }
    public int? AnimalId { get; set; }
    public int? MarkId { get; set; }
    //public Animal? animal  { get; set; }
    public int? ProviderId { get; set; }
  //  public Provider? provider { get; set; }
    public int? CategoryId { get; set; }
   // public Category? category { get; set; }
    public DateTime Date { get; set; }
}
