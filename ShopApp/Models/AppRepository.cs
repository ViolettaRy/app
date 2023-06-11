using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.System;

namespace ShopApp.Models;

public class AppRepository : IDisposable
{
    public readonly SQLiteConnection database;
    public AppRepository(string dbName)
    {
       var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
        // var dbPath = "C:\Users\Пользователь\source\repos\AppShopp";
        database = new SQLiteConnection(dbPath);
        database.CreateTable<Category>();
        database.CreateTable<Animal>();
        database.CreateTable<Provider>();
        database.CreateTable<Product>();
        database.CreateTable<Mark>();
        database.CreateTable<TemplateProduct>();
        database.CreateTable<Revenue>();
        database.CreateTable<Profit>(); 
        database.CreateTable<Revision>();
    }
    public List<Category> GetCategories()
    {
        return database.Table<Category>().ToList();
    }
    public List<Revenue> GetRevenues()
    {
        return database.Table<Revenue>().ToList();
    }
    public List<Revision> GetRevisions()
    {
        return database.Table<Revision>().ToList();
    }
    public List<Profit> GetProfits()
    {
        return database.Table<Profit>().ToList();
    }
    public List<Animal> GetAnimals()
    {
        return database.Table<Animal>().ToList();
    }
    public List<Mark> GetMark()
    {
        return database.Table<Mark>().ToList();
    }
    public List<Provider> GetProviders()
    {
        return database.Table<Provider>().ToList();
    }
    public List<ProductVisible> GetProducts()
    {
        var entities = database.Table<Product>().ToList();

        var result = new List<ProductVisible>();
        foreach (var item in entities) 
        {   var entity = new ProductVisible();
            entity.Id = item.Id;
            entity.Article = item.Article;
            entity.ProductName = item.ProductName;
            entity.Price = item.Price;
            entity.CostPrice = item.CostPrice;
            entity.CostPriceT = item.CostPriceT;
            entity.PriceT = item.PriceT;
            entity.Quantity = item.Quantity;
            entity.Date = item.Date;
            var anim = database.Table<Animal>().Where(x => x.Id == item.AnimalId).First();
            entity.Animal = anim.Name;
            var cat = database.Table<Category>().Where(x => x.Id == item.CategoryId).First();
            entity.Category = cat.Name;
            var prov = database.Table<Provider>().Where(x => x.Id == item.ProviderId).First();
            entity.ProviderName = prov.Name;
            var mar = database.Table<Mark>().Where(x => x.Id == item.MarkId).First();
            entity.Mark = mar.Name;
            result.Add(entity);
        }
        return result;
    }
    public List<TemplateProductVisible> GetTemplates()
    {
        var entities = database.Table<TemplateProduct>().ToList();

        var result = new List<TemplateProductVisible>();
        foreach (var item in entities)
        {
            var entity = new TemplateProductVisible();
            entity.Id = item.Id;
            entity.Article = item.Article;
            entity.ProductName = item.ProductName;
            entity.Price = item.Price;
            entity.CostPrice = item.CostPrice;
            var anim = database.Table<Animal>().Where(x => x.Id == item.AnimalId).First();
            entity.Animal = anim.Name;
            var cat = database.Table<Category>().Where(x => x.Id == item.CategoryId).First();
            entity.Category = cat.Name;
            var prov = database.Table<Provider>().Where(x => x.Id == item.ProviderId).First();
            entity.ProviderName = prov.Name;
            var mar = database.Table<Mark>().Where(x => x.Id == item.MarkId).First();
            entity.Mark = mar.Name;
            result.Add(entity);
        }
        return result;
    }
    public int CreateTemplate(TemplateProduct templateproduct)
    {
        return database.Insert(templateproduct);
    }
    public int CreateRevenue(Revenue revenue)
    {
        return database.Insert(revenue);
    }
    public int CreateRevision(Revision revision)
    {
        return database.Insert(revision);
    }
    public int CreateProfit(Profit profit)
    {
        return database.Insert(profit);
    }
    public int CreateProduct(Product product)
    {
        return database.Insert(product);
    }
    public int CreateProvider(Provider provider)
    {
        return database.Insert(provider);
    }
    public int CreateCategory(Category category)
    {
        return database.Insert(category);
    }
    public int CreateAnimal(Animal animal)
    {
        return database.Insert(animal);
    }
    public int CreateMark(Mark mark)
    {
        return database.Insert(mark);
    }
    public int UpdateCategory(Category category)
    {
        return database.Update(category);
    }
    public int UpdateRevenue(Revenue revenue)
    {
        return database.Update(revenue);
    }
    public int UpdateRevision(Revision revision)
    {
        return database.Update(revision);
    }
    public int UpdateTemplate(TemplateProduct item)
    {
        var entity = database.Table<TemplateProduct>().Where(x => x.Id == item.Id).First();
        entity.Id = item.Id;
        entity.Article = item.Article;
        entity.ProductName = item.ProductName;
        entity.Price = item.Price;
        entity.CostPrice = item.CostPrice;
        entity.AnimalId = item.AnimalId;
        entity.CategoryId = item.CategoryId;
        entity.ProviderId = item.ProviderId;
        entity.MarkId = item.MarkId;
        return database.Update(entity);
    }
    public int UpdateProduct(Product item)
    {
        var entity = database.Table<Product>().Where(x => x.Id == item.Id).First();
        entity.Id = item.Id;
        entity.Article = item.Article;
        entity.ProductName = item.ProductName;
        entity.Price = item.Price;
        entity.CostPrice = item.CostPrice;
        entity.AnimalId = item.AnimalId;
        entity.CategoryId = item.CategoryId;
        entity.ProviderId = item.ProviderId;
        entity.MarkId = item.MarkId;
        entity.Quantity = item.Quantity;
        entity.Date = entity.Date;
        entity.CostPriceT = item.CostPriceT; 
        entity.PriceT = item.PriceT;
        return database.Update(entity);
    }
    public int UpdateProvider(Provider provider)
    {
        return database.Update(provider);
    }
    public int UpdateMark(Mark mark)
    {
        return database.Update(mark);
    }
    public int UpdateAnimal(Animal animal)
    {
        return database.Update(animal);
    }
    public int UpdateProfit(Profit profit)
    {
        return database.Update(profit);
    }
    public int DeleteRevenue(int id)
    {
        var entities = database.Table<Revenue>().Where(x => x.Id == id).First();
        return database.Delete(entities);
    }
    public int DeleteRevision(int id)
    {
        var entities = database.Table<Revision>().Where(x => x.Id == id).First();
        return database.Delete(entities);
    }
    public int DeleteProduct(int id)
    {
        var entities = database.Table<Product>().Where(x => x.Id == id).First();
        return database.Delete(entities);
    }
    public int DeleteProfit(int id)
    {
        var entities = database.Table<Profit>().Where(x => x.Id == id).First();
        return database.Delete(entities);
    }
    public void Dispose()
    {
        database.Dispose();
    }
}