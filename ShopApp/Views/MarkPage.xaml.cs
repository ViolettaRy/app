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
                        var Mark = new Mark()
                        {
                            Name = Entry.Text
                        };
                        _db.CreateMark(Mark);
                        GetMarks();
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

                            var Mark = collectionView.SelectedItem as Mark;
                            if (Mark is null)
                                return;

                            Mark.Name = Entry.Text;
                            _db.UpdateMark(Mark);
                            GetMarks();
                        }
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� ������� � ������� ������ �����!", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("������ ��� ������", "�������� �����", "�������");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("������ ��� ����������", "�������� ��������", "�������");
}
    private void GetMarks()
    {
        collectionView.ItemsSource = _db.GetMark();
        Entry.Text = "";
    }
}