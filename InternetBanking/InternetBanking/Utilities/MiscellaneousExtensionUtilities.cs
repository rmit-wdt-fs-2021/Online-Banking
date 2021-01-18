namespace InternetBanking.Utilities
{
    /// <summary>
    /// Code referenced from Tutorial 06.
    /// </summary>
    public static class MiscellaneousExtensionUtilities
    {
        public static bool HasMoreThanNDecimalPlaces(this decimal value, int n) => decimal.Round(value, n) != value;
        public static bool HasMoreThanTwoDecimalPlaces(this decimal value) => value.HasMoreThanNDecimalPlaces(2);
    }
}
