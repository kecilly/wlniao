using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.Domain;

namespace Shijia.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class AccountService
        : GenericManagerBase<Domain.Account>, IAccountService
    {
        public Shijia.Dao.IAccountDao AccountDao { set; get; }
        
        public IList<Domain.Account> LoadAllByPage(out long total, int page, int rows, string order, string sort)
        {
            return AccountDao.LoadAllByPage(out total, page, rows, order, sort).ToList();
        }
        public override object Save(Domain.Account entity)
        {
            return base.Save(entity);
        }

        public Domain.Account Get(string account)
        {
            return AccountDao.Get(account);
        }

        public Account Get(string username, string password)
        {
            var entity = AccountDao.Get(username);         
            if (entity != null && entity.AccountPassword == password)
            {
                return entity;
            }  
            return null;
        }

        public void Update(Domain.Account entity, string password)
        {
            entity.AccountPassword = password;
            base.Update(entity);
        }


        private static string _lock = "lock";

        /// <summary>
        /// 根据用户名获取
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Domain.Account GetByUserName(String UserName)
        {
            try
            {
                Account model = Get(UserName);
                return model;
            }
            catch (Exception ex) { }
            return null;
        }

        public Result Add(String username, String password, String email = "", String mobile = "")
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("用户名未填写");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("登录密码未填写");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(password, 5);
                }
                try
                {
                    lock (_lock)
                    {
                        Account model = Get(username);
                        if (model != null)
                        {
                            result.Add("用户已存在，禁止重复注册");
                        }
                        else
                        {                            
                                Account account = new Account();
                                account.AccountUserName = username;
                                account.AccountPassword = password;
                                account.AccountEmail = email;
                                account.AccountMobile = mobile;
                                account.AccountJoinTime = DateTime.Now;
                                account.AccountLastLogin = DateTime.Now;
                                account.IsStop = 0;
                                CurrentRepository.Save(account);
                                result.Add("注册成功!");                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Add("错误：" + ex.Message);
                }
            }
            return result;
        }
        public Result CheckLogin(String username, String password)
        {           
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    result.Add("用户名未填写");
                }
                else if (string.IsNullOrEmpty(password))
                {
                    result.Add("登录密码未填写");
                }
                else
                {
                    if (password.Length != 32)
                    {
                        password = Encryptor.Md5Encryptor32(password, 5);
                    }
                    var account = Get(username);
                    if (account != null && account.AccountPassword == password)
                    {
                        if (account.IsStop != 1)
                        {
                            account.AccountLastLogin = DateTime.Now;
                            Update(account);
                        }
                        else
                        {
                            result.Add("帐号“" + account + "”已被禁止登录");
                        }
                    }
                    else if (account == null)
                    {
                        result.Add("用户名不存在！");
                    }
                    else
                    {
                        result.Add("用户名或密码错误");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }
        public Result ChangePwd(String username, String password, String oldpassword = "")
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("用户名未填写");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("登录密码未填写");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(password, 5);
                }
                if (!string.IsNullOrEmpty(oldpassword) && oldpassword.Length != 32)
                {
                    oldpassword = Encryptor.Md5Encryptor32(oldpassword, 5);
                }
                var account = Get(username);
                if (string.IsNullOrEmpty(oldpassword) || account.AccountPassword == oldpassword)
                {
                    account.AccountPassword = password;
                    Update(account);
                }
                else
                {
                    result.Add("您输入的旧密码不正确");
                }
            }
            return result;
        }
        public Result ChangeStop(String username)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    result.Add("用户名未填写");
                }
                else
                {
                    var account = Get(username);
                    if (account != null)
                    {
                        account.IsStop = account.IsStop == 0 ? 1 : 0;
                        Update(account);
                    }
                    else
                    {
                        result.Add("更改用户状态失败");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add("更改用户状态失败");
            }
            return result;
        }

        public IList<Account> GetPage(string where = "", int pageindex = 0, int pagesize = 10)
        {
            try
            {
                long totalcount;
                return LoadAllByPage(out totalcount, pageindex, pagesize,string.Empty,string.Empty);
            }
            catch
            {
                return null;
            }
        }

    }
}