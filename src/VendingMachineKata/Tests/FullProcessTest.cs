using NUnit.Framework;

namespace VendingMachineKata.Tests
{
    [TestFixture]
    public class FullProcessTest
    {
        [Test]
        public void CustomerIsAbleToReturnCoinsAfterSelectingSoldOutProduct()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddStock(Product.Candy);
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$1.00"));

            vendingMachine.SelectProduct(Product.Cola);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$1.00"));

            vendingMachine.ReturnCoins();
            Assert.That(vendingMachine.EmptyCoinReturn(), Is.EquivalentTo(new[] { "quarter", "quarter", "quarter", "quarter" }));
        }
    }
}