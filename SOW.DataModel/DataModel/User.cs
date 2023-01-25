using Microsoft.AspNetCore.Identity;

namespace SOW.DataModel
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }

        [PersonalData]
        public string? Surname { get; set; }

        [PersonalData]
        public string? Patronymic { get; set; }

    }
}
