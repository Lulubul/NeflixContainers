using System;

namespace Profiles.API.Application.Model
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string AvatarUrl { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string MaturityLevel { get; set; }
    }
}
