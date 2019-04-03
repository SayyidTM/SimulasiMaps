using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LokasiRumah
    {
        public int ID { get; set; }
        public int Tipe { get; set; }
        public string Alamat { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}