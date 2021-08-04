using Model;
using System;

namespace IRepository
{
    public interface IUserRepository
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="userPass"></param>
        /// <returns></returns>
        UserModel GetUsers(string userAccount,string userPass);
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="log"></param>
        void AddLogin(LogInfo log);
    }
}
