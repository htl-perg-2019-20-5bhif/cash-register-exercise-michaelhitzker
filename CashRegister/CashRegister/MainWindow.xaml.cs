using CashRegisterAPI2.Model;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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


        public MainWindow()
        {
            InitializeComponent();

            Products = new ObservableCollection<Product> {
                new Product()
                {
                    ProductName = "Bananen 1kg",
                    Id= 1,
                    UnitPrice = 2.99
                },
                new Product()
                {
                    ProductName = "Äpfel 1kg",
                    Id= 2,
                    UnitPrice = 1.99
                },
                new Product()
                {
                    ProductName = "Trauben Weiß 500g",
                    Id= 3,
                    UnitPrice = 0.99
                },
                new Product()
                {
                    ProductName = "Himbeeren 125g",
                    Id= 4,
                    UnitPrice =1.29
                }
            };

            ShoppingBasket = new ObservableCollection<CashRegister.Model.ReceiptLine>();

            AddToBasketCommand = new DelegateCommand<int?>(OnAddToBasket);
            this.DataContext = this;
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
