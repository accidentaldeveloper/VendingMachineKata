using NUnit.Framework;

namespace VendingMachineKata.Tests
{
    [TestFixture]
    public class VendingMachineTests
    {
        [Test]
        public void WhenVendingMachineIsCreatedItDisplaysInsertCoin()
        {
            var vendingMachine = new VendingMachine();
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenQuarterIsInsertedItDisplays25Cents()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AcceptCoin("quarter");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.25"));
        }
    }
}