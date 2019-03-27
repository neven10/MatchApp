using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string  UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string  Surname { get; set; }
    }
}
