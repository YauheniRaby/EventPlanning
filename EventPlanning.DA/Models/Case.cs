using System;
using System.Collections.Generic;

namespace EventPlanning.DA.Models
{
    public class Case
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CountMembers { get; set; }

        public int UserId { get; set; }       
       
        public User User { get; set; }

        public bool IsRemove { get; set; }      
        
        public IEnumerable<CaseParam> CaseParams { get; set; }

        public IEnumerable<Participation> Participations { get; set; }        
    }
}
