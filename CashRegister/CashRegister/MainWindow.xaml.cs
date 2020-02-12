using CashRegisterAPI2.Model;
using Polly;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CashRegister
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<CashRegister.Model.ReceiptLine> ShoppingBasket { get; set; }

        public DelegateCommand<int?> AddToBasketCommand { get; }

        private readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:64196"),
            Timeout = TimeSpan.FromSeconds(500)
        };


        private double TotalPriceValue;
        public double TotalPrice
        {
            get => TotalPriceValue;
            set
            {
                TotalPriceValue = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly AsyncPolicy RetryPolicy = Policy.Handle<HttpRequestException>().RetryAsync(5);


        public MainWindow()
        {
            InitializeComponent();

            ShoppingBasket = new ObservableCollection<CashRegister.Model.ReceiptLine>();

            AddToBasketCommand = new DelegateCommand<int?>(OnAddToBasket);
            this.DataContext = this;

            Loaded += async (_, __) => await this.InitAsync();
        }

        public async Task InitAsync()
        {
            var productsString = await RetryPolicy.ExecuteAndCaptureAsync(
                async () => await HttpClient.GetStringAsync("/api/products"));
            Products = JsonSerializer.Deserialize<ObservableCollection<Product>>(productsString.Result);
        }

        private void OnAddToBasket(int? productId)
        {
            var product = Products.First(p => p.Id == productId);

            var basketItem = ShoppingBasket.FirstOrDefault(p => p.ProductId == productId);
            System.Diagnostics.Debug.WriteLine(basketItem);
            if (basketItem != null)
            {
                System.Diagnostics.Debug.WriteLine("IF");
                basketItem.Amount++;
                basketItem.TotalPrice = Math.Round(product.UnitPrice * basketItem.Amount, 2);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ELSE");
                ShoppingBasket.Add(new CashRegister.Model.ReceiptLine
                {
                    ProductId = product.Id,
                    Amount = 1,
                    ProductName = product.ProductName,
                    TotalPrice = product.UnitPrice
                });
            }

            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            var total = 0.0;
            foreach (var item in ShoppingBasket)
            {
                total += item.TotalPrice;
            }
            TotalPrice = Math.Round(total, 2);
        }
    }
}
