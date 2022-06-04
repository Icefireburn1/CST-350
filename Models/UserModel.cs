using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Models
{
    public class UserModel
    {
        public UserModel(string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.age = age;
            this.state = state;
            this.email = email;
            this.username = username;
            this.password = password;
        }

        public UserModel() { }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [DisplayName("First Name")]
        public string firstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        [DisplayName("Sex")]
        public string sex { get; set; }

        [Required]
        [Range(18,120)]
        [DisplayName("Age")]
        public int age { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [DisplayName("State")]
        public string state { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DisplayName("Username")]
        public string username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }

    }
}
