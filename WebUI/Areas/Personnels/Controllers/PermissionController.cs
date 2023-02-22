using AutoMapper;
using Business.Services.Abstract;
using Business.ViewModels.PermissionVM;
using Core.Entites.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebUI.Areas.Personnels.Controllers
{
    [Area("Personnels")]
    public class PermissionController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _service;
        private readonly IPermissionService _permissionService;

        public PermissionController(SignInManager<Employee> signInManager, UserManager<Employee> userManager, IMapper mapper, IEmployeeService service, IPermissionService permissionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _service = service;
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> PermissionList()
        {
            var user = await _userManager.GetUserAsync(User);

            var result = _permissionService.GetPersonnelsPermissions(user.UserName);

            ViewBag.Message = result.isSuccess ? result.Success.ToString() : result.Error.ToString();
            ViewBag.PermissionDay = user.AnnualPermission;

            return View(result.ResultData);
        }

        [HttpGet]
        public async Task<IActionResult> CancelPermission(Guid id)
        {
            var result = await _permissionService.CancelPermissionAsync(id);

            TempData["Message"] = result.isSuccess ? result.Success.ToString() : result.Error.ToString();

            return RedirectToAction(nameof(PermissionList));
        }

        [HttpGet]
        public IActionResult RequestNewPermission()
        {
            return View(new PermissionCreate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestNewPermission(PermissionCreate model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                var result = await _permissionService.CreateNewPermissionRequestAsync(model, user);

                if (result.isSuccess)
                {
                    TempData["Message"] = result.Success.ToString();
                    return RedirectToAction(nameof(PermissionList));
                }
                ViewBag.Message = result.Error.ToString();
            }
            return View(model);
        }
    }
}
