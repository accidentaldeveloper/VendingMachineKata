﻿using NUnit.Framework;

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

        [Test]
        public void WhenSelectedProductIsNotOutOfStockAndSufficientValueInsertedThenProductIsDispensedAndRemovedFromStock()
        {
            vendingMachine.AddStock(Product.Chips);
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("THANK YOU"));

            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
        }

        [Test]
        public void WhenOtherProductIsInStockThenDisplaySoldOut()
        {
            vendingMachine.AddStock(Product.Cola);
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
        }

        [Test]
        public void WhenSoldOutProductIsSelectedThenDisplayReturnsToDefaultAfterDisplayingSoldOut()
        {
            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("INSERT COIN"));
        }

        [Test]
        public void WhenSoldOutProductIsSelectedThenDisplayReturnsToDefaultAfterDisplayingSoldOut2()
        {
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("quarter");
            vendingMachine.AcceptCoin("nickel");

            vendingMachine.SelectProduct(Product.Chips);
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("SOLD OUT"));
            Assert.That(vendingMachine.GetDisplay(), Is.EqualTo("$0.55"));
        }
    }
}