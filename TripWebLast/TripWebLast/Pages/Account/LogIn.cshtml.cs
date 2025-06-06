using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripWebLast.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TripWebLast.Pages.Account
{
    [AllowAnonymous]
    public class LogIn: PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogIn(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "邮箱地址是必填项")]
            [EmailAddress(ErrorMessage = "请输入有效的邮箱地址")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name ="密码")]
            public string Password { get; set; }

            [Display(Name = "是否保存")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "非法登入请求");
                    return Page();
                }
            }

            return Page();
        }
    }
}