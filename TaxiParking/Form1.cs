using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TaxiParkingLibrary;

namespace TaxiParking
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Список со всеми записями о парковках такси
        /// </summary>
        static List<Parking> parkings = new List<Parking>();
        static HashSet<int> IDs = new HashSet<int>();

        /// <summary>
        /// Объект в котором храниться текущее состояние сортировки
        /// </summary>
        static SortType sortType = SortType.defaultSort;
        FIlter filter = new FIlter();
        public Form1()
        {
            InitializeComponent();

            // Текущее резрешение экрана устройства
            Size resolution = Screen.PrimaryScreen.Bounds.Size; 
            // Установка минимального размера формы
            MinimumSize = new Size(resolution.Width / 2, resolution.Height / 2);
            // Установка максимального размера формы
            MaximumSize = resolution;

            // Всплывающие подсказки при наведении на кнопки панели инструментов
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(createButton, "Создать новую запись");
            toolTip.SetToolTip(editButton, "Редактировать запись");
            toolTip.SetToolTip(deleteButton, "Удалить запись");
            toolTip.SetToolTip(filterButton, "Отфильтровать отображаемые записи");
            toolTip.SetToolTip(sortButton, "Отсортировать отображаемые запись");

            SetupDataGridView();
        }

        // MARK: DataGridView setting up
        /// <summary>
        /// Настройка различных свойств элемента dataGridView
        /// </summary>
        private void SetupDataGridView()
        {

            dataGridView.ColumnCount = 11;

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView.Font, FontStyle.Bold);

            dataGridView.Name = "Taxi Parkings Data Grid View";
            dataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView.GridColor = Color.Black;
            dataGridView.RowHeadersVisible = false;

            dataGridView.Columns[0].Name = "ROWNUM";
            dataGridView.Columns[1].Name = "Name";
            dataGridView.Columns[2].Name = "AdmArea";
            dataGridView.Columns[3].Name = "District";
            dataGridView.Columns[4].Name = "Address";
            dataGridView.Columns[5].Name = "LocationDescription";
            dataGridView.Columns[6].Name = "Longitude_WGS84";
            dataGridView.Columns[7].Name = "Latitude_WGS84";
            dataGridView.Columns[8].Name = "CarCapacity";
            dataGridView.Columns[9].Name = "Mode";
            dataGridView.Columns[10].Name = "global_id";

            // dataGridView.Columns[4].DefaultCellStyle.Font =
                new Font(dataGridView.DefaultCellStyle.Font, FontStyle.Italic);

            dataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        /// <summary>
        /// Добавление новой строки в конец таблицы с обновлением массива всех парковок 
        /// </summary>
        /// <param name="parking"></param>
        private void AddRowToDataGridViewWithParkingsUpdate(Parking parking)
        {
            dataGridView.Rows.Add(parking.ROWNUM, parking.Name, parking.ParkingLocation.AdmArea, 
                parking.ParkingLocation.District, parking.Address, parking.LocationDescription, 
                parking.ParkingLocation.Longitude_WGS84, parking.ParkingLocation.Latitude_WGS84, 
                parking.CarCapacity, parking.Mode, parking.Global_id);
            parkings.Add(parking);
            IDs.Add(parking.Global_id);
        }

        /// <summary>
        /// Добавление новой строки в конец таблицы без обновления массива всех парковок 
        /// </summary>
        /// <param name="parking"></param>
        private void AddRowToDataGridView(Parking parking)
        {
            dataGridView.Rows.Add(parking.ROWNUM, parking.Name, parking.ParkingLocation.AdmArea,
                parking.ParkingLocation.District, parking.Address, parking.LocationDescription,
                parking.ParkingLocation.Longitude_WGS84, parking.ParkingLocation.Latitude_WGS84,
                parking.CarCapacity, parking.Mode, parking.Global_id);
        }

        /// <summary>
        /// Добавление новой строки в таблицу в строчку с переданным индексом
        /// </summary>
        /// <param name="parking"></param>
        private void AddRowToDataGridViewAfterCurrentLine(Parking parking)
        {
            int numberOfLine = 0;
            if (dataGridView.SelectedRows.Count > 0)
            {
                numberOfLine = dataGridView.SelectedRows[0].Index + 1;
            }

            int id = int.Parse(dataGridView.Rows[numberOfLine - 1].Cells[10].ToString());

            dataGridView.Rows.Insert(numberOfLine, parking.ROWNUM, parking.Name, parking.ParkingLocation.AdmArea,
                parking.ParkingLocation.District, parking.Address, parking.LocationDescription,
                parking.ParkingLocation.Longitude_WGS84, parking.ParkingLocation.Latitude_WGS84,
                parking.CarCapacity, parking.Mode, parking.Global_id);
            // CHECK: 
            int index = 0;
            for (int i = 0; i < parkings.Count; ++i)
                if (parkings[i].Global_id == id)
                {
                    index = i;
                    break;
                }
            
            parkings.Insert(index + 1, parking);
            IDs.Add(parking.Global_id);
            UpdateRowNumbersInDataGridView();
            UpdateRowNumbersInParkingsArray();
        }

        /// <summary>
        /// После получения данных из модального окна "Редактирование записи" редактирует запись в таблице и массиве парковок
        /// </summary>
        /// <param name="parking">Новая запись, которую необходимо записать вместо старой</param>
        private void EditRowInDataGridViewInCurrentLine(Parking parking)
        {
            int numberOfLine = 0;
            if (dataGridView.SelectedRows.Count > 0)
            {
                numberOfLine = dataGridView.SelectedRows[0].Index;
            }

            int id = int.Parse(dataGridView.Rows[numberOfLine].Cells[10].ToString());

            dataGridView.Rows.RemoveAt(numberOfLine);
            dataGridView.Rows.Insert(numberOfLine, parking.ROWNUM, parking.Name, parking.ParkingLocation.AdmArea,
                parking.ParkingLocation.District, parking.Address, parking.LocationDescription,
                parking.ParkingLocation.Longitude_WGS84, parking.ParkingLocation.Latitude_WGS84,
                parking.CarCapacity, parking.Mode, parking.Global_id);

            // CHECK:
            int index = 0;
            for (int i = 0; i < parkings.Count; ++i) 
                if (parkings[i].Global_id == id)
                {
                    index = i;
                    break;
                }
            parkings.RemoveAt(index);
            parkings.Insert(index, parking);
            IDs.Add(parking.Global_id);
        }

        // MARK: Actions

        /// <summary>
        /// Обрабатывает открытие нового файла, предварительно уточняя у пользователя уверен ли он, 
        /// что хочет открыть новый файл, так как все несохраненные данные будут стерты
        /// </summary>
        /// <param name="sender">Кнопка "Открыть"</param>
        /// <param name="e">Некоторые параметры</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var content = string.Empty;
            var path = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (parkings.Count > 0)
                {
                    const string message = "Вы уверены, что хотите открыть новый файл? Текущие несохраненные данные будут удалены.";
                    const string caption = "Form Closing";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    // If the no button was pressed ...
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    path = openFileDialog.FileName;

                    // Read current file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    dataGridView.Rows.Clear();
                    parkings.Clear();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        reader.ReadLine();
                        while (reader.Peek() != -1)
                        {
                            content = reader.ReadLine();
                            
                            try  {
                                AddRowToDataGridViewWithParkingsUpdate(new Parking(content));
                            }
                            catch (TaxiParkingException exception)
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }

                        UpdateRowNumbersInDataGridView();
                        UpdateRowNumbersInParkingsArray();
                    }
                }
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки создания новой записи 
        /// </summary>
        /// <param name="sender">Кнопка "Создать"</param>
        /// <param name="e">Некоторые параметры</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            ModalDialogForm createNewNoteModelDialog = new ModalDialogForm(IDs);

            // Показывает модальное окно 
            if (createNewNoteModelDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Считывает введенную пользователем запись и вставляет ее в таблицу
                AddRowToDataGridViewAfterCurrentLine(createNewNoteModelDialog._Parking);
            }
            createNewNoteModelDialog.Dispose();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки удаления 
        /// </summary>
        /// <param name="sender">Кнопка "Удалить"</param>
        /// <param name="e">Некоторые параметры</param>
        void DeleteButton_Click(object sender, EventArgs e)
        {
            int numberOfLine = 0;
            if (dataGridView.SelectedRows.Count > 0)
            {
                // TODO: 

                numberOfLine = dataGridView.SelectedRows[0].Index;
                int id = int.Parse(dataGridView.Rows[numberOfLine].Cells[10].ToString());
                int index = 0;
                for (int i = 0; i < parkings.Count; ++i)
                    if (parkings[i].Global_id == id)
                    {
                        index = i;
                        break;
                    }
                parkings.RemoveAt(index);
                dataGridView.Rows.RemoveAt(numberOfLine);
                UpdateRowNumbersInDataGridView();
                UpdateRowNumbersInParkingsArray();
            }
        }

        /// <summary>
        /// Обновляет номера строк в соответствии с текущим порядком записей
        /// </summary>
        void UpdateRowNumbersInDataGridView()
        {
            for (int i = 0; i < dataGridView.Rows.Count; ++i)
            {
                dataGridView.Rows[i].Cells[0].Value = i + 1;
            }
        }

        void UpdateRowNumbersInParkingsArray()
        {
            for (int i = 0, number = 1; i < parkings.Count; ++i)
                if (parkings[i].IsVisibleInTable)
                {
                    parkings[i].ROWNUM = number;
                    number++;
                }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки редактирования текущей записи
        /// </summary>
        /// <param name="sender">Кнопка "Редактировать"</param>
        /// <param name="e">Некоторые параметры</param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                ModalDialogForm editNoteModelDialog = new ModalDialogForm(IDs);
                editNoteModelDialog.InitWithData(parkings[dataGridView.SelectedRows[0].Index]);

                // Показывает модальное окно 
                if (editNoteModelDialog.ShowDialog(this) == DialogResult.OK)
                {
                    // Считывает отредактированную пользователем запись и перезаписывает ее в таблице
                    EditRowInDataGridViewInCurrentLine(editNoteModelDialog._Parking);
                    UpdateRowNumbersInDataGridView();
                    UpdateRowNumbersInParkingsArray();
                }
                editNoteModelDialog.Dispose();
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одну запись");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку сортировки на панели инструментов
        /// </summary>
        /// <param name="sender">Кнопка "Сортировать"</param>
        /// <param name="e">Некоторые параметры</param>
        private void SortButton_Click(object sender, EventArgs e)
        {
            SortModalDialogForm sortDataModelDialog = new SortModalDialogForm(sortType);

            // Показывает модальное окно 
            if (sortDataModelDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Получает введенные пользователем данные о сортировки
                sortType = sortDataModelDialog.sortType;
                SortDataByCurrentSortType();
                UpdateRowNumbersInDataGridView();
                UpdateRowNumbersInParkingsArray();
            }
            sortDataModelDialog.Dispose();
        }

        /// <summary>
        /// Сортирует данные по выбранному в модальном окне типу сортировки (сортировка вызвана при помощи панели инструментов)
        /// </summary>
        private void SortDataByCurrentSortType()
        {   
            if (sortType == SortType.byFieldCarCapacity)
            {
                parkings.Sort((Parking first, Parking second) =>
                {
                    if (first.CarCapacity < second.CarCapacity)
                        return -1;
                    if (first.CarCapacity == second.CarCapacity)
                        return 0;
                    return 1;
                });
            }
            else if (sortType == SortType.byFieldGlobalId)
            {
                parkings.Sort((Parking first, Parking second) =>
                {
                    if (first.Global_id < second.Global_id)
                        return -1;
                    if (first.Global_id == second.Global_id)
                        return 0;
                    return 1;
                });
            }
            else if (sortType == SortType.byNumberOfParkings)
            {
                parkings.Sort((Parking first, Parking second) =>
                {
                    if (first.TotalNumberOfParkingSpacesInDistrict(parkings) < second.TotalNumberOfParkingSpacesInDistrict(parkings))
                        return -1;
                    if (first.TotalNumberOfParkingSpacesInDistrict(parkings) == second.TotalNumberOfParkingSpacesInDistrict(parkings))
                        return 0;
                    return 1;
                });
            }
            else if (sortType == SortType.defaultSort)
            {
                parkings.Sort((Parking first, Parking second) =>
                {
                    if (first.Order < second.Order)
                        return -1;
                    if (first.Order == second.Order)
                        return 0;
                    return 1;
                });
            }
            dataGridView.Rows.Clear();
            foreach (var parking in parkings)
                AddRowToDataGridView(parking);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки по полю CarCapacity из верхнего меню
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void CarCapacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parkings.Sort((Parking first, Parking second) =>
            {
                if (first.CarCapacity < second.CarCapacity)
                    return -1;
                if (first.CarCapacity == second.CarCapacity)
                    return 0;
                return 1;
            });

            dataGridView.Rows.Clear();
            foreach (var parking in parkings)
                AddRowToDataGridView(parking);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки по полю global_id из верхнего меню
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void GlobalIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parkings.Sort((Parking first, Parking second) =>
            {
                if (first.Global_id < second.Global_id)
                    return -1;
                if (first.Global_id == second.Global_id)
                    return 0;
                return 1;
            });

            dataGridView.Rows.Clear();
            foreach (var parking in parkings)
                AddRowToDataGridView(parking);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки по количеству парковок в том же районе из верхнего меню
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void ByNumberOfParkingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parkings.Sort((Parking first, Parking second) =>
            {
                if (first.TotalNumberOfParkingSpacesInDistrict(parkings) < second.TotalNumberOfParkingSpacesInDistrict(parkings))
                    return -1;
                if (first.TotalNumberOfParkingSpacesInDistrict(parkings) == second.TotalNumberOfParkingSpacesInDistrict(parkings))
                    return 0;
                return 1;
            });

            dataGridView.Rows.Clear();
            foreach (var parking in parkings)
                AddRowToDataGridView(parking);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки из верхнего меню и получает неотсортированные данные
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void DefaultOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parkings.Sort((Parking first, Parking second) =>
            {
                if (first.Order < second.Order)
                    return -1;
                if (first.Order == second.Order)
                    return 0;
                return 1;
            });

            dataGridView.Rows.Clear();
            foreach (var parking in parkings)
                AddRowToDataGridView(parking);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Фильтр",
        /// получает от пользователя значения фильтра и использует их для обработки значений
        /// </summary>
        /// <param name="sender">Кнопка "Фильтр"</param>
        /// <param name="e">Некоторые параметры</param>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            FilterModalDialogForm filterDataModelDialog = new FilterModalDialogForm(filter);

            // Показывает модальное окно 
            if (filterDataModelDialog.ShowDialog(this) == DialogResult.OK)
            {

                filter.AdmAreaValue = filterDataModelDialog.admAreaSubstring;
                filter.MinCarCapacity = filterDataModelDialog.minCarCapacity;
                filter.MaxCarCapacity = filterDataModelDialog.maxCarCapacity;
                
                FilterDataByCurrentFilterType();
                UpdateRowNumbersInDataGridView();
            }
            filterDataModelDialog.Dispose();
        }

        private void FilterDataByCurrentFilterType()
        {
            dataGridView.Rows.Clear();
            for (int i = 0; i < parkings.Count; ++i)
            {
                if (parkings[i].CarCapacity >= filter.MinCarCapacity &&
                    parkings[i].CarCapacity <= filter.MaxCarCapacity &&
                    parkings[i].ParkingLocation.AdmArea.Contains(filter.AdmAreaValue))
                {
                    AddRowToDataGridView(parkings[i]);
                }
            }
        }
    }
}
