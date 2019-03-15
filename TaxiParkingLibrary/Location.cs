using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiParkingLibrary
{
    public class Location
    {
        public string AdmArea { get; set; }
        public string District { get; set; }
        public double Longitude_WGS84 { get; set; }
        public double Latitude_WGS84 { get; set; }


        public Location(string admArea, string district, string longitude, string latitude)
        {
            AdmArea = admArea;
            if (AdmArea.Length == 0)
                throw new TaxiParkingException("Обязательное к заполнению поле AdmArea не может быть пустым");

            District = district;
            if (District.Length == 0)
                throw new TaxiParkingException("Обязательное к заполнению поле District не может быть пустым");

            try
            {
                Longitude_WGS84 = double.Parse(longitude);
                if (Longitude_WGS84 < -180 || Longitude_WGS84 > 180)
                    throw new TaxiParkingException();
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля Longitude_WGS84 - должно быть действительное число на отрезке [-180; 180]");
            }

            try
            {
                Latitude_WGS84 = double.Parse(latitude);
                if (Latitude_WGS84 < -90 || Latitude_WGS84 > 90)
                    throw new TaxiParkingException();
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля Latitude_WGS84  - должно быть действительное число на отрезке [-90; 90]");
            }
        }
    }
}
