using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.DATA.EF.Models/*.Metadata*/
{
    //internal class Partials
    //{
    //}


    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
    }

    [ModelMetadataType(typeof(ProductStatusMetadata))]
    public partial class ProductStatus { }

    [ModelMetadataType(typeof(ProductTypeMetadata))]
    public partial class ProductType { }

    [ModelMetadataType(typeof(RegionMetadata))]
    public partial class Region { }

    [ModelMetadataType(typeof(SaleMetadata))]
    public partial class Sale { }

    [ModelMetadataType(typeof(SaleProductMetadata))]
    public partial class SaleProduct { }

    [ModelMetadataType(typeof(VendorMetadata))]
    public partial class Vendor { }
    
    #region Country
    [ModelMetadataType(typeof(CountryMetadata))]
    public partial class Country { }
    #endregion


    #region Customer
    [ModelMetadataType(typeof(CustomerMetadata))]
    public partial class Customer { }
    #endregion


    #region Department
    [ModelMetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    #endregion


    #region Employee
    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        public string FullName { get { return FirstName + " " + LastName; } }
    }
    #endregion


    #region Location
    [ModelMetadataType(typeof(LocationMetadata))]
    public partial class Location { }
    #endregion


    #region OrderProduct
    [ModelMetadataType(typeof(OrderProductMetadata))]
    public partial class OrderProduct { }
    #endregion


    #region Order
    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }
    #endregion
}

