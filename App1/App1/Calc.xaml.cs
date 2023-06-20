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
        private Entry LoanEntry;
        private Entry MonthEntry;
        private Picker PaymentTypePicker;
        private Slider Slider;
        private Label SliderLabel;
        private Label MonthlyPaymentLabel;
        private Label TotalLabel;
        private Label OverpaymentLabel;

        public Calc (string user)
        {
            InitializeComponent();

            var userLabel = new Label
            {
                Text = $"Welcome back, {user}!",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            LoanEntry = new Entry
            {
                Placeholder = "Сумма кредита",
                Margin = new Thickness(30, 0, 0, 0),
                StyleId = "else"
            };

            MonthEntry = new Entry
            {
                Placeholder = "Срок (месяцев)",
                Margin = new Thickness(30, 0, 0, 0),
                StyleId = "else"
            };

            PaymentTypePicker = new Picker
            {
                Title = "Payment type",
                Margin = new Thickness(30, 0, 0, 0),
                StyleId = "else"
            };

            PaymentTypePicker.Items.Add("Аннуитетный");
            PaymentTypePicker.Items.Add("Дифференцированный");

            Slider = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 5,
                Margin = new Thickness(30, 0, 0, 0)
            };

            MonthlyPaymentLabel = new Label
            {
                Text = "Monthly payment: ",
                Margin = new Thickness(30, 0, 0, 0)
            };

            TotalLabel = new Label
            {
                Text = "Total: ",
                Margin = new Thickness(30, 0, 0, 0)
            };

            SliderLabel = new Label
            {
                Text = $"Selected percent: {Slider.Value}%",
                Margin = new Thickness(30, 0, 5, 0)
            };

            OverpaymentLabel = new Label
            {
                Text = "Overpayment: ",
                Margin = new Thickness(30, 0, 0, 0)
            };

            Slider.ValueChanged += (s, e) =>
            {
                double selectedInterestRate = Slider.Value;
                SliderLabel.Text = $"Selected percent: {selectedInterestRate}%";
                UpdateCalculation(s, e);
            };

            LoanEntry.TextChanged += UpdateCalculation;
            MonthEntry.TextChanged += UpdateCalculation;
            PaymentTypePicker.SelectedIndexChanged += UpdateCalculation;
            Slider.ValueChanged += UpdateCalculation;

            Content = new StackLayout
            {
                Children = { userLabel, LoanEntry, MonthEntry, PaymentTypePicker, Slider, SliderLabel, MonthlyPaymentLabel, TotalLabel, OverpaymentLabel }
            };
        }
        private void UpdateCalculation (object sender, EventArgs e)
        {
            Back(LoanEntry.Text);
            double loanAmount;
            double.TryParse(LoanEntry.Text, out loanAmount);
            int loanTerm;
            int.TryParse(LoanEntry.Text, out loanTerm);
            double interestRate = Slider.Value;
            double monthlyPayment = 0;
            double totalAmount = 0;
            double overpayment = 0;
            switch (PaymentTypePicker.SelectedIndex)
            {
                case 0:
                {
                    double monthlyInterestRate = interestRate / 100 / 12;
                    double factor = Math.Pow(1 + monthlyInterestRate, loanTerm);
                    monthlyPayment = loanAmount * monthlyInterestRate * factor / (factor - 1);
                    totalAmount = monthlyPayment * loanTerm;
                    overpayment = totalAmount - loanAmount;
                }
                break;
                case 1:
                {
                    monthlyPayment = loanAmount / loanTerm;
                    double monthlyInterest = loanAmount * (interestRate / 100) / 12;
                    totalAmount = loanAmount + (monthlyInterest * loanTerm);
                    overpayment = totalAmount - loanAmount;
                }
                break;
                case 2:
                {
                    monthlyPayment = loanAmount;
                    totalAmount = loanAmount;
                    double totalInterest = loanAmount * (interestRate / 100);
                    overpayment = totalInterest;
                }
                break;
            }
            MonthlyPaymentLabel.Text = $"Monthly payment: {monthlyPayment:C}";
            TotalLabel.Text = $"Total: {totalAmount:C}";
            OverpaymentLabel.Text = $"ПеOverpayment: {overpayment:C}";
        }
        private void Back (string x)
        {
            try
            {
                if (long.Parse(x) > 30000000)
                {
                    DisplayAlert("Уведомление", "Сумма кредита не может быть больше 30000000", "ОK");
                }
            } catch
            {
                DisplayAlert("Уведомление", "Слишком большое число", "ОK");
            }
        }
        //public Calc ()
        //{
        //    InitializeComponent();
        //}

        //private void SliderValueChange (object sender, ValueChangedEventArgs e)
        //{
        //    SliderLabel.Text = $"{Slider.Value}%";
        //    try
        //    {
        //        if (LoanEntry.Text != "" && MonthEntry.Text != "")
        //        {
        //            Calculation(LoanEntry.Text, MonthEntry.Text, PaymentTypePicker.SelectedIndex, Slider.Value);
        //        } else
        //        {
        //            MonthlyPaymentLabel.Text = "Ежемесячный платеж: ...";
        //            TotalLabel.Text = "Общая сумма: ...";
        //            OverpaymentLabel.Text = "Переплата: ...";
        //        }
        //    } catch (Exception)
        //    {
        //        MonthlyPaymentLabel.Text = "Ежемесячный платеж:...";
        //        TotalLabel.Text = "Общая сумма:...";
        //        OverpaymentLabel.Text = "Переплата:...";
        //        this.DisplayToastAsync("Введи число");
        //    }
        //}

        //private void Calculation (string Summa, string Month, int TypePayment, double Slider)
        //{
        //    if (Convert.ToDouble(Month) != 0 && Convert.ToDouble(Summa) != 0)
        //    {
        //        switch (TypePayment)
        //        {
        //            case 0:
        //            {
        //                double EveryMonthPay = (Convert.ToDouble(Summa) + Convert.ToDouble(Summa) * Slider / 100) / Convert.ToDouble(Month);

        //                MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {Math.Round(((Convert.ToDouble(Summa) + Convert.ToDouble(Summa) * Slider / 100) / Convert.ToDouble(Month)), 2)}";
        //                TotalLabel.Text = $"Общая сумма: {Math.Round(EveryMonthPay * Convert.ToDouble(Month), 2)}";
        //                OverpaymentLabel.Text = $"Переплата: {Math.Round(EveryMonthPay * int.Parse(Month) - double.Parse(Summa), 2)}";
        //            }
        //            break;
        //            default:
        //            {
        //                MonthlyPaymentLabel.Text = "Ежемесячный платеж:...";
        //                TotalLabel.Text = "Общая сумма:...";
        //                OverpaymentLabel.Text = "Переплата:...";
        //            }
        //            break;
        //        }
        //    } else
        //    {
        //        MonthlyPaymentLabel.Text = "Ежемесячный платеж:...";
        //        TotalLabel.Text = "Общая сумма:...";
        //        OverpaymentLabel.Text = "Переплата:...";
        //    }
        //}
    }
}