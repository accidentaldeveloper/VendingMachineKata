using System.Linq;
using NUnit.Framework;

namespace VendingMachineKata.Tests
{
    [TestFixture]
    public class VendingMachineTests
    {
        private VendingMachine vendingMachine;

        [SetUp]
        public void CreateAndStockVendingMachine()
        {
            var vm = new VendingMachine();
            vm.AddStock(Product.Candy);
            vm.AddStock(Product.Chips);
            vm.AddStock(Product.Cola);
            this.vendingMachine = vm;
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
        public void WhenInvalidCoinIsInsertedItIsPlacedInTheCoinReturn()
        {
            vendingMachine.AcceptCoin("penny");
            Assert.That(vendingMachine.EmptyCoinReturn(), Is.EquivalentTo(new[] {"penny"}));

            vendingMachine.AcceptCoin("sfdeljknesv");
            vendingMachine.AcceptCoin("penny");
            Assert.That(vendingMachine.EmptyCoinReturn(), Is.EquivalentTo(new[] { "penny", "sfdeljknesv" }));
        }

        [Test]
        public void WhenProductIsSelectedMachineDisplaysProductPriceThenStandardDisplay()
        {
            vendingMachine.SelectProduct(Product.Cola);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("PRICE $1.00"));

            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenProductIsSelectedMachineDisplaysProductPriceThenStandardDisplay2()
        {
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("PRICE $0.50"));

            vendingMachine.SelectProduct(Product.Candy);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("PRICE $0.65"));

            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenProductIsSelectedAndInsufficientValueMachineDisplaysProductPriceThenValueInserted()
        {
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("PRICE $0.50"));

            vendingMachine.SelectProduct(Product.Candy);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("PRICE $0.65"));

            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.25"));
        }

        [Test]
        public void WhenProductIsSelectedAndEqualValueInsertedThenDisplayThankYouAndSetValueToZero()
        {
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("THANK YOU"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenProductIsSelectedAndGreaterValueInsertedThenDisplayThankYouAndSetValueToZero()
        {
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("nickel");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.55"));
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("THANK YOU"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenProductIsSelectedAndGreaterValueInsertedThenExcessGoesToCoinReturn()
        {
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("nickel");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.55"));
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("THANK YOU"));
            var returnedCoins = vendingMachine.EmptyCoinReturn().ToList();

            // I just need to confirm that the correct value was dispensed
            foreach (var returnedCoin in returnedCoins)
            {
                vendingMachine.AcceptCoin(returnedCoin);
            }
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.05"));
        }

        [Test]
        public void WhenCoinsAreInsertedAndReturnCoinsIsSelectedThenCoinValueGoesToCoinReturn()
        {
            vendingMachine.AcceptCoin("nickel");
            vendingMachine.AcceptCoin("dime");
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.15"));

            vendingMachine.ReturnCoins();
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
            var expectedCoins = new[] { "nickel", "dime" };
            Assert.That(vendingMachine.EmptyCoinReturn(), Is.EquivalentTo(expectedCoins));
        }
    }
}