using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web_Rest_DvirAriel.Models {
    public class FileHandler {

        public void DeletFile(string filePath) {
            File.Delete(filePath);
        }
        public void SaveToFile(LocationInfo info, string fileName) {
            using (StreamWriter streamWriter = System.IO.File.AppendText(fileName)) {
                streamWriter.WriteLine(Convert.ToString(info.Lon) + ','
                    + Convert.ToString(info.Lat) + ',' + Convert.ToString(info.Heading)
                    + ',' + Convert.ToString(info.Rudder) + ',' + Convert.ToString(info.Throttle));
            }

        }
    }
}