using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_RaceCondition.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        private const int timeLoginLimit = 10;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnPost()
        {
            var user = _userService.Login(Email, Password);
            if (user == null)
            {
                string message = UpdateLoginTimeAttemp() > 10 ? "You haved login fail for many time, try again after " : "Invalid login attempt.";
                ViewData["Mesage"] = message;
                ModelState.AddModelError("", message);
                return Page();
            }
            HttpContext.Session.SetInt32("CurrentUserId", user.Id);
            HttpContext.Session.SetString("CurrentUserName", user.Name.ToString());
            return RedirectToPage("/Index");
        }

        public int UpdateLoginTimeAttemp()
        {
            int? timeLogin = HttpContext.Session.GetInt32("LoginTimeAttemp");
            if (timeLogin == null)
            {
                HttpContext.Session.SetInt32("LoginTimeAttemp", 1);
                return 1;
            }
            else
            {
                HttpContext.Session.SetInt32("LoginTimeAttemp", timeLogin.Value + 1);
                return timeLogin.Value + 1;
            }
        }
    }
}
