using ShopApp.Models;
using System.Text.RegularExpressions;

namespace ShopApp.Views;

public partial class TemplatePage : ContentPage
{
    private readonly AppRepository _db;
    public TemplatePage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetTemplates();
        pickerCommand.Items.Add("Добавить");
        pickerCommand.Items.Add("Изменить");
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("Добавить"))
            {
                if (picker3.SelectedItem != null && picker1.SelectedItem != null && picker2.SelectedItem != null && picker.SelectedItem != null && !AddProductName.Text.Equals("") && !AddArticle.Text.Equals("") && !AddPrice.Text.Equals("") && !AddCostPrice.Text.Equals(""))
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
                    bool isDigit1(string text)
                    {
                        char[] characters = text.ToCharArray();

                        foreach (char c in characters)
                        {
                            if (!char.IsNumber(c))
                            {
                                return  false;
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
                    if (isDigit(AddCostPrice.Text) && isDigit(AddPrice.Text) && isDigit1(AddArticle.Text))
                    {
                        if (isDigit2(AddArticle.Text) && isDigit2(AddProductName.Text) && isDigit2(AddPrice.Text) && isDigit2(AddCostPrice.Text))
                        {
                            int Article = Convert.ToInt32(AddArticle.Text);
                            if (_db.database.Table<TemplateProduct>().Where(x => x.Article == Article).Any())
                            {
                                DisplayAlert("Ошибка при заполнении", "Одинаковых Артиклов не может быть", "Закрыть");
                            }
                            else
                            {
                                picker.ItemsSource = null;

                                var anim = _db.database.Table<Animal>().Where(x => x.Name.Equals(picker.SelectedItem)).First();
                                int animId = anim.Id;
                                var cat = _db.database.Table<Category>().Where(x => x.Name.Equals(picker1.SelectedItem)).First();
                                int catId = cat.Id;
                                var prov = _db.database.Table<Provider>().Where(x => x.Name.Equals(picker2.SelectedItem)).First();
                                int provId = prov.Id;
                                var mar = _db.database.Table<Mark>().Where(x => x.Name.Equals(picker3.SelectedItem)).First();
                                int marId = mar.Id;

                                var template = new TemplateProduct()
                                {
                                    ProductName = AddProductName.Text,
                                    Article = Convert.ToInt32(AddArticle.Text),
                                    Price = Convert.ToDecimal(AddPrice.Text),
                                    CostPrice = Convert.ToDecimal(AddCostPrice.Text),
                                    AnimalId = animId,
                                    ProviderId = provId,
                                    CategoryId = catId,
                                    MarkId = marId
                                };
                                _db.CreateTemplate(template);
                                GetTemplates();
                            }
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой", "Закрыть");
                    }
                    else if (isDigit1(AddArticle.Text) == false) DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой, а также в Артикуле должны быть только цифры", "Закрыть");
                    else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела, а также в экономические поля можно вводить только цифры и запятую", "Закрыть");
                }
                else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
            }

            if (pickerCommand.SelectedItem.Equals("Изменить"))
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
                        if (isDigit(AddCostPrice.Text) && isDigit(AddPrice.Text) && isDigit1(AddArticle.Text))
                        {
                            if (isDigit2(AddArticle.Text) && isDigit2(AddProductName.Text) && isDigit2(AddPrice.Text) && isDigit2(AddCostPrice.Text))
                            {
                                int Article = Convert.ToInt32(AddArticle.Text);
                                if (_db.database.Table<TemplateProduct>().Where(x => x.Article == Article).Any())
                                {
                                    if (collectionView.SelectedItem is null)
                                        return;

                                    var templateId = _db.database.Table<TemplateProduct>().Where(x => x.Article.Equals(AddArticle.Text)).First();

                                    var template = new TemplateProduct();
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
                                    _db.UpdateTemplate(template);
                                    GetTemplates();
                                }
                                else DisplayAlert("Ошибка при заполнении", "Введите такой же Артикль", "Закрыть");
                            }
                            else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой", "Закрыть");
                        }
                        else if (isDigit1(AddArticle.Text) == false) DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой, а также в Артикуле должны быть только цифры", "Закрыть");
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела, а также в экономические поля можно вводить только цифры и запятую", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Товар", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("Ошибка при заполнении", "Выберите действие", "Закрыть");
    }
    private void GetTemplates()
    {
        collectionView.ItemsSource = _db.GetTemplates();
        picker.Items.Clear();
        picker1.Items.Clear();
        picker2.Items.Clear();
        picker3.Items.Clear();
        AddArticle.Text = "";
        AddProductName.Text = "";
        AddPrice.Text = "";
        AddCostPrice.Text = "";
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
    }
}