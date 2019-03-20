using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class User
    {
        public int Id { get; set; }
        public int Guid { get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public string  Name { get; set; }
        public string  Surname { get; set; }
    }
}
