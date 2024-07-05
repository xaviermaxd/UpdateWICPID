using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }
        public DateTime Birthdate { get; set; }
        public int Notfound { get; set; }
        public int ExecutionUpdateWicPIDID { get; set; }
    }
}