using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService Service;
        public PublisherController(IPublisherService Service)
        {
            this.Service = Service;
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await Service.Add(model);
            if (result)
            {
                TempData["msg"] = "Add Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var record = await Service.GetById(id);
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await Service.Update(model);
            if (result)
            {
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var record = await Service.Delete(id);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> GetAll()
        {
            var data = await Service.GetAll();
            return View(data);
        }
    }
}
