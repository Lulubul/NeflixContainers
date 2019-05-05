using System;

namespace Identity.API.Application.Model
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlanId { get; set; }
    }
}
