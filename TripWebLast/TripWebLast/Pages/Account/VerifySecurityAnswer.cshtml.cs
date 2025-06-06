using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripWebLast.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TripWebLast.Pages.Account
{
    public class VerifySecurityAnswer : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifySecurityAnswer(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string SecurityQuestion { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "�����ַ�Ǳ�����")]
            [EmailAddress(ErrorMessage = "��������Ч�������ַ")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "��ȫ�����")]
            public string SecurityAnswer { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("./ForgotPassword");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            SecurityQuestion = user.SecurityQuestion;
            Input = new InputModel
            {
                Email = email
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                if (user.SecurityAnswer == Input.SecurityAnswer)
                {
                    return RedirectToPage("./ResetPassword", new { email = Input.Email });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "��ȫ����Ĵ𰸲���ȷ");
                    SecurityQuestion = user.SecurityQuestion;
                    return Page();
                }
            }

            return Page();
        }
    }
}