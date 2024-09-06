using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_RaceCondition.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

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
                ModelState.AddModelError("", "Invalid login attempt.");
                return Page();
            }
            HttpContext.Session.SetInt32("CurrentUserId", user.Id);
            HttpContext.Session.SetString("CurrentUserName", user.Name.ToString());
            return RedirectToPage("/Index");
        }
    }
}
