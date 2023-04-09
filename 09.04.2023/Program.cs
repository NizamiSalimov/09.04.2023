using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace RegistrationAndLoginSystem
{
    class User
    {
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;
        public bool IsAdmin;

        public User(string firstName, string lastName, string email, string password, bool isAdmin)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
    class Program
    {
        static List<User> users = new List<User>();
        static User currentUser = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Available commands : ");
            Console.WriteLine("/register");
            Console.WriteLine("/login");
            Console.WriteLine("/exit");

            
            users.Add(new User("Super", "Admin", "admin@gmail.com", "123321", true));

            while (true)
            {
                Console.Write("Enter a command: ");
                string command = Console.ReadLine();

                if (command == "/register")
                {
                    Register();
                }
                else if (command == "/login")
                {
                    Login();
                }
                else if (command == "/exit")
                {
                    Console.WriteLine("Exited the program");
                    return;
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }
        }

        
        static void Register()
        {
            bool name = false;
            while (!name)
            {
                Console.Write("Pls enter name :");
                string ad = Console.ReadLine();

                if (ad.Length < 3 || ad.Length > 30)
                {
                    Console.WriteLine("Bir daha yoxlayin!");
                }
                else
                {
                    name = true;
                }
            }

            bool lastname = false;
            while (!lastname)
            {
                Console.Write("Pls enter last name :");
                string soyad = Console.ReadLine();

                if (soyad.Length < 3 || soyad.Length > 30)
                {
                    Console.WriteLine("Bir daha yoxlayin!");
                }
                else
                {
                    lastname = true;
                }
            }

            Console.Write("Please enter your email:");
            string email = Console.ReadLine();

            while (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email. Please enter again:");
                email = Console.ReadLine();
            }

            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine();

            Console.WriteLine("Please confirm your password:");
            string confirmPassword = Console.ReadLine();

            while (password != confirmPassword)
            {
                Console.WriteLine("Passwords do not match. Please enter again:");
                password = Console.ReadLine();
                Console.WriteLine("Please confirm your password:");
                confirmPassword = Console.ReadLine();
            }

            
            bool emailExists = false;
            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    emailExists = true;
                    Console.WriteLine("Email already exists. Please try again with a different email.");
                    break;
                }
            }

            if (emailExists)
            {
                return;
            }

            

            Console.WriteLine("You successfully registered, now you can login with your new account!");
        }


        static void Login()
        {
            Console.Write("Email Address: ");
            string email = Console.ReadLine()!;

            Console.Write("Password: ");
            string password = Console.ReadLine()!;

            User user = FindUser(email, password);

            if (user == null)
            {
                Console.WriteLine("Invalid email or password!");
            }
            else
            {
                currentUser = user;
                
                if (currentUser.IsAdmin)
                {
                    Console.WriteLine("Welcome to your account!", currentUser.FirstName, currentUser.LastName);
                }
            }
        }

        static bool IsValidEmail(string email)
        {
            if (email == null)
            {
                return false;
            }

            foreach (char c in email)
            {
                if (c == '\"' || c == '(' || c == ')' || c == ',' || c == ':' || c == ';' || c == '<' || c == '>' || c == '@' || c == '[' || c == '\\' || c == ']' || c == ' ')
                {
                    return false;
                }
            }
            if (email.IndexOf('@') != email.LastIndexOf('@'))
            {
                return false;
            }

            int dotIndex = email.LastIndexOf('.');
            if (dotIndex == -1 || dotIndex < email.IndexOf('@'))
            {
                return false;
            }

            return true;
        }

        static User FindUser(string email, string password)
        {
            foreach (User user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }

    

}