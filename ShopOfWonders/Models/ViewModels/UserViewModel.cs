using SOW.DataModels;
using System.ComponentModel.DataAnnotations;

namespace SOW.ShopOfWonders.Models.ViewModels
{
    public class UserViewModel
    {

        public string Login { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronomic { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Login = user.UserName;
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
            Patronomic = user.Patronymic;
            PhoneNumber = user.PhoneNumber;
        }
    }
}
