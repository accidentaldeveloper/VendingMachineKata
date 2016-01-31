using System;
using System.Collections;
using System.Collections.Generic;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private decimal valueInserted = 0m;
        private string oneTimeDisplay = null;
        private decimal coinReturnContents;
        private readonly List<Product> stock = new List<Product>();

        public decimal CoinReturnContents => coinReturnContents;

        public string GetDisplay()
        {
            if (oneTimeDisplay != null)
            {
                var temp = oneTimeDisplay;
                oneTimeDisplay = null;
                return temp;
            }

            if (this.valueInserted == 0)
            {
                return "INSERT COIN";
            }

            return this.valueInserted.ToString("C");
        }

        public void AcceptCoin(string coinPhysicalDescription)
        {
            if (coinPhysicalDescription == "quarter")
            {
                this.valueInserted += 0.25m;
            }

            if (coinPhysicalDescription == "dime")
            {
                this.valueInserted += 0.10m;
            }

            if (coinPhysicalDescription == "nickel")
            {
                this.valueInserted += 0.05m;
            }

            // TODO: Handle the need for pennies and other invalid coins to go to the coin return
        }

        public void SelectProduct(Product productName)
        {
            decimal price = GetProductPrice(productName);
            if (!stock.Contains(productName))
            {
                oneTimeDisplay = "SOLD OUT";
                return;
            }

            var difference = valueInserted - price;
            if (difference >= 0)
            {
                stock.Remove(productName);
                oneTimeDisplay = "THANK YOU";
                valueInserted = 0m;
                coinReturnContents += difference;
                return;
            }

            this.oneTimeDisplay = $"PRICE {price:C}";
        }

        public void ReturnCoins()
        {
            coinReturnContents = valueInserted;
            valueInserted = 0m;
        }

        internal void AddStock(Product product)
        {
            this.stock.Add(product);
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

    public enum Product
    {
        Cola,
        Chips,
        Candy
    }
}