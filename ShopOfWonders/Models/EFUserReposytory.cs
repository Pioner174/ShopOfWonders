using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models.Interfaces;
using SOW.ShopOfWonders.Models.ViewModels;

namespace SOW.ShopOfWonders.Models
{
    public class EFUserReposytory : IUserConnector
    {
        //Предоставляет API-интерфейсы для использования БД
        private IdentityContext _context;
        //Предоставляет API для управления пользователем в хранилище сохраняемости
        private UserManager<User> _userManger;

        public EFUserReposytory(IdentityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManger = userManager;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {

            }
        }

        public async Task<List<UserViewModel>> GetUsersVM()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Login = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Patronomic = user.Patronymic,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture
            }).ToList();
        }

        public async Task<UserViewModel> GetUserVMForID(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserVM(UserViewModel userVM)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserVM(UserViewModel userVM)
        {
            throw new NotImplementedException();
        }
    }
}
