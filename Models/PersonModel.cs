using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Q1WebApp.Models
{
    public class PersonModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }

        public PersonModel() { }

        public PersonModel(int id, string name, string surname, string password, DateTime lastLogin)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Password = password;
            LastLogin = lastLogin;
        }
    }
}