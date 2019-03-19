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
    public partial class SortModalDialogForm : Form
    {
        /// <summary>
        /// Выбранный тип сортировки
        /// </summary>
        public SortType sortType;

        /// <summary>
        /// Инициализирует форму и все её компоненты
        /// </summary>
        public SortModalDialogForm()
        {
            InitializeComponent();
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(okButton, "Применить изменения");
        }

        /// <summary>
        /// В случае, если тип сортировки уже был выбран ранее, устанавливает флаг в соотвествующий элемент
        /// </summary>
        /// <param name="sortType">Выбранный тип сортировки</param>
        public SortModalDialogForm(SortType sortType)
        {
            InitializeComponent();
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(okButton, "Применить изменения");
            switch (sortType)
            {
                case SortType.defaultSort:
                    defaultSortButton.Checked = true;
                    break;
                case SortType.byFieldCarCapacity:
                    carCapacitySortButton.Checked = true;
                    break;
                case SortType.byFieldGlobalId:
                    global_idSortButton.Checked = true;
                    break;
                case SortType.byNumberOfParkings:
                    numberOfParkingsSortButton.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку "ОК", устанавливая результат работы как успешный 
        /// </summary>
        /// <param name="sender">Кнопка "ОК"</param>
        /// <param name="e">Некоторые параметры</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
       
        /// <summary>
        /// Обрабатывает выбор пользователем другого типа сортировки при помощи элемента RadioButton
        /// </summary>
        /// <param name="sender">Элемент управления RadioButton</param>
        /// <param name="e">Некоторые параметры</param>
        private void SortButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                switch (radioButton.Text)
                {
                    case "не сортировать":
                        sortType = SortType.defaultSort;
                        break;
                    case "по возрастанию значения поля CarCapacity":
                        sortType = SortType.byFieldCarCapacity;
                        break;
                    case "по возрастанию значения поля  global_id":
                        sortType = SortType.byFieldGlobalId;
                        break;
                    case "по количеству парковок в выбранном округе":
                        sortType = SortType.byNumberOfParkings;
                        break;
                }
            }
        }
    }
}
