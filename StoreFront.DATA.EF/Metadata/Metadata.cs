using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models/*.Metadata*/
{
    #region Metadata Notes

    /*
    * COMMON METADATA ATTRIBUTES
    * Validation:
    * [Required(ErrorMessage = "Message Here")]
    * - forces us to provide a value for a property
    * [StringLength(##, ErrorMessage = "Message Here")]
    * - maximum number of characters a user can enter for a value
    * [DataType(DataType.SomeType)]
    * - forces default validation based on 'SomeType' that is selected (options: .PostalCode, .PhoneNumber, .EmailAddress...)
    * [Range(minVal#, maxVal#)]
    * - only allows input between the specified minimum and maximum values
    * 
    * Display:
    * [Display(Name = "Name Goes Here")]
    * - updates the label for a property - initially tied to table header values
    * [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true/false)]
    * - changes the way values are presented in a view.
    * - replace {0:d} with your preferred formatting (like 0:c, 0:n0...)
    * - ApplyFormatInEditMode will forcibly validate the string formatting in textboxes
    *      IF this is set to true:
    *      - When editing/creating a record with formatting like {0:d}, you must input the date as 'MM/dd/yyyy'
    *      - When editing/creating a record with formatting like {0:c}, you must input the value as $5.99 
    *      IF this is set to false:
    *      - Special formatting characters are not required when editing/creating
    * */

    #endregion
    public class CategoryMetadata
    {
        //public int CategoryId { get; set; }

        [Required]
        [Display(Name ="Category Name")]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        public string CategoryName { get; set; } = null!;
    }

    public class ClassLocationMetadata
    {
        //public int LocationId { get; set; }

        [Required]
        [Display(Name = "Campus")]
        [StringLength(100, ErrorMessage = "*Max 100 Characters")]
        public string CampusName { get; set; } = null!;

        [Required]
        [StringLength(150, ErrorMessage = "*Max 150 Characters")]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        public string State { get; set; } = null!;

    }

    public class OrderMetadata
    {
        //public int OrderId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Purchase Date")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "[N/A")]
        public DateTime? PurchaseDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}", NullDisplayText = "[N/A")]
        [Display(Name = "Purchase Price")]
        public decimal? PurchasePrice { get; set; }


        public int ProductId { get; set; }
    }

    public class ProductMetadata
    {
        //public int ProductId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [StringLength(500, ErrorMessage = "*Max 500 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Description { get; set; }


        public string? ProductImage { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(100, ErrorMessage = "*Max 100 Characters")]
        public string ProductName { get; set; } = null!;


        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }

        [Display(Name = "Online")]
        public bool IsOnline { get; set; }

    }

    public class UserDetailMetadata
    {
        //public int UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [StringLength(100, ErrorMessage = "*Max 100 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Address { get; set; }

        [StringLength(150, ErrorMessage = "*Max 150 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? City { get; set; }

        [StringLength(150, ErrorMessage = "*Max 150 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Time Zone")]
        public string? TimeZone { get; set; }

        [StringLength(5, ErrorMessage = "*Max 5 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        public string? PostalCode { get; set; }

        [StringLength(15, ErrorMessage = "*Max 15 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Country { get; set; }

        [StringLength(24, ErrorMessage = "*Max 24 Digit Phone Number")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [StringLength(150, ErrorMessage = "*Max 150 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Occupation { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}", NullDisplayText = "[N/A]")]
        public decimal? Salary { get; set; }

        [StringLength(255, ErrorMessage = "*Max 255 Characters")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }
    }



}//end namespace
