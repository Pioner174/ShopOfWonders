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
        public Task<UserViewModel> GetUserVMForID(long id);

        /// <summary>
        /// Получение всех UserViewModel 
        /// </summary>
        /// <returns></returns>
        public Task<List<UserViewModel>> GetUsersVMList();

        /// <summary>
        /// Сохранение пользвателя по созданной UserViewModel
        /// </summary>
        /// <param name="userVM"></param>
        public Task<bool> SaveUserVMInBD(UserViewModel userVM);

        /// <summary>
        /// Удаление User по id
        /// </summary>
        /// <param name="id"></param>
        public Task<bool> DeleteUserForId(long id);

        /// <summary>
        /// Обновленние данных UserVM
        /// </summary>
        /// <param name="userVM"></param>
        public Task<bool> UpdateUserForUserVM(UserViewModel userVM);

    }
}
