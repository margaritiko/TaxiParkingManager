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
        static decimal MaxNumberOfNotes { get; set; }

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
            MaxNumberOfNotes = numericUpDown.Value;
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

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

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
            
            if (dataGridView.Rows.Count == 0)
            {
                dataGridView.Rows.Insert(numberOfLine, parking.ROWNUM, parking.Name, parking.ParkingLocation.AdmArea,
                parking.ParkingLocation.District, parking.Address, parking.LocationDescription,
                parking.ParkingLocation.Longitude_WGS84, parking.ParkingLocation.Latitude_WGS84,
                parking.CarCapacity, parking.Mode, parking.Global_id);

                parkings.Insert(numberOfLine, parking);
                IDs.Add(parking.Global_id);
                UpdateRowNumbersInDataGridView();
                UpdateRowNumbersInParkingsArray();

                return;
            }
            
            int id = int.Parse(dataGridView.Rows[numberOfLine - 1].Cells[10].Value.ToString());

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

            int id = int.Parse(dataGridView.Rows[numberOfLine].Cells[10].Value.ToString());

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
            try
            {
                var content = string.Empty;
                var path = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (parkings.Count > 0)
                    {
                        const string message = "Вы уверены, что хотите открыть новый файл? Текущие несохраненные данные будут удалены.";
                        const string caption = "Открытие нового файла";
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
                        IDs.Clear();
                        // Сбрасывает фильтр
                        filter.Init();

                        // На время считывания файла изменение максимального количества строк невозможно
                        numericUpDown.Enabled = false;

                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            reader.ReadLine();
                            while (reader.Peek() != -1)
                            {
                                content = reader.ReadLine();

                                try
                                {
                                    if (dataGridView.Rows.Count == MaxNumberOfNotes)
                                    {
                                        const string message = "Добавить оставшихся записей невозможно: будет превышено максимальное количество записей в таблице";
                                        const string caption = "Ошибка добавления оставшихся записей";
                                        var result = MessageBox.Show(message, caption,
                                                                     MessageBoxButtons.OK,
                                                                     MessageBoxIcon.Warning);

                                        // If the OK button was pressed ...
                                        if (result == DialogResult.OK)
                                        {
                                            break;
                                        }
                                    }
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
                        numericUpDown.Enabled = true;
                    }
                }
            }
            catch
            {
                // Ошибка возникает, когда пользователь не имеет разрешения на запись в определенное место.
                MessageBox.Show("Во время открытия файла возникла ошибка. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки создания новой записи 
        /// </summary>
        /// <param name="sender">Кнопка "Создать"</param>
        /// <param name="e">Некоторые параметры</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count == MaxNumberOfNotes)
                {
                    const string message = "Добавить новую запись невозможно: измените максимальное количество записей в таблице или удалите существующие записи и попробуйте снова";
                    const string caption = "Ошибка добавления новой записи";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning);

                    // If the OK button was pressed ...
                    if (result == DialogResult.OK)
                    {
                        return;
                    }
                }
                ModalDialogForm createNewNoteModelDialog = new ModalDialogForm(IDs);

                // Показывает модальное окно 
                if (createNewNoteModelDialog.ShowDialog(this) == DialogResult.OK)
                {
                    // Считывает введенную пользователем запись и вставляет ее в таблицу
                    AddRowToDataGridViewAfterCurrentLine(createNewNoteModelDialog._Parking);
                }
                createNewNoteModelDialog.Dispose();
            }
            catch
            {
                MessageBox.Show("Возникла проблема с добавлением новых данных. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки удаления 
        /// </summary>
        /// <param name="sender">Кнопка "Удалить"</param>
        /// <param name="e">Некоторые параметры</param>
        void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int numberOfLine = 0;
                if (dataGridView.SelectedRows.Count > 0)
                {
                    // TODO: 

                    numberOfLine = dataGridView.SelectedRows[0].Index;
                    int id = int.Parse(dataGridView.Rows[numberOfLine].Cells[10].Value.ToString());
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
                else
                {
                    MessageBox.Show("Вы не выбрали ни одну запись");
                }
            }
            catch
            {
                MessageBox.Show("Возникла проблема с удалением данных. Попробуйте снова.");
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

        /// <summary>
        /// Обновляет номера строк в массиве parkings в соответствии с текущим порядком записей
        /// </summary>
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
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    int numberOfLine = dataGridView.SelectedRows[0].Index;
                    int id = int.Parse(dataGridView.Rows[numberOfLine].Cells[10].Value.ToString());
                    int index = 0;
                    for (int i = 0; i < parkings.Count; ++i)
                        if (parkings[i].Global_id == id)
                        {
                            index = i;
                            break;
                        }
                    
                    ModalDialogForm editNoteModelDialog = new ModalDialogForm(IDs, parkings[index].Global_id);
                    editNoteModelDialog.InitWithData(parkings[index]);

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
                    MessageBox.Show("Вы не выбрали ни одну запись.");
                }
            }
            catch
            {
                MessageBox.Show("Возникла проблема с изменением данных. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку сортировки на панели инструментов
        /// </summary>
        /// <param name="sender">Кнопка "Сортировать"</param>
        /// <param name="e">Некоторые параметры</param>
        private void SortButton_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Возникла проблема с сортировкой данных. Попробуйте снова.");
            }
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
                    if (first.TotalNumberOfParkingSpacesInAdmArea(parkings) < second.TotalNumberOfParkingSpacesInAdmArea(parkings))
                        return -1;
                    if (first.TotalNumberOfParkingSpacesInAdmArea(parkings) == second.TotalNumberOfParkingSpacesInAdmArea(parkings))
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

            FilterDataByCurrentFilterType();
            UpdateRowNumbersInDataGridView();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки по полю CarCapacity из верхнего меню
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void CarCapacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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

                FilterDataByCurrentFilterType();
                UpdateRowNumbersInDataGridView();
            }
            catch
            {
                MessageBox.Show("Во время сортировки произошла ошибка. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки по полю global_id из верхнего меню
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void GlobalIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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

                FilterDataByCurrentFilterType();
                UpdateRowNumbersInDataGridView();
            }
            catch
            {
                MessageBox.Show("Во время сортировки произошла ошибка. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки по количеству парковок в том же районе из верхнего меню
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void ByNumberOfParkingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                parkings.Sort((Parking first, Parking second) =>
                {
                    if (first.TotalNumberOfParkingSpacesInAdmArea(parkings) < second.TotalNumberOfParkingSpacesInAdmArea(parkings))
                        return -1;
                    if (first.TotalNumberOfParkingSpacesInAdmArea(parkings) == second.TotalNumberOfParkingSpacesInAdmArea(parkings))
                        return 0;
                    return 1;
                });

                dataGridView.Rows.Clear();
                foreach (var parking in parkings)
                    AddRowToDataGridView(parking);

                FilterDataByCurrentFilterType();
                UpdateRowNumbersInDataGridView();
            }
            catch
            {
                MessageBox.Show("Во время сортировки произошла ошибка. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки сортировки из верхнего меню и получает неотсортированные данные
        /// </summary>
        /// <param name="sender">Кнопка сортировки</param>
        /// <param name="e">Некоторые параметры</param>
        private void DefaultOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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

                FilterDataByCurrentFilterType();
                UpdateRowNumbersInDataGridView();
            }
            catch
            {
                MessageBox.Show("Во время сортировки произошла ошибка. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Фильтр",
        /// получает от пользователя значения фильтра и использует их для обработки значений
        /// </summary>
        /// <param name="sender">Кнопка "Фильтр"</param>
        /// <param name="e">Некоторые параметры</param>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Во время фильтрации произошла ошибка. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Осуществляет фильтрацию данных и обновление номеров строк.
        /// </summary>
        private void FilterDataByCurrentFilterType()
        {
            dataGridView.Rows.Clear();
            for (int i = 0; i < parkings.Count; ++i)
            {
                if (parkings[i].CarCapacity >= filter.MinCarCapacity &&
                    parkings[i].CarCapacity <= filter.MaxCarCapacity &&
                    parkings[i].ParkingLocation.AdmArea.ToLower().Contains(filter.AdmAreaValue.ToLower()))
                {
                    AddRowToDataGridView(parkings[i]);
                }
            }
        }

        /// <summary>
        /// Вызывается при изменении максимального возможного количества строк в таблице.
        /// </summary>
        /// <param name="sender">Элемент управления NumericUpDown</param>
        /// <param name="e">Некоторые аргументы</param>
        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var numericUpDown = (NumericUpDown)sender;
            MaxNumberOfNotes = numericUpDown.Value;
            if (numericUpDown.Value < dataGridView.Rows.Count)
            {
                numericUpDown.Value = dataGridView.Rows.Count;
                const string message = "Для того, чтобы уменьшить максимальное количество записей, удалите ненужные записи и попробуйте снова";
                const string caption = "Ошибка изменения значения максимального количества записей";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Warning);
                
                if (result == DialogResult.OK)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Выводит информацию о разработчике.
        /// </summary>
        /// <param name="sender">Пункт меню "Справка"</param>
        /// <param name="e">Некоторые параметры</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Разработчик: Коннова Маргарита, БПИ184(1)";
            const string caption = "Информация о разработчике";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
            
            if (result == DialogResult.OK)
            {
                return;
            }
        }

        /// <summary>
        /// Обрабатывает запись данных в конец существующего файла.
        /// Не записывает данные с уже существующим значением global_id.
        /// Количество записей после дозаписывания данных не будет превышать 354
        /// (не поместившиеся данные будут проигнорированы).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDataToFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string message = "Обратите внимание, что если записываемые данные содержат значение поля global_id," +
                " уже существующее в изменяемом файле, эта запись о парковке будет пропущена";
            const string caption = "Предупреждение о добавлении данных в конец файла";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Warning);
            
            if (result == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV-File|*.csv",
                    Title = "Добавление данных в существующий файл"
                };

                // Показывает окно SaveFileDialog.
                saveFileDialog.ShowDialog();

                // Открывает файл для сохранения, если имя файла не пустое.
                if (saveFileDialog.FileName != "")
                {
                    try
                    {
                        HashSet<int> idFromFile;
                        using (StreamReader reader = new StreamReader(saveFileDialog.FileName))
                        {
                            idFromFile = new HashSet<int>();

                            reader.ReadLine();
                            string content;
                            while (reader.Peek() != -1)
                            {
                                content = reader.ReadLine();

                                try
                                {
                                    idFromFile.Add(new Parking(content).Global_id);
                                }
                                catch (TaxiParkingException exception)
                                {
                                    MessageBox.Show(exception.Message);
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("Данные в изменяемом файле некорректны");
                                    return;
                                }
                            }
                        }
                        var leftString = numericUpDown.Maximum - idFromFile.Count;
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, true, Encoding.UTF8))
                        {
                            for (int i = 0, numberOfWritenString = 0; i < dataGridView.Rows.Count; ++i)
                            {
                                if (leftString == 0)
                                    break;
                                if (!idFromFile.Contains(int.Parse(dataGridView.Rows[i].Cells[10].Value.ToString()))) {
                                    writer.WriteLine(ConvertRowToString(numberOfWritenString, i, idFromFile.Count + 1));
                                    numberOfWritenString++;
                                    leftString--;
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так. Выберите другой файл для изменения и попробуйте снова.");
                    }
                }
            }
            catch 
            {
                // Ошибка возникает, когда пользователь не имеет разрешения на запись в определенное место.
                MessageBox.Show("Пожалуйста, выберете другое место для сохранения файла.");
            }
        }

        /// <summary>
        ///Преобразовывает строку таблицы с данным индексом в строку для записи в файл.
        /// </summary>
        /// <param name="ii">Номер строки в таблице.</param>
        /// <param name="index">Номер строки для отображения.</param>
        /// <param name="delta">Количество строк, расположенных выше создаваемой для корректного создания номера строки.</param>
        /// <returns></returns>
        private string ConvertRowToString(int ii, int index, int delta)
        {
            try
            {
                string result = (ii + delta).ToString() + ";";

                for (int i = 1; i < dataGridView.Rows[index].Cells.Count; ++i)
                {
                    result += $"\"{dataGridView.Rows[index].Cells[i].Value}\";";
                }

                return result;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Обрабатывает сохранение данных как нового файла.
        /// </summary>
        /// <param name="sender">Пункт меню "Сохранить как новый файл"</param>
        /// <param name="e">Некоторые параметры.</param>
        private void SaveAsNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Все данные из таблицы будут записаны в эту строку
                string csv = string.Empty;

                // Добавляет заголовки в CSV-файл
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    csv += column.HeaderText + ';';
                }

                // Добавляет переход на новую строку
                csv += "\r\n";
                // Добавляет все оставшиеся строки
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    int i = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (i == 0)
                            csv += $"{cell.Value.ToString()};";
                        else
                            csv += $"\"{cell.Value.ToString()}\";";
                        i++;
                    }

                    // Добавляет переход на новую строку
                    csv += "\r\n";
                }

                // Сохраняет данные как CSV-файл на диск
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV-File|*.csv",
                    Title = "Сохранение нового файла"
                };

                // Показывает окно SaveFileDialog.
                Console.WriteLine(csv);
                // Открывает файл для сохранения, если имя файла не пустое.
                if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
                    File.WriteAllText(saveFileDialog.FileName, csv, Encoding.UTF8);
            }
            catch
            {
                // Ошибка возникает, когда пользователь не имеет разрешения на запись в определенное место.
                MessageBox.Show("Пожалуйста, выберете другое место для сохранения файла.");
            }
        }

        /// <summary>
        /// Обрабатывает перезапись существующего файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeCurrentFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV-File|*.csv",
                    Title = "Перезапись существующего файла"
                };

                // Показывает окно SaveFileDialog.
                saveFileDialog.ShowDialog();

                // Открывает файл для сохранения, если имя файла не пустое.
                if (saveFileDialog.FileName != "")
                {
                    try
                    {
                        if (File.Exists(saveFileDialog.FileName))
                        {
                            File.WriteAllText(saveFileDialog.FileName, "", Encoding.UTF8);
                            using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, true, Encoding.UTF8))
                            {
                                // Все данные из таблицы будут записаны в эту строку
                                string csv = string.Empty;

                                // Добавляет заголовки в CSV-файл
                                foreach (DataGridViewColumn column in dataGridView.Columns)
                                {
                                    csv += column.HeaderText + ';';
                                }
                                writer.WriteLine(csv);
                                for (int i = 0; i < dataGridView.Rows.Count; ++i)
                                {
                                    writer.WriteLine(ConvertRowToString(i, i, 1));
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так. Выберите другой файл для перезаписи и попробуйте снова.");
                    }
                }
            }
            catch
            {
                // Ошибка возникает, когда пользователь не имеет разрешения на запись в определенное место.
                MessageBox.Show("Пожалуйста, выберете другое место для сохранения файла.");
            }
        }
        
    }
}
