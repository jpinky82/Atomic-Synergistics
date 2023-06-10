using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.DATA.EF.Models//.Metadata
{
    //internal class Metadata
    //{
    //}

    #region Product
public class ProductMetadata
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "* Product Name is required")]
        [StringLength(150)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        //[Range(0, (double)decimal.MaxValue)]        
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Range(0, (double)decimal.MaxValue)]
        [Display(Name = "Costs Per Unit")]
        public decimal? CostPerUnit { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Range(0, (double)decimal.MaxValue)]
        [Display(Name = "Price Per Unit")]
        public decimal? PricePerUnit { get; set; }

        [StringLength(50)]
        [Display(Name = "Unit Type")]
        public string? UnitType { get; set; }

        [Range(0, (double)short.MaxValue)]
        [Display(Name = "Units In Stock")]
        public short? UnitsInStock { get; set; }

        [Range(0, (double)short.MaxValue)]
        [Display(Name = "Units On Order")]
        public short? UnitsOnOrder { get; set; }
        [Display(Name = "Product Status")]
        public int? ProductStatusId { get; set; }
        [Display(Name = "ProductType")]
        public int ProductTypeId { get; set; }
        [Display(Name = "Vendor")]
        public int? VendorId { get; set; }

        [StringLength(100)]
        [Display(Name = "Image")]
        public string? ProductImage { get; set; }
    }
    #endregion

    #region Product Status
    public class ProductStatusMetadata
    {
        public int ProductStatusId { get; set; }

        [Required(ErrorMessage = "* Product Status is required")]
        [Display(Name = "Product Status")]
        [StringLength(100)]
        public string ProductStatusName { get; set; } = null!;
    }

    #endregion

    #region ProductType
    public class ProductTypeMetadata
    {
        public int ProductTypeId { get; set; }

        [Required(ErrorMessage = "Product Type is required")]
        [StringLength(150)]
        [Display(Name = "Product Type")]
        public string ProductTypeName { get; set; } = null!;
    }
    #endregion

    #region Region
    public class RegionMetadata
    {
        [Display(Name ="Region")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Region is required")]
        [StringLength(100)]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; } = null!;
    }
    #endregion

    #region Sale
    public class SaleMetadata
    {
        public int SaleId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }


        [Display(Name = "Salesperson")]
        public int SalespersonId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Range(0, (double)decimal.MaxValue)]
        [Display(Name = "Sale Total")]
        public decimal? SaleTotal { get; set; }
    }
    #endregion

    #region SaleProduct

    public class SaleProductMetadata
    {
        public int SaleProductId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }

        [Range(0, (double)short.MaxValue)]
        [Display(Name = "Quantity")]
        public short SaleQuantity { get; set; }
    }
    #endregion

    #region Vendor
    public class VendorMetadata
    {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Vendor Name is required")]
        [StringLength(150)]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; } = null!;

        [Required(ErrorMessage = "Contact is required")]
        [StringLength(75)]
        [Display(Name = "Contact")]
        public string VendorContact { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string VendorPhone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string VendorEmail { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string VendorAddress { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        [Display(Name = "City")]
        public string VendorCity { get; set; } = null!;

        [StringLength(2)]
        [Display(Name = "State")]
        public string? VendorState { get; set; }

        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string? VendorPostalCode { get; set; }
        public int CountryId { get; set; }
    }
    #endregion

    #region Countries
    public class CountryMetadata
    {
        public int CountryID { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; } = null!;
        [Display(Name = "Region")]
        public int RegionID { get; set; }
    }
    #endregion

    #region Customers
    public class CustomerMetadata
    {


        public int CustomerId { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = null!;
        [Required]
        [StringLength(75)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; } = null!;
        [Required]
        [StringLength(20)]
        [Display(Name = "Phone")]
        public string ContactPhone { get; set; } = null!;
        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string ContactEmail { get; set; } = null!;
        [Required]
        [StringLength(100)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; } = null!;
        [Required]
        [StringLength(100)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; } = null!;

        [StringLength(2)]
        [Display(Name = "State")]
        public string? BillingState { get; set; }
        [StringLength(10)]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string? BillingPostalCode { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
    #endregion

    #region Departments
    public class DepartmentMetadata
    {
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; } = null!;
        [Display(Name = "Location")]
        public int? LocationId { get; set; }
    }
    #endregion

    #region Employees
    public class EmployeeMetadata
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        [StringLength(100)]
        [Display(Name = "Position")]
        public string? Position { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Salary")]
        public decimal? Salary { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Termination Date")]
        [DataType(DataType.Date)]
        public DateTime? TerminationDate { get; set; }
        [StringLength(100)]
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [StringLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }
        [StringLength(2)]
        [Display(Name = "State")]
        public string? State { get; set; }
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]

        public string? PostalCode { get; set; }
        [Display(Name = "Country ID")]
        public int? CountryId { get; set; }
        [StringLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [StringLength(20)]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }
    }
    #endregion

    #region Locations
    public class LocationMetadata
    {
        public int LocationId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string LocationAddress { get; set; } = null!;
        [Required]
        [StringLength(100)]
        [Display(Name = "City")]
        public string LocationCity { get; set; } = null!;
        [StringLength(2)]
        [Display(Name = "State")]
        public string? LocationState { get; set; }
        [StringLength(10)]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string? LocationPostalCode { get; set; }
        public int? CountryId { get; set; }
    }
    #endregion

    #region OrderProducts
    public class OrderProductMetadata
    {
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Order Quantity")]
        public short OrderQuantity { get; set; }
    }
    #endregion

    #region Orders
    public class OrderMetadata
    {
        public int OrderId { get; set; }
        [StringLength(50)]
        [Display(Name = "PO Number")]
        public string? Ponumber { get; set; }
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Order Total")]
        public decimal? OrderTotal { get; set; }
    }
    #endregion
}


