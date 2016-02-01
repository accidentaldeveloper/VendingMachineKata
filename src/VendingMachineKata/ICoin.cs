using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VendingMachineKata
{
    internal interface ICoin
    {
        string Description { get; }
    }

    internal class ValidCoin : ICoin
    {
        public ValidCoin(ValidCoinType validCoinType, string description)
        {
            ValidCoinType = validCoinType;
            Description = description;
        }

        public ValidCoinType ValidCoinType { get; }

        public string Description { get; }

        public decimal Value => GetCoinValue(this.ValidCoinType);

        public static decimal GetCoinValue(ValidCoinType validCoinType)
        {
            switch (validCoinType)
            {
                case ValidCoinType.Quarter:
                    return 0.25m;
                case ValidCoinType.Dime:
                    return 0.10m;
                case ValidCoinType.Nickel:
                    return 0.05m;
                default:
                    throw new ArgumentOutOfRangeException(nameof(validCoinType), validCoinType, null);
            }
        }

        public static bool TryCreate(string description, out ValidCoin validCoin)
        {
            ValidCoinType validCoinType;
            if (TryGetType(description, out validCoinType))
            {
                validCoin = new ValidCoin(validCoinType, description);
                return true;
            }

            validCoin = null;
            return false;
        }

        private static bool TryGetType(string description, out ValidCoinType validCoinType)
        {
            switch (description)
            {
                case "quarter":
                    validCoinType = ValidCoinType.Quarter;
                    return true;
                case "nickel":
                    validCoinType = ValidCoinType.Nickel;
                    return true;
                case "dime":
                    validCoinType = ValidCoinType.Dime;
                    return true;
                default:
                    validCoinType = default(ValidCoinType);
                    return false;
            }
        }
    }

    public enum ValidCoinType
    {
        Quarter,
        Dime,
        Nickel
    }
}