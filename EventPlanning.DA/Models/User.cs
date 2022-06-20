using System;
using System.Collections.Generic;

namespace EventPlanning.DA.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public bool IsVerifiedEmail { get; set; }

        public string HashPassword { get; set; }

        public string Salt { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public bool IsRemove { get; set; }

        public IEnumerable<Case> CasesOwner{ get; set; }

        public IEnumerable<Participation> Participations { get; set; }

        public string VerifiedCode { get; set; }
    }
}
