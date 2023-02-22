using AutoMapper;
using Business.Services.Abstract;
using Business.SystemResult;
using Business.ViewModels.Common;
using Core.Entites.Concrete;
using DataAccess.EfRepository.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Personnels.Controllers
{
    [Area("Personnels")]
    public class HomeController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _service;

        public HomeController(SignInManager<Employee> signInManager, UserManager<Employee> userManager, IMapper mapper, IEmployeeService service)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            await _service.UpdateAnnualPermission(user);
            EmployeeSummary employee = _mapper.Map<EmployeeSummary>(user);
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            return View(user);
        }

        public async Task<IActionResult> Update()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            EmployeeUpdate employee = _mapper.Map<EmployeeUpdate>(user);

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeUpdate model)
        {

            if (ModelState.IsValid)
            {
                ResultService<EmployeeUpdate> result = await _service.UpdateEmployeeAsync(model);

                if (result.isSuccess)
                {
                    TempData["Message"] = result.Success.ToString();
                    return RedirectToAction(nameof(Summary));
                }
                ViewBag.ErrorMessage = result.Error.Message.ToString();
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            string _baseUrl = "~/";
            await _signInManager.SignOutAsync();
            return Redirect(_baseUrl);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View(new EmployeeChangePassword());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(EmployeeChangePassword model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                var verifyResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

                ViewBag.Message = verifyResult.Succeeded ? "Şifre Değiştirildi" : "Şifre doğrulanamadı. Tekrar deneyiniz";
            }
            return View(model);
        }
    }
}
