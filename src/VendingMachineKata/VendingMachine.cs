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
            this.oneTimeDisplay = "$1.00";
        }
    }
}