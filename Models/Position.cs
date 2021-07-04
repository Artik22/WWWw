using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<User> Users { get; set; }// роль в системе

        public virtual ICollection<Group> Groups{ get; set; }// Должность  сотрудника
        public Position()
        {
            Articles = new List<Article>();
            Users = new List<User>();
            Groups = new List<Group>();
            //  DocumentСatalogs = new List<DocumentСatalog>();
        }
    }
}
