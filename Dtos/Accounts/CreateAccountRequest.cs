namespace bank_api.Dtos.Accounts
{
    public class CreateAccountRequest
    {
        public int IdClient { get; set; }
        public string Number { get; set; }
        public string Password { get; internal set; }
    }
}
