using System.ComponentModel.DataAnnotations;
using TripWebLast.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TripWebLast.Pages.Account
{
    public class Register: PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public Register(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "邮箱地址是必填项")] 
            [EmailAddress(ErrorMessage = "请输入有效的邮箱地址")] 
            [Display(Name = "邮箱地址")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "密码长度不能小于六个字符.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "确认密码")]
            [Compare("Password", ErrorMessage = "两次输入的密码内容不匹配")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "安全问题")]
            public string SecurityQuestion { get; set; }

            [Required]
            [Display(Name = "安全问题的答案")]
            public string SecurityAnswer { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    SecurityQuestion = Input.SecurityQuestion,
                    SecurityAnswer = Input.SecurityAnswer
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}