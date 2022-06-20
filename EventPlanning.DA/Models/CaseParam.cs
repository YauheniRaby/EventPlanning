namespace EventPlanning.DA.Models
{
    public class CaseParam
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public Case Case { get; set; }

        public int CaseId { get; set; }
    }
}
