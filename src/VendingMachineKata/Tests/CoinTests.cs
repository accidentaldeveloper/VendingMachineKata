using System.Runtime.Remoting;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace VendingMachineKata.Tests
{
    [TestFixture]
    public class CoinTests
    {
        [Test]
        public void CreateReturnsCoinWithAppropriateTypeAndValue()
        {
            ValidCoin validCoin;
            var success = ValidCoin.TryCreate("quarter", out validCoin);
            Assert.That(success, Is.EqualTo(true));
            Assert.That(validCoin.Value, Is.EqualTo(0.25m));
        }
    }
}