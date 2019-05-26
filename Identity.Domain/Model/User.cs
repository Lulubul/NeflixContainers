using System;

namespace Identity.Domain.Model
{
    public class User: Entity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlanId { get; set; }
        public string Token { get; set; }
    }
}
