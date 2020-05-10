using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    }
}