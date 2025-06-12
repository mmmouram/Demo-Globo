using System;

namespace CustomerRelationship.Domain.Orders
{
    public class FiscalNote
    {
        public string Code { get; set; }
        public string Series { get; set; }
        public DateTime EmissionDate { get; set; }
        public decimal Value { get; set; }

        public static FiscalNote Create(string code, string series, DateTime emissionDate, decimal value)
        {
            return new FiscalNote
            {
                Code = code,
                Series = series,
                EmissionDate = emissionDate,
                Value = value
            };
        }
    }
}
