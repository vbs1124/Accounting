using Microsoft.AspNet.Identity.EntityFramework;

namespace Vserv.Accounting.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}