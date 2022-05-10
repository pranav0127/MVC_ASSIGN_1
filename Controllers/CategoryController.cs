using MVC_ASSIGN1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ASSIGN1.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            var cat = LoadCategory();
            return View(cat);
        }
        

        public ActionResult Index_SP()
        {
            var cat = LoadCategory_SP();
            return View(cat);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            InsertCategory(model.Name);
            return RedirectToAction("Index");
        }


        [NonAction]
        private List<Category> LoadCategory()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select category_id, category_name from categories";
            cmd.Connection = Database.Connection.conn();
            cmd.CommandType = CommandType.Text;
            List<Category> categories = new List<Category>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var category = new Category();
                        category.Id = (int)reader["category_id"];
                        category.Name = reader.GetString(reader.GetOrdinal("category_name"));
                        categories.Add(category);
                    }
                }
                return categories;
            }
        }

        private List<Category> LoadCategory_SP()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "PROC_SELECT_CATEGORIES_MVC";
            cmd.Connection = Database.Connection.conn();
            cmd.CommandType = CommandType.StoredProcedure;
            List<Category> categories = new List<Category>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var category = new Category();
                        category.Id = (int)reader["category_id"];
                        category.Name = reader.GetString(reader.GetOrdinal("category_name"));
                        categories.Add(category);
                    }
                }
                return categories;
            }
        }

        private void InsertCategory(string Categoryname)
        {
            try
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "Insert into categories(category_name) values(@categoryname)";
                cmd.Parameters.AddWithValue("@categoryname", Categoryname);
                cmd.Connection = Database.Connection.conn();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

            }

        }
    }
}