using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomePage.Models
{
    public class CourseModel

    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
        public int SelectedClassId { get; set; }


        public CourseModel(int classId, string className, string classDescription, decimal classPrice)
        {
            ClassId = classId;
            ClassName = className;
            ClassDescription = classDescription;
            ClassPrice = classPrice;

        }
    }


}