using AutoMapper;
using Business.Services.Abstract;
using Business.Services.Concrete;
using Business.SystemResult;
using Business.ViewModels.Common;
using Core.Entites.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Rules;
using System.Text.RegularExpressions;

namespace WebUI.Areas.Managers.Controllers
{
    [Area("managers")]
    public class PersonnelController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _service;
        private readonly IMailService _mailService;


        public PersonnelController(UserManager<Employee> userManager, SignInManager<Employee> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IEmployeeService service, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _service = service;
            _mailService = mailService;

        }

        public async Task<IActionResult> PersonnelList()
        {
            var userManager = await _userManager.GetUserAsync(User);
            if (userManager is null) return Redirect("~/");

            var userList = (await _userManager.GetUsersInRoleAsync("Personnel"))
                .Where(x => x.CompanyInfo == userManager.CompanyInfo)
                .ToList();
            List<EmployeeListItem> employeeList = _mapper.Map<List<EmployeeListItem>>(userList).OrderBy(x => x.FullName).ToList();
            return View(employeeList);
        }

        [HttpGet]
        public async Task<IActionResult> PersonnelCreate()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Redirect("~/");

            EmployeeRegister newEmployee = new()
            {
                RoleSelected = "Personnel",
                CompanyInfo = user.CompanyInfo,
            };
            return View(newEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> PersonnelCreate(EmployeeRegister personnel)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users.Any(x => x.Email == personnel.Email))
                {
                    int userCount = _userManager.Users.Where(x =>
                    x.FirstName == personnel.FirstName
                    && x.LastName == personnel.LastName
                    && x.CompanyInfo == personnel.CompanyInfo).ToList().Count;
                    personnel.Email = CreateNextEmail(personnel.Email, userCount);
                }

                var user = _mapper.Map<Employee>(personnel);
                user.UserName = user.Email;

                var result = await _userManager.CreateAsync(user, "Bilgeadam.1234");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Personnel");

                    if (user.IsActive)
                    {
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                        _mailService.SendEmailAsync
                            (
                            user.Email,
                            "Şifre oluşturma",
                            "İK Yönetim Platformuna Hoş geldiniz. Lütfen şifre belirlemek için linke tıklayınız : " + "<a href=\"" + callbackUrl + "\">Şifre Oluştur</a>"
                            );
                    }

                    TempData["Message"] = $"{user.ToString()} personeli başarıyla eklendi";
                    return RedirectToAction(nameof(PersonnelList));
                }
                ViewBag.Message("Yeni personel eklenemedi.");
            }
            return View(personnel);
        }

        /// <summary>
        /// Aynı ad ve soyada sahip kullanıcı mevcutsa soyadından sonra sayı ekleyerek kullanıcı adı oluşturur.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string CreateNextEmail(string email, int userCount)
        {
            var mailArr = email.Split('@');
            return (mailArr[0] + userCount + "@" + mailArr[1]);
        }

        [HttpGet]
        public async Task<ActionResult> PersonnelDetails(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return Redirect("~/");

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> PersonnelUpdate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return Redirect("~/");

            EmployeeUpdate personnel = _mapper.Map<EmployeeUpdate>(user);
            return View(personnel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonnelUpdate(EmployeeUpdate personnel)
        {
            if (ModelState.IsValid)
            {
                ResultService<EmployeeUpdate> result = await _service.UpdateEmployeeAsync(personnel);
                if (result.isSuccess)
                {
                    ViewBag.Message = "Güncelleme işlemi başarılı";
                    return View(result.ResultData);
                }
                ViewBag.Message = result.Error.Message.ToString();
            }
            return View(personnel);
        }
    }
}
