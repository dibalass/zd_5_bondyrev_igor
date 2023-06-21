using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;


namespace App1
{
    public partial class MainPage : ContentPage
    {
        static public string login;
        static public string password;
        public MainPage()
        {
            InitializeComponent();
        }
        private bool CheckFields(string _login, string _password)
        {
            if (_login == null)
            {
                DisplayAlert("Ошибка", "Заполни логин", "ОК");
                return false;
            }
            if (_password == null)
            {
                DisplayAlert("Ошибка", "Заполни пароль", "ОК");
                return false;
            }
            login = _login;
            password = _password;
            return true;
        }

        private void LogButton_Clicked(object sender, EventArgs e)
        {
            if (CheckFields(Login.Text, Password.Text))
            {
                Navigation.PushAsync(new Calc());
            }
        }
        private void Change_click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CurrencyExchanger());
        }
    }
}
