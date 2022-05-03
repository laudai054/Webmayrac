using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer
{
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng nếu bỏ qua)</param>
        /// <returns></returns>
        IList<T> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số dòng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng nếu bỏ qua)</param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy 1 bản ghi (1 dòng dữ liệu) dựa vào ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Bổ sung dữ liệu T, hàm trả về id (IDENTITY) của dữ liệu được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// Xóa dữ liệu dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Kiểm tra xem có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
