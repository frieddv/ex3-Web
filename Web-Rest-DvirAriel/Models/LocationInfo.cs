using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Xml;

namespace Web_Rest_DvirAriel.Models {

    public class LocationSampler {

        private TcpClient client;
        private IPEndPoint ep;
        private NetworkStream stream;
        private BinaryReader reader;

        private static LocationSampler instance;

        public LocationSampler(string ip, int port) {
            if (instance != null)
                instance.client.Close();
            client = new TcpClient();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            while (!client.Connected) {
                try {
                    client.Connect(ep);
                }
                catch (Exception) { }
            }
            instance = this;
        }

        public static LocationSampler Instance {
            get {
                if (instance == null)
                    instance = new LocationSampler("127.0.0.1", 5400);
                return instance;
            }
        }
        
        private double requestInfo(string requestCommand) {
            stream = client.GetStream();
            byte[] cmd = Encoding.ASCII.GetBytes(requestCommand);
            stream.Write(cmd, 0, cmd.Length);

            reader = new BinaryReader(client.GetStream());
            string info = string.Empty;
            char s = reader.ReadChar();
            bool isInfo = false;
            while (info == string.Empty || s != '\n') {
                if (isInfo) {
                    if (s == '\'')
                        break;
                    info += s;
                }
                else {
                    if (s == '\'')
                        isInfo = true;
                }
                s = reader.ReadChar();
            }
            try {
                return Convert.ToDouble(info);
            }
            catch (Exception) {
                return 0;
            }
        }

        public LocationInfo GetCurrentInfo() {
            LocationInfo info = new LocationInfo();
            info.Lon = requestInfo("get /position/longitude-deg\r\n");
            info.Lat = requestInfo("get /position/latitude-deg\r\n");
            info.Rudder = requestInfo("get /controls/flight/rudder\r\n");
            info.Throttle = requestInfo("get /controls/engines/current-engine/throttle\r\n");
            info.Heading = requestInfo("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
            return info;
        }
    }

    public class LocationInfo {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Rudder { get; set; }
        public double Throttle { get; set; }
        public double Heading { get; set; }

        public void ToXml(XmlWriter writer) {
            writer.WriteStartElement("InfoLocation");
            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteEndElement();
        }
    }
}