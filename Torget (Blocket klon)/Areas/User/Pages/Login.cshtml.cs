using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Areas.User.Pages
{
    public class LoginModel : PageModel
    {
        private SignInManager<TorgetUser> _sm;

        public class LoginDetails
        {
            [BindProperty]
            public string UserName { get; set; }
            [BindProperty]
            public string Password { get; set; }
        }

        public LoginModel(SignInManager<TorgetUser> sm)
        {
            _sm = sm;
        }
        public void OnGet()
        {
        }

        public async Task OnPostLogInAsync(LoginDetails loginDetails)
        {
            Console.WriteLine(loginDetails.UserName);
            await _sm.PasswordSignInAsync(loginDetails.UserName, loginDetails.Password, false, false);
            Redirect("~/Index");
        }
    }
}
