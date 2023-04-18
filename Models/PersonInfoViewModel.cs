using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Q1WebApp.Models
{
    public class PersonInfoViewModel
    {
       /* public PersonInfoViewModel(PersonModel personModel, InfoModel infoModel)
        {
            this.personModel = personModel;
            this.infoModel = infoModel;
        }*/
        public PersonModel personModel { get; set; }
        public InfoModel infoModel { get; set; }

    }
}