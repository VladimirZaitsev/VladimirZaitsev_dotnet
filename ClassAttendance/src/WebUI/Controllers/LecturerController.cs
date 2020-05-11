using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class LecturerController : Controller
    {
        private readonly LecturerService _lecturerService;

        public LecturerController(LecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var models = _lecturerService.GetLecturers();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Lecturer();

            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                await _lecturerService.AddLecturerAsync(lecturer);

                return RedirectToAction(nameof(List));
            }

            return View(lecturer);
        }

        [HttpGet]
        public IActionResult Update()
        {
            var lecturer = new Lecturer();

            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                await _lecturerService.UpdateLecturerAsync(lecturer);

                return RedirectToAction(nameof(List));
            }

            return View(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _lecturerService.DeleteLecturerAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}