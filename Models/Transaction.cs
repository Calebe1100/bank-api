using bank_api.Enums;

namespace bank_api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public double Value  { get; set; }
        public short Type  { get; set; }
    }
}
