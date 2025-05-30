namespace bank_api.Dtos.Transactions
{
    public class CreateTransactionRequest
    {
        public int IdAccount { get; set; }
        public int Value { get; set; }
        public short Type { get; set; }
    }
}
