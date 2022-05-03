using SV18T1021246.BusinessLayer;
using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021246.Web.Controllers
{
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(page,
                                                            pageSize,
                                                            searchValue,
                                                            out rowCount);
            Models.EmployeePaginationResultModel model = new Models.EmployeePaginationResultModel()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
            };


            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên mới";
            Employee model = new Employee()
            {
                EmployeeID = 0
            };

            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{employeeID?}")]
        public ActionResult Edit(string employeeID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật thông tin nhân viên";
            return View("Create", model);

        }
        [HttpPost]
        public ActionResult Save(Employee model, string birthDateString, HttpPostedFileBase uploadPhoto)
        {
            
            try
            {
                DateTime d = DateTime.ParseExact(birthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                model.BirthDate = d;
            }
            catch
            {
                ModelState.AddModelError("BirthDate", "Invalid Birthdate");
            }

            if(uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Employees");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = $"/Images/Employees/{fileName}";
            }

            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName))
                ModelState.AddModelError("FullName", "Họ tên không được để trống");
            if (string.IsNullOrEmpty(model.Notes))
                model.Notes = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật thông tin nhân viên";
                return View("Create", model);
            }
            if (model.EmployeeID == 0)
                CommonDataService.AddEmployee(model);
            else
                CommonDataService.UpdateEmployee(model);

            return RedirectToAction("Index");

            //return Json(new
            //{
            //    Model = model
            //});
            //if (model.EmployeeID > 0)
            //{
            //    CommonDataService.UpdateEmployee(model);
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    CommonDataService.AddEmployee(model);
            //    return RedirectToAction("Index");
            //}
        }

        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{employeeID}")]
        public ActionResult Delete(int employeeID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(employeeID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}