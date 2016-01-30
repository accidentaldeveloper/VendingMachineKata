using NUnit.Framework;

namespace VendingMachineKata.Tests
{
    [TestFixture]
    public class VendingMachineTests
    {
        private VendingMachine vendingMachine;

        [SetUp]
        public void CreateVendingMachine()
        {
            this.vendingMachine = new VendingMachine();
        }

        [Test]
        public void WhenVendingMachineIsCreatedItDisplaysInsertCoin()
        {
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenQuarterIsInsertedItDisplays25Cents()
        {
            vendingMachine.AcceptCoin("quarter");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.25"));
        }

        [Test]
        public void WhenDimeIsInsertedItDisplays10Cents()
        {
            vendingMachine.AcceptCoin("dime");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.10"));
        }

        [Test]
        public void WhenCoinsAreInsertedItDisplaysTotalValue()
        {
            vendingMachine.AcceptCoin("dime");
            vendingMachine.AcceptCoin("quarter");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.35"));
        }

        [Test]
        public void WhenCoinsAreInsertedItDisplaysTotalValue2()
        {
            vendingMachine.AcceptCoin("dime");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("nickel");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.40"));
        }

        [Test]
        public void WhenPennyIsInsertedDisplayValueDoesNotChange()
        {
            vendingMachine.AcceptCoin("penny");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));

            vendingMachine.AcceptCoin("dime");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.10"));

            vendingMachine.AcceptCoin("penny");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.10"));
        }

        [Test]
        public void WhenProductIsSelectedMachineDisplaysProductPriceThen()
        {
            vendingMachine.SelectProduct("cola");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$1.00"));
        }
    }
}