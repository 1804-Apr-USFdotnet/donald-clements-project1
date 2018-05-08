using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RevViews.Models
{
    public class ContactUs
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Field must be at least 5 characters, and not exceed 100")]
        public string Name { get; set; }
        [Required][DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Field must be at least 5 characters, and not exceed 100")]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Field must be at least 10 characters, and not exceed 1000.")]
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; }
    }
}