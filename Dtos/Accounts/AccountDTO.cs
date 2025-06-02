namespace bank_api.Dtos.Accounts
{
    public class AccountDTO
    {
        public int Id { get; internal set; }
        public int IdClient { get; internal set; }
        public string Number { get; internal set; }

        //Campo calculado, valor da conta
        public double Value { get; set; }

    }
}
