using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profiles.API.Application.Model
{
    public class UserProfile
    {
        public string AvatarUrl { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string MaturityLevel { get; set; }
    }
}
