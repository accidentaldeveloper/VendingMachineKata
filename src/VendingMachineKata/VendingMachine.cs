using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        ////private decimal valueInserted = 0m;
        private string oneTimeDisplay = null;
        private List<ICoin> coinReturnContents = new List<ICoin>();
        private readonly List<Product> stock = new List<Product>();
        private readonly List<ValidCoin> currentSessionCoins = new List<ValidCoin>();

        public IReadOnlyList<ICoin> CoinReturnContents => coinReturnContents.AsReadOnly();

        public string GetDisplay()
        {
            if (oneTimeDisplay != null)
            {
                var temp = oneTimeDisplay;
                oneTimeDisplay = null;
                return temp;
            }

            var currentSessionValue = GetCurrentSessionCoinValue();
            if (currentSessionValue == 0)
            {
                return "INSERT COIN";
            }

            return currentSessionValue.ToString("C");
        }

        public void AcceptCoin(string coinPhysicalDescription)
        {
            ValidCoin validCoin;
            if (ValidCoin.TryCreate(coinPhysicalDescription, out validCoin))
            {
                this.currentSessionCoins.Add(validCoin);
            }
            else
            {
                var invalidCoin = new InvalidCoin(coinPhysicalDescription);
                coinReturnContents.Add(invalidCoin);
            }

        }

        public void SelectProduct(Product productName)
        {
            decimal price = GetProductPrice(productName);
            if (!stock.Contains(productName))
            {
                oneTimeDisplay = "SOLD OUT";
                return;
            }

            var currentSessionValue = GetCurrentSessionCoinValue();
            var difference = currentSessionValue - price;
            if (difference >= 0)
            {
                stock.Remove(productName);
                oneTimeDisplay = "THANK YOU";
                
                // TODO: This "throws away" the inserted coins. We cannot determine the total coins in the machine.
                currentSessionCoins.Clear();
                var changeCoins = new List<ICoin>();
                coinReturnContents.AddRange(changeCoins);
                return;
            }

            this.oneTimeDisplay = $"PRICE {price:C}";
        }

        public void ReturnCoins()
        {
            coinReturnContents.AddRange(this.currentSessionCoins);
            this.currentSessionCoins.Clear();
        }

        internal void AddStock(Product product)
        {
            this.stock.Add(product);
        }

        private decimal GetCurrentSessionCoinValue()
        {
            return this.currentSessionCoins.Sum(x => x.Value);
        }

        private decimal GetProductPrice(Product productName)
        {
            switch (productName)
            {
                case Product.Cola:
                    return 1m;
                case Product.Chips:
                    return .50m;
                case Product.Candy:
                    return .65m;
                default:
                    throw new ArgumentOutOfRangeException(nameof(productName), productName, $"That product is not supported");
            }
        }
    }
}