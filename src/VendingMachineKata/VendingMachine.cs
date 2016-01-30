namespace VendingMachineKata
{
    public class VendingMachine
    {
        private decimal valueInserted = 0m;

        public string GetDisplay()
        {
            if (this.valueInserted == 0)
            {
                return "INSERT COIN";
            }

            return this.valueInserted.ToString("C");
        }

        public void AcceptCoin(string coinPhysicalDescription)
        {
            this.valueInserted = 0.25m;
        }
    }
}