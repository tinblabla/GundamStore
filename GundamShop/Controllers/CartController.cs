using GundamShop.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EF;

namespace GundamShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
       
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem (long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart; // tức là đã có cái list này rồi từ session
                if (list.Exists(x=>x.Product.ID == productId))
                {
                    //truong co gio hang roi
                    //Khi nào có rồi thì ép cái kiểu

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity; // ép vào 2 sp thì tăng lên 2
                        }

                    }
                }
                else
                {
                    //tao moi doi tuong cartitem
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //gán vào Session
                Session[CartSession] = list;

            }
            else
            {
                //tao moi doi tuong cartitem
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);

        }

        [HttpPost]
        public ActionResult Payment(string shipname,string mobile, string addres, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = addres;
            order.ShipMoblie = mobile;
            order.ShipName = shipname;
            order.ShipEmail = email;

            var id = new OrderDao().Insert(order);
            var cart = (List<CartItem>)Session[CartSession];
            var detailDao = new Model.Dao.OrderDetailDao();
            try
            {
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                }
            }catch(Exception ex)
            {
                return Redirect("/loi-thanh-toan");
                throw;
            }
            return Redirect("/hoan-thanh");

        }
        public ActionResult Success()
        {
            return View();
        }
        
        



    }
}