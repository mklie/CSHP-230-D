using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePageEF
{
    class DBAccessor
    {
        private static readonly HomePageDBEntities entities;

        static DBAccessor()
        {
            entities = new HomePageDBEntities();
            entities.Database.Connection.Open();
        }

        public static HomePageDBEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}
