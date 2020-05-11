using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectService _subjectService;

        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var models = _subjectService.GetSubjects();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Subject();

            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.AddSubjectAsync(subject);

                return RedirectToAction(nameof(List));
            }

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _subjectService.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.UpdateSubjectAsync(subject);

                return RedirectToAction(nameof(List));
            }

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _subjectService.DeleteSubjectAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}