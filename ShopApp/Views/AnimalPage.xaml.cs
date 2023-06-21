using ShopApp.Models;

namespace ShopApp.Views;

public partial class AnimalPage : ContentPage
{
    private readonly AppRepository _db;
    public AnimalPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetAnimals();
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
                        var animal = new Animal()
                        {
                            Name = Entry.Text
                        };
                        _db.CreateAnimal(animal);
                        GetAnimals();
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
                            var animal = collectionView.SelectedItem as Animal;
                            animal.Name = Entry.Text;
                            _db.UpdateAnimal(animal);
                            GetAnimals();
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
    private void GetAnimals()
    {
        collectionView.ItemsSource = _db.GetAnimals();
        Entry.Text = "";
    }
}