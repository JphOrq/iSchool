using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace MVCDemoProject.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Please enter a valid student ID here!")]
        [Display(Name = "ID #")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please enter a valid student name here!")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please input your complete name!")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please enter the date of birth.")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MMMM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select a gender.")]
        public string Gender { get; set; }

        // Calculated property for Age
        public int Age
        {
            get
            {
                // Calculate the age based on the current year
                int currentYear = DateTime.Now.Year;
                int yearOfBirth = DateOfBirth.Year;
                int age = currentYear - yearOfBirth;
                // Adjust age if the birthday hasn't occurred yet this year
                if (DateOfBirth > DateTime.Now.AddYears(-age))
                    age--;
                return age;
            }
        }

        [Display(Name = "Date Joined")]
        [DisplayFormat(DataFormatString = "{0:MMMM/dd/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
    }
}