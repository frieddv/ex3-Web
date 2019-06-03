using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Web_Rest_DvirAriel.Models;

namespace Web_Rest_DvirAriel.Controllers
{
    public class DisplayController : Controller {
        private bool IsValidIP(string ipstring) {
            if (String.IsNullOrWhiteSpace(ipstring)) {
                return false;
            }
            string[] split = ipstring.Split('.');
            if (split.Length != 4)
                return false;
            byte result;
            return split.All(r => byte.TryParse(r, out result));

        }

        [HttpPost]
        public string getArgs() {
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

        //todo: all the views over here needed to be change, fter we will create the right views for everyone

        // GET: Display
        public ActionResult Index14(string IPOrFilename, int portOrRate) {
            if (IsValidIP(IPOrFilename)) {
                //maybe instead opening singleton that will connect as client to the flightgear
                ViewBag.IP = System.Net.IPAddress.Parse(IPOrFilename);
                ViewBag.port = portOrRate;
                Implement this!! //for the IP assignment
            }
            else {
                ViewBag.filename = IPOrFilename;
                ViewBag.rate = portOrRate;
                Implement this!! //for the file reading assignment
            }
            return View();
        }

        //todo: returning the right view
        public ActionResult Index2(string IP, int port, int rate) {
            ViewBag.IP = IP;
            ViewBag.port = port;
            ViewBag.rate = rate;
            return View();
        }
        //todo: returning the right view
        public ActionResult Index3(string IP, int port, int rate, int length, string filename) {
            ViewBag.IP = IP;
            ViewBag.port = port;
            ViewBag.rate = rate;
            ViewBag.length = length;
            ViewBag.filename = filename;
            return View();
        }
    }
}