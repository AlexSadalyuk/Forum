using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstExample.WebUI.Models;
using FirstExample.Core.Interfaces.BusinessLogic;
using FirstExample.Core.Entities.Dto;

using Microsoft.Owin.Security;
using System.Security.Claims;

namespace FirstExample.WebUI.Controllers
{
    public class AccountController : Controller
    {


        private readonly IUserManager _userManager;

        public IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //GET: Login
        public ActionResult Login()
        {
            if (AuthManager.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLogin model)
        {
            if (ModelState.IsValid)
            {

                if(_userManager.LoginApproved(model.Name, model.Password))
                {
                    if(_userManager.IsUserActivated(model.Name)) {
                        ClaimsIdentity claims = new ClaimsIdentity("ApplicationCookie", 
                                                                    ClaimsIdentity.DefaultNameClaimType, 
                                                                    ClaimsIdentity.DefaultRoleClaimType);
                        claims.AddClaims(new[] {
                                        new Claim(ClaimTypes.Name, model.Name, ClaimValueTypes.String),
                                    });

                        AuthManager.SignOut();
                        AuthManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claims);
                        return RedirectToAction("Index", "Post");
                    }
                    TempData["Info"] = "You need to activate your account. Please, check your email";
                    _userManager.CreateNewToken(model.Name);
                }
                else
                {
                    AddErrors(_userManager.Errors);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Registration()
        {
            if (AuthManager.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(AccountRegistration model)
        {

            if (ModelState.IsValid)
            {
                UserRegistration userReg = new UserRegistration
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };
                _userManager.CreateUser(userReg);
                if (_userManager.Errors.Any())
                {
                    AddErrors(_userManager.Errors);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            TempData["Info"] = "You've got an email with activation link.";
            return RedirectToAction("Login");
        }

        public ActionResult ConfirmEmail(string id)
        {
            _userManager.ActivateUser(id);

            if (_userManager.Errors.Any())
            {
                TempData["Info"] = _userManager.Errors["token"];
            }
            else
            {
                TempData["Info"] = "Now you can login";
            }

            return RedirectToAction("Login"); ;
        }

        [ChildActionOnly]
        public ActionResult Navbar()
        {

            ViewBag.IsSignIn = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.UserName = HttpContext.User.Identity.Name;

            return PartialView();
        }

        private void AddErrors(IDictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                ModelState.AddModelError(pair.Key, pair.Value);
            }
            dictionary.Clear();
        }
    }
}