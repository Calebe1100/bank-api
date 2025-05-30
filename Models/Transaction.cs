namespace bank_api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public int Value  { get; set; }
        public short Type  { get; set; }
    }
}
