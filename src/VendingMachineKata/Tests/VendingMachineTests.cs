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
    }
}