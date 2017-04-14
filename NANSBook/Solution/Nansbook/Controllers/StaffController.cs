using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;
using System.Web.Mvc;
using Nansbook.Models;
using Nansbook.Helpers;

namespace Nansbook.Controllers
{
    public class StaffController : Controller
    {
        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Users()
        {
            NansbookEntities db = new NansbookEntities();
            List<UserProfile> list = db.UserProfiles.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();

            ViewBag.Roles = db.webpages_Roles.OrderBy(r => r.RoleName);
            ViewBag.States = db.States.OrderBy(s => s.Name);

            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddUser(UserModel model)
        {
            try
            {
                //create user
                string pwd = Membership.GeneratePassword(8, 2);
                WebSecurity.CreateUserAndAccount(model.UserName, pwd, new { FirstName = model.FirstName, MiddleName = model.MiddleName, LastName = model.LastName, NickName = model.NickName, Address1 = model.Address, Address2 = model.Address2, City = model.City, State = model.StateId, HomePhone = model.HomePhone, MobilePhone = model.MobilePhone, SocialSecurity = model.SocialSecurity, PhotoFileName = "default.jpg" });
                System.Web.Security.Roles.AddUserToRole(model.UserName, model.Role);

                //Upload photo file
                if (model.PhotoFileName != null)
                {
                    NansbookEntities db = new NansbookEntities();
                    UserProfile user = db.UserProfiles.Single(u => u.UserName == model.UserName);

                    Result result = new Result();
                    Uploader.UploadFile(model.PhotoFileName, "profilepics", out result);

                    //Save in database
                    if (result.Success && result.DynObject != null)
                    {
                        user.PhotoFileName = result.DynObject;

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            result.Success = false;
                            result.MessageForUser = ex.Message;
                        }
                    }
                    db.SaveChanges();
                }
                
                //send email to new user
                string server = Request.ServerVariables["server_name"];
                string port = Request.ServerVariables["server_port"] != null ? ":" + Request.ServerVariables["server_port"] : string.Empty;
                string baseAddress = server + port;
                string loginLink = string.Format("http://{0}/account/login", baseAddress);

                Result result2 = new Result();
                Emailer.SendMail("Nansbook Invitation", string.Format("Your manager has sent you an invitation for Nansbook. Please use the password provided and click the link below to log into your account. Once inside, you can update your password and profile.<br/><br/>Password: {0}<br/><br/><a href='{1}'>{1}</a><br/><br/>Thanks,<br/><br/>- Nansbook Admin Staff", pwd, loginLink), "admin@techstarapps.com", "Nansbook Admin", model.UserName, out result2);

                //msg back to screen
                if (result2.Success)
                {
                    TempData["msg"] = string.Format("You have successfully invited {0} {1}({2}). Have this user check his/her email for the password and login link.", model.FirstName, model.LastName, model.UserName);
                }
                else
                {
                    TempData["msg"] = string.Format("There was a problem sending the invitation email.<br/><br/>{0}<br/><br/>Delete the user and add him/her again later.", result2.MessageForUser);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred adding this user. {0}", ex.Message);
            }

            return RedirectToAction("Users");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdateUser(UserModel model)
        {
            NansbookEntities db = new NansbookEntities();
            UserProfile user = db.UserProfiles.Single(u => u.UserId == model.Id);

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.Address1 = model.Address;
            user.Address2 = model.Address2;
            user.City = model.City;
            user.State = model.StateId > 0 ? model.StateId : user.State;
            user.HomePhone = model.HomePhone;
            user.MobilePhone = model.MobilePhone;
            user.SocialSecurity = model.SocialSecurity;

            //Upload photo file
            if (model.PhotoFileName != null)
            {
                Result result = new Result();
                Uploader.UploadFile(model.PhotoFileName, "profilepics", out result);

                //Save in database
                if (result.Success && result.DynObject != null)
                {
                    user.PhotoFileName = result.DynObject;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.MessageForUser = ex.Message;
                    }
                }
            }
            db.SaveChanges();

            //update role
            System.Web.Security.Roles.RemoveUserFromRoles(model.UserName, System.Web.Security.Roles.GetRolesForUser(model.UserName));
            System.Web.Security.Roles.AddUserToRole(model.UserName, model.Role);

            //Update password
            if (!string.IsNullOrEmpty(model.Password))
            {
                string token = WebSecurity.GeneratePasswordResetToken(model.UserName);
                WebSecurity.ResetPassword(token, model.Password);
            }

            
            return RedirectToAction("Users");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelUser(UserModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if(ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    int id2 = int.Parse(id);
                    UserProfile user = db.UserProfiles.Single(u => u.UserId == id2);

                    if (user != null)
                    {
                        //remove user from roles
                        System.Web.Security.Roles.RemoveUserFromRoles(user.UserName, System.Web.Security.Roles.GetRolesForUser(user.UserName));

                        //remove user
                        db.UserProfiles.Remove(user);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Roles()
        {
            NansbookEntities db = new NansbookEntities();
            List<webpages_Roles> list = db.webpages_Roles.OrderBy(r => r.RoleName).ToList();

            ViewBag.Users1 = db.UserProfiles.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);

            return View(list);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult AddRole(RoleModel model)
        {
            try
            {
                //create role
                if (!System.Web.Security.Roles.RoleExists(model.RoleName))
                {
                    System.Web.Security.Roles.CreateRole(model.RoleName);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred creating this role. {0}", ex.Message);
            }

            return RedirectToAction("Roles");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult UpdateRole(RoleModel model)
        {
            try
            {
                //update role
                NansbookEntities db = new NansbookEntities();
                webpages_Roles role = db.webpages_Roles.Single(r => r.RoleId == model.Id);
                role.RoleName = model.RoleName;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred updating this role. {0}", ex.Message);
            }

            return RedirectToAction("Roles");
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult DelRole(RoleModel model)
        {
            NansbookEntities db = new NansbookEntities();
            string[] ids = model.IdsForDelete.Split(',');

            if (ids.Count() > 0)
            {
                foreach (string id in ids)
                {
                    int id2 = int.Parse(id);
                    webpages_Roles role = db.webpages_Roles.Single(r => r.RoleId == id2);

                    if (role != null)
                    {
                        //remove users from role
                        string[] users = System.Web.Security.Roles.GetUsersInRole(role.RoleName);
                        if(users.Count() > 0)
                        {
                            System.Web.Security.Roles.RemoveUsersFromRole(users, role.RoleName);
                        }

                        //remove role
                        System.Web.Security.Roles.DeleteRole(role.RoleName);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Roles");
        }


        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Permissions()
        {
            return View();
        }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string IdsForDelete { get; set; }
        public string RoleName { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string IdsForDelete { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public HttpPostedFileBase PhotoFileName { get; set; }
        public string SocialSecurity { get; set; }
        public string Role { get; set; }
    }
}
