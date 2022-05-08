using SV18T1021246.BusinessLayer;
using SV18T1021246.DomainModel;
using SV18T1021246.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021246.Web.Controllers
{
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        /// <summary>
        /// Hiển thị danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        // GET: Supplier
        public ActionResult Index()
        {
            PaginationSearchInput model = Session["SUPPLIER_SEARCH"] as PaginationSearchInput;
            if (model == null)
            {
                model = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }
            return View(model);
        }

        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(input.Page,
                                                       input.PageSize,
                                                       input.SearchValue,
                                                       out rowCount);

            Models.SupplierPaginationResultModel model = new Models.SupplierPaginationResultModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session["SUPPLIER_SEARCH"] = input;

            return View(model);
        }



        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp mới";

            Supplier model = new Supplier()
            {
                SupplierID = 0
            };

            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{supplierID?}")]
        public ActionResult Edit(string supplierID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View("Create", model);

        }

        [HttpPost]
        public ActionResult Save (Supplier model)
        {
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError("SupplierName", "Tên nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Tên địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Phải chọn quốc gia");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";
            if (string.IsNullOrWhiteSpace(model.Phone))
                model.Phone = "";

            //Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.SupplierID > 0)
                    ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
                else
                    ViewBag.Title = "Bổ sung nhà cung cấp";

                return View("Create", model);
            }

            //Xử lý lưu dữ liệu vào CSDL
            if (model.SupplierID > 0)
            {
                CommonDataService.UpdateSupplier(model);
            }
            else
            {
                CommonDataService.AddSupplier(model);
            }
            return RedirectToAction("Index");
        } 

        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(int supplierID)
        {
            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(supplierID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}