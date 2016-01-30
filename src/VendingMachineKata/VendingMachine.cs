using System;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private decimal valueInserted = 0m;
        private string oneTimeDisplay = null;

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
            decimal price;
            if (TryGetProductPrice(productName, out price))
            {
                this.oneTimeDisplay = $"PRICE {price:C}";
            }
        }

        private bool TryGetProductPrice(Product productName, out decimal price)
        {
            switch (productName)
            {
                case Product.Cola:
                    price = 1m;
                    return true;
                case Product.Chips:
                    price = .50m;
                    return true;
                case Product.Candy:
                    price = .65m;
                    return true;
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