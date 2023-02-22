using AutoMapper;
using Business.Services.Abstract;
using Business.SystemResult;
using Business.ViewModels.PermissionVM;
using Core.Entites.Concrete;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Managers.Controllers
{
    [Area("managers")]
    public class PermissionRequestController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmployeeService _service;
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public PermissionRequestController(UserManager<Employee> userManager, SignInManager<Employee> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IEmployeeService service, IPermissionService permissionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _service = service;
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> PendingRequestList()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            var result = _permissionService.GetPendingPermissionsRequestViaCompany(user.CompanyInfo);

            ViewBag.Message = result.isSuccess ? result.Success.ToString() : result.Error.ToString();

            return View(result.ResultData);
        }
        
        [HttpGet]
        public async Task<IActionResult> DoneRequestList()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            var result = _permissionService.GetDonePermissionsRequestViaCompany(user.CompanyInfo);

            ViewBag.Message = result.isSuccess ? result.Success.ToString() : result.Error.ToString();

            return View(result.ResultData);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveRequest(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            var result = await _permissionService.ApprovePermissionAsync(id);

            TempData["Message"] = result.isSuccess ? result.Success.ToString() : result.Error.ToString();

            return RedirectToAction(nameof(PendingRequestList));
        }

        [HttpGet]
        public async Task<IActionResult> RejectRequest(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            var result = await _permissionService.RejectPermissionRequest(id);

            TempData["Message"] = result.isSuccess ? result.Success.ToString() : result.Error.ToString();

            return RedirectToAction(nameof(PendingRequestList));
        }
    }
}
