using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripWebLast.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TripWebLast.Pages.Account
{
    public class ResetPassword : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPassword(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "�����ַ�Ǳ�����")]
            [EmailAddress(ErrorMessage = "��������Ч�������ַ")]
            [Display(Name ="�����ַ")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "����Ӧ��������6���ַ��ĳ���", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "ȷ������")]
            [Compare("Password", ErrorMessage = "�����ȷ�����벻ƥ��.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string email)
        {
            if (email == null)
            {
                return BadRequest("������ʼ�����Ϊ��");
            }
            Input = new InputModel
            {
                Email = email
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}