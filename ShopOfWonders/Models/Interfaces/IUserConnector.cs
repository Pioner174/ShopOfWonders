using SOW.ShopOfWonders.Models.ViewModels;

namespace SOW.ShopOfWonders.Models.Interfaces
{
    public interface IUserConnector
    {

        /// <summary>
        /// Получение UserViewModel по id User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UserViewModel> GetUserVMForIDAsync(long id);

        /// <summary>
        /// Получение всех UserViewModel 
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<UserViewModel>> GetUsersVMListAsync();

        /// <summary>
        /// Сохранение пользвателя по созданной UserViewModel
        /// </summary>
        /// <param name="userVM"></param>
        public Task<bool> SaveUserVMInBDAsync(UserViewModel userVM);

        /// <summary>
        /// Удаление User по id
        /// </summary>
        /// <param name="id"></param>
        public Task<bool> DeleteUserForIdAsync(long id);

        /// <summary>
        /// Обновленние данных UserVM
        /// </summary>
        /// <param name="userVM"></param>
        public Task<bool> UpdateUserForUserVMAsync(UserViewModel userVM);

    }
}
