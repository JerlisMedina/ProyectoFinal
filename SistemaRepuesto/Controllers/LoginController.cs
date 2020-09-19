using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SistemaRepuesto.Models;
using System.Web.Helpers;

namespace SistemaRepuesto.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel _login)
        {
            //if (ModelState.IsValid) //validating the user inputs  
            //{
                bool isExist = false;
                using (RepuestoEntities db = new RepuestoEntities())  // out Entity name is "SampleMenuMasterDBEntites"  
                {
                    isExist = db.Logins.Where(x => x.UserName.Trim().ToLower() == _login.UserName.Trim().ToLower() &&
                                              x.Password.Trim().ToLower() == _login.Password.Trim().ToLower()).Any(); //validating the user name in tblLogin table whether the user name is exist or not  
                    if (isExist)
                    {
                        LoginModel _loginCredentials = db.Logins.Where(x => x.UserName.Trim().ToLower() == _login.UserName.Trim().ToLower()).Select(x => new LoginModel
                        {
                            UserName = x.UserName,
                            PersonalName = x.PersonalName,
                            RoleName = x.Role.RoleDescripcion,
                            LoginRoleID = x.LoginRoleID,
                            LoginId = x.LoginID
                        }).FirstOrDefault();  // Get the login user details and bind it to LoginModel class  
                        List<MenuModel> _menus = db.SubMenus.Where(x => x.SubRoleID == _loginCredentials.LoginRoleID).Select(x => new MenuModel
                        {
                            MainMenuID = x.MainMenu.MainMenuID,
                            MainDescripcion = x.MainMenu.MainDescripcion,
                            SubID = x.SubID,
                            SubDescripcion = x.SubDescripcion,
                            SubController = x.SubController,
                            SubAction = x.SubAction,
                            RoleId = x.SubRoleID,
                            RoleName = x.Role.RoleDescripcion
                        }).OrderBy(x => x.MainMenuID).ToList(); //Get the Menu details from entity and bind it in MenuModels list.  
                        FormsAuthentication.SetAuthCookie(_loginCredentials.UserName, false); // set the formauthentication cookie  
                        Session["LoginCredentials"] = _loginCredentials; // Bind the _logincredentials details to "LoginCredentials" session  
                        Session["MenuMaster"] = _menus; //Bind the _menus list to MenuMaster session  
                        Session["UserName"] = _loginCredentials.UserName;
                        Session["PersonalName"] = _loginCredentials.PersonalName;
                    return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Please enter the valid credentials!...";
                        return View();
                    }
                }
            //}
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }
    }
}