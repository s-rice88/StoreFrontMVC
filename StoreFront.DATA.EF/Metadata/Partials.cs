using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace StoreFront.DATA.EF.Models/*.Metadata*/
{

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {

    }

    [ModelMetadataType(typeof(ClassLocationMetadata))]
    public partial class ClassLocation
    {

    }

    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order
    {

    }

    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product
    {

    }

    [ModelMetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {

    }

}//end namespace
