using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxiParkingLibrary;

namespace TaxiParking
{
    public partial class ModalDialogForm : Form
    {
        HashSet<int> IDs = new HashSet<int>();
        int idToCompare = -100;
        /// <summary>
        /// Список со строками, которые используются при закрытии формы для создания нового объекта типа Parking
        /// </summary>
        List<string> Data
        {
            get;
            set;
        }

        /// <summary>
        /// Объект типа Parking, создаваемый в данном конкретном объекте класса ModalDialogForm
        /// </summary>
        public Parking _Parking
        {
            get;
            set;
        }
        /// <summary>
        /// Создает пустую форму (используется при нажатии пользователем кнопки создания записи)
        /// </summary>
        public ModalDialogForm(HashSet<int> ids, int _id = -100)
        {
            InitializeComponent();
            IDs = ids;
            idToCompare = _id;
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(okButton, "Применить изменения");
        }
   
        private void OkButton_Click(object sender, EventArgs e)
        {
            Data = new List<string>
            {
                "0",
                nameTextBox.Text,
                admAreaTextBox.Text,
                districtTextBox.Text,
                addressTextBox.Text,
                locationDescriptionTextBox.Text,
                longitudeTextBox.Text,
                latitudeTextBox.Text,
                carCapacityTextBox.Text,
                modeTextBox.Text,
                globalIDTextBox.Text
            };

            try
            {
                _Parking = new Parking(Data);
                Console.WriteLine("ID");
                Console.WriteLine(_Parking.Global_id);
                Console.WriteLine(idToCompare);
                if (_Parking.Global_id != idToCompare && IDs.Contains(_Parking.Global_id))
                    throw new TaxiParkingException("Запись с данным global_id уже существует, " +
                        " введите другое значения для global_id и попробуйте снова");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (TaxiParkingException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void InitWithData(Parking data)
        {
            nameTextBox.Text = data.Name;
            admAreaTextBox.Text = data.ParkingLocation.AdmArea;
            districtTextBox.Text = data.ParkingLocation.District;
            addressTextBox.Text = data.Address;
            locationDescriptionTextBox.Text = data.LocationDescription;
            longitudeTextBox.Text = data.ParkingLocation.Longitude_WGS84.ToString();
            latitudeTextBox.Text = data.ParkingLocation.Latitude_WGS84.ToString();
            carCapacityTextBox.Text = data.CarCapacity.ToString();
            modeTextBox.Text = data.Mode;
            globalIDTextBox.Text = data.Global_id.ToString();
        }
        
    }
}
