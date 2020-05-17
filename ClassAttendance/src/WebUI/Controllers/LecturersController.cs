using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Facades;

namespace WebUI.Controllers
{
    public class LecturersController : Controller
    {
        private readonly LecturersFacade _lecturersFacade;

        public LecturersController(LecturersFacade lecturersFacade)
        {
            _lecturersFacade = lecturersFacade;
        }

        [HttpGet]
        public IActionResult List()
        {
            var models = _lecturersFacade.GetLecturers();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Lecturer();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                await _lecturersFacade.AddLecturerAsync(lecturer);

                return RedirectToAction(nameof(List));
            }

            return View(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _lecturersFacade.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                await _lecturersFacade.UpdateLecturerAsync(lecturer);

                return RedirectToAction(nameof(List));
            }

            return View(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _lecturersFacade.DeleteLecturerAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}