using AutoMapper;
using Business.Services.Abstract;
using Business.ViewModels.Common;
using Core.Entites.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;


        private readonly string _baseUrl = "~/";

        public AccountController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Employee> signInManager, IMailService mailService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _mapper = mapper;

            CreateRoles();
        }

        private async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Manager"))
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!await _roleManager.RoleExistsAsync("Personnel"))
                await _roleManager.CreateAsync(new IdentityRole("Personnel"));
        }

        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            EmployeeRegister newEmployee = new()
            {
                RoleSelected = "Manager",
                ReturnUrl = returnUrl,
            };
            return View(newEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> Register(EmployeeRegister employee, string? returnUrl = null)
        {
            employee.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? ("~/");


            if (ModelState.IsValid)
            {
                var user = _mapper.Map<Employee>(employee);
                user.UserName = user.Email;


                if (employee.Photo is not null)
                {
                    var extension = Path.GetExtension(employee.Photo.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    employee.Photo.CopyTo(stream);
                    user.Photo = newImageName;
                }

                var result = await _userManager.CreateAsync(user, "Bilgeadam.1234");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Manager");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                ModelState.AddModelError("Password", "Yeni kullanıcı yaratılamadı.");
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var signedInUser = await _userManager.GetUserAsync(User);
            if (signedInUser is not null && signedInUser.IsActive)
            {
                if (await _userManager.IsInRoleAsync(signedInUser, "Manager"))
                    return Redirect($"{_baseUrl}Managers/Home/Summary");
                if (await _userManager.IsInRoleAsync(signedInUser, "Personnel"))
                    return Redirect($"{_baseUrl}Personnels/Home/Summary");
            }

            EmployeeLogin user = new();
            user.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(EmployeeLogin user)
        {
            if (ModelState.IsValid)
            {
                var employee = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

                if (employee is not null && employee.IsActive)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(employee, "Manager"))
                            return Redirect($"{_baseUrl}Managers/Home/Summary");
                        if (await _userManager.IsInRoleAsync(employee, "Personnel"))
                            return Redirect($"{_baseUrl}Personnels/Home/Summary");
                    }
                }
                ViewBag.Message = "Hatalı kullanıcı adı veya şifre!";
                ModelState.AddModelError(string.Empty, "Hatalı kullanıcı adı veya şifre!");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(EmployeeForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    _mailService.SendEmailAsync
                        (
                        model.Email,
                        "Şifre yenileme",
                        "Lütfen şifre belirlemek için linke tıklayınız : " + "<a href=\"" + callbackUrl + "\">Şifre Oluştur</a>"
                        );

                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                ModelState.AddModelError("Hata", "Kullanıcı tanımlanamadı");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(EmployeeResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Kullanıcı bulunamadı");
                    return View();
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
