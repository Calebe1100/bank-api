namespace bank_api.Dtos.Transactions
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public double Value { get; set; }
        public short Type { get; set; }
        public short Operation { get; set; }
        public string CreationDate { get; set; }
    }
}
