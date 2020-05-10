using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.Students;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var studentList = await _studentService.GetStudentListAsync();

            return View(studentList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = _studentService.GetViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddStudentAsync(model.Student);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _studentService.GetViewModel(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(StudentManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _studentService.EditStudentAsync(model.Student);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}