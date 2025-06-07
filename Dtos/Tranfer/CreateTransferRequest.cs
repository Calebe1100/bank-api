namespace bank_api.Dtos.Tranfer
{
    public class CreateTransferRequest
    {
        public int TargetClient { get; set; }
        public int TargetAccount { get; set; }
        public double Value { get; set; }
    }
}
