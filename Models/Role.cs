using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }// пример админ простой пользователь редактор
        public virtual ICollection<User> Users{ get; set; }
        public Role()
        {
            Users = new List<User>();
            //  DocumentСatalogs = new List<DocumentСatalog>();
        }
    }
}
