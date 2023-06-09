using Microsoft.Maui.Controls;
using ShopApp.Models;
using System.Reflection;
namespace ShopApp.Views;

public partial class RevisionPage : ContentPage
{
    private readonly AppRepository _db;
    public RevisionPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetProducts();
        GetRevision();
        pickerCommand.Items.Add("��������� �������");
        pickerCommand.Items.Add("�������� �������");
        pickerCommand.Items.Add("������� �������");
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


        decimal sumPriceTotalMonth = 0;
        decimal sumpriceTotalLastMonth = 0;

        var PriceT = _db.GetProducts();
        foreach (var item in PriceT)
        {
            if (item.PriceT != null)
            {
                DateTime date = (DateTime)item.Date;
                if (date.Month == DateTime.Now.Month)
                    sumPriceTotalMonth += item.PriceT;
                if (date.Month == DateTime.Now.Month - 1)
                    sumpriceTotalLastMonth += item.PriceT;
            }
        }
        Result3.Text = sumpriceTotalLastMonth.ToString();
        Result4.Text = sumPriceTotalMonth.ToString();
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("��������� �������"))
            {
                if (!AddRevenue.Text.Equals("") && !AddPriceTotal.Text.Equals("") && !AddExpenses.Text.Equals("") && !AddMonth.Text.Equals(""))
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
                    if (isDigit(AddRevenue.Text) && isDigit(AddPriceTotal.Text) && isDigit(AddExpenses.Text))
                    {
                        if (isDigit1(AddRevenue.Text) && isDigit1(AddPriceTotal.Text) && isDigit1(AddExpenses.Text) && isDigit1(AddMonth.Text))
                        {
                            var revision = new Revision()
                            {
                                RevisionTotal = Convert.ToDecimal(AddPriceTotal.Text) - Convert.ToDecimal(AddRevenue.Text) - Convert.ToDecimal(AddExpenses.Text),
                                RevenueMonth = Convert.ToDecimal(AddRevenue.Text),
                                Expenses = Convert.ToDecimal(AddExpenses.Text),
                                PriceTotal = Convert.ToDecimal(AddPriceTotal.Text),
                                Month = AddMonth.Text
                            };
                            _db.CreateRevision(revision);
                            GetRevision();
                        }
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������, � ����� � ������������� ���� ����� ������� ������ �����", "�������");
                }
                else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
            }
            if (pickerCommand.SelectedItem.Equals("�������� �������"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (!AddRevenue.Text.Equals("") && !AddPriceTotal.Text.Equals("") && !AddExpenses.Text.Equals("") && !AddMonth.Text.Equals(""))
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
                        if (isDigit(AddRevenue.Text) && isDigit(AddPriceTotal.Text) && isDigit(AddExpenses.Text))
                        {
                            if (isDigit1(AddRevenue.Text) && isDigit1(AddPriceTotal.Text) && isDigit1(AddExpenses.Text) && isDigit1(AddMonth.Text))
                            {
                                if (collectionView.SelectedItem is null)
                                    return;

                                var revision = collectionView.SelectedItem as Revision;
                                if (revision is null)
                                    return;

                                revision.RevisionTotal = Convert.ToDecimal(AddPriceTotal.Text) - Convert.ToDecimal(AddRevenue.Text) - Convert.ToDecimal(AddExpenses.Text);
                                revision.RevenueMonth = Convert.ToDecimal(AddRevenue.Text);
                                revision.Expenses = Convert.ToDecimal(AddExpenses.Text);
                                revision.PriceTotal = Convert.ToDecimal(AddPriceTotal.Text);
                                revision.Month = AddMonth.Text;
                                _db.UpdateRevision(revision);
                                GetRevision();
                            }
                            else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� ��� �������", "�������");
                        }
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������, � ����� � ������������� ���� ����� ������� ������ �����", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("������ ��� ������", "�������� �������", "�������");
                }
                collectionView.SelectedItem = null;
            }

            if (pickerCommand.SelectedItem.Equals("������� �������"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (collectionView.SelectedItem is null)
                        return;

                    var revision = collectionView.SelectedItem as Revision;
                    if (revision is null)
                        return;
                    _db.DeleteRevision(revision.Id);
                    GetRevision();
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
    private void GetRevision()
    {
        collectionView.ItemsSource = _db.GetRevisions();
        AddRevenue.Text = "";
        AddExpenses.Text = "";
        AddPriceTotal.Text = "";
        AddMonth.Text = "";
    }
}