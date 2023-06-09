using ShopApp.Models;

namespace ShopApp.Views;

public partial class CategoryPage : ContentPage
{
    private readonly AppRepository _db;


    public CategoryPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetCategories();
        pickerCommand.Items.Add("Добавить");
        pickerCommand.Items.Add("Изменить");
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("Добавить"))
            {
                if (!Entry.Text.Equals(""))
                {
                    bool isDigit1(string text)
                    {
                        char[] characters = text.ToCharArray();

                        foreach (char c in characters)
                        {
                            if (!char.IsLetter(c))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    if (isDigit1(Entry.Text))
                    {
                        var Category = new Category()
                        {
                        Name = Entry.Text
                        };
                        _db.CreateCategory(Category);
                        GetCategories();
                    }
                    else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела и вводите только БУКВЫ!", "Закрыть");
                }
                else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
            }
            if (pickerCommand.SelectedItem.Equals("Изменить"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (!Entry.Text.Equals(""))
                    {
                        bool isDigit1(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsLetter(c))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        if (isDigit1(Entry.Text))
                        {
                            if (collectionView.SelectedItem is null)
                                return;

                            var Category = collectionView.SelectedItem as Category;
                            if (Category is null)
                                return;

                            Category.Name = Entry.Text;
                            _db.UpdateCategory(Category);
                            GetCategories();
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела и вводите только БУКВЫ!", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Категорию", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("Ошибка при заполнении", "Выберите действие", "Закрыть");
    }
    private void GetCategories()
    {
        collectionView.ItemsSource = _db.GetCategories();
        Entry.Text = "";
    }
}