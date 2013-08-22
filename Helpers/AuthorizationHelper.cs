using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _250ml_MVC4_2.Models;

namespace _250ml_MVC4_2.Helpers
{
    public class AuthorizationHelper
    {
        public static bool IsAuthorized(int UserId, int HappeningId) {
            UsersContext db = new UsersContext();
            int OwnerId = db.Happenings.Find(HappeningId).Owner.UserId;
            return (OwnerId == UserId);
        }

    }
}