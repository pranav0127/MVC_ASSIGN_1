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
    public class BrandController : Controller
    {
        // GET: Brand
        [HttpGet]
        public ActionResult Index()
        {
            var brand = LoadBrand();
            return View("Index",brand);
        }
        
        public ActionResult Index_SP()
        {
            var brand = LoadBrand_SP();
            return View(brand);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand model)
        {
            InsertBrand(model.Name);
            return RedirectToAction("Index");
        }



        private List<Brand> LoadBrand()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "Select brand_id, brand_name from brands";
            cmd.Connection = Database.Connection.conn();
            cmd.CommandType = CommandType.Text;
            List<Brand> brands = new List<Brand>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var brand = new Brand();
                        brand.Id = (int)reader["brand_id"];
                        brand.Name = reader.GetString(reader.GetOrdinal("brand_name"));
                        brands.Add(brand);
                    }
                }
                return brands;
            }
        }

        private List<Brand> LoadBrand_SP()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "PROC_SELECT_BRANDS_MVC";
            cmd.Connection = Database.Connection.conn();
            cmd.CommandType = CommandType.StoredProcedure;
            List<Brand> brands = new List<Brand>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var brand = new Brand();
                        brand.Id = (int)reader["brand_id"];
                        brand.Name = reader.GetString(reader.GetOrdinal("brand_name"));
                        brands.Add(brand);
                    }
                }
                return brands;
            }
        }

        private void InsertBrand(string Brandname)
        {
            try
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "Insert into brands(brand_name) values(@Brandname)";
                cmd.Parameters.AddWithValue("@Brandname", Brandname);
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