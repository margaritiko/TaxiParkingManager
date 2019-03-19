using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiParkingLibrary
{

    public class Parking
    {
        private Location _parkingLocation;
        private int _rownum;
        private string _name;
        private string _address;
        private string _locationDescription;
        private int _carCapacity;
        private string _mode;
        private int _global_id;

        delegate void convertGivenDataDelegate(List<string> data);
        /// <summary>
        /// Определяет, является ли текущая запись видимой в таблице
        /// </summary>
        public bool IsVisibleInTable { get; set; } = true;

        /// <summary>
        /// Определяет исходный порядок текущей записи
        /// </summary>
        public int Order { get; private set; }

        // All fields from CSV table
        public Location ParkingLocation { get => _parkingLocation; private set => _parkingLocation = value; }
        public int ROWNUM { get => _rownum; set => _rownum = value; }
        public string Name { get => _name; private set => _name = value; }
        public string Address { get => _address; private set => _address = value; }
        public string LocationDescription { get => _locationDescription; private set => _locationDescription = value; }
        public int CarCapacity { get => _carCapacity; private set => _carCapacity = value; }
        public string Mode { get => _mode; private set => _mode = value; }
        public int Global_id { get => _global_id; private set => _global_id = value; }

        /// <summary>
        /// Преобразовать считанную строку в массив строк
        /// </summary>
        /// <param name="stringWithData">Исходная считанная строка</param>
        /// <returns>Массив строк</returns>
        List<string> ParseStringIntoArrayWithData(string stringWithData)
        {
            List<string> result = new List<string>();
            string collect = "";
            int startIndex = 0;
            while (stringWithData[startIndex] != ';')
            {
                collect += stringWithData[startIndex];
                startIndex++;
            }
            result.Add(collect);
            startIndex++;
            bool isNewField = false;
            collect = "";
            for (int i = startIndex; i < stringWithData.Length; ++i)
            {
                if (stringWithData[i] == '"')
                {
                    if (isNewField)
                    {
                        result.Add(collect);
                        collect = "";
                    }
                    isNewField = !isNewField;
                    continue;
                }

                if (stringWithData[i] == ';' && !isNewField)
                    continue;

                collect += stringWithData[i];
            }

            return result;
        }

        public Parking(List<string> data)
        {
            for (int i = 0; i < data.Count; ++i)
            {
                data[i] = RemoveWhitespaces(data[i]);
            }

            convertGivenDataDelegate convert = ReadROWNUM;
            convert += ReadName;
            convert += ReadLocation;
            convert += ReadAddress;
            convert += ReadLocationDescription;
            convert += ReadCarCapacity;
            convert += ReadMode;
            convert += ReadGlobal_id;

            try
            {
                convert(data);
                Order = ROWNUM;
            }
            catch (TaxiParkingException exception)
            {
                throw exception;
            }
        }

        public Parking(string stringWithDataFromCSVFile)
        {

            List<string> data = ParseStringIntoArrayWithData(stringWithDataFromCSVFile);
            for (int i = 0; i < data.Count; ++i)
            {
                data[i] = RemoveWhitespaces(data[i]);
            }

            convertGivenDataDelegate convert = ReadROWNUM;
            convert += ReadName;
            convert += ReadLocation;
            convert += ReadAddress;
            convert += ReadLocationDescription;
            convert += ReadCarCapacity;
            convert += ReadMode;
            convert += ReadGlobal_id;

            try
            {
                convert(data);
                Order = ROWNUM;
            }
            catch (TaxiParkingException exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Преобразовывает ROWNUM значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadROWNUM(List<string> data)
        {
            const int index = 0;
            try
            {
                ROWNUM = int.Parse(data[index]);
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля ROWNUM");
            }
        }

        /// <summary>
        /// Преобразовывает Name значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadName(List<string> data)
        {
            const int index = 1;
            try
            {
                Name = data[index];
                if (Name.Length == 0)
                    throw new TaxiParkingException("Обязательное к заполнению поле Name не может быть пустым");
            }
            catch (TaxiParkingException exception)
            {
                throw exception;
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля Name");
            }
        }

        /// <summary>
        /// Преобразовывает Address значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadAddress(List<string> data)
        {
            const int index = 4;
            try
            {
                Address = data[index];
                if (Address.Length == 0)
                    throw new TaxiParkingException("Обязательное к заполнению поле Address не может быть пустым");
            }
            catch (TaxiParkingException exception)
            {
                throw exception;
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля Address");
            }
        }

        /// <summary>
        /// Преобразовывает LocationDescription значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadLocationDescription(List<string> data)
        {
            const int index = 5;
            try
            {
                LocationDescription = data[index];
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля LocationDescription");
            }
        }

        /// <summary>
        /// Преобразовывает CarCapacity значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadCarCapacity(List<string> data)
        {
            const int index = 8;
            try
            {
                CarCapacity = int.Parse(data[index]);
                if (CarCapacity <= 0)
                    throw new TaxiParkingException();
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля CarCapacity - должно быть положительное целое число");
            }
        }

        /// <summary>
        /// Преобразовывает Mode значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadMode(List<string> data)
        {
            const int index = 9;
            try
            {
                Mode = data[index];
                if (Mode.Length == 0)
                    throw new TaxiParkingException("Обязательное к заполнению поле Mode не может быть пустым");
            }
            catch (TaxiParkingException exception)
            {
                throw exception;
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля Mode");
            }
        }

        /// <summary>
        /// Преобразовывает global_id значение, считанное из таблицы 
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadGlobal_id(List<string> data)
        {
            const int index = 10
;
            try
            {
                Global_id = int.Parse(data[index]);
                if (Global_id <= 0)
                    throw new TaxiParkingException();
            }
            catch
            {
                throw new TaxiParkingException("Некорректное значение поля global_id - должно быть положительное целое число");
            }
        }

        /// <summary>
        /// Обрабатывает конкретные данные из массива и формирует по ним объект типа Location
        /// </summary>
        /// <param name="data">Массив с данными</param>
        private void ReadLocation(List<string> data)
        {
            try
            {
                ParkingLocation = new Location(data[2], data[3], data[6], data[7]);
            }
            catch (TaxiParkingException exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Удаляет повторяющиеся пробелы в строке и игнорирует пробелф в начале строки
        /// </summary>
        /// <param name="value">Строка для обработки</param>
        /// <returns>Строка без повторяющихся пробелов</returns>
        private string RemoveWhitespaces(string value)
        {
            string newString = "";
            for (int i = 0; i < value.Length; ++i)
            {
                if (value[i] == ' ' && (newString.Length == 0 || newString[newString.Length - 1] == ' '))
                    continue;
                newString += value[i];
            }
            return newString;
        }


        /// <summary>
        /// Возвращает суммарное количество мест на парковках такси района, 
        /// которому принадлежит данная парковка.
        /// </summary>
        /// <param name="parkings">Набор объектов типа парковка.</param>
        /// <returns>Количество мест во всех районе.</returns>
        public int TotalNumberOfParkingSpacesInDistricts(List<Parking> parkings)
        {
            int totalNumberOfParkingSpaces = 0;
            foreach (var parking in parkings)
                if (parking.ParkingLocation.District == this.ParkingLocation.District)
                    totalNumberOfParkingSpaces++;

            return totalNumberOfParkingSpaces;
        }

        /// <summary>
        /// Возвращает суммарное количество мест на парковках такси округа, 
        /// которому принадлежит данная парковка.
        /// </summary>
        /// <param name="parkings">Набор объектов типа парковка.</param>
        /// <returns>Количество мест во всех округах.</returns>
        public int TotalNumberOfParkingSpacesInAdmArea(List<Parking> parkings)
        {
            int totalNumberOfParkingSpaces = 0;
            foreach (var parking in parkings)
                if (parking.ParkingLocation.AdmArea == this.ParkingLocation.AdmArea)
                    totalNumberOfParkingSpaces++;

            return totalNumberOfParkingSpaces;
        }
    }


}
