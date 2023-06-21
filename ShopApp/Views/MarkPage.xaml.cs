using Microsoft.Maui.Controls;
using ShopApp.Models;

namespace ShopApp.Views;

public partial class MarkPage : ContentPage
{
    private readonly AppRepository _db;


    public MarkPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetMarks();
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
                        var Mark = new Mark()
                        {
                            Name = Entry.Text
                        };
                        _db.CreateMark(Mark);
                        GetMarks();
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
                            var Mark = collectionView.SelectedItem as Mark;
                            Mark.Name = Entry.Text;
                            _db.UpdateMark(Mark);
                            GetMarks();
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела и вводите только БУКВЫ!", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Марку", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("Ошибка при заполнении", "Выберите действие", "Закрыть");
}
    private void GetMarks()
    {
        collectionView.ItemsSource = _db.GetMark();
        Entry.Text = "";
    }
}