
using ShopApp.Models;

namespace ShopApp.Views;

public partial class ProviderPage : ContentPage
{
    private readonly AppRepository _db;


    public ProviderPage(AppRepository db)
    {
        _db = db;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetProviders();
        pickerCommand.Items.Add("��������");
        pickerCommand.Items.Add("��������");
    }
    private void Clicked(object sender, EventArgs e)
    {
        if (pickerCommand.SelectedItem != null)
        {
            if (pickerCommand.SelectedItem.Equals("��������"))
            {
                if (!Entry.Text.Equals("") && !Entry2.Text.Equals(""))
                {
                    bool isDigit1(string text)
                    {
                        char[] characters = text.ToCharArray();

                        foreach (char c in characters)
                        {
                            if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !char.IsSymbol(c))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    bool isDigit(string text)
                    {
                        char[] characters = text.ToCharArray();
                        var c = characters[0];
                        if (c != ' ')
                        {
                            return true;
                        }
                        return false;
                    }
                    if (isDigit1(Entry.Text))
                    {
                        if (isDigit(Entry.Text)&& isDigit(Entry2.Text))
                        {
                            var provider = new Provider()
                            {
                                Name = Entry.Text,
                                Adress = Entry2.Text
                            };
                            _db.CreateProvider(provider);
                            GetProviders();
                        }
                        else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "������� ������ �����! � ��� ����������", "�������");
                }
                else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
            }
            if (pickerCommand.SelectedItem.Equals("��������"))
            {
                if (collectionView.SelectedItem != null)
                {
                    if (!Entry.Text.Equals("") && !Entry2.Text.Equals(""))
                    {
                        bool isDigit1(string text)
                        {
                            char[] characters = text.ToCharArray();

                            foreach (char c in characters)
                            {
                                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !char.IsSymbol(c))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        bool isDigit(string text)
                        {
                            char[] characters = text.ToCharArray();
                            var c = characters[0];
                            if (c != ' ')
                            {
                                return true;
                            }
                            return false;
                        }
                        if (isDigit1(Entry.Text))
                        {
                            if (isDigit(Entry.Text) && isDigit(Entry2.Text))
                            {
                                if (collectionView.SelectedItem is null)
                                    return;

                                var provider = collectionView.SelectedItem as Provider;
                                if (provider is null)
                                    return;

                                provider.Name = Entry.Text;
                                provider.Adress = Entry2.Text;
                                _db.UpdateProvider(provider);
                                GetProviders();
                            }
                            else DisplayAlert("������ ��� ����������", "���������, ����� � ������ �� ���� �������", "�������");
                        }
                        else DisplayAlert("������ ��� ����������", "������� ������ �����! � ��� ����������", "�������");
                    }
                    else DisplayAlert("������ ��� ����������", "��������� ��� ����", "�������");
                }
                if (collectionView.SelectedItem == null)
                {
                    DisplayAlert("������ ��� ������", "�������� ����������", "�������");
                }
                collectionView.SelectedItem = null;
            }
        }
        else DisplayAlert("������ ��� ����������", "�������� ��������", "�������");
    }
    private void GetProviders()
    {
        collectionView.ItemsSource = _db.GetProviders();
        Entry.Text = "";
        Entry2.Text = "";
    }
}