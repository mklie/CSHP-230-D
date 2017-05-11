using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace HomePage.Models
{
    public class UserCourseModel
    {
        public int UserId { get; set; }  
        public int ClassId { get; set; }
        [DisplayName("Name")]
        public string ClassName { get; set; }
        [DisplayName("Description")]
        public string ClassDescription { get; set; }
        [DisplayName("Paid")]
        public decimal ClassPrice { get; set; }

    }
}