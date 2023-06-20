using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace App1
{
    public partial class MainPage :ContentPage /*TabbedPage*/
    {
        public MainPage ()
        {
            InitializeComponent();

            var welcomeLbl = new Label
            {
                StyleId = "header",
                Text = "С возвращением",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            var login = new Entry
            {
                Placeholder = "Введи логин",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                StyleId = "entry"
            };

            var password = new Entry
            {
                Placeholder = "Введи пароль",
                IsPassword = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                StyleId = "entry"
            };

            var remember = new RadioButton
            {
                Content = "Запомнить",
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White
            };

            var login_button = new Button
            {
                Text = "Войти",
                StyleId = "button",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var signInLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { remember, login_button, new Label { Text = "Забыли пароль? ", HorizontalOptions = LayoutOptions.End } }
            };

            var errorMsgLbl = new Label
            {
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.Center
            };

            //var moneyBttn = new Button
            //{
            //    Text = "Curreny Exchange",
            //    StyleId = "button",
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
            //moneyBttn.Clicked += (sender, e) =>
            //{
            //    string user = userEntry.Text;
            //    Navigation.PushAsync(new CurrencyExchanger(user));
            //};

            //login_button.Clicked += OnNextPageButtonClicked;
            //async void OnNextPageButtonClicked (object sender, EventArgs e)

            //{

            //    await Navigation.PushAsync(new Calc(login.Text));

            //}

            login_button.Clicked += (sender, e) =>
            {
                if (login.Text == "" || password.Text == ""/*string.IsNullOrWhiteSpace(userEntry.Text) || string.IsNullOrWhiteSpace(psswdEntry.Text)*/)
                {
                    errorMsgLbl.Text = "Заполни поля Логин и Пароль";
                } else
                {
                    Navigation.PushAsync(new NavigationPage(new Calc(login.Text)));
                }
            };

            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(25),
                Children = { welcomeLbl, login, password, login_button, errorMsgLbl, remember, signInLayout/*, moneyBttn*/ }
            };

            Content = stackLayout;
        }
        //public MainPage()
        //{
        //    InitializeComponent();
        //}        
    }
}
