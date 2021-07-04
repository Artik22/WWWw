using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
   
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }// дата Создания
        public DateTime DateOfEdit { get; set; }// дата Изменения 

        public virtual ICollection<User> Users { get; set; }// пользователи
        public virtual ICollection<Group> Groups { get; set; }// группы документов
        public virtual ICollection<Position> Positions { get; set; }// Должность  сотрудника
        public Article()
        {
            Users = new List<User>();
            Groups = new List<Group>();
            Positions = new List<Position>();
        }

      
   
    }
}
