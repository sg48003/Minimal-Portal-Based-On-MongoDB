using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
using NMBP_Portal.ViewModels;

namespace NMBP_Portal.Controllers
{
    public class PortalController : Controller
    {
        private readonly MongoDatabase db = new MongoDatabase();

        public ActionResult Index()
        {
            var result = db.GetNews(10);
            return View(result);
        }

        public ActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(int id, string comment)
        {
            db.AddComment(id, comment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNews(NewsFormViewModel model, HttpPostedFileBase upload)
        {
            var news = new News
            {
                Headline = model.Headline,
                Text = model.Text,
                Author = model.Author 
            };

            if (upload != null && upload.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    news.Picture = reader.ReadBytes(upload.ContentLength);
                }
            }

            db.AddNews(news);

            return RedirectToAction("Index");
        }
    }
}