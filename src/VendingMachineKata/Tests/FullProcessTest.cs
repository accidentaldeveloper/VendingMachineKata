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

        [Test]
        public void CustomerIsAbleToSelectProductAndReceiveChange()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddStock(Product.Candy);
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("penny");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("dime");
            vendingMachine.AcceptCoin("dime");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.70"));

            vendingMachine.SelectProduct(Product.Candy);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("THANK YOU"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
            var change = vendingMachine.EmptyCoinReturn();
            foreach (var coin in change)
            {
                vendingMachine.AcceptCoin(coin);
            }

            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.05"));
            vendingMachine.SelectProduct(Product.Candy);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.05"));
        }
    }
}