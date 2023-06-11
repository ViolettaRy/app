using Microsoft.Maui.Controls;
using ShopApp.Models;
using System.Reflection;

namespace ShopApp.Views;

public partial class ProfitPage : ContentPage
{
    private readonly AppRepository _db;
    public ProfitPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetProducts(); 
        GetProfit(); 
        pickerCommand.Items.Add("Добавить Прибыль");
        pickerCommand.Items.Add("Изменить Прибыль");
        pickerCommand.Items.Add("Удалить Прибыль");
    }
    private void GetProducts()
    {
        decimal sumRevenueMonth = 0;
        decimal sumRevenueLastMonth = 0;

        var revenue = _db.GetRevenues();
        foreach (var item in revenue)
        {
            if (item.RevenueTotal != null)
            {
                DateTime date = (DateTime)item.Date;
                if (date.Month == DateTime.Now.Month)
                    sumRevenueMonth += item.RevenueTotal;
                if (date.Month == DateTime.Now.Month - 1)
                    sumRevenueLastMonth += item.RevenueTotal;
            }
        }
        Result2.Text = sumRevenueMonth.ToString();
        Result.Text = sumRevenueLastMonth.ToString();


        decimal sumCostTotalMonth = 0;
        decimal sumCostTotalLastMonth = 0;

        var CostTotal = _db.GetProducts();
        foreach (var item in CostTotal)
        {
            if (item.CostPriceT != null)
            {
                DateTime date = (DateTime)item.Date;
                if (date.Month == DateTime.Now.Month)
                    sumCostTotalMonth += item.CostPriceT;
                if (date.Month == DateTime.Now.Month - 1)
                    sumCostTotalLastMonth += item.CostPriceT;
            }
        }
        Result3.Text = sumCostTotalLastMonth.ToString();
        Result4.Text = sumCostTotalMonth.ToString();
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("Добавить Прибыль"))
            {
                if (!AddRevenue.Text.Equals("") && !AddSalary.Text.Equals("") && !AddExpenses.Text.Equals("") && !AddCostTotal.Text.Equals("") && !AddMonth.Text.Equals(""))
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
                    if (isDigit(AddRevenue.Text) && isDigit(AddSalary.Text) && isDigit(AddExpenses.Text) && isDigit(AddCostTotal.Text))
                    {
                        if (isDigit1(AddRevenue.Text) && isDigit1(AddSalary.Text) && isDigit1(AddExpenses.Text) && isDigit1(AddCostTotal.Text) && isDigit1(AddMonth.Text))
                        {
                            var profit = new Profit()
                            {
                                ProfitMargin = Convert.ToDecimal(AddRevenue.Text) - Convert.ToDecimal(AddSalary.Text) - Convert.ToDecimal(AddExpenses.Text) - Convert.ToDecimal(AddCostTotal.Text),
                                RevenueMonth = Convert.ToDecimal(AddRevenue.Text),
                                Salary = Convert.ToDecimal(AddSalary.Text),
                                Expenses = Convert.ToDecimal(AddExpenses.Text),
                                CostTotal = Convert.ToDecimal(AddCostTotal.Text),
                                Month = AddMonth.Text
                            };
                            _db.CreateProfit(profit);
                            GetProfit();
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела, а также в экономические поля можно вводить только цифры", "Закрыть");
                }
                else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
            }

            if (pickerCommand.SelectedItem.Equals("Изменить Прибыль"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (!AddRevenue.Text.Equals("") && !AddSalary.Text.Equals("") && !AddExpenses.Text.Equals("") && !AddCostTotal.Text.Equals("") && !AddMonth.Text.Equals(""))
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
                        if (isDigit(AddRevenue.Text) && isDigit(AddSalary.Text) && isDigit(AddExpenses.Text) && isDigit(AddCostTotal.Text))
                        {
                            if (isDigit1(AddRevenue.Text) && isDigit1(AddSalary.Text) && isDigit1(AddExpenses.Text) && isDigit1(AddCostTotal.Text) && isDigit1(AddMonth.Text))
                            {
                                if (collectionView.SelectedItem is null)
                                    return;

                                var profit = collectionView.SelectedItem as Profit;
                                if (profit is null)
                                    return;

                                profit.ProfitMargin = Convert.ToDecimal(AddRevenue.Text) - Convert.ToDecimal(AddSalary.Text) - Convert.ToDecimal(AddExpenses.Text) - Convert.ToDecimal(AddCostTotal.Text);
                                profit.RevenueMonth = Convert.ToDecimal(AddRevenue.Text);
                                profit.Salary = Convert.ToDecimal(AddSalary.Text);
                                profit.Expenses = Convert.ToDecimal(AddExpenses.Text);
                                profit.CostTotal = Convert.ToDecimal(AddCostTotal.Text);
                                profit.Month = AddMonth.Text;
                                _db.UpdateProfit(profit);
                                GetProfit();
                            }
                            else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела или запятой", "Закрыть");
                        }
                        else DisplayAlert("Ошибка при заполнении", "Проверьте, чтобы в начале не было пробела, а также в экономические поля можно вводить только цифры", "Закрыть");
                    }
                    else DisplayAlert("Ошибка при заполнении", "Заполните все поля", "Закрыть");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Прибыль", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
            if (pickerCommand.SelectedItem.Equals("Удалить Прибыль"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (collectionView.SelectedItem is null)
                        return;

                    var profit = collectionView.SelectedItem as Profit;
                    if (profit is null)
                        return;
                    _db.DeleteProfit(profit.Id);
                    GetProfit();
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("Ошибка при выборе", "Выберите Ревизию", "Закрыть");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("Ошибка при заполнении", "Выберите действие", "Закрыть");
    }
    private void GetProfit()
    {
        collectionView.ItemsSource = _db.GetProfits();
        AddRevenue.Text = "";
        AddSalary.Text = "";
        AddExpenses.Text = "";
        AddCostTotal.Text = "";
        AddMonth.Text = "";
    }
}