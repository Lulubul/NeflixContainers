using Microsoft.WindowsAzure.Storage.Table;

namespace Identity.Infrastructure
{
    public class UserEntity : TableEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PlanId { get; set; }
    }
}