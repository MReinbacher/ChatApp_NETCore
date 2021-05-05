using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    public class LogoutController : Controller
    {
        // GET: /logout/
        public IActionResult Index()
        {
            HttpContext.Session.Remove("user");
            return Redirect("~/login");
        }
    }
}