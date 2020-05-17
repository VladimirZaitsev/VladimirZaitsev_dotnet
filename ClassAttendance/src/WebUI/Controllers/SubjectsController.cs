using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Facades;

namespace WebUI.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly SubjectsFacade _subjectsFacade;

        public SubjectsController(SubjectsFacade subjectsFacade)
        {
            _subjectsFacade = subjectsFacade;
        }

        [HttpGet]
        public IActionResult List()
        {
            var models = _subjectsFacade.GetSubjects();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Subject();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectsFacade.AddSubjectAsync(subject);

                return RedirectToAction(nameof(List));
            }

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _subjectsFacade.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectsFacade.UpdateSubjectAsync(subject);

                return RedirectToAction(nameof(List));
            }

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _subjectsFacade.DeleteSubjectAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}