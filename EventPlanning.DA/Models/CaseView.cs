using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.DA.Models
{
    public class CaseView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CountMembers { get; set; }

        public int CountMembersActual { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public IEnumerable<CaseParamView> CaseParams { get; set; }
    }
}
