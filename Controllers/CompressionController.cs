using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PdfCompressionService.Controllers
{
    public class CompressionController : Controller
    {
        // GET: CompressionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompressionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompressionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompressionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompressionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompressionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompressionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompressionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
