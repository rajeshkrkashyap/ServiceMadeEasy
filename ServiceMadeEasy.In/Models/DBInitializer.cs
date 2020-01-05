using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServiceMadeEasy.In.Models
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            DateTime dateTime = new DateTime();

            IList<IdentityRole> seedRoles = new List<IdentityRole>();
            seedRoles.Add(new IdentityRole() { Id = "0", Name = "Admin" });
            seedRoles.Add(new IdentityRole() { Id = "1", Name = "Sponsor" });
            seedRoles.Add(new IdentityRole() { Id = "2", Name = "Employee" });

            foreach (IdentityRole item in seedRoles)
            {
                context.Roles.Add(item);
            }       

            base.Seed(context);
        }
    }
}