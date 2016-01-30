using System;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private decimal valueInserted = 0m;
        private string oneTimeDisplay = null;
        private decimal coinReturnContents;

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

        }

        public void SelectProduct(Product productName)
        {
            decimal price = GetProductPrice(productName);
            var difference = valueInserted - price;
            if (difference >= 0)
            {
                oneTimeDisplay = "THANK YOU";
                valueInserted = 0m;
                coinReturnContents += difference;
                return;
            }

            this.oneTimeDisplay = $"PRICE {price:C}";
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
            }

            throw new ArgumentOutOfRangeException("That product is not supported");
        }
    }

    public enum Product
    {
        Cola,
        Chips,
        Candy
    }
}