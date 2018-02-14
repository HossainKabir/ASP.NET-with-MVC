using System.ComponentModel.DataAnnotations;

namespace EatmEntityFramework.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [StringLength(250)]
        public string AccountHolderName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public double Balance { get; set; }
    }
}