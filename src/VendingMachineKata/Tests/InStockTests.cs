using NUnit.Framework;

namespace VendingMachineKata.Tests
{
    [TestFixture]
    public class InStockTests
    {
        private VendingMachine vendingMachine;

        [SetUp]
        public void CreateVendingMachine()
        {
            this.vendingMachine = new VendingMachine();
        }

        [Test]
        public void WhenSelectedProductIsOutOfStockThenDisplaySoldOut()
        {
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
        }

        [Test]
        public void WhenSelectedProductIsOutOfStockAndSufficientValueInsertedThenDisplaySoldOut()
        {
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
        }

    }
}