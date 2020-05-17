using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Facades;

namespace WebUI.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupsFacade _groupFacade;

        public GroupsController(GroupsFacade groupService)
        {
            _groupFacade = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _groupFacade.GetGroupViewModelsAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Group();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Group group)
        {
            if (ModelState.IsValid)
            {
                await _groupFacade.AddGroupAsync(group);

                return RedirectToAction(nameof(List));
            }

            return View(group);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _groupFacade.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Group group)
        {
            if (ModelState.IsValid)
            {
                await _groupFacade.UpdateGroupAsync(group);

                return RedirectToAction(nameof(List));
            }

            return View(group);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupFacade.DeleteGroupAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
