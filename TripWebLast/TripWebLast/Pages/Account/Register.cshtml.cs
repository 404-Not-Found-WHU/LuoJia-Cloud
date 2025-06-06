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
            [Required(ErrorMessage = "�����ַ�Ǳ�����")] 
            [EmailAddress(ErrorMessage = "��������Ч�������ַ")] 
            [Display(Name = "�����ַ")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "���볤�Ȳ���С�������ַ�.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "����")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "ȷ������")]
            [Compare("Password", ErrorMessage = "����������������ݲ�ƥ��")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "��ȫ����")]
            public string SecurityQuestion { get; set; }

            [Required]
            [Display(Name = "��ȫ����Ĵ�")]
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