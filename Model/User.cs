using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AEGEE_Project.Model
{
    class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        bool result = Name.All(Char.IsLetter);
                        if (result)
                        {
                            error = "Name should have only letters";
                        }
                        break;
                    case "Surname":
                        bool result2 = Surname.All(Char.IsLetter);
                        if (result2)
                        {
                            error = "Surname should have only letters";
                        }
                        break;
                }
                return error;
            }
        }

        
    }
}
