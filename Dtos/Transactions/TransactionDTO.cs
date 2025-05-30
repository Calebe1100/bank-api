namespace bank_api.Dtos.Transactions
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public int Value { get; set; }
        public short Type { get; set; }
    }
}
