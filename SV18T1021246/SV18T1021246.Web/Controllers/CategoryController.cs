using SV18T1021246.BusinessLayer;
using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021246.Web.Controllers
{
    [Authorize]
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(page,
                                                            pageSize,
                                                            searchValue,
                                                            out rowCount);
            Models.CategoryPaginationResultModel model = new Models.CategoryPaginationResultModel()
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
            ViewBag.Title = "Bổ sung sản phẩm mới";

            Category model = new Category()
            {
                CategoryID = 0
            };

            return View(model);
        }

        [Route("edit/{categoryID?}")]
        public ActionResult Edit(string categoryID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(categoryID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
            var model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật sản phẩm mới";
            return View("Create", model);

        }

        [HttpPost]
        public ActionResult Save(Category model)
        {
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "Nhập tên sản phẩm");
            if (string.IsNullOrWhiteSpace(model.Description))
                model.Description = "";

            //Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.CategoryID > 0)
                    ViewBag.Title = "Cập nhật thông tin sản phẩm";
                else
                    ViewBag.Title = "Bổ sung sản phẩm";

                return View("Create", model);
            }

            //Xử lý lưu dữ liệu vào CSDL
            if (model.CategoryID > 0)
            {
                CommonDataService.UpdateCategory(model);
            }
            else
            {
                CommonDataService.AddCategory(model);
            }
            return RedirectToAction("Index");
        }

        [Route("delete/{categoryID}")]
        public ActionResult Delete(int categoryID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}