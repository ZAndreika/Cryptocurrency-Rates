using CryptocurrencyRateWebApp.DBWorkers;
using CryptocurrencyRateWebApp.Models;
using CryptocurrencyRateWebApp.Utils;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CryptocurrencyRateWebApp.Controllers
{
    public class AccountController : Controller
    {
        private CRWADbContext DbContext = new CRWADbContext();

        [HttpGet]
        public ActionResult Registration() {
            return View(new RegisteringUser());
        }

        [HttpPost]
        public ActionResult Registration(RegisteringUser regUser) {
            if (!ModelState.IsValid) {
                return View();
            }
            // check if user with such email exists
            if (DbContext.Users.Where(u => u.Email == regUser.Email).FirstOrDefault() != null) {
                ModelState.AddModelError("Email", "User with such email already exists");
                return View(regUser);
            }

            User newUser = new User();
            newUser.Email = regUser.Email;

            // hash the password
            string passwordHash = PasswordHasher.HashPassword(regUser.Password);
            newUser.Password = passwordHash;

            // insert to database
            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();

            // set session parameters
            Session["email"] = newUser.Email;
            return RedirectToAction("Index", "Crypto");
        }

        [HttpGet]
        public ActionResult Login() {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Login(User user) {
            if (!ModelState.IsValid) {
                return View();
            }
            // find user with such email
            User verifyingUser = DbContext.Users.Where(u=>u.Email == user.Email).FirstOrDefault();
            if (verifyingUser == null) {
                ModelState.AddModelError("Email", "There is no user with such email");
                return View(user);
            }

            string userPasswordHash = PasswordHasher.HashPassword(user.Password);

            // compare password hashes
            if (verifyingUser.Password != userPasswordHash) {
                ModelState.AddModelError("Password", "Incorrect password");
                return View(user);
            }
            // set session parameters
            Session["email"] = user.Email;
            return RedirectToAction("Index", "Crypto");
        }

        public ActionResult Logout() {
            // reset session parameters
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            return RedirectToAction("Login");
        }
    }
}