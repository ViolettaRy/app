using ShopApp.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopApp.Views;

public partial class ProductPage : ContentPage
{
    private readonly AppRepository _db;
    public ProductPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetProducts();
        pickerCommand.Items.Add("�������� �����");
        pickerCommand.Items.Add("������� ���������� � ������");
        pickerCommand.Items.Add("�������� ���������� � ������");
        DatePicker.Date = DateTime.Now;
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("�������� �����"))
            {
                if ((picker4.SelectedItem != null && !AddQuantity.Text.Equals("")) || ((picker4.SelectedItem == null || picker4.SelectedItem.Equals("�� �������")) && picker3.SelectedItem != null && picker1.SelectedItem != null && picker2.SelectedItem != null && picker.SelectedItem != null && !AddProductName.Text.Equals("") && !AddArticle.Text.Equals("") && !AddPrice.Text.Equals("") && !AddCostPrice.Text.Equals("") && !AddQuantity.Text.Equals("")))
                {
                    if ((picker4.SelectedItem == null || picker4.SelectedItem.Equals("�� �������")) && picker3.SelectedItem != null && picker1.SelectedItem != null && picker2.SelectedItem != null && picker.SelectedItem != null && !AddProductName.Text.Equals("") && !AddArticle.Text.Equals("") && !AddPrice.Text.Equals("") && !AddCostPrice.Text.Equals("") && !AddQuantity.Text.Equals(""))
                    {
                        bool isDigit(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsNumber(c) && c != ',')
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        bool isDigit1(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsNumber(c))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        bool isDigit2(string text)
                        {
                            char[] characters = text.ToCharArray();
                            var c = characters[0];
                            if (c != ' ' && c != ',')
                            {
                                return true;
                            }
                            return false;
                        }
                        if (isDigit(AddCostPrice.Text) && isDigit(AddPrice.Text) && isDigit1(AddArticle.Text) && isDigit1(AddQuantity.Text))
                        {
                            if (isDigit2(AddArticle.Text) && isDigit2(AddProductName.Text) && isDigit2(AddPrice.Text) && isDigit2(AddCostPrice.Text) && isDigit2(AddQuantity.Text))
                            {
                                var anim = _db.database.Table<Animal>().Where(x => x.Name.Equals(picker.SelectedItem)).First();
                                int animId = anim.Id;
                                var cat = _db.database.Table<Category>().Where(x => x.Name.Equals(picker1.SelectedItem)).First();
                                int catId = cat.Id;
                                var prov = _db.database.Table<Provider>().Where(x => x.Name.Equals(picker2.SelectedItem)).First();
                                int provId = prov.Id;
                                var mar = _db.database.Table<Mark>().Where(x => x.Name.Equals(picker3.SelectedItem)).First();
                                int marId = mar.Id;

                                var product = new Product()
                                {
                                    ProductName = AddProductName.Text,
                                    Article = Convert.ToInt32(AddArticle.Text),
                                    Price = Convert.ToDecimal(AddPrice.Text),
                                    CostPrice = Convert.ToDecimal(AddCostPrice.Text),
                                    Date = DatePicker.Date,
                                    MarkId = marId,
                                    AnimalId = animId,
                                    ProviderId = provId,
                                    CategoryId = catId,
                                    Quantity = Convert.ToInt32(AddQuantity.Text),
                                    CostPriceT = Convert.ToDecimal(AddCostPrice.Text) * Convert.ToInt32(AddQuantity.Text),
                                    PriceT = Convert.ToDecimal(AddPrice.Text) * Convert.ToInt32(AddQuantity.Text)
                                };
                                _db.CreateProduct(product);
                                GetProducts(); 
                            }
                            else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������", "�������");
                        }
                        else if (isDigit1(AddArticle.Text) == false) DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������, � ����� � �������� ������ ���� ������ �����", "�������");
                        else if (isDigit1(AddQuantity.Text) == false) DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������, � ����� � ���������� ������ ���� ������ �����", "�������");
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������, � ����� � ������������� ���� ����� ������� ������ �����", "�������");
                    }

                    if (picker4.SelectedItem != null && !AddQuantity.Text.Equals(""))
                    {
                        bool isDigit(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsNumber(c))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        bool isDigit2(string text)
                        {
                            char[] characters = text.ToCharArray();
                            var c = characters[0];
                            if (c != ' ' && c != ',')
                            {
                                return true;
                            }
                            return false;
                        }
                        if (isDigit(AddQuantity.Text))
                        {
                            if (isDigit2(AddQuantity.Text))
                            {
                                var resultTemplate = _db.database.Table<TemplateProduct>().Where(x => x.Article.Equals(picker4.SelectedItem)).First();
                                var prod = _db.database.Table<Product>().Where(x => x.Article.Equals(resultTemplate.Article)).FirstOrDefault();

                                var product = new Product()
                                {
                                    ProductName = resultTemplate.ProductName,
                                    Article = Convert.ToInt32(resultTemplate.Article),
                                    Price = resultTemplate.Price,
                                    CostPrice = resultTemplate.CostPrice,
                                    Date = DatePicker.Date,
                                    MarkId = resultTemplate.MarkId,
                                    AnimalId = resultTemplate.AnimalId,
                                    ProviderId = resultTemplate.ProviderId,
                                    CategoryId = resultTemplate.CategoryId,
                                    Quantity = Convert.ToInt32(AddQuantity.Text),
                                    CostPriceT = resultTemplate.CostPrice * Convert.ToInt32(AddQuantity.Text),
                                    PriceT = resultTemplate.Price * Convert.ToInt32(AddQuantity.Text)
                                };
                                _db.CreateProduct(product);
                                GetProducts();
                            }
                            else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������", "�������");
                        }
                        else DisplayAlert("������ ��� ����������", "������� ����������", "�������");
                    }
                }
                else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
            }

            if (pickerCommand.SelectedItem.Equals("�������� ���������� � ������"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (picker3.SelectedItem != null && picker1.SelectedItem != null && picker2.SelectedItem != null && picker.SelectedItem != null && !AddProductName.Text.Equals("") && !AddArticle.Text.Equals("") && !AddPrice.Text.Equals("") && !AddCostPrice.Text.Equals(""))
                    {
                        bool isDigit(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsNumber(c) && c != ',')
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        bool isDigit1(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsNumber(c))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        bool isDigit2(string text)
                        {
                            char[] characters = text.ToCharArray();
                            var c = characters[0];
                            if (c != ' ' && c != ',')
                            {
                                return true;
                            }
                            return false;
                        }
                        if (isDigit(AddCostPrice.Text) && isDigit(AddPrice.Text) && isDigit1(AddQuantity.Text) && isDigit1(AddArticle.Text))
                        {
                            if (isDigit2(AddArticle.Text) && isDigit2(AddProductName.Text) && isDigit2(AddPrice.Text) && isDigit2(AddCostPrice.Text) && isDigit2(AddQuantity.Text))
                            {
                                int Article = Convert.ToInt32(AddArticle.Text);
                                if (_db.database.Table<Product>().Where(x => x.Article == Article).Any())
                                {
                                    if (collectionView.SelectedItem is null)
                                        return;
                                    var ProductId = collectionView.SelectedItem as ProductVisible;
                                    if (ProductId is null)
                                        return;

                                    var templateId = _db.database.Table<Product>().Where(x => x.Id == ProductId.Id).First(); ;
                                    var template = new Product();
                                    if (templateId is null)
                                        return;

                                    var anim = _db.database.Table<Animal>().Where(x => x.Name.Equals(picker.SelectedItem)).First();
                                    int animId = anim.Id;
                                    var cat = _db.database.Table<Category>().Where(x => x.Name.Equals(picker1.SelectedItem)).First();
                                    int catId = cat.Id;
                                    var prov = _db.database.Table<Provider>().Where(x => x.Name.Equals(picker2.SelectedItem)).First();
                                    int provId = prov.Id;
                                    var mar = _db.database.Table<Mark>().Where(x => x.Name.Equals(picker3.SelectedItem)).First();
                                    int marId = mar.Id;

                                    template.Id = templateId.Id;
                                    template.ProductName = AddProductName.Text;
                                    template.Article = Convert.ToInt32(AddArticle.Text);
                                    template.Price = Convert.ToDecimal(AddPrice.Text);
                                    template.CostPrice = Convert.ToDecimal(AddCostPrice.Text);
                                    template.AnimalId = animId;
                                    template.ProviderId = provId;
                                    template.CategoryId = catId;
                                    template.MarkId = marId;
                                    template.Quantity = Convert.ToInt32(AddQuantity.Text);
                                    template.CostPriceT = Convert.ToDecimal(AddCostPrice.Text) * Convert.ToInt32(AddQuantity.Text);
                                    template.PriceT = Convert.ToDecimal(AddPrice.Text) * Convert.ToInt32(AddQuantity.Text);

                                    _db.UpdateProduct(template);
                                    GetProducts();
                                }
                                else DisplayAlert("������ ��� ����������", "������� ����� �� �������", "�������");
                            }
                            else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������", "�������");
                        }
                        else if (isDigit1(AddArticle.Text) == false) DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������, � ����� � �������� ������ ���� ������ �����", "�������");
                        else if (isDigit1(AddQuantity.Text) == false) DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������, � ����� � ���������� ������ ���� ������ �����", "�������");
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������, � ����� � ������������� ���� ����� ������� ������ �����", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("������ ��� ������", "�������� �����", "�������");
                }
                collectionView.SelectedItem = null;
            }

            if (pickerCommand.SelectedItem.Equals("������� ���������� � ������"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (collectionView.SelectedItem is null)
                        return;

                    var product = collectionView.SelectedItem as ProductVisible;
                    if (product is null)
                        return;
                    _db.DeleteProduct(product.Id);
                    GetProducts();
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("������ ��� ������", "�������� �������", "�������");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("������ ��� ����������", "�������� ��������", "�������");
    }
    private void GetProducts()
    {
        collectionView.ItemsSource = _db.GetProducts();
        picker.Items.Clear();
        picker1.Items.Clear();
        picker2.Items.Clear();
        picker3.Items.Clear();
        picker4.Items.Clear();
        AddArticle.Text="";
        AddProductName.Text = "";
        AddPrice.Text = "";
        AddCostPrice.Text = "";
        AddQuantity.Text = "";
        var anim = _db.GetAnimals();
        foreach (var item in anim)
        {
            picker.Items.Add(item.Name);
        }
        var prov = _db.GetProviders();
        foreach (var item in prov)
        {
            picker2.Items.Add(item.Name);
        }
        var cat = _db.GetCategories();
        foreach (var item in cat)
        {
            picker1.Items.Add(item.Name);
        }
        var mar = _db.GetMark();
        foreach (var item in mar)
        {
            picker3.Items.Add(item.Name);
        }
        var temp = _db.GetTemplates();
        picker4.Items.Add("�� �������");
        foreach (var item in temp)
        {
            picker4.Items.Add(item.Article.ToString());
        }
    }
}