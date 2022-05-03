using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021246.Web.Models
{
    public class SupplierPaginationResultModel : PaginationResultModel
    {
        public List<Supplier> Data { get; set; }
    }
}