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
        public Task<UserViewModel> GetUserVMForID(int id);

        /// <summary>
        /// Получение всех UserViewModel 
        /// </summary>
        /// <returns></returns>
        public Task<List<UserViewModel>> GetUsersVM();

        /// <summary>
        /// Сохранение пользвателя по созданной UserViewModel
        /// </summary>
        /// <param name="userVM"></param>
        public Task SaveUserVM(UserViewModel userVM);

        /// <summary>
        /// Удаление User по id
        /// </summary>
        /// <param name="id"></param>
        public Task DeleteUser(int id);

        /// <summary>
        /// Обновленние данных UserVM
        /// </summary>
        /// <param name="userVM"></param>
        public Task UpdateUserVM(UserViewModel userVM);

    }
}
