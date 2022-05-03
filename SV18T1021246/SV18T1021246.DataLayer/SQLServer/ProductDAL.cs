using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Product data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @" select count(*)
                                     from    Products
                                     where    (@searchValue = N'')
                                          or (
                                                  ProductName like @searchValue
                                              )";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }

            return count;
        }

        public bool Delete(int productID)
        {
            throw new NotImplementedException();
        }

        public Product Get(int productID)
        {
            Product result = null;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @productID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                    };
                }

                cn.Close();
            }

            return result;

        }

        public bool InUsed(int productID)
        {
            throw new NotImplementedException();
        }
        /*
         * declare @page int = 1;
         *          @pageSize int = 5;
         *          @categoryID int = 0;
         *          @supplierID int = 0;
         *          @searchvalue nvarchar(255) = N'';
          select *, 
               row_number() over(order by ProductName) as RowNumber
          from Products as p
          where ((@categoryID = 0) or (p.CategoryID = @categoryID)) and
               ((@supplierID = 0) or (p.SupplierID = @supplierID)) and
               ((@searchValue = '') or (p.ProductName like @searchValue))
         */
        public IList<Product> List(int page, int pageSize, string searchValue, int categoryID, int supplierID)
        {
            List<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using(SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from
                                        (
                                            select    *,
                                                    row_number() over(order by ProductName) as RowNumber
                                            from    Products as p
                                            where   
                                            (
                                                    ((@categoryID = 0) or (p.CategoryID = @categoryID)) and
                                                    ((@supplierID = 0) or (p.SupplierID = @supplierID)) and
                                                    ((@searchValue = '') or (p.ProductName like @searchValue))
                                            ) as t
                                    where   (@pageSize = 0) or (t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                                    order by t.RowNumber";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"])
                    });
                }

                dbReader.Close();

                cn.Close();
            }

            return data;
        }

        public bool Update(Product data)
        {
            throw new NotImplementedException();
        }
    }
}
