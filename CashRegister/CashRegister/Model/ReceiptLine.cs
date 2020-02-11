using System.ComponentModel;

namespace CashRegister.Model
{
    public class ReceiptLine : INotifyPropertyChanged
    {

        private int ProductIdValue;
        public int ProductId
        {
            get => ProductIdValue;
            set
            {
                ProductIdValue = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        private int AmountValue;
        public int Amount
        {
            get => AmountValue;
            set
            {
                AmountValue = value;
                OnPropertyChanged(nameof(Amount));
            }
        }


        private string ProductNameValue;
        public string ProductName
        {
            get => ProductNameValue;
            set
            {
                ProductNameValue = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }


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
    }
}
