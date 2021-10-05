using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GundamShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Gioithieu()
        {
            return View();
        }
        public ActionResult Chitiet()
        {
            return View();

        }
        public ActionResult BST()
        {
            return View();

        }
        public ActionResult DieuKhoan()
        {
            return View();

        }
        public ActionResult Thongtin()
        {
            return View();
        }
        // GET: Home
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(100);
            ViewBag.NewProducts1 = productDao.ListNewProduct1(3);
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1); //Hiển thị thông tin vừa truy xuất ở sql lên web
        
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult MainMenuLeft()
        {
            var model = new MenuDao().ListByGroupId(2); //Hiển thị thông tin vừa truy xuất ở sql lên web
        return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter(); //Hiển thị thông tin vừa truy xuất ở sql lên web
            return PartialView(model);
        }
    }

}