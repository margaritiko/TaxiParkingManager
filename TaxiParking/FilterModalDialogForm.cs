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
    public partial class FilterModalDialogForm : Form
    {
        public uint minCarCapacity, maxCarCapacity;
        public string admAreaSubstring = "";

        /// <summary>
        /// Инициализирует форму и все её компоненты
        /// </summary>
        public FilterModalDialogForm(FIlter filter)
        {
            InitializeComponent();

            admAreaTextField.Text = filter.AdmAreaValue;

            carCapacityMinTextField.Text = filter.MinCarCapacity.ToString();
            carCapacityMaxTextField.Text = filter.MaxCarCapacity.ToString();
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку "ОК", при этом проверяя корректность введенных значений 
        /// </summary>
        /// <param name="sender">Кнопка "ОК"</param>
        /// <param name="e">Некоторые параметры</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!(carCapacityMinTextField.Text.Length == 0 && carCapacityMaxTextField.Text.Length == 0))
            {
                try
                {
                    minCarCapacity = uint.Parse(carCapacityMinTextField.Text);
                    maxCarCapacity = uint.Parse(carCapacityMaxTextField.Text);
                    if (minCarCapacity > maxCarCapacity)
                        throw new TaxiParkingException("Минимальное значение не может превышать максимальное");
                }
                catch (TaxiParkingException exception)
                {
                    MessageBox.Show(exception.Message);
                    return;
                }
                catch 
                {
                    MessageBox.Show("Введенные данные для фильтрации по полю AdmArea не являются неотрицательными целыми числами");
                    return;
                }
            }
            else
            {
                minCarCapacity = 0;
                maxCarCapacity = uint.MaxValue;
            }

            admAreaSubstring = admAreaTextField.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
