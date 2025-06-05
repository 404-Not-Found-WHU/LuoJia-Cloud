using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripWebLast.Data;
using Microsoft.AspNetCore.Identity;

namespace TripWebLast.Pages.Account
{
    public class ForgotPassword: PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ForgotPassword(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                return RedirectToPage("./VerifySecurityAnswer", new { email = Input.Email });
            }

            return Page();
        }
    }
}