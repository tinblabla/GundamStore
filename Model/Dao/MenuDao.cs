using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        GundamShopDbContext db = null; //Tất cả user đều null vì ko có
        public MenuDao()
        {
            db = new GundamShopDbContext(); //tạo 1 cái db chứa
        }
        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
