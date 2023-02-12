    using Microsoft.AspNetCore.Identity;

namespace SOW.DataModels
{
    public class User : IdentityUser<long>
    {
        [PersonalData]
        public string? Name { get; set; }

        [PersonalData]
        public string? Surname { get; set; }

        [PersonalData]
        public string? Patronymic { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public bool IsDeleted { get; set; } = false;

        public IEnumerable<Comment>? Comments { get; set; }
    }
}
