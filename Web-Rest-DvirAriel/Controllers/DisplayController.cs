using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Web_Rest_DvirAriel.Models;

namespace Web_Rest_DvirAriel.Controllers
{
    public class DisplayController : Controller {
        private string endMarker = "END";

        [HttpPost]
        public string GetArgs() {
            var info = LocationSampler.Instance.GetCurrentInfo();
            return ToXml(info);
        }

        private string ToXml(LocationInfo info) {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("InfoLocations");

            info.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }

        // GET: Display
        [HttpGet]
        public ActionResult Index1(int IP1, int IP2, int IP3, int IP4, int port) {
            string IP = IP1 + "." + IP2 + "." + IP3 + "." + IP4;
            var location = new LocationSampler(IP, port).GetCurrentInfo();
            ViewBag.lon = location.Lon;
            ViewBag.lat = location.Lat;
            return View();
        }

        // GET: Display
        [HttpGet]
        public ActionResult Index2(string IP, int port, int rate) {
            var location = new LocationSampler(IP, port).GetCurrentInfo();
            ViewBag.lon = location.Lon;
            ViewBag.lat = location.Lat;
            ViewBag.rate = rate;
            return View();
        }
        // GET: Display
        [HttpGet]
        public ActionResult Index3(string IP, int port, int rate, int length, string filename) {
            FileHandler fileHandler = new FileHandler();
            var location = new LocationSampler(IP, port).GetCurrentInfo();
            ViewBag.lon = location.Lon;
            ViewBag.lat = location.Lat;
            ViewBag.rate = rate;
            ViewBag.length = length;
            @Session["filename"] = filename;
            fileHandler.DeletFile(AppDomain.CurrentDomain.BaseDirectory + @"\" + Session["filename"] + ".txt");
            return View();
        }

        // POST: SAVE
        [HttpPost]
        public string SaveToFile() {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + @Session["filename"] + ".txt";
            LocationInfo info = LocationSampler.Instance.GetCurrentInfo();
            FileHandler fileHandler = new FileHandler();
            fileHandler.SaveToFile(info, filePath);
            return ToXml(info);
        }

        // GET: Display
        [HttpGet]
        public ActionResult Index4(string fileName, int rate) {
            ViewBag.rate = rate;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + fileName + ".txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length - 1] = endMarker;
            string[] values = lines[0].Split(',');
            if (values[0] != endMarker) {
                lines = lines.Skip(1).ToArray();
                ViewBag.lon = Convert.ToDouble(values[0]);
                ViewBag.lat = Convert.ToDouble(values[1]);
            }
            @Session["lines"] = lines;
            return View();
        }

        // GET: Home/default
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public string GetNextLocationFromFile() {
            string[] lines = (string[])Session["lines"];
            string nextLine = lines[0];
            Session["lines"] = lines.Skip(1);
            LocationInfo info = new LocationInfo {
                Lat = 1000,
                Lon = 1000
            };
            var values = nextLine.Split(',');
            if (values[0] != endMarker) {
                try {
                    info.Lon = Convert.ToDouble(values[0]);
                    info.Lat = Convert.ToDouble(values[1]);
                }
                catch (Exception) {
                    info.Lat = 1000;
                    info.Lon = 1000;
                }
            }
            return ToXml(info);
        }
    }
}