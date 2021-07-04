using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName{ get; set; }
        public virtual ICollection<Article> Articles{ get; set; }
        public virtual ICollection<User> Users { get; set; }// роль в системе

        public virtual ICollection<Position> Positions { get; set; }// Должность  сотрудника
        public Group()
        {
            Articles = new List<Article>();
            Users = new List<User>();
            Positions = new List<Position>();
            //  DocumentСatalogs = new List<DocumentСatalog>();
        }

    }
}
