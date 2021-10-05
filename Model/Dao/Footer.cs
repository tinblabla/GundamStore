using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public  class FooterDao
    {
        GundamShopDbContext db = null; //Tất cả user đều null vì ko có
        public FooterDao()
        {
            db = new GundamShopDbContext(); //tạo 1 cái db chứa
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true); //Truy xuất thông tin từ sql ra lên footer (ntext)
        }
    }
    
}
