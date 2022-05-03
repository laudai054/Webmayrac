using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021246.Web.Models
{
    public class EmployeePaginationResultModel : PaginationResultModel
    {
        public List<Employee> Data { get; set; }
    }
}