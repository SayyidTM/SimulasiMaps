using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RumahController : Controller
    {
        // GET: Rumah
        public ActionResult Index()
        {
            return View();
        }
        public Action Location()
        {
            string markers = "[";
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetMap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    markers += "{";
                    markers += string.Format("'tipe':'{ 0}'", sdr["Tipe"]);
                    markers += string.Format("'alamat':'{ 0}'", sdr["Alamat"]);
                    markers += string.Format("'lat':'{ 0}'", sdr["Latitude"]);
                    markers += string.Format("'lng':'{ 0}'", sdr["Longtitude"]);
                    markers += "},";
                }
            }
            markers += "]";
            ViewBag.Markers = markers;
            return View(ViewBag);
        }
        [HttpPost]
        public ActionResult Location(LokasiRumah lokasiRumah)
        {
            if (ModelState.IsValid)
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spAddNewLocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Tipe", lokasiRumah.Tipe);
                    cmd.Parameters.AddWithValue("@Alamat", lokasiRumah.Alamat);
                    cmd.Parameters.AddWithValue("@Latitude", lokasiRumah.Latitude);
                    cmd.Parameters.AddWithValue("@Longtitude", lokasiRumah.Longitude);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {

            }
            return RedirectToAction("Location");
        }
    }
}