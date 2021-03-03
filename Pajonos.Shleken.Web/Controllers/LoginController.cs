using Pajonos.Shleken.Services;
using Pajonos.Shleken.Services.Models;
using Pajonos.Shleken.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pajonos.Shleken.Web.Controllers
{
   
    public class LoginController : Controller
    {
        
          
            [HttpGet]
            [AllowAnonymous]
            public ActionResult Login(string ReturnUrl)
            {
             EnsureLoggedOut();
            UsersLoginViewModel Users = new UsersLoginViewModel();
            Users.ReturnUrl = ReturnUrl;
            return View(Users);
            }
            private void EnsureLoggedOut()
            {
                // If the request is (still) marked as authenticated we send the Users to the logout action
                if (Request.IsAuthenticated)
                    Logout2();
            }

        //   public ActionResult Logout2()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Login","");

        //}

        //POST: Logout
      
            public ActionResult Logout2()
            {
                try
                {
                    // First we clean the authentication ticket like always
                    //required NameSpace: using System.Web.Security;
                    FormsAuthentication.SignOut();

                    // Second we clear the principal to ensure the Users does not retain any authentication
                    //required NameSpace: using System.Security.Principal;
                    HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                    Session.Clear();
                    System.Web.HttpContext.Current.Session.RemoveAll();

                    // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                    // this clears the Request.IsAuthenticated flag since this triggers a new request
                    return RedirectToLocal();
                }
                catch
                {
                    throw;
                }
            }
            //GET: SignInAsync
            private void SignInRemember(string UsersName, bool isPersistent = false)
            {
                // Clear any lingering authencation data
                FormsAuthentication.SignOut();

                // Write the authentication cookie
                FormsAuthentication.SetAuthCookie(UsersName, isPersistent);
            }
            [HttpPost]
            [AllowAnonymous]
            //[ValidateAntiForgeryToken]
            public ActionResult Login(UsersLoginViewModel Users)
            {
            foreach (var item in Userservice.Get())
            {
                if (item.Name == Users.Name && item.Password == Users.Password)
                {
                    Session["CurrentUsers"] = item;
                    Userservice.UserId = item.Id;
                    SignInRemember(Users.Name, true);

                    return RedirectToLocal(Users.ReturnUrl);
                }
            }

            ViewBag.Login = "the values incorrect";
            return View();


        }

            [HttpGet]
            public ActionResult Register()
            {

                return View(new UsersLoginViewModel());
            }

            [HttpPost]
            public ActionResult Register(UsersLoginViewModel Users)
            {
               
               
                    foreach (var us in Userservice.Get())
                    {
                        if (us.Name == Users.Name)
                        {
                            ViewBag.RegisterUsers = "User name exists already";
                            return View(Users);
                        }
                    }
                

                Userservice.Save(Users as UsersViewModel);
            
                return RedirectToAction("Login");
            }

            private ActionResult RedirectToLocal(string returnURL = "")
            {
                try
                {
                    // If the return url starts with a slash "/" we assume it belongs to our site
                    // so we will redirect to this "action"
                    if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                        return Redirect(returnURL);

                    // If we cannot verify if the url is local to our host we redirect to a default location
                    return RedirectToAction("Index", "Root");
                }
                catch
                {
                    throw;
                }
            }
        
    

}
}