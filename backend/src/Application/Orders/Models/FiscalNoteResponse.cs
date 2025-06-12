using CustomerRelationship.Application.Common;
using CustomerRelationship.Domain.Orders;

namespace CustomerRelationship.Application.Orders.Models
{
    public class FiscalNoteResponse : BaseResponse
    {
        public string Code { get; set; }
        public string Series { get; set; }
        public string EmissionDate { get; set; }
        public decimal Value { get; set; }

        public static implicit operator FiscalNoteResponse(FiscalNote note)
        {
            if (note == null) return null;
            return new FiscalNoteResponse
            {
                Code = note.Code,
                Series = note.Series,
                EmissionDate = note.EmissionDate.ToString("yyyy-MM-dd"),
                Value = note.Value
            };
        }
    }
}
