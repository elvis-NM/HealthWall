using System.Linq;
using secrets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace secrets.Controllers
{
    public class HomeController : Controller
    {
        private int? SessionUserId
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }

        private secretsContext dbContext;
        public HomeController(secretsContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult HandleLogin(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                // make some checks!
                // check for user in db with email!
                User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == user.LogEmail);

                // if null, no user exists with that email
                if (userInDb == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                }

                else
                {
                    // verify pw in db vs pw in form
                    PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                    PasswordVerificationResult result = hasher.VerifyHashedPassword(user, userInDb.Password, user.LogPassword);

                    if (result == 0)
                    {
                        ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    }

                    else
                    {
                        // we now can store this user's id in session!
                        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                        return RedirectToAction("Index", "Secret");
                    }

                }

            }
            return View("Index");
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // check for uniqueness of email!
                if (dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use.");
                    return View("Index");
                }
                // hash pw
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hashedPw = hasher.HashPassword(user, user.Password);

                user.Password = hashedPw;

                dbContext.Add(user);
                // once we save changes...
                // user.UserId will be updated!
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Index", "Secret");

            }
            return View("Index");
        }

    }
}