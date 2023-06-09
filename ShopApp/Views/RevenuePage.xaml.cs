using ShopApp.Models;
using System.Text.RegularExpressions;

namespace ShopApp.Views;

public partial class RevenuePage : ContentPage
{
    private readonly AppRepository _db;


    public RevenuePage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetRevenue();
        pickerCommand.Items.Add("Добавить");
        pickerCommand.Items.Add("Изменить");
        pickerCommand.Items.Add("Удалить");
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("Добавить"))
            {
                if (!Entry.Text.Equals(""))
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
                        var c = characters[0];
                        if (c != ',' && c != ' ')
                        {
                            return true;
                        }
                        return false;
                    }
                    if (isDigit(Entry.Text))
                    {
                        if (isDigit1(Entry.Text))
                        {
                            var revenue = new Revenue()
                            {
                                RevenueTotal = Convert.ToDecimal(Entry.Text),
                                Date = DatePicker.Date,
                            };
                            _db.CreateRevenue(revenue);
                            GetRevenue();
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела, а также можно вводить только цифры", "Закрыть");
                }
                else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
            }
            if (pickerCommand.SelectedItem.Equals("Изменить"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (!Entry.Text.Equals(""))
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
                            var c = characters[0];
                            if (c != ',' && c != ' ')
                            {
                                return true;
                            }
                            return false;
                        }
                        if (isDigit(Entry.Text))
                        {
                            if (isDigit1(Entry.Text))
                            {
                                if (collectionView.SelectedItem is null)
                                    return;

                                var revenue = collectionView.SelectedItem as Revenue;
                                if (revenue is null)
                                    return;

                                revenue.RevenueTotal = Convert.ToInt32(Entry.Text);
                                revenue.Date = DatePicker.Date;

                                _db.UpdateRevenue(revenue);
                                GetRevenue();
                            }
                            else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой", "Закрыть");
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела, а также можно вводить только цифры", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Выручку", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
            if (pickerCommand.SelectedItem.Equals("Удалить"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (collectionView.SelectedItem is null)
                        return;

                    var revenue = collectionView.SelectedItem as Revenue;
                    if (revenue is null)
                        return;
                    _db.DeleteRevenue(revenue.Id);
                    GetRevenue();
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Выручку", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("Ошибка при заполнении", "Выберите действие", "Закрыть");
    }
    private void GetRevenue()
    {
        collectionView.ItemsSource = _db.GetRevenues();
        Entry.Text = "";
    }
}
