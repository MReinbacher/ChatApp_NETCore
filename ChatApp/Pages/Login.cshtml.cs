using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("user") != null) {
                return Redirect("~/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            HttpContext.Session.SetString("user", Request.Form["name"]);
            await HttpContext.Session.CommitAsync();
            return Redirect("~/");
        }
    }
}
