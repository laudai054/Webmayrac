using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021246.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm và lấy dữ liệu KH dưới dạng phân trang
    /// </summary>
    public class CustomerPaginationResultModel : PaginationResultModel
    {
        public List<Customer> Data { get; set; }
    }
}