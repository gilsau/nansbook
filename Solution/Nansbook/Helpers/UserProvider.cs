using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nansbook.Models;

namespace Nansbook.Helpers
{
    public static class UserProvider
    {
        public static string SetCurrentStore(int storeId) {
            
            //get current user
            UserProfile currUser = (UserProfile)HttpContext.Current.Session["User"];
            
            //update current store for this user in db
            NansbookEntities db = new NansbookEntities();

            //set all stores for this user to non-current
            IEnumerable<UserXStore> userStores = db.UserXStores.Where(us => us.UserId == currUser.UserId);
            foreach (UserXStore us in userStores)
            {
                us.Current = false;
            }

            //set this store for this user as current
            UserXStore userStoreToUpdate = db.UserXStores.FirstOrDefault(us => us.UserId == currUser.UserId && us.StoreId == storeId);
            userStoreToUpdate.Current = true;
            
            db.SaveChanges();

            //update current user with new info from db
            HttpContext.Current.Session["User"] = db.UserProfiles.FirstOrDefault(up => up.UserId == currUser.UserId);

            return GetCurrentUserStore().Store.Name;
        }

        public static UserXStore GetCurrentUserStore()
        {
            //get current user
            UserProfile currUser = (UserProfile)HttpContext.Current.Session["User"];
            UserXStore userStore = null;

            if (currUser != null)
            {

                //get current user's UserXStore object
                userStore = currUser.UserXStores.FirstOrDefault(us => us.Current == true);

                //if none exists, Enter the Default store for the current user
                if (userStore == null)
                {
                    NansbookEntities db = new NansbookEntities();

                    //add default store for user
                    UserXStore userStoreToAdd = new UserXStore();
                    userStoreToAdd.UserId = currUser.UserId;
                    userStoreToAdd.StoreId = 1; //1 = independent, no store affiliation
                    userStoreToAdd.Current = true;
                    db.UserXStores.Add(userStoreToAdd);
                    db.SaveChanges();

                    //reset session user with new info
                    currUser = db.UserProfiles.FirstOrDefault(up => up.UserId == currUser.UserId);
                    HttpContext.Current.Session["User"] = currUser;

                    //get userstore object for user
                    userStore = currUser.UserXStores.FirstOrDefault(us => us.Current == true);
                }
            }

            return userStore;
        }
    }
}