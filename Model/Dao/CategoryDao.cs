using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        GundamShopDbContext db = null; //Tất cả user đều null vì ko có
        public CategoryDao()
        {
            db = new GundamShopDbContext(); //tạo 1 cái db chứa
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();  //Truy xuất thông tin từ sql ra lên menu
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
