using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Castle.NewsManagement
{
    public class Permissions : IPermissionProvider {
        public static readonly Permission NewsAdmin = new Permission { Description = "新闻管理", Name = "NewsAdmin" };
      


        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
               NewsAdmin
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] { NewsAdmin }
                }

            };
        }
    }
}


