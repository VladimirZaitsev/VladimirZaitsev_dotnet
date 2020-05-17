using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.StudentModel;
using WebUI.Facades;

namespace WebUI.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentsFacade _studentsFacade;

        public StudentsController(StudentsFacade studentsFacade)
        {
            _studentsFacade = studentsFacade;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var studentList = await _studentsFacade.GetStudentListAsync();

            return View(studentList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = _studentsFacade.GetViewModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(StudentManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _studentsFacade.AddStudentAsync(model.Student);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _studentsFacade.GetViewModel(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(StudentManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _studentsFacade.EditStudentAsync(model.Student);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentsFacade.DeleteStudentAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}