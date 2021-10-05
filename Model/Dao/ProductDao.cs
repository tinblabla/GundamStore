using Model.EF;
using Model.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {

        GundamShopDbContext db = null; //Tất cả user đều null vì ko có
        public ProductDao()
        {
            db = new GundamShopDbContext(); //tạo 1 cái db chứa
        }
        //Sản phẩm
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.Where(x => x.ViewCount == 0).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListNewProduct1(int top)
        {
            return db.Products.Where(x => x.ViewCount == 1).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        //Search
        public List<string> ListName(string keyword)
        {

            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        //Trang
        public List<Product> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model =  db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x=>x.CreatedDate).Skip((pageIndex - 1)* pageSize).Take(pageSize).ToList(); //đầu tiên lấy ra tổng tất cả các sản phẩm (db.Products.Where(x => x.CategoryID == categoryID)) sau đó lấy từ bản ghi số 
            return model;    
        }
        public List<Product> ListRelatedProduct(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        //Xem
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
