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

        public void SelectProduct(string productName)
        {
            decimal price;
            if (TryGetProductPrice(productName, out price))
            {
            this.oneTimeDisplay = $"PRICE {price:C}";

            }
        }

        private bool TryGetProductPrice(string productName, out decimal price)
        {
            switch (productName)
            {
                case "cola":
                    price = 1m;
                    return true;
                case "chips":
                    price = .50m;
                    return true;
                case "candy":
                    price = .65m;
                    return true;
            }

            price = 0m;
            return false;
        }
    }
}