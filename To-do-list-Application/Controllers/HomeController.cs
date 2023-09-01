
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using To_do_list_Application.Models;
using static To_do_list_Application.Models.Context;

namespace To_do_list_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult About()
        {
            return View();
        }
        [Authorize]
        public IActionResult Index()
        {
            // Check if the user has logged in successfully
            

            List<UserModel> users = new List<UserModel>();
            using (var db = new Demo())
            {
                users = db.Tasks.ToList();
                ViewBag.Users = users;

                return View();
            }
        }



        [Authorize]
        [HttpPost]

public IActionResult Index(UserModel user)
        { 

    if (!string.IsNullOrWhiteSpace(user.TaskName))
    {
        using (var db = new Demo())
        {
            db.Add(user);
            db.SaveChanges();
        }
    }
    
    return RedirectToAction("Index"); // Redirect back to the Index view
}




        public IActionResult Delete(int id)
        {
            // Check if the user is authenticated
           

            using (var db = new Demo())
            {
                var taskToDelete = db.Tasks.FirstOrDefault(x => x.id == id);
                if (taskToDelete != null)
                {
                    db.Tasks.Remove(taskToDelete);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index"); // Redirect back to the index page
        }




        public IActionResult Login()
        {
           
              
            return View();
        }
        [HttpPost]
       
        public IActionResult Login(string username, string password)
        {
            // Simulate a list of valid user credentials
            List<UserCredentials> validCredentials = new()
    {
        new UserCredentials { Username = "admin", Password = "adminpass" },
        new UserCredentials { Username = "user", Password = "userpass" }
        // Add more users and passwords here
    };

            if (validCredentials.Any(cred => cred.Username == username && cred.Password == password))
            {
                // Clear the error message before redirecting
                ViewBag.ErrorMessage = null;

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            // Add additional claims as needed
        };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true // Set as needed
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                        new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                // Set a flag in TempData to indicate successful login
                TempData["IsLoggedIn"] = true;
                

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials.";
                return View();
            }
        }

        public IActionResult Logout()
        {
           
            return RedirectToAction("Login", "Home");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
