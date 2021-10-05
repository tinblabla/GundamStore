using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {

        GundamShopDbContext db = null; //Tất cả user đều null vì ko có
        public ProductCategoryDao()
        {
            db = new GundamShopDbContext(); //tạo 1 cái db chứa
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status==true).OrderBy(x => x.DisplayOrder).ToList();  //Truy xuất thông tin từ sql ra lên menu
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
