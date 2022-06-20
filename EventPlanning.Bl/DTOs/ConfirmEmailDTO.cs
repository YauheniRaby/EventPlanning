namespace EventPlanning.Bl.DTOs
{
    public class ConfirmEmailDTO
    {
        public int UserId { get; set; }

        public string VerifiedCode { get; set; }
    }
}
