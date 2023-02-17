using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models.Interfaces;
using SOW.ShopOfWonders.Models.ViewModels;

namespace SOW.ShopOfWonders.Models.Services
{

    public class EFUserReposytory : IUserConnector
    {
        //Предоставляет API-интерфейсы для использования БД
        private IdentityContext _context;

        public const string Empty = null;
        public EFUserReposytory(IdentityContext context)
        {
            _context = context;
        }


        public async Task<UserViewModel> GetUserVMForIDAsync(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (user != null)
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Login = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronomic = user.Patronymic,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture
                };
            }
            return null;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersVMListAsync()
        {
            var users = await _context.Users.Where(u => u.IsDeleted == false).ToListAsync();
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

  
        public async Task<bool> SaveUserVMInBDAsync(UserViewModel userVM)
        {
            try
            {
                await _context.Users.AddAsync(new User
                {
                    UserName = userVM.Login,
                    Email = userVM.Email,
                    Name = userVM.Name,
                    Surname = userVM.Surname,
                    Patronymic = userVM.Patronomic,
                    PhoneNumber = userVM.PhoneNumber,
                    ProfilePicture = userVM.ProfilePicture
                });
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateUserForUserVMAsync(UserViewModel userVM)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userVM.Id && x.IsDeleted == false);
            if(user == null)
    {
                return false;
            }

            if (!string.IsNullOrEmpty(userVM.Login) && user.UserName != userVM.Login)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id != userVM.Id && x.UserName == userVM.Login);

                if (existingUser != null)
                {
                    throw new Exception("User with this login already exists");
                }

                user.UserName = userVM.Login;
            }

            if (!string.IsNullOrEmpty(userVM.Email) && user.Email != userVM.Email)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id != userVM.Id && x.Email == userVM.Email);

                if (existingUser != null)
                {
                    throw new Exception("User with this email already exists");
                }

                user.Email = userVM.Email;
            }

            user.Name = !string.IsNullOrEmpty(userVM.Name) ? userVM.Name : user.Name;
            user.Surname = !string.IsNullOrEmpty(userVM.Surname) ? userVM.Surname : user.Surname;
            user.Patronymic = !string.IsNullOrEmpty(userVM.Patronomic) ? userVM.Patronomic : user.Patronymic;
            user.PhoneNumber = !string.IsNullOrEmpty(userVM.PhoneNumber) ? userVM.PhoneNumber : user.PhoneNumber;
            user.ProfilePicture = userVM.ProfilePicture ?? user.ProfilePicture;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserForIdAsync(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (user != null)
            {
                user.IsDeleted = true;
                return true;
            }
            return false;
        }
    }
}
