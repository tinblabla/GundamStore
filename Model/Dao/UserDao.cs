using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class UserDao
    {
        GundamShopDbContext db = null; //Tất cả user đều null vì ko có
        public UserDao()
        {
            db = new GundamShopDbContext(); //tạo 1 cái db chứa
        }
        public long Insert(User entity)   //Lưu thông tin vào Database
        {
            db.Users.Add(entity);
            db.SaveChanges();             
            return entity.ID;
        }
        public User GetById(string userName) //Hàm lấy ID user
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public int Login(string userName, string passWord)  //So dk với database 
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result ==null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckUserEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
