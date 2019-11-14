using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AEGEE_Project.Model
{
    public class User
    {
        [Key]
        [Index("IX_UserId", 0, IsUnique = true)]
        public int Id { get; set; }
        [MaxLength(100)]
        [Index("IX_UserLogin", 1, IsUnique = true)]
        public string Login { get; set; }
        [MaxLength(25)]
        public string Password { get; set; }
        [MaxLength(25)]
        public string Rights { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(25)]
        public string Surname { get; set; }

        public int Age { get; set; }
        
        public string Photo { get; set; }


    }
}
