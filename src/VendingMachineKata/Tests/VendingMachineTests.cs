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
    }
}