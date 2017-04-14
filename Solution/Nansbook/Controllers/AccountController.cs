using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using Nansbook.Filters;
using Nansbook.Models;
using Nansbook.Helpers;

namespace Nansbook.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(int logout=0)
        {
            if (logout > 0) {
                WebSecurity.Logout();
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            string retView = "Login";

            if (WebSecurity.Login(model.UserName, model.Password, true))
            {
                NansbookEntities db = new NansbookEntities();
                Session["User"] = (UserProfile)db.UserProfiles.Single(u => u.UserName == model.UserName);
                
                retView = "../Home/Index";
            }
            else {
                TempData["msg"] = "Incorrect username/password combination. Please try again or click the question mark to retrieve your password.";
            }

            return View(retView);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signup(SignupModel model)
        {
            try
            {
                string token = WebSecurity.CreateUserAndAccount(model.Email, model.Password, new { FirstName = model.FirstName, LastName = model.LastName, PhotoFileName = "default.jpg" }, true);
                Roles.AddUserToRole(model.Email, "Admin");

                string server = Request.ServerVariables["server_name"];
                string port = Request.ServerVariables["server_port"] != null ? ":" + Request.ServerVariables["server_port"] : string.Empty;
                string baseAddress = server + port;
                string verifyLink = string.Format("http://{0}/account/confirm?token={1}", baseAddress, token);

                Result result = new Result();
                Emailer.SendMail("Nansbook Signup", string.Format("Hi {1},<br/><br/>Thanks for signing up with Nansbook. Please click the link below to verify your account.<br/><a href='{0}'>{0}</a><br/><br/>Thanks,<br/><br/>- Nansbook Admin Staff", verifyLink, model.FirstName), "admin@techstarapps.com", "Nansbook Admin", model.Email, out result);

                if (result.Success)
                {
                    TempData["msg"] = "You have successfully signed up. Please check your email and verify your account.";
                }
                else 
                {
                    TempData["msg"] = string.Format("There was a problem sending the verification email.<br/><br/>{0}<br/><br/>Try to sign up with a different email address.", result.MessageForUser); 
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("A problem occurred during signup. {0}", ex.Message);
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Confirm(string token)
        {
            //account is confirmed
            if (WebSecurity.ConfirmAccount(token))
            {
                TempData["msg"] = "Your account has been confirmed.<br/>Please set your security questions below.";
                ViewBag.UserId = WebSecurity.GetUserIdFromPasswordResetToken(token);
            }
            else
            {
                TempData["msg"] = "There was a problem confirming your account.";
            }

            //send back security questions
            NansbookEntities db = new NansbookEntities();
            return View(db.SecurityQuestions.ToList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Confirm(ConfirmModel model)
        {
            NansbookEntities db = new NansbookEntities();
            int userId = db.webpages_Membership.Single(u => u.ConfirmationToken == model.Token).UserId;

            UserProfile user = db.UserProfiles.Single(u => u.UserId == userId);
            user.SecQuestion1 = model.SecurityQuestion1;
            user.SecQuestion2 = model.SecurityQuestion2;
            user.SecQuestion3 = model.SecurityQuestion3;
            user.SecAnswer1 = model.SecurityAnswer1;
            user.SecAnswer2 = model.SecurityAnswer2;
            user.SecAnswer3 = model.SecurityAnswer3;
            db.SaveChanges();

            string server = Request.ServerVariables["server_name"];
            string port = Request.ServerVariables["server_port"] != null ? ":" + Request.ServerVariables["server_port"] : string.Empty;
            string baseAddress = server + port;
            string setup1Link = string.Format("http://{0}/account/setup1", baseAddress);

            TempData["msg"] = string.Format("Your security questions have been saved. Please proceed to the login screen.<br/><br/><a href='{0}'>Login</a>", setup1Link);

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Setup()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SetupAdmin()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SetupUser(int manager=0)
        {
            return View();
        }

        public class ConfirmModel
        {
            public string Token { get; set; }
            public int SecurityQuestion1 { get; set; }
            public int SecurityQuestion2 { get; set; }
            public int SecurityQuestion3 { get; set; }
            public string SecurityAnswer1 { get; set; }
            public string SecurityAnswer2 { get; set; }
            public string SecurityAnswer3 { get; set; }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotUser()
        {
            TempData["msg"] = "Provide three of the following, to retrieve your security questions.";
            ViewBag.Step = 1;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotUser(ForgotUserModel model)
        {
            NansbookEntities db = new NansbookEntities();
            UserProfile user = model.UserId > 0 ? db.UserProfiles.Single(u => u.UserId == model.UserId) : null;

            //get security questions
            if (model.Step == 1)
            {
                List<UserProfile> users = null;
                if (model.FirstName != null)
                {
                    users = db.UserProfiles.Where(u => u.FirstName == model.FirstName).ToList();
                }
                if (model.LastName != null)
                {
                    users = users.Where(u => u.LastName == model.LastName).ToList();
                }
                if (model.HomePhone != null)
                {
                    users = users.Where(u => u.HomePhone == model.HomePhone).ToList();
                }
                if (model.MobilePhone != null)
                {
                    users = users.Where(u => u.MobilePhone == model.MobilePhone).ToList();
                }
                if (model.SocialSecurity != null)
                {
                    users = users.Where(u => u.SocialSecurity == model.SocialSecurity).ToList();
                }

                //no users are found
                if(users.Count() == 0)
                {
                    TempData["msg"] = "No user was found. Please contact your administrator.";
                    ViewBag.Step = 1;
                }

                //more than one user was found
                else if (users.Count() > 1)
                {
                    TempData["msg"] = "More than one user was found. Please contact your administrator.";
                    ViewBag.Step = 1;
                }

                //one user was found
                else if(users.Count() == 1)
                {
                    //user id
                    ViewBag.UserId = users[0].UserId;
                    
                    //questions
                    List<SecurityQuestion> questions = new List<SecurityQuestion>();
                    questions.Add(users[0].SecurityQuestion);
                    questions.Add(users[0].SecurityQuestion1);
                    questions.Add(users[0].SecurityQuestion2);
                    ViewBag.Questions = questions;

                    //message sent back
                    TempData["msg"] = "Answer the security questions below to retrieve your username.";

                    //increase step
                    ViewBag.Step = 2;
                }
                
            }
            else if (model.Step == 2)
            {
                bool answered = true;
                
                if(user.SecAnswer1 != model.SecurityAnswer1)
                {
                    answered = false;
                }
                else if (user.SecAnswer2 != model.SecurityAnswer2)
                {
                    answered = false;
                }
                else if (user.SecAnswer2 != model.SecurityAnswer2)
                {
                    answered = false;
                }
                else 
                {
                    string server = Request.ServerVariables["server_name"];
                    string port = Request.ServerVariables["server_port"] != null ? ":" + Request.ServerVariables["server_port"] : string.Empty;
                    string baseAddress = server + port;
                    string loginLink = string.Format("http://{0}/account/login", baseAddress);

                    TempData["msg"] = string.Format("You have answered your security questions successfully!<br/><br/>Your username is {0}<br/><br/>Use the link below to login.<br/><br/><a href='{1}'>Login</a>", user.UserName, loginLink);
                }

                if (!answered)
                {
                    //user id
                    ViewBag.UserId = model.UserId;

                    //questions
                    List<SecurityQuestion> questions = new List<SecurityQuestion>();
                    questions.Add(user.SecurityQuestion);
                    questions.Add(user.SecurityQuestion1);
                    questions.Add(user.SecurityQuestion2);
                    ViewBag.Questions = questions;

                    //message sent back
                    TempData["msg"] = "You answered one of your security questions incorrectly.";

                    //increase step
                    ViewBag.Step = 2;
                }
            }
            
            return View();
        }

        public class ForgotUserModel
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string HomePhone { get; set; }
            public string MobilePhone { get; set; }
            public string SocialSecurity { get; set; }
            public string SecurityQuestion1 { get; set; }
            public string SecurityQuestion2 { get; set; }
            public string SecurityQuestion3 { get; set; }
            public string SecurityAnswer1 { get; set; }
            public string SecurityAnswer2 { get; set; }
            public string SecurityAnswer3 { get; set; }
            public int Step { get; set; }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPass()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult ViewProfile(int id=0)
        {
            id = id == 0 ? WebSecurity.CurrentUserId : id;

            NansbookEntities db = new NansbookEntities();
            UserProfile up = db.UserProfiles.SingleOrDefault(u => u.UserId == id);

            ViewBag.Roles = db.webpages_Roles;
            ViewBag.States = db.States;

            return View(up);
        }

        [HttpPost]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult ViewProfile(UserModel model)
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

            //get drop down lists
            ViewBag.Roles = db.webpages_Roles;
            ViewBag.States = db.States;

            //return updated user
            UserProfile up = db.UserProfiles.SingleOrDefault(u => u.UserId == model.Id);
            return View(up);
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Notifications()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionCheckAttribute]
        public ActionResult Settings()
        {
            return View();
        }
    }
}
