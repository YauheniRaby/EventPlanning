using System;
using System.Collections.Generic;

namespace EventPlanning.Bl.DTOs
{
    public class CaseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CountMembers { get; set; }

        public int CountMembersActual { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public IEnumerable<CaseParamDTO> CaseParams { get; set; }
    }
}
