using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBootcamp32.Model
{
    [Table ("TB_M_User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset RegisterDate { get; set; }

        public Role Role { get; set; }


        public User()
        {

        }

        public User(string name, string email, string password, DateTimeOffset registerDate, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.RegisterDate = registerDate;
            this.Role = role;
        }
        
    }
}
