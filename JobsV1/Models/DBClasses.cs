using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsV1.Models
{
    public class AppUser
    {
        public string UserName { get; set; }
    }

    public class DBClasses
    {
        JobDBContainer db = new JobDBContainer();

        public IList<AppUser> getUsers()
        {
            var data = db.Database.SqlQuery<AppUser>("Select UserName from AspNetUsers");
            return data.ToList();
        }
    }

    
}