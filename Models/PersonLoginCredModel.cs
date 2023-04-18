using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Q1WebApp.Models
{
    public class PersonLoginCredModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public PersonLoginCredModel() { }
        public PersonLoginCredModel(string name, string pass) {
            Username = name;
            Password = pass;
        }
    }
}