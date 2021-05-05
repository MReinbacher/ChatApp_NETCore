using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models;
using System;
using System.Globalization;
using ChatApp.Data;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatContext _db;

        public ChatController(ChatContext db)
        {
            _db = db;
        }

        // GET: /Chat/
        [HttpGet]
        public void Index()
        {

        }

        // GET: /Chat/Message
        [HttpGet]
        public async Task<ActionResult> Message(int? id)
        {
            if (id != null)
            {
                return Json(await _db.Messages.FindAsync(id));
            }
            else
            {
                return Json(await _db.Messages.ToListAsync());
            }
        }

        [HttpGet]
        public IActionResult UserName()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                return Json(new
                {
                    username = HttpContext.Session.GetString("user")
                });
            }
            else
            {
                return NotFound();
            }

        }
    }
}