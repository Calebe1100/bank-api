namespace bank_api.Dtos.Transactions
{
    public class CreateTransactionRequest
    {
        public int IdAccount { get; set; }
        public double Value { get; set; }
        public short Type { get; set; }
    }
}
