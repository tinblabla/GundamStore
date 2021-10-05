using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using System.Threading.Tasks;

namespace GundamShop.Controllers
{
    public class ContactController : Controller
    {
        GundamShopDbContext context;
        public ContactController()
        {
            context = new GundamShopDbContext();
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View(context.Feedbacks.ToList());
        }

        public ActionResult Create()
        {
            return View(new Feedback());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create (Feedback model)
        {
            if(ModelState.IsValid)
            {
                context.Feedbacks.Add(model);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return (View(model));
        }
    }
}