//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace RevViews.Models
{
    public class Review
    {
        [Key] public int? ReviewID { get; set; }
        [Required]
        public int RestaurantID { get; set; }
        [Required]
        public string Username { get; set; }

        [Range(0, 5, ErrorMessage = "Enter number between 0 to 5")] [Required] public int Rating { get; set; }
        [Required]
        public string Headline { get; set; }
        [Required][DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Original Review Date")]
        public DateTime ReviewedOn { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}