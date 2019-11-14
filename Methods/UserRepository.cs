using AEGEE_Project.Model;
using AEGEE_Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AEGEE_Project.Methods
{
    class UserRepository
    {

        public User LogIn()
        {
            using (UserContext context = new UserContext())
            {
                User user1 = new User();
                LoginWindow loginWindow = new LoginWindow();
                MessageBox.Show("Lol");
                foreach (User user in context.Users.ToList())
                {
                    if (user.Login == loginWindow.LoginBox.Text)
                    {
                        if (user.Password == loginWindow.PasswordBox.Text)
                        {
                            Console.Clear();
                            Console.WriteLine($"Welcome {user.Name}");
                            return user;
                        }
                        Console.WriteLine("Wrong Password");
                        return user1;
                    }
                }
                Console.WriteLine("Login doesn't exist");
                return user1;
            }
        }


    }
}
