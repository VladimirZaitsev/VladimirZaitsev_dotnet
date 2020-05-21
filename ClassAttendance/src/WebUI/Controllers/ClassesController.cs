﻿using BLL.Exceptions;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Facades;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ClassesFacade _classesFacade;
        private readonly ILogger<ClassesController> _logger;

        public ClassesController(ClassesFacade classesFacade, ILogger<ClassesController> logger)
        {
            _classesFacade = classesFacade;
            _logger = logger;
        }

        public Uri Referer => new Uri(Request.Headers["Referer"].ToString());

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _classesFacade.GetClassViewModelsAsync();
            _logger.LogInformation("Fetched classes list");

            return View(models);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = _classesFacade.GetClassManageViewModel();

            return View(lecturer);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Class item, List<string> groups)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _classesFacade.AddClassAsync(item, groups);
                    _logger.LogInformation("Added new class");

                    return RedirectToAction(nameof(List));
                }
                catch (BusinessLogicException ex)
                {
                    _logger.LogError(ex.Message);
                    var error = new ErrorViewModel
                    {
                        ErrorMessage = ex.Message,
                        ReturnUrl = Referer,
                    };

                    return View("Error", error);
                }  
            }

            return View(item);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _classesFacade.GetClassManageViewModel(id);

            return View(item);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Class item, List<string> groups)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _classesFacade.UpdateClassAsync(item, groups);
                    _logger.LogInformation("Updated class");

                    return RedirectToAction(nameof(List));
                }
                catch (BusinessLogicException ex)
                {
                    _logger.LogError(ex.Message);
                    var error = new ErrorViewModel
                    {
                        ErrorMessage = ex.Message,
                        ReturnUrl = Referer,
                    };

                    return View("Error", error);
                }
            }

            return View(item);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _classesFacade.DeleteClassAsync(id);
                _logger.LogInformation("Deleted class");

                return RedirectToAction(nameof(List));
            }
            catch (BusinessLogicException ex)
            {
                _logger.LogError(ex.Message);
               
                var error = new ErrorViewModel
                {
                    ErrorMessage = ex.Message,
                    ReturnUrl = Referer,
                };

                return View("Error", error);
            }
            
        }
    }
}