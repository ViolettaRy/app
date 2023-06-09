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
        pickerCommand.Items.Add("��������");
        pickerCommand.Items.Add("��������");
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("��������"))
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
                    else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� � ������� ������ �����!", "�������");
                }
                else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
            }
            if (pickerCommand.SelectedItem.Equals("��������"))
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
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� � ������� ������ �����!", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("������ ��� ������", "�������� ���������", "�������");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("������ ��� ����������", "�������� ��������", "�������");
    }
    private void GetCategories()
    {
        collectionView.ItemsSource = _db.GetCategories();
        Entry.Text = "";
    }
}