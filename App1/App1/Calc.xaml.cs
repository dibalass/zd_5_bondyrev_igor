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
    public partial class Calc :ContentPage
    {
        public Calc ()
        {
            InitializeComponent();
        }
        private void SliderValueChange(object sender, ValueChangedEventArgs e)
        {
            SliderLabel.Text = $"{Slider.Value}%";

            if (Summa.Text != "" && Months.Text != "")
            {
                Calculation(Summa.Text, Months.Text, PaymentType.SelectedIndex, Slider.Value);
            }
            else
            {
                EveryMonth.Text = "Ежемесячный платеж: ...";
                Total.Text = "Общая сумма: ...";
                Overpayment.Text = "Переплата: ...";
            }
        }
        private void Calculation (string summa, string months, int typePayment, double interestRate)
        {

            if (Months.Text != "" && Summa.Text != "")
            {
                try
                {
                    if (Convert.ToDouble(months) > 0 && Convert.ToDouble(summa) > 0)
                    {

                        double monthlyInterestRate = interestRate / 1200;
                        double annuityFactor = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, int.Parse(months)) / (Math.Pow(1 + monthlyInterestRate, int.Parse(months)) - 1);
                        double annuityPayment = Math.Round(double.Parse(summa) * annuityFactor, 2);

                        EveryMonth.Text = $"Ежемесячный платеж: {annuityPayment}";
                        Total.Text = $"Общая сумма: {Math.Round(annuityPayment, 2) * int.Parse(months)}";
                        Overpayment.Text = $"Переплата: {Math.Round(Math.Round(annuityPayment, 2) * int.Parse(months) - Math.Round(double.Parse(summa), 2), 2)}";


                    }
                    else
                    {
                        Months.Text = "";
                        Summa.Text = "";
                        DisplayAlert("Ошибка", "Значения должны быть положительными", "ОК");
                        EveryMonth.Text = "Ежемесячный платеж: ...";
                        Total.Text = "Общая сумма: ...";
                        Overpayment.Text = "Переплата: ...";
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Вы должны вводить только числа", "ОК");
                    Months.Text = "";
                    Summa.Text = "";
                    EveryMonth.Text = "Ежемесячный платеж: ...";
                    Total.Text = "Общая сумма: ...";
                    Overpayment.Text = "Переплата: ...";
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Вы не ввели значения", "ОК");
                Months.Text = "";
                Summa.Text = "";
            }
        }
    }
}