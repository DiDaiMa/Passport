using Common;
using IRepository;
using Model;
using System;
using System.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        DapperHelp<UserModel> dapper = new DapperHelp<UserModel>();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userAccountt"></param>
        /// <param name="userPass"></param>
        /// <returns></returns>
        public UserModel GetUsers(string userAccountt, string userPass)
        {
            string sql = $"select * from Users where Account=@userName and Password=@userPass";
            return dapper.ExecuteQuery(sql, new { @userName = userAccountt, @userPass = userPass }).FirstOrDefault();
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="log"></param>
        public void AddLogin(LogInfo log)
        {
            string sql = "insert into LogsModel values(@LoginName,@LoginIp,@LoginUserId,@LoginDel,@LoginCreateTime,@LoginMsg)";

            dapper.ExecuteNonQuery(sql, new { @LoginName = log.LoginName, @LoginIp = log.LoginIp, @LoginUserId=001,@LoginDel=0,@LoginCreateTime = log.LoginCreateTime, @LoginMsg = log.LoginMsg });
        }

    }
}
