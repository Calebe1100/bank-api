using bank_api.Dtos.Transactions;

namespace bank_api.Dtos.Releases
{
    public class UpdateTransactionRequest : CreateTransactionRequest
    {
        public int Id { get; set; }
    }
}
