namespace EventPlanning.DA.Models
{
    public class Participation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }

        public int VerifiedCode { get; set; }

        public string VerifiedPhone { get; set; }

        public bool IsVerified { get; set; }
    }
}
