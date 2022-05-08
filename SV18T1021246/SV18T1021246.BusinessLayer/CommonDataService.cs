using SV18T1021246.DataLayer;
using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.BusinessLayer
{
    /// <summary>
    /// các chức năng nghiệp vụ liên quan đến dữ liệu chung
    /// (nhà cung cấp, khách hàng, người giao hàng, nhân viên, loại hàng)
    /// </summary>
    public static class CommonDataService
    {
        //private static readonly ICustomerDAL customerDB;
        private static readonly ICountryDAL countryDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        /// <summary>
        /// 
        /// </summary>
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                    supplierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                    shipperDB = new DataLayer.SQLServer.ShipperDAL(connectionString);
                    employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                    break;
                default:
                    //categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }

        

        

        /// <summary>
        /// TÌm kiếm và lấy danh sách KH
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize">Số dòng mỗi trang (0 nếu lấy toàn bộ / không phẩn trang)</param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page,
                                                    int pageSize,
                                                    string searchValue,
                                                    out int rowCount)
        {
            /*
            if (page <= 0)
                page = 1;
            */
            if (pageSize < 0)
                pageSize = 0;

            rowCount = customerDB.Count(searchValue);

            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lay thong tin 1 ma KH
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Bo sung them 1 KH
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cap nhat thong tin 1 KH
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Xoa 1 KH theo maKH
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Kiem tra 1 KH hien co du lieu lien quan hay ko
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        

        /*
         * 
         * 
         */

        public static List<Supplier> ListOfSuppliers(int page,
                                                    int pageSize,
                                                    string searchValue,
                                                    out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);

            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier (int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static int AddSupplier (Supplier data)
        {
            return supplierDB.Add(data);
        }

        public static bool UpdateSupplier (Supplier data)
        {
            return supplierDB.Update(data);
        }
        public static bool DeleteSupplier (int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }
        public static bool InUsedSupplier (int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }


        /*
         * 
         * 
         */

        public static List<Category> ListOfCategories(int page, int pageSize,
                                                    string searchValue,
                                                    out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);

            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }

        /*
         * 
         * 
         */

        public static List<Shipper> ListOfShippers(int page, int pageSize,
                                                    string searchValue,
                                                    out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);

            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
       
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }
        
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }

        /*
         * 
         * 
         */

        public static List<Employee> ListOfEmployees(int page, int pageSize,
                                                    string searchValue,
                                                    out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);

            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }

        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }

        /*
         * 
         * 
         */

        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
    }
}
