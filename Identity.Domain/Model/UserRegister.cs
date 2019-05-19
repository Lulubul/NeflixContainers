namespace Identity.Domain.Model
{
    public class UserRegister: Entity
    {
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Password { get; set; }
        
        public string PlanId { get; set; }
    }
}
