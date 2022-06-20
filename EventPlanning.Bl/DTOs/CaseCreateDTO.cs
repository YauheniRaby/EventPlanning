using System.Collections.Generic;

namespace EventPlanning.Bl.DTOs
{
    public class CaseCreateDTO
    {
        public string Name { get; set; }

        public int? CountMembers { get; set; }

        public IEnumerable<CaseParamDTO> CaseParams { get; set; }
    }
}
