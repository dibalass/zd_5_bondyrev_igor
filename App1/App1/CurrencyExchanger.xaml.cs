using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyExchanger : ContentPage
    {
        public CurrencyExchanger()
        {
            InitializeComponent();
        }

        private void usdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (double.Parse(USD.Text) >= 0)
                {
                    EUR.Text = (double.Parse(USD.Text) * 1.075).ToString();
                }
                else
                {
                    DisplayAlert("Ошибка", "Значение должно быть положительным", "ОК");
                    USD.Text = "";
                    EUR.Text = "..";
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Введи число", "ОК");
                USD.Text = "";
                EUR.Text = "..";
            }
        }
    }
}