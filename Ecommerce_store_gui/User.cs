using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_store_gui
{
    public class User
    {
        protected string username;
        protected string password;
        protected Address address;

        public User() { }

        public User(string username, string password, Address address)
        {
            Username = username;
            Password = password;
            Address = address;
        }
        public string Username
        {
            get { return username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be null or empty.", nameof(Username));
                }

                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Username cannot contain numbers.", nameof(Username));
                }

                username = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password cannot be null or empty.", nameof(Password));
                }
                password = value;
            }
        }

        public Address Address
        {
            get { return address; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Address), "Address cannot be null.");
                }
                address = value;
            }
        }

        public override string ToString()
        {
            return $"Username: {Username}, Address: {Address}";
        }
    }
}
