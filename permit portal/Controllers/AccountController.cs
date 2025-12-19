using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using permit_portal.Models.ViewModel;

namespace permit_portal.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> userManager;
        public readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerVM.Username,
                Email = registerVM.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerVM.Password);

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(identityUser, "User");
                return RedirectToAction("Login");

            }
            return View(identityResult);
        }

        [HttpGet]

        public async Task<IActionResult> Login ()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginVM loginVM)

        {
           var signInResult= await signInManager.PasswordSignInAsync(loginVM.Username,loginVM.Password, false, false);
            if (signInResult !=null && signInResult.Succeeded)
            {
                return RedirectToAction("Index" ,"Home");   
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout ()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
