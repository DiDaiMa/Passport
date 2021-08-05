using Common;
using IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using SSO.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Server.Controllers
{
    public class LoginController : Controller
    {
        private readonly JWTTokenOptions _tokenOptions;
        private IUserRepository _user;
        private string LoginIn = Config.MySqlString;
        public LoginController(JWTTokenOptions tokenOptions,IUserRepository user)
        {
            _tokenOptions = tokenOptions;
            _user = user;
        }

        [HttpGet]
        public IActionResult Checktoken(string token)
        {
            RedisHelper redis = new RedisHelper();
            string value = redis.CacheRedis.StringGet(token);
            string Id = redis.CacheRedis.StringGet("UserId_" + token);
            string Name = redis.CacheRedis.StringGet("UserName_" + token);
            if (value != null && Id != "")
            {
                return Json(new { code = 200, token = token, UserId = Id, UserName = Name, msg = "登录成功" });
            }
            else
            {
                redis.CacheRedis.KeyDelete(token);
                redis.CacheRedis.KeyDelete("UserId_" + token);
                return Json(new { code = 400, token = "", UserId = Id, UserName = Name, msg = "登录失败" });
            }
        }

        //登录页面
        [HttpGet]
        public IActionResult Login(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return Redirect("http://localhost:64438/Login?token=" + token);
            }
            return View();
        }

        //登录方法
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            //日志
            LogInfo log = new LogInfo();
            log.LoginName = login.UserAccount;
            log.LoginCreateTime = DateTime.Now;
            log.LoginIp = HttpContext.Connection.RemoteIpAddress.ToString();  //获取 用户IP
            TokenResult JwtToken = new TokenResult();
            string token = "";

            var users = _user.GetUsers(login.UserAccount, login.UserPass);

            if (users != null)
            {
                //生成
                JwtToken = new JwtTokenHelper().AuthorizeToken(users.Account, _tokenOptions);
                token = JsonConvert.SerializeObject(JwtToken.Access_token);
                RedisHelper redis = new RedisHelper();
                redis.CacheRedis.StringSet(token, JsonConvert.SerializeObject(login));
                redis.CacheRedis.StringSet("UserId_" + token, users.Id);
                redis.CacheRedis.StringSet("UserName_" + token, users.Name);
                redis.CacheRedis.KeyExpire(token, DateTime.Now.AddMinutes(15));
                redis.CacheRedis.KeyExpire("UserId_" + token, DateTime.Now.AddMinutes(15));
                redis.CacheRedis.KeyExpire("UserName_" + token, DateTime.Now.AddMinutes(15));

                log.LoginMsg = "登录成功";
                _user.AddLogin(log);
                return Json(new { url= "http://localhost:8080/Login" + "?token=" + token,result=0});
            }
            else
            {
                log.LoginMsg = "登录失败";
                _user.AddLogin(log);
                return Json(new { result = 1 });
            }
        }
    }
}
