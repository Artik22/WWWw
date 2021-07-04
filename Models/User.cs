using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
   
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirsName { get; set; } //имя
        public string LastName { get; set; } //фамилия
     //   public string UserPosition { get; set; }// должность пользователя
        public DateTime DateOfAcceptance { get; set; }// дата принятия на работу
        public DateTime DateRegistration { get; set; }// дата регистрации 
     //   public int? RoleId { get; set; }
        public virtual ICollection<Article> Articles { get; set; }// документы
        public virtual ICollection<Role> Roles { get; set; }// роль в системе
        public virtual ICollection<Group> Groups{ get; set; }// группы документов
        public virtual ICollection<Position> Positions{ get; set; }// Должность  сотрудника
        public User() 
        {
            Articles = new List<Article>();
            Roles = new List<Role>();
            Groups = new List<Group>();
            Positions = new List<Position>();
        }
    }
}
