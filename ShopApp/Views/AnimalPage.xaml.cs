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
                        var animal = new Animal()
                        {
                            Name = Entry.Text
                        };
                        _db.CreateAnimal(animal);
                        GetAnimals();
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

                            var animal = collectionView.SelectedItem as Animal;
                            if (animal is null)
                                return;

                            animal.Name = Entry.Text;
                            _db.UpdateAnimal(animal);
                            GetAnimals();
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела и вводите только БУКВЫ!", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Животного", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("Ошибка при заполнении", "Выберите действие", "Закрыть");
    }
    private void GetAnimals()
    {
        collectionView.ItemsSource = _db.GetAnimals();
        Entry.Text = "";
    }
}