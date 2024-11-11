using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;

namespace RegionPlus
{
    public partial class frm_MainForm : Form
    {
        public frm_MainForm(string[] args)
        {
            //Получаем параметры
            this.args = args;

            InitializeComponent();


            try
            {
                //Если нам при запуске приложения был передан параметр от AutoUpdater, то не будем проверять обновление
                if (args.Length > 0)
                {
                    //RUNNINGPROCESS = args[0];
                    //txt_RunningProccess.Text = RUNNINGPROCESS;
                }
                else //А вот если не передали, то проверим обновление
                {
                    GetNewVersion();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            //Прогресс сохранения. Инициализируем потоки
            ProcessWorkBackground.WorkerReportsProgress = true;
            ProcessWorkBackground.WorkerSupportsCancellation = true;
        }

        #region Глобальные переменные
        //Параметры
        string[] args;

        //Структура подключения
        public struct structServerInfo
        {
            public string Server;
            public string SID;
            public string Login;
            public string Password;
        }

        //Настройки подключения
        public SqlConnection RegionConnect = new SqlConnection();
        public structServerInfo ServerInfo = new structServerInfo();
        public string connStringPattern = "Data Source=%SERVER%;Initial Catalog=%SID%;" +
            "Persist Security Info=True;UID=%LOGIN%;PWD=%PASSWORD%";

        public string MakeConnString(string ConnString, structServerInfo SInfo)
        {
            return ConnString.
                Replace("%SERVER%", SInfo.Server).
                Replace("%SID%", SInfo.SID).
                Replace("%LOGIN%", SInfo.Login).
                Replace("%PASSWORD%", SInfo.Password);
        }
        //Команды SQL
        private SqlCommand m_sqlCmd;

        //Время в логе - dt, которую передаём в date_time (Полезные коды.Работа с временем)
        public DateTime datenow = DateTime.Now;
        //Что мы ищем через Inputbox
        public static string inputbox_search = "";

        //Пользователь
        public string RegionUser = "";
        //Группа        
        public int UserGroup = 0;


        #endregion Глобальные переменные

        #region Подключение

        //Вызов формы подключения к БД
        private void tol_ConnectDB_Click(object sender, EventArgs e)
        {
            //if (RegionConnect.State != System.Data.ConnectionState.Closed)
            //{
            //    RegionConnect.Close();
            //}
            //frm_Connect FormConnect = new frm_Connect();
            //FormConnect.FormParent = this;
            //FormConnect.ShowDialog();
            //if (FormConnect.DialogResult == DialogResult.OK)
            //{
            //    try
            //    {
            //        RegionConnect.ConnectionString = MakeConnString(connStringPattern, ServerInfo);
            //        RegionConnect.Open();
            //    }
            //    catch
            //    {
            //        this.Text = "Регион +";
            //    };
            //    if (RegionConnect.State == System.Data.ConnectionState.Open)
            //    {
            //        this.Text = "Регионе + подключен к " + ServerInfo.Server + @"\" + ServerInfo.SID;
            //        tol_ConnectDB.Enabled = false;
            //        tol_DisconnectDB.Enabled = true;
            //    }
            //}

            if (RegionConnect.State != System.Data.ConnectionState.Closed)
            {
                RegionConnect.Close();
            }
            frm_Connect FormConnect = new frm_Connect
            {
                FormParent = this
            };
            FormConnect.ShowDialog();
            if (FormConnect.DialogResult == DialogResult.OK)
            {
                try
                {
                    RegionConnect.ConnectionString = MakeConnString(connStringPattern, ServerInfo);
                    RegionConnect.Open();
                }
                catch
                {
                    this.Text = "Регион +";
                };
                if (RegionConnect.State == System.Data.ConnectionState.Open)
                {
                    this.Text = "Регион + подключен к " + ServerInfo.Server + @"\" + ServerInfo.SID;
                    tol_ConnectDB.Enabled = false;
                    tol_DisconnectDB.Enabled = true;

                    //Запросим по коду роли, что пользователю можно
                    StatusRole(UserGroup);
                }
            }
        }

        private void tol_DisconnectDB_Click(object sender, EventArgs e)
        {
            if (RegionConnect.State == System.Data.ConnectionState.Open)
            {
                RegionConnect.Close();
                tol_ConnectDB.Enabled = true;
                tol_DisconnectDB.Enabled = false;

                //Переводим в режим пользователя
                StatusRole(0);
            }
        }

        #endregion Подключение

        #region Выполнение SQL подключения и выполнения скрипта

        System.Data.DataTable dt = new System.Data.DataTable();

        //Выполнение SQL подключения и выполнения скрипта
        private System.Data.DataTable GetDataTable(SqlConnection conn, string SQLText)
        {
            //создаём новую таблицу, чтобы старых данных не было
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                SqlDataAdapter RegionDataAdapter = new SqlDataAdapter();
                SqlCommand RegionCommand = new SqlCommand(SQLText, conn);
                RegionDataAdapter.SelectCommand = RegionCommand;
                DataSet RegionDataSet = new DataSet();
                RegionDataAdapter.Fill(dt);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString());
            };
            return dt;
        }

        public void Execution_SQL_Script(string SQL_Script)
        {
            if (RegionConnect.State == System.Data.ConnectionState.Open)
            {
                sts_DateTimeStartExecutionSQLScript.Text = "Дата начала выполнения запроса: " + date_time(datenow) + "";

                //отправляем и получаем значение в dgv_Data
                dgv_Data.DataSource = GetDataTable(RegionConnect, SQL_Script);

                sts_DateTimeEndExecutionSQLScript.Text = "Дата окончания выполнения запроса: " + date_time(datenow) + "";

                //Получим количество записей и покажем на форме в поле tol_Info
                sts_NumberRows.Text = "Количество записей: " + dgv_Data.RowCount.ToString();
            }
        }

        #endregion Выполнение SQL подключения и выполнения скрипта

        #region Кнопки Меню (Отчеты)

        #region БШД

        //Список оборудования с IP-адресами
        private void tol_ListDeviceIP_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(SQL_List_Devices_IP);
        }

        //Список оборудования с IP-адресами и их последний статус
        private void tol_ListDeviceIPStatus_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(SQL_List_Devices_IP_STATUS);
            //Раскраска
            ListDeviceIPStatusColor();

        }

        //Раскрашиваем столбец в зависимости от статуса оборудования
        void ListDeviceIPStatusColor()
        {
            for (int j = 0; j < dgv_Data.Rows.Count; j++)
            {
                if (dgv_Data.Rows[j].Cells[8].Value.ToString() == "OFF")
                {
                    dgv_Data.Rows[j].Cells[8].Style.BackColor = Color.Red;
                }
                if (dgv_Data.Rows[j].Cells[8].Value.ToString() == "ON")
                {
                    dgv_Data.Rows[j].Cells[8].Style.BackColor = Color.GreenYellow;
                }
            }
        }

        #endregion БШД

        #region Терминалы
        private void tol_ListTerminal_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(SQL_ListTerminal);
        }

        #endregion Терминалы

        #region АГЗУ
        private void tol_ListAGZU_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(SQL_ListAGZU);
        }

        #endregion АГЗУ

        #region Все скважины
        //Все скважины и адреса контроллеров
        private void tol_All_Kontrollers_SKV_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(SQL_List_SKV_Kontrollers_All);
        }

        //Поиск адреса контроллера скважины
        private void tol_All_Kontrollers_SKV_Search_Click(object sender, EventArgs e)
        {
            //Очищаем данные от предыдущего запроса
            inputbox_search = "";
            //Показываем форму
            InputBox.InputBox inputBox = new InputBox.InputBox();
            inputbox_search = inputBox.getString();
            //Если в inputbox_search что-то передали, то будем искать, если нажали отмену, то там null искать не надо
            if (inputbox_search != null)
            {

                Execution_SQL_Script(SQL_List_SKV_Kontrollers_All_Search(inputbox_search));
            }

        }

        //Контроллеры в оффлайн
        private void tol_All_Kontrollers_SKV_Status_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_Status);
        }

        #endregion Все скважины

        #region Добывающие скважины

        //Все скважины
        private void tol_All_SKV_OIL_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_SKV_OIL);
        }

        //Скважины с контроллерами
        private void tol_All_Kontrollers_SKV_OIL_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_OIL);
        }

        //Скважины с СУ
        private void tol_All_Kontrollers_SKV_OIL_SU_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_OIL_SU);
        }

        //Пришивка СУ
        private void tol_All_Kontrollers_SKV_OIL_SU_Driver_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_OIL_SU_Driver);
        }

        //Скважины с беспроводными датчиками давления
        private void tol_All_Kontrollers_SKV_OIL_Rosemount_EndressHauser_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_OIL_Rosemount_EndressHauser);
        }

        //Скважины с СКЖ
        private void tol_All_Kontrollers_SKV_OIL_SKZH_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_OIL_SKZH);
        }

        //Скважины с ингибитором
        private void tol_All_Kontrollers_SKV_OIL_Ingibitor_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_OIL_Ingibitor);
        }

        //Поиск адреса контроллера скважины
        private void tol_All_Kontrollers_SKV_OIL_Search_Click(object sender, EventArgs e)
        {
            //Очищаем данные от предыдущего запроса
            inputbox_search = "";
            //Показываем форму
            InputBox.InputBox inputBox = new InputBox.InputBox();
            inputbox_search = inputBox.getString();
            //Если в inputbox_search что-то передали, то будем искать, если нажали отмену, то там null искать не надо
            if (inputbox_search != null)
            {
                Execution_SQL_Script(All_Kontrollers_SKV_OIL_Search(inputbox_search));
            }
        }



        #endregion Добывающие скважины

        #region Нагнетательные скважины

        private void tol_All_SKV_WATER_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_SKV_WATER);
        }

        //Скважины с контроллерами
        private void tol_All_Kontrollers_SKV_WATER_Click(object sender, EventArgs e)
        {
            Execution_SQL_Script(All_Kontrollers_SKV_WATER);
        }

        //Поиск адреса контроллера скважины
        private void tol_All_Kontrollers_SKV_WATER_Search_Click(object sender, EventArgs e)
        {
            //Очищаем данные от предыдущего запроса
            inputbox_search = "";
            //Показываем форму
            InputBox.InputBox inputBox = new InputBox.InputBox();
            inputbox_search = inputBox.getString();
            //Если в inputbox_search что-то передали, то будем искать, если нажали отмену, то там null искать не надо
            if (inputbox_search != null)
            {
                Execution_SQL_Script(All_Kontrollers_SKV_WATER_Search(inputbox_search));
            }
        }

        #endregion Нагнетательные скважины

        #region

        DataTable dt_DNSNasosAlarm = new DataTable();  //Таблица, где находятся аварии насосов

        private void tol_TekStatusNasos_Click(object sender, EventArgs e)
        {
            //Очищаем таблицу
            try
            {
                //Удаляем записи
                dgv_Data.Rows.Clear();
                //Удаляем столбцы
                //int CellCount = dgv_Data.Columns.Count - 1;
                //dgv_Data.Columns.Remove(dgv_Data.Columns[CellCount]);
                //dgv_Data.ColumnCount = 0;
                dgv_Data.Columns.RemoveAt(dgv_Data.Columns.Count - 1);

                //Убираем источник
                //dgv_Data.DataSource = null;
                DataTable dt = new DataTable();
                //sda.Fill(dt);
                //dataGridView1.Rows.Clear();
                //dataSet1.Clear();
                dgv_Data.DataSource = dt;
            }
            catch
            { }

            SPISOK_NASOSOV_TEK_ALARMS();
            COLOR_ALARM();
            tmr_Refresh.Stop();
            Timer();
        }


        void SPISOK_NASOSOV_TEK_ALARMS()
        {
            try
            {
                DataTable dt = new DataTable();
                dgv_Data.DataSource = dt;
                dgv_Data.DataSource = null;

                //Cписок объектов
                List<NAGRSTALTRN> list_staltrn = new List<NAGRSTALTRN>();

                string SQL_Script = List_DNS_Nasos_Alarm;
                dt_DNSNasosAlarm = GetDataTable(RegionConnect, SQL_Script);

                foreach (DataRow dr in dt_DNSNasosAlarm.Rows)
                {
                    //Определяем у объекта его тип
                    string dr_RAJON = dr["RAJON"].ToString().Trim();
                    string dr_MEST = dr["MEST"].ToString().Trim();
                    string dr_DNSNAME = dr["DNSNAME"].ToString().Trim();
                    string dr_DNSGEO = dr["DNSGEO"].ToString().Trim();
                    string dr_NASOSNAME = dr["NAGRNAME"].ToString().Trim();
                    string dr_GEO = dr["NAGRGEO"].ToString().Trim();
                    DateTime dr_OTDAY = DateTime.Parse(dr["OTDAY"].ToString());
                    string dr_TYPE = dr["TYPE"].ToString().Trim();
                    string dr_TIMESPAN = dr["TIMESPAN"].ToString().Trim();
                    list_staltrn.Add(new NAGRSTALTRN(dr_MEST, dr_RAJON, dr_DNSNAME, dr_DNSGEO, dr_NASOSNAME, dr_GEO, dr_OTDAY, dr_TYPE, dr_TIMESPAN));
                }

                //Обходим список в цикле и изменяем дату
                int i = 0;

                for (i = 0; i < list_staltrn.Count; i++)
                {                 

                    list_staltrn[i].TIMESTRING = ConvertToTime(Convert.ToInt32(list_staltrn[i].TIMESTRING)).ToString();
                }

                dgv_Data.ColumnCount = 8;
                dgv_Data.ColumnHeadersVisible = true;
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

                //// Set the column header names.
                dgv_Data.Columns[0].HeaderText = @"Район";
                dgv_Data.Columns[0].Width = 160;
                dgv_Data.Columns[1].HeaderText = @"Месторождение";
                dgv_Data.Columns[1].Width = 160;
                dgv_Data.Columns[2].HeaderText = @"Номер ДНС";
                dgv_Data.Columns[2].Width = 60;
                dgv_Data.Columns[3].HeaderText = @"Название";
                dgv_Data.Columns[3].Width = 120;
                dgv_Data.Columns[4].HeaderText = @"Номер насоса";
                dgv_Data.Columns[4].Width = 120;
                dgv_Data.Columns[5].HeaderText = @"Дата события";
                dgv_Data.Columns[5].Width = 120;
                dgv_Data.Columns[6].HeaderText = @"Тип события";
                dgv_Data.Columns[6].Width = 100;
                dgv_Data.Columns[7].HeaderText = @"Время работы\простоя";
                dgv_Data.Columns[7].Width = 180;


                int v = 0;

                for (v = 0; v < list_staltrn.Count; v++)
                {
                    dgv_Data.Rows.Add(list_staltrn[v].RAJON, list_staltrn[v].MEST, list_staltrn[v].DNSGEO, list_staltrn[v].DNSNAME, list_staltrn[v].GEO, list_staltrn[v].OTDAY, list_staltrn[v].TYPE, list_staltrn[v].TIMESTRING);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void COLOR_ALARM()
        {
            for (int j = 0; j < dgv_Data.Rows.Count; j++)
            {
                if (dgv_Data.Rows[j].Cells[6].Value.ToString() == "ОСТАНОВ")
                {
                    dgv_Data.Rows[j].Cells[0].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[1].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[2].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[3].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[4].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[5].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[6].Style.BackColor = Color.Yellow;
                    dgv_Data.Rows[j].Cells[7].Style.BackColor = Color.Yellow;
                }
            }
        }


        #endregion

        #endregion Кнопки Меню (Отчеты)

        #region Конфигурация

        private void tol_Config_Click(object sender, System.EventArgs e)
        {
            //MessageBox.Show("Данный функционал пока находится в стадии разработки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //frmConfig f = new frmConfig();
            //f.ShowDialog();
        }

        #endregion Конфигурация

        #region О программе

        private void tol_About_Click(object sender, EventArgs e)
        {
            frm_AboutForm AboutForm = new frm_AboutForm();
            AboutForm.ShowDialog();
        }

        #endregion О программе

        #region Выход из приложения

        private void tol_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion Выход из приложения

        #region SQL запросы

        #region БШД

       public string SQL_List_Devices_IP = @""
       + "SELECT TABLE_OBJ.TYPOBJ as 'Тип оборудования', dbo.GetClass(TABLE_OBJ.RAJON) AS 'Район', dbo.GetClass(TABLE_OBJ.MEST) AS 'Месторождение', dbo.GetCode(TABLE_OBJ.GEOTERM) AS 'Терминал', dbo.GetCode(TABLE_OBJ.GEOKUST) AS 'Куст', dbo.GetCode(TABLE_OBJ.GEO) AS 'Номер', TABLE_OBJ.IPADR as 'IP-адрес' FROM  "
       + "(  "
       + "/* Терминал */ "
       + "SELECT TC.ID, NULL AS IDPARENT, TC.IPADR AS HOST, 'Y' AS ISHOST, 'Teрминал' AS TYPOBJ, (TC.RAJON) AS RAJON, (TC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (TC.GEO)AS GEO, TC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         TERMCFG TC "
       + "WHERE TC.IPADR <> '' AND TC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/*Доп. контроллеры*/ "
       + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         DOPCONTR DC INNER JOIN "
       + "                      TERMCFG TC ON DC.IDPARENT = TC.ID "
       + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         DOPCONTR DC INNER JOIN "
       + "                      UZELUCFG UC ON DC.IDPARENT = UC.ID LEFT JOIN "
       + "                      DNSCFG DC1 ON DC1.ID = UC.IDPARENT LEFT JOIN "
       + "                      TERMCFG TC ON DC1.IDPARENT = TC.ID "
       + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         DOPCONTR DC INNER JOIN "
       + "                      UZELZCFG UZ ON DC.IDPARENT = UZ.ID LEFT JOIN "
       + "                      PIPECFG PC ON UZ.IDPARENT = PC.ID LEFT JOIN "
       + "                      TERMCFG TC ON PC.IDPARENT = TC.ID "
       + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         DOPCONTR DC INNER JOIN "
       + "                      GZUCFG GC ON DC.IDPARENT = GC.ID LEFT JOIN "
       + "                      TERMCFG TC ON GC.IDPARENT = TC.ID "
       + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         DOPCONTR DC INNER JOIN "
       + "                      MSKCFG MC ON DC.IDPARENT = MC.ID LEFT JOIN "
       + "                      GZUCFG GC ON MC.IDPARENT = GC.ID LEFT JOIN "
       + "                      TERMCFG TC ON GC.IDPARENT = TC.ID "
       + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         DOPCONTR DC INNER JOIN "
       + "                      NSKCFG NC ON DC.IDPARENT = NC.ID LEFT JOIN "
       + "                      VRGCFG VC ON NC.IDPARENT = VC.ID LEFT JOIN "
       + "                      TERMCFG TC ON VC.IDPARENT = TC.ID "
       + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/* ГЗУ */ "
       + "SELECT GC.ID, GC.IDPARENT, GC.IPADR AS HOST, 'Y' AS ISHOST, 'ГЗУ'  AS TYPOBJ, (GC.RAJON)AS RAJON, (GC.MEST) as MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (GC.GEO)AS GEO, GC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         GZUCFG GC LEFT OUTER JOIN "
       + "                      TERMCFG TC ON GC.IDPARENT = TC.ID "
       + "WHERE GC.IPADR <> '' AND GC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/* ВРГ */ "
       + "SELECT VC.ID, VC.IDPARENT, VC.IPADR AS HOST, 'Y' AS ISHOST, 'ВРГ' AS TYPOBJ, (VC.RAJON)AS RAJON, (VC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (VC.GEO)AS GEO, VC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         VRGCFG VC LEFT OUTER JOIN "
       + "                      TERMCFG TC ON VC.IDPARENT = TC.ID "
       + "WHERE VC.IPADR <> '' AND VC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/* Ингибитор */ "
       + "SELECT IC.ID, IC.IDPARENT, IC.IPADR AS HOST, 'Y' AS ISHOST, 'Ингибитор' AS TYPOBJ, (IC.RAJON)AS RAJON, (IC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (IC.GEO)AS GEO, IC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         INGBCFG IC INNER JOIN "
       + "                      GZUCFG GC ON IC.IDPARENT = GC.ID AND IC.TYPUSE = 'UD0001' LEFT JOIN "
       + "                      TERMCFG TC ON IC.GEOTERM = TC.GEO AND IC.MEST = TC.MEST "
       + "WHERE IC.IPADR <> '' AND IC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "SELECT     IC.ID, IC.IDPARENT, IC.IPADR AS HOST, 'Y' AS ISHOST, 'Ингибитор' AS TYPOBJ, (IC.RAJON)AS RAJON, (IC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (IC.GEO)AS GEO, IC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         INGBCFG IC INNER JOIN "
       + "                      MSKCFG MC ON IC.GEO = MC.GEO AND IC.RAJON = MC.RAJON AND MC.MEST = IC.MEST AND IC.TYPUSE = 'UD0002' LEFT JOIN "
       + "                      GZUCFG GC ON MC.GEOGZU = GC.GEO AND MC.MEST = GC.MEST LEFT JOIN "
       + "                      TERMCFG TC ON IC.GEOTERM = TC.GEO AND IC.MEST = TC.MEST "
       + "WHERE IC.IPADR <> '' AND IC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/* Нефтяная скважина */ "
       + "SELECT  MC.ID, MC.IDPARENT, MC.IPADR AS HOST, 'Y' AS ISHOST, 'Нефтяная скважина' AS TYPOBJ, (MC.RAJON)AS RAJON, (MC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (MC.GEO)AS GEO, MC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         MSKCFG MC LEFT OUTER JOIN "
       + "                      TERMCFG TC ON MC.GEOTERM = TC.GEO AND MC.MEST = TC.MEST AND MC.RAJON = TC.RAJON "
       + "WHERE MC.IPADR <> '' AND MC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/* Нагнетательная скважина */ "
       + "SELECT NC.ID, NC.IDPARENT, NC.IPADR AS HOST, 'Y' AS ISHOST, 'Нагнетательная скважина' AS TYPOBJ, (NC.RAJON)AS RAJON, (NC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (NC.GEO)AS GEO, NC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         NSKCFG NC LEFT OUTER JOIN "
       + "                      TERMCFG TC ON NC.GEOTERM = TC.GEO AND NC.MEST = TC.MEST AND NC.RAJON = TC.RAJON "
       + "WHERE NC.IPADR <> '' AND NC.IPADR IS NOT NULL "
       + "UNION ALL "
       + "/* МДС */ "
       + "SELECT MDC.ID, MDC.IDPARENT, MDC.IPADR AS HOST, 'Y' AS ISHOST, 'МДС' AS TYPOBJ, (MDC.RAJON)AS RAJON, (MDC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (MDC.GEO)AS GEO, MDC.IPADR, TC.NCEX, TC.NBRIG "
       + "FROM         MDSCFG MDC LEFT OUTER JOIN "
       + "                      TERMCFG TC ON MDC.GEOTERM = TC.GEO AND MDC.MEST = TC.MEST AND MDC.RAJON = TC.RAJON "
       + "WHERE MDC.IPADR <> '' AND MDC.IPADR IS NOT NULL "
       + ") AS TABLE_OBJ "
       + "ORDER BY TABLE_OBJ.IDPARENT, TABLE_OBJ.RAJON, TABLE_OBJ.MEST, TABLE_OBJ.GEOTERM, TABLE_OBJ.GEOKUST, TABLE_OBJ.TYPOBJ, TABLE_OBJ.GEO ASC ";

        public string SQL_List_Devices_IP_SERVICE = "" +
        @"SELECT TABLE_OBJ.HOST AS HOST, '45' AS PINGFREQ FROM  "
        + "(  "
        + "/* Терминал */ "
        + "SELECT TC.ID, NULL AS IDPARENT, TC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Teрминал' AS TYPOBJ, (TC.RAJON) AS RAJON, (TC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (TC.GEO)AS GEO, TC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         TERMCFG TC "
        + "WHERE TC.IPADR <> '' AND TC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/*Доп. контроллеры*/ "
        + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         DOPCONTR DC INNER JOIN "
        + "                      TERMCFG TC ON DC.IDPARENT = TC.ID "
        + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         DOPCONTR DC INNER JOIN "
        + "                      UZELUCFG UC ON DC.IDPARENT = UC.ID LEFT JOIN "
        + "                      DNSCFG DC1 ON DC1.ID = UC.IDPARENT LEFT JOIN "
        + "                      TERMCFG TC ON DC1.IDPARENT = TC.ID "
        + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         DOPCONTR DC INNER JOIN "
        + "                      UZELZCFG UZ ON DC.IDPARENT = UZ.ID LEFT JOIN "
        + "                      PIPECFG PC ON UZ.IDPARENT = PC.ID LEFT JOIN "
        + "                      TERMCFG TC ON PC.IDPARENT = TC.ID "
        + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         DOPCONTR DC INNER JOIN "
        + "                      GZUCFG GC ON DC.IDPARENT = GC.ID LEFT JOIN "
        + "                      TERMCFG TC ON GC.IDPARENT = TC.ID "
        + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         DOPCONTR DC INNER JOIN "
        + "                      MSKCFG MC ON DC.IDPARENT = MC.ID LEFT JOIN "
        + "                      GZUCFG GC ON MC.IDPARENT = GC.ID LEFT JOIN "
        + "                      TERMCFG TC ON GC.IDPARENT = TC.ID "
        + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "SELECT DC.ID, DC.IDPARENT, DC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Доп. контроллер' AS TYPOBJ, (DC.RAJON)AS RAJON, (DC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (DC.GEO)AS GEO, DC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         DOPCONTR DC INNER JOIN "
        + "                      NSKCFG NC ON DC.IDPARENT = NC.ID LEFT JOIN "
        + "                      VRGCFG VC ON NC.IDPARENT = VC.ID LEFT JOIN "
        + "                      TERMCFG TC ON VC.IDPARENT = TC.ID "
        + "WHERE DC.IPADR <> '' AND DC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/* ГЗУ */ "
        + "SELECT GC.ID, GC.IDPARENT, GC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'ГЗУ'  AS TYPOBJ, (GC.RAJON)AS RAJON, (GC.MEST) as MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (GC.GEO)AS GEO, GC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         GZUCFG GC LEFT OUTER JOIN "
        + "                      TERMCFG TC ON GC.IDPARENT = TC.ID "
        + "WHERE GC.IPADR <> '' AND GC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/* ВРГ */ "
        + "SELECT VC.ID, VC.IDPARENT, VC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'ВРГ' AS TYPOBJ, (VC.RAJON)AS RAJON, (VC.MEST)AS MEST, (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (VC.GEO)AS GEO, VC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         VRGCFG VC LEFT OUTER JOIN "
        + "                      TERMCFG TC ON VC.IDPARENT = TC.ID "
        + "WHERE VC.IPADR <> '' AND VC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/* Ингибитор */ "
        + "SELECT IC.ID, IC.IDPARENT, IC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Ингибитор' AS TYPOBJ, (IC.RAJON)AS RAJON, (IC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (IC.GEO)AS GEO, IC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         INGBCFG IC INNER JOIN "
        + "                      GZUCFG GC ON IC.IDPARENT = GC.ID AND IC.TYPUSE = 'UD0001' LEFT JOIN "
        + "                      TERMCFG TC ON IC.GEOTERM = TC.GEO AND IC.MEST = TC.MEST "
        + "WHERE IC.IPADR <> '' AND IC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "SELECT     IC.ID, IC.IDPARENT, IC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Ингибитор' AS TYPOBJ, (IC.RAJON)AS RAJON, (IC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (IC.GEO)AS GEO, IC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         INGBCFG IC INNER JOIN "
        + "                      MSKCFG MC ON IC.GEO = MC.GEO AND IC.RAJON = MC.RAJON AND MC.MEST = IC.MEST AND IC.TYPUSE = 'UD0002' LEFT JOIN "
        + "                      GZUCFG GC ON MC.GEOGZU = GC.GEO AND MC.MEST = GC.MEST LEFT JOIN "
        + "                      TERMCFG TC ON IC.GEOTERM = TC.GEO AND IC.MEST = TC.MEST "
        + "WHERE IC.IPADR <> '' AND IC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/* Нефтяная скважина */ "
        + "SELECT  MC.ID, MC.IDPARENT, MC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Нефтяная скважина' AS TYPOBJ, (MC.RAJON)AS RAJON, (MC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (MC.GEO)AS GEO, MC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         MSKCFG MC LEFT OUTER JOIN "
        + "                      TERMCFG TC ON MC.GEOTERM = TC.GEO AND MC.MEST = TC.MEST AND MC.RAJON = TC.RAJON "
        + "WHERE MC.IPADR <> '' AND MC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/* Нагнетательная скважина */ "
        + "SELECT NC.ID, NC.IDPARENT, NC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'Нагнетательная скважина' AS TYPOBJ, (NC.RAJON)AS RAJON, (NC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (NC.GEO)AS GEO, NC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         NSKCFG NC LEFT OUTER JOIN "
        + "                      TERMCFG TC ON NC.GEOTERM = TC.GEO AND NC.MEST = TC.MEST AND NC.RAJON = TC.RAJON "
        + "WHERE NC.IPADR <> '' AND NC.IPADR IS NOT NULL "
        + "UNION ALL "
        + "/* МДС */ "
        + "SELECT MDC.ID, MDC.IDPARENT, MDC.IPADR AS HOST, 'Y' AS ISHOST, 'Y' AS DOPING, 'МДС' AS TYPOBJ, (MDC.RAJON)AS RAJON, (MDC.MEST), (TC.GEO)AS GEOTERM, (TC.GEOKUST)AS GEOKUST, (MDC.GEO)AS GEO, MDC.IPADR, TC.NCEX, TC.NBRIG "
        + "FROM         MDSCFG MDC LEFT OUTER JOIN "
        + "                      TERMCFG TC ON MDC.GEOTERM = TC.GEO AND MDC.MEST = TC.MEST AND MDC.RAJON = TC.RAJON "
        + "WHERE MDC.IPADR <> '' AND MDC.IPADR IS NOT NULL "
        + ") AS TABLE_OBJ "
        + "WHERE TABLE_OBJ.ISHOST = 'Y' AND DOPING = 'Y' "
        + "ORDER BY TABLE_OBJ.HOST ASC ";

        
        public string SQL_List_Devices_IP_STATUS = @"
        SELECT        TOP (100) PERCENT TABLE_TEMP.TYPOBJ AS 'Тип оборудования', dbo.GetClass(TABLE_TEMP.RAJON) AS 'Район', dbo.GetClass(TABLE_TEMP.MEST) 
                                 AS 'Месторождение', dbo.GetCode(TABLE_TEMP.GEOTERM) AS 'Терминал', dbo.GetCode(TABLE_TEMP.GEOKUST) AS 'Куст', dbo.GetCode(TABLE_TEMP.GEO) 
                                 AS 'Номер', TABLE_TEMP.HOST AS 'IP-адрес', TABLE_TEMP.RECORDINGDATE AS 'Дата опроса', TABLE_TEMP.STATUS AS 'Статус', 
                                 dbo.MSKTEK.OTDAY AS 'Данные со скважины'
        FROM            (SELECT        TOP (100) PERCENT INFOOBJIPV.ID, INFOOBJIPV.TYPOBJ, INFOOBJIPV.RAJON, INFOOBJIPV.MEST, INFOOBJIPV.GEOTERM, INFOOBJIPV.GEOKUST, 
                                                            INFOOBJIPV.GEO, ALLPING.HOST, ALLPING.RECORDINGDATE, ALLPING.STATUS
                                  FROM            dbo.INFOOBJIP AS ALLPING INNER JOIN
                                                                (SELECT        HOST, MAX(RECORDINGDATE) AS LASTRECORDINGDATE
                                                                  FROM            dbo.INFOOBJIP
                                                                  GROUP BY HOST) AS LASTPING ON ALLPING.HOST = LASTPING.HOST AND 
                                                            ALLPING.RECORDINGDATE = LASTPING.LASTRECORDINGDATE INNER JOIN
                                                            dbo.INFOOBJIPV AS INFOOBJIPV ON INFOOBJIPV.HOST = ALLPING.HOST
                                  ORDER BY INFOOBJIPV.TYPOBJ, INFOOBJIPV.RAJON, INFOOBJIPV.MEST, INFOOBJIPV.GEOTERM, INFOOBJIPV.GEOKUST, INFOOBJIPV.GEO) 
                                 AS TABLE_TEMP INNER JOIN
                                 dbo.MSKTEK ON TABLE_TEMP.ID = dbo.MSKTEK.ID
        ORDER BY 'Тип оборудования', TABLE_TEMP.RAJON, TABLE_TEMP.MEST, TABLE_TEMP.GEOTERM, TABLE_TEMP.GEOKUST, TABLE_TEMP.GEO ";


        #endregion БШД

        #region Терминалы

       public string SQL_ListTerminal = @""
       + "SELECT   "
       + "(dbo.GetClass(TERM.RAJON)) as 'Район',    "
       + "(dbo.GetClass(TERM.MEST)) as 'Месторождение',    "
       + "(dbo.GetCode(TERM.GEO)) AS 'Терминал',    "
       + "(dbo.GetClass(TERM.TYPCONTR)) as 'Тип контроллера',     "
       + "(TERM.ADRCONTR) as 'Адрес терминала',    "
       + "(TERM.IPADR) as 'IP-адрес терминала',   "
       + "(TERM.NUMLIN) as 'Номер линии(номер COM-порта)',   "
       + "(TERM.DIRECTCONTR) as 'Номер направления контроллера',   "
       + "(dbo.GetClass(TERM.TYPLIN)) as 'Тип линии'   "
       + "FROM TERMCFG TERM   "
       + "ORDER BY TERM.RAJON, TERM.MEST, TERM.GEO ASC   ";

        #endregion Терминалы

        #region АГЗУ

        public string SQL_ListAGZU = @""
       + "SELECT    "
       + "(dbo.GetClass(GZU.RAJON)) as 'Район',     "
       + "(dbo.GetClass(GZU.MEST)) as 'Месторождение',     "
       + "(dbo.GetCode(GZU.GEOTERM)) AS 'Терминал',     "
       + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)     "
       + "         FROM TERMCFG t    "
       + "        WHERE t.GEO = GZU.GEOTERM) as 'Контроллер терминала',     "
       + "(SELECT TOP 1 UPPER(t.ADRCONTR)    "
       + "         FROM TERMCFG t    "
       + "         WHERE t.GEO = GZU.GEOTERM) as 'Адрес терминала',     "
       + "(dbo.GetCode(GZU.GEO)) AS 'АГЗУ',      "
       + "(dbo.GetClass(GZU.TYPCONTR)) as 'Контроллера АГЗУ',      "
       + "(GZU.ADRCMK) as 'Адрес АГЗУ',    "
       + "(GZU.IPADR) as 'IP-адрес АГЗУ'    "
       + "FROM GZUCFG GZU    "
       + "ORDER BY GZU.RAJON, GZU.MEST, GZU.GEO ASC    ";

        #endregion АГЗУ

        #region Все скважины

        #region Все скважины и адреса контроллеров
        string SQL_List_SKV_Kontrollers_All = @""
        + "SELECT  "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   "
        + "UNION ALL   "
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
         + "        WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ВРГ ' + dbo.GetCode(g.GEOVRG)) AS 'Объект',     "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "('') as 'Тип дополнительного контроллера',    "
        + "('') as 'Адрес дополнительного контроллера',    "
        + "('') as 'Адрес СКЖ',    "
        + "('') as 'Адрес СКЖ2',    "
        + "('') AS 'Адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Адрес КОС1 ингибитора'    "
        + "FROM NSKCFG g   ";

        #endregion Все скважины и адреса контроллеров

        #region Поиск адреса контроллера скважины

        static string SQL_List_SKV_Kontrollers_All_Search(string inputbox_search)
        {
          string SQLSelect = @""
        + "SELECT * "
        + "FROM "
        + "( "
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   "
        + "UNION ALL   "
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
         + "        WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ВРГ ' + dbo.GetCode(g.GEOVRG)) AS 'Объект',     "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "('') as 'Тип дополнительного контроллера',    "
        + "('') as 'Адрес дополнительного контроллера',    "
        + "('') as 'Адрес СКЖ',    "
        + "('') as 'Адрес СКЖ2',    "
        + "('') AS 'Адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Адрес КОС1 ингибитора'    "
        + "FROM NSKCFG g   "
        + ") AS TEMP_TABLE  "
        + "WHERE [TEMP_TABLE].[Адрес терминала] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес контроллера объекта] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес основного контроллера] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес дополнительного контроллера] = '" + inputbox_search + "'  OR [TEMP_TABLE].[Адрес СКЖ] = '" + inputbox_search + "'  OR[TEMP_TABLE].[Адрес СКЖ2] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес Rosemount(Endress Hauser)] = '" + inputbox_search + "' OR [TEMP_TABLE].[Логический адрес Rosemount(Endress Hauser)] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес КОС1 ингибитора] = '" + inputbox_search + "'";


            return SQLSelect;
        }

        #endregion Поиск адреса контроллера скважины

        #region Контроллеры offline

        string All_Kontrollers_SKV_Status = @""
        + "SELECT dbo.GetClass(I.[MEST]) as 'Месторождение',    "
        + "dbo.GetClass(I.[RAJON]) as 'Район',    "
        + "dbo.GetCode(I.[GEO]) as 'Скважина',    "
        + "CASE    "
        + "    WHEN S.IDEVENT=12 THEN 'Offline контроллера СУ'    "
        + "	WHEN S.IDEVENT=14 THEN 'Offline дополнительного контроллера'    "
        + "ELSE 'Неизвестная ошибка'    "
        + "END AS 'Тип неисправности',    "
        + "CONVERT(varchar, S.[OTDAY],120) as 'Дата Offline'    "
        + "FROM STALTEK S    "
        + "INNER JOIN INFOOBJ I    "
        + "   ON I.ID=S.ID    "
        + "WHERE S.OTDAY > dateadd(hour,-22, convert(datetime, convert(date, GETDATE(),109),2))    "
        + "AND S.OTDAY<dateadd(hour,2, convert(datetime, convert(date, GETDATE(),109),2))    "
        + "AND S.TYPOBJ=72 AND S.TYPEVENT= 2 AND S.IDEVENT IN (12,14) AND S.YESEVENT=1     "
        + "ORDER BY I.MEST, I.RAJON, I.GEO    ";

        #endregion Контроллеры offline

        #endregion Все скважины

        #region Добывающие скважины

        #region Все скважины

        string All_SKV_OIL = @""
        + "SELECT  "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   ";

        #endregion Все скважины

        #region Скважины с контроллерами
        string All_Kontrollers_SKV_OIL = @""
        + "SELECT * "
        + "FROM "
        + "( "
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   "
        + ") AS TEMP_TABLE  "
        + "WHERE [TEMP_TABLE].[Адрес основного контроллера] >= '0' OR [TEMP_TABLE].[Адрес дополнительного контроллера] >= '0'  OR [TEMP_TABLE].[Адрес СКЖ] >= '0'  OR[TEMP_TABLE].[Адрес СКЖ2] >= '0' OR [TEMP_TABLE].[Адрес Rosemount(Endress Hauser)] >= '0' OR [TEMP_TABLE].[Логический адрес Rosemount(Endress Hauser)] >= '0' OR [TEMP_TABLE].[Адрес КОС1 ингибитора] >= '0'";

        #endregion Скважины с контроллерами

        #region Скважины со СУ

        string All_Kontrollers_SKV_OIL_SU = @""
        + "SELECT  "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   "
        + "WHERE ADRCMK >= '0' OR ADRCMKDOP >= '0'";

        #endregion Скважины со СУ

        #region Прошивка СУ
        string All_Kontrollers_SKV_OIL_SU_Driver = @""
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',   "
        + "(dbo.GetCode(g.GEOKUST)) AS 'Куст',   "
        + "(dbo.GetCode(g.GEOGZU)) AS 'ГЗУ',   "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(dbo.GetClass(g.SPOS)) as 'Способ эксплуатации',   "
        + "(dbo.GetClass(g.TYPNAS)) as 'Тип насоса',   "
        + "(dbo.GetClass(g.TYPSU)) as 'Тип СУ',   "
        + "(dbo.GetClass(g.TYPTMS)) as 'ТипТМС',   "
        + "(g.NAMEOBJ) as 'Версия прошивки СУ',   "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип осн. контроллера',    "
        + "(g.ADRCMK) as 'Адрес осн. контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип доп. контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес доп. контроллера'   "
        + "FROM MSKCFG g   ";

        #endregion Прошивка СУ

        #region Скважины с беспроводными датчиками давления

        string All_Kontrollers_SKV_OIL_Rosemount_EndressHauser = @""
        + "SELECT  "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   "
        + "WHERE ADRCMKROSEMOUNT >= '0' OR LOGADRROSEMOUNT >= '0'";

        #endregion Скважины с беспроводными датчиками давления

        #region Скважины с СКЖ

        string All_Kontrollers_SKV_OIL_SKZH = @""
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
        + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
        + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
        + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
        + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
        + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
        + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
        + "FROM MSKCFG g   "
        + "WHERE ADRCMK_SKG >= '0' OR ADRCMK_SKG2 >= '0'";

        #endregion Скважины с СКЖ

        #region Скважины с ингибитором

        string All_Kontrollers_SKV_OIL_Ingibitor = @""
        + "SELECT    "
        + "(dbo.GetClass(ING.RAJON)) as 'Район',     "
        + "(dbo.GetClass(ING.MEST)) as 'Месторождение',     "
        + "(dbo.GetCode(ING.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)     "
        + "         FROM TERMCFG t    "
        + "         WHERE t.GEO = ING.GEOTERM) as 'Контроллер терминала',     "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)    "
        + "         FROM TERMCFG t    "
        + "         WHERE t.GEO = ING.GEOTERM) as 'Адрес терминала',     "
        + "(dbo.GetCode(ING.GEO)) AS 'Ингибитор',    "
        + "(dbo.GetClass(ING.TYPUSE)) AS 'Тип',    "
        + "(ING.IPADR) AS 'IP-aдрес',     "
        + "(dbo.GetClass(ING.TYPCONTR)) as 'Тип основного контроллера',     "
        + "(ING.ADRCMK) as 'Адрес основного контроллера'     "
        + "FROM INGBCFG ING INNER JOIN    "
        + "                      GZUCFG GC ON ING.IDPARENT = GC.ID AND ING.TYPUSE='UD0001' LEFT JOIN    "
        + "                      TERMCFG TC ON ING.GEOTERM = TC.GEO AND ING.MEST = TC.MEST    "
        + "UNION ALL    "
        + "SELECT    "
        + "(dbo.GetClass(ING.RAJON)) as 'Район',     "
        + "(dbo.GetClass(ING.MEST)) as 'Месторождение',     "
        + "(dbo.GetCode(ING.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)     "
        + "         FROM TERMCFG t    "
        + "         WHERE t.GEO = ING.GEOTERM) as 'Контроллер терминала',     "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)    "
        + "        FROM TERMCFG t    "
        + "         WHERE t.GEO = ING.GEOTERM) as 'Адрес терминала',     "
        + "(dbo.GetCode(ING.GEO)) AS 'Ингибитор',    "
        + "(dbo.GetClass(ING.TYPUSE)) AS 'Тип',    "
        + "(ING.IPADR) AS 'IP-aдрес',     "
        + "(dbo.GetClass(ING.TYPCONTR)) as 'Тип основного контроллера',     "
        + "(ING.ADRCMK) as 'Адрес основного контроллера'     "
        + "FROM INGBCFG ING INNER JOIN    "
        + "              MSKCFG MC ON ING.GEO=MC.GEO AND ING.RAJON=MC.RAJON AND MC.MEST=ING.MEST AND ING.TYPUSE='UD0002'    "
        + "                      LEFT JOIN    "
        + "                      GZUCFG GC ON MC.GEOGZU = GC.GEO AND MC.MEST = GC.MEST LEFT JOIN    "
        + "                      TERMCFG TC ON ING.GEOTERM = TC.GEO AND ING.MEST = TC.MEST    ";

        #endregion Скважины с ингибитором

        #region Поиск адреса контроллера скважины

        static string All_Kontrollers_SKV_OIL_Search(string inputbox_search)
        {
            string SQLSelect = @""
          + "SELECT * "
          + "FROM "
          + "( "
          + "SELECT   "
          + "(dbo.GetClass(g.RAJON)) as 'Район',    "
          + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
          + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
          + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
          + " FROM TERMCFG t   "
          + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
          + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
          + "         FROM TERMCFG t   "
          + "         WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
          + "('ГЗУ ' + dbo.GetCode(g.GEOGZU)) AS 'Объект',   "
          + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
          + "         FROM GZUCFG t   "
          + "         WHERE t.GEO = g.GEOGZU) as 'Контроллера объекта',     "
          + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
          + "         FROM GZUCFG t   "
          + "         WHERE t.GEO = g.GEOGZU) as 'Адрес контроллера объекта',    "
          + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
          + "(g.IPADR) AS 'IP-aдрес скважины',    "
          + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
          + "(g.ADRCMK) as 'Адрес основного контроллера',     "
          + "(dbo.GetClass(g.TYPCONTRDOP)) as 'Тип дополнительного контроллера',    "
          + "(g.ADRCMKDOP) as 'Адрес дополнительного контроллера',    "
          + "(g.ADRCMK_SKG) as 'Адрес СКЖ',    "
          + "(g.ADRCMK_SKG2) as 'Адрес СКЖ2',    "
          + "(g.ADRCMKROSEMOUNT) AS 'Адрес Rosemount(Endress Hauser)',    "
          + "(g.LOGADRROSEMOUNT) AS 'Логический адрес Rosemount(Endress Hauser)',    "
          + "(g.CHSOSTINGBKOC1) AS 'Адрес КОС1 ингибитора'    "
          + "FROM MSKCFG g   "
          + ") AS TEMP_TABLE  "
          + "WHERE [TEMP_TABLE].[Адрес терминала] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес контроллера объекта] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес основного контроллера] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес дополнительного контроллера] = '" + inputbox_search + "'  OR [TEMP_TABLE].[Адрес СКЖ] = '" + inputbox_search + "'  OR[TEMP_TABLE].[Адрес СКЖ2] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес Rosemount(Endress Hauser)] = '" + inputbox_search + "' OR [TEMP_TABLE].[Логический адрес Rosemount(Endress Hauser)] = '" + inputbox_search + "' OR [TEMP_TABLE].[Адрес КОС1 ингибитора] = '" + inputbox_search + "'";
            return SQLSelect;
        }
        #endregion Поиск адреса контроллера скважины

        #endregion Добывающие скважины

        #region Нагнетательные скважины

        #region Все скважины

        string All_SKV_WATER = @""
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
         + "        WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ВРГ ' + dbo.GetCode(g.GEOVRG)) AS 'Объект',     "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера'     "
        + "FROM NSKCFG g   ";

        #endregion Все скважины

        #region Все скважины с контроллерами

        string All_Kontrollers_SKV_WATER = @""
        + "SELECT * "
        + "FROM "
        + "( "
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
         + "        WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ВРГ ' + dbo.GetCode(g.GEOVRG)) AS 'Объект',     "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "('') as 'Тип дополнительного контроллера',    "
        + "('') as 'Адрес дополнительного контроллера',    "
        + "('') as 'Адрес СКЖ',    "
        + "('') as 'Адрес СКЖ2',    "
        + "('') AS 'Адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Адрес КОС1 ингибитора'    "
        + "FROM NSKCFG g   "
        + ") AS TEMP_TABLE  "
        + "WHERE [TEMP_TABLE].[Адрес основного контроллера] >= '0'";

        #endregion Все скважины с контроллерами

        #region Поиск адреса контроллера скважины

        static string All_Kontrollers_SKV_WATER_Search(string inputbox_search)
        {
            string SQLSelect = @""
        + "SELECT * "
        + "FROM "
        + "( "
        + "SELECT   "
        + "(dbo.GetClass(g.RAJON)) as 'Район',    "
        + "(dbo.GetClass(g.MEST)) as 'Месторождение',    "
        + "(dbo.GetCode(g.GEOTERM)) AS 'Терминал',    "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)   "
        + " FROM TERMCFG t   "
        + " WHERE t.GEO = g.GEOTERM) as 'Контроллер терминала',   "
        + "(SELECT TOP 1 UPPER(t.ADRCONTR)   "
        + "         FROM TERMCFG t   "
         + "        WHERE t.GEO = g.GEOTERM) as 'Адрес терминала',    "
        + "('ВРГ ' + dbo.GetCode(g.GEOVRG)) AS 'Объект',     "
        + "(SELECT TOP 1 dbo.GetClass(t.TYPCONTR)    "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Контроллера объекта',     "
        + "(SELECT TOP 1 UPPER(t.ADRCMK)   "
        + "         FROM GZUCFG t   "
        + "         WHERE t.GEO = g.GEOVRG) as 'Адрес контроллера объекта',    "
        + "(dbo.GetCode(g.GEO)) AS 'Номер скважины',    "
        + "(g.IPADR) AS 'IP-aдрес скважины',    "
        + "(dbo.GetClass(g.TYPCONTR)) as 'Тип основного контроллера',    "
        + "(g.ADRCMK) as 'Адрес основного контроллера',     "
        + "('') as 'Тип дополнительного контроллера',    "
        + "('') as 'Адрес дополнительного контроллера',    "
        + "('') as 'Адрес СКЖ',    "
        + "('') as 'Адрес СКЖ2',    "
        + "('') AS 'Адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Логический адрес Rosemount(Endress Hauser)',    "
        + "('') AS 'Адрес КОС1 ингибитора'    "
        + "FROM NSKCFG g   "
        + ") AS TEMP_TABLE  "
        + "WHERE [TEMP_TABLE].[Адрес основного контроллера] = '" + inputbox_search + "'";
            return SQLSelect;
        }

        #endregion Поиск адреса контроллера скважины


        #endregion Нагнетательные скважины

        #region ДНС

        #region Текущиее состояние насосов

        public string List_DNS_Nasos_Alarm = @"
SELECT * FROM (
SELECT        dbo.GetClass(I.RAJON) AS RAJON, dbo.GetClass(I.MEST) AS MEST, dbo.DNSCFG.NAMEOBJ AS DNSNAME, dbo.GetCode(dbo.DNSCFG.GEO) AS DNSGEO, dbo.NAGRCFG.NAMEOBJ AS NAGRNAME, dbo.GetCode(I.GEO) 
                         AS NAGRGEO, CONVERT(varchar, S.OTDAY, 120) AS OTDAY, CASE WHEN S.YESEVENT = 0 THEN 'ОСТАНОВ' WHEN S.YESEVENT = 1 THEN 'РАБОТА' ELSE 'Неизвестная ошибка' END AS TYPE, DATEDIFF(ss, S.OTDAY, 
                         GETDATE()) AS TIMESPAN
FROM            dbo.STALTEK AS S INNER JOIN
                         dbo.INFOOBJ AS I ON I.ID = S.ID INNER JOIN
                         dbo.DNSCFG ON I.IDPARENT = dbo.DNSCFG.ID INNER JOIN
                         dbo.NAGRCFG ON I.ID = dbo.NAGRCFG.ID
WHERE        (I.TYPOBJ = 97) AND (S.IDEVENT = 1) AND (S.TYPEVENT = 2)
) AS TEMTABLE
ORDER BY TEMTABLE.MEST, TEMTABLE.DNSGEO, TEMTABLE.NAGRGEO ASC
";

        #endregion Текущиее состояние насосов

        #endregion ДНС

        #endregion SQL запросы

        #region Работа с временем

        //Передаем и возвращаем текущее время
        public string date_time(DateTime date)
        {
            DateTime date_time = DateTime.Now;
            return date_time.ToString();
        }

        //Текущий день недели
        public static string DaysOfWeekPropis(DateTime date)
        {
            //Определяем переменную для количества значений в массиве
            int FCount;
            FCount = 7;
            string[] d = new string[FCount];
            //Заполняем массив
            d[0] = "Воскресенье";
            d[1] = "Понедельник";
            d[2] = "Вторник";
            d[3] = "Среда";
            d[4] = "Четверг";
            d[5] = "Пятница";
            d[6] = "Суббота";
            return d[Convert.ToUInt32(date.DayOfWeek)];
        }

        //Текущий месяц года
        public static string NameMonthPropis(DateTime date)
        {
            // Объявляем текстовый массив и перечисляем месяца
            string[] d = new string[]
            {
                "Января",
                "Февраля",
                "Марта",
                "Апреля",
                "Мая",
                "Июня",
                "Июля",
                "Августа",
                "Сентября",
                "Октября",
                "Ноября",
                "Декабря"
            };
            return Convert.ToString(date.Day) + " " +
            d[Convert.ToUInt32(date.Month - 1)] + " " +
            Convert.ToString(date.Year) + " года";
        }

        //Перевод секунд в дни, часы, минуты и секунды
        static public string ConvertToTime(double timeSeconds)
        {
            int mySeconds = System.Convert.ToInt32(timeSeconds);
            //86400 секунд в 1 дне
            int myDays = mySeconds / 86400;
            mySeconds %= 86400;
            //3600 секунд в 1 часе
            int myHours = mySeconds / 3600;
            mySeconds %= 3600;
            //60 секунд в 1 минуте
            int myMinutes = mySeconds / 60;
            mySeconds %= 60;


            string mySec = mySeconds.ToString(),
            myMin = myMinutes.ToString(),
            myHou = myHours.ToString(),
            myDay = myDays.ToString();

            if (myDays < 10) { myDay = myDay.Insert(0, ""); }
            if (myHours < 10) { myHou = myHou.Insert(0, "0"); }
            if (myMinutes < 10) { myMin = myMin.Insert(0, "0"); }
            if (mySeconds < 10) { mySec = mySec.Insert(0, "0"); }

            //Слова 
            string slovoDay = "";
            string slovoHour = "";
            string slovoMinute = "";
            string slovoSecond = "";
            string chisloDay = "";
            string chisloHour = "";
            string chisloMinute = "";
            string chisloSecond = "";

            if ((myDays <= 0) || (myDays == 00))
            {
                slovoDay = "";
                chisloDay = "";
            }
            else
            {
                slovoDay = "дн.";
                chisloDay = myDays.ToString();
            }

            if ((myHours <= 0))
            {
                slovoHour = "";
                chisloHour = "";
            }
            else
            {
                slovoHour = "час.";
                chisloHour = myHours.ToString();
            }

            if (myMinutes <= 0)
            {
                slovoMinute = "";
                chisloMinute = "";
            }
            else
            {
                slovoMinute = "мин.";
                chisloMinute = myMinutes.ToString();
            }

            if (mySeconds <= 0)
            {
                slovoSecond = "";
                chisloSecond = "";
            }
            else
            {
                slovoSecond = "сек.";
                chisloSecond = mySeconds.ToString();
            }
            string result = chisloDay + " " + slovoDay + " " + chisloHour + " " + slovoHour + " " + chisloMinute + " " + slovoMinute + " " + chisloSecond + " " + slovoSecond + "";
            return result.Trim();
        }




        #endregion Работа с временем

        #region Старый код

        //private void dgMonitor_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        //{
        //  object v = dgv_Data.Rows[e.RowIndex].Cells["Status"].Value;
        //  if (v != null && v.ToString() == "OFF")
        //  {
        //    foreach (DataGridViewCell i in dgv_Data.Rows[e.RowIndex].Cells)
        //    {
        //      i.Style.BackColor = Color.Orange;
        //    }
        //  }
        //}

        #endregion Старый код

        #region Выгрузка в Excel
        private void tol_ExportExcel_Click(object sender, EventArgs e)
        {
            DataToCSV();
        }

        private void DataToCSV()
        {
            try
            {
                // Exporting to csv is no big deal, we do it anyway
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Results.csv";

                StreamWriter sw = new StreamWriter(sfd.FileName, false);

                string fileRow = "";
                string cell = "";

                // lets get the dataColumn's titles first
                string titles = "";
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    titles += dt.Columns[x].ColumnName + ";";
                }
                sw.WriteLine(titles);

                // and then we go for the data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fileRow = "";
                    cell = "";

                    for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                    {
                        cell = dt.Rows[i][j].ToString();

                        if (cell == null)
                        {
                            cell = "";
                        }
                        //если данные содержат ; ,
                        //мы добавим к ячейке "", так что Excel
                        //поймет, что запятая не разделитель столбцов
                        if (cell.Contains(";"))
                        {
                            cell = "\"" + cell + "\"";
                        }
                        fileRow += cell + ";";
                    }
                    sw.WriteLine(fileRow);
                }
                sw.Close();

                //Открываем файл
                OpenFileCSVXML(sfd.FileName);

            }
            catch
            {

            }

        }
        #endregion Export CSV

        #region Export XML
        private void tol_ExportXML_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProcessWorkBackground.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    ProcessWorkBackground.RunWorkerAsync();
                }
            }
            catch 
            {

            }
        }

        #region Поток
        private void DataToXML_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;

                //Название листа в файле
                DataTable dtCustomer = new DataTable("Results");

                //заголовок
                string titles = "";
                string cell = "";

                //сумма
                int totalData = dgv_Data.RowCount;
                //процент


                //заголовок полей. Находим и записываем
                for (int i = 1; i < dgv_Data.Columns.Count + 1; i++)
                {
                    titles = dgv_Data.Columns[i - 1].HeaderText;
                    dtCustomer.Columns.Add(new DataColumn(titles));
                    //lst_Log.Items.Add(@"" + titles + "");
                }

                //Уличная магия. Пробегаемя по строками и столбцам.
                //P.S. Тут я ничего не понимаю. Код прохождения по полям писал Коля и Валентин. :)
                for (int i = 0; i < dgv_Data.Rows.Count - 1; i++)
                {
                    DataRow drItem = dtCustomer.NewRow();
                    for (int j = 0; j < dgv_Data.Columns.Count; j++)
                    {
                        titles = dgv_Data.Columns[j].HeaderText;

                        cell = dgv_Data.Rows[i].Cells[j].Value.ToString();
                        drItem[titles] = cell.ToString();
                    }

                    //запись в файл
                    dtCustomer.Rows.Add(drItem);

                    //передаём информацию в поток о состоянии i
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        //if (i== Convert.ToInt32 (totalData/100))  { System.Threading.Thread.Sleep(1); }
                        worker.ReportProgress(i);
                    }
                }

                //если true - то с заголовком полей, если false - то нет.
                ExcelCreator.Create(dtCustomer, @"Results.xml", true);
                OpenFileCSVXML("Results.xml");
            }
            catch
            {

            }
        }

        // This event handler updates the progress.
        private void DataToXML_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            sts_NumberRows.Text = ("Количество записей: " + e.ProgressPercentage.ToString() + "");
        }

        // This event handler deals with the results of the background operation.
        private void DataToXML_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                DateTime data_time = DateTime.Now;
            }
            else if (e.Error != null)
            {
            }
        }

        //открываем файлы, который создали
        private void OpenFileCSVXML(string FileName)
        {
            try
            {
                Process process_start = new Process();
                process_start.StartInfo.WorkingDirectory = @"%windir%\system32\";
                process_start.StartInfo.FileName = @"cmd.exe";
                process_start.StartInfo.Arguments = @"/c explorer.exe " + FileName + "";
                process_start.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process_start.Start();
            }
            catch
            {
            }
        }














        #endregion Поток

        #endregion Export XML

        #region Проверить обновление

        public void GetNewVersion()
        {
            if (System.IO.File.Exists(StartPath + "RegionAutoUpdater.exe"))
            {
                try
                {
                    //Запускаем файл с обновлением;
                    Process process_start = new Process();
                    process_start.StartInfo.WorkingDirectory = StartPath;
                    process_start.StartInfo.FileName = "RegionAutoUpdater.exe";
                    process_start.StartInfo.Arguments = "RegionPlus.exe";
                    process_start.Start();
                    //Убиваем приложение
                    Application.Exit();
                    Process.GetCurrentProcess().Kill();
                }
                catch
                {

                    ////Файл обновления не найден                      
                    MessageBox.Show("Файл RegionAutoUpdater.exe не найден!\r\nАвтоматическое обновление не будет происходить!\r\nПриложение не будет запущено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Убиваем приложение и не запустим... ибо нехер работать с необновляемым ПО :)
                    Application.Exit();
                    Process.GetCurrentProcess().Kill();
                }

            }
            else
            {
                //Файл обновления не найден                      
                MessageBox.Show("Файл RegionAutoUpdater.exe не найден!\r\nАвтоматическое обновление не будет происходить!\r\nПриложение не будет запущено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Убиваем приложение и не запустим... ибо нехер работать с необновляемым ПО :)
                Application.Exit();
                Process.GetCurrentProcess().Kill();
            }
        }

        #endregion Проверить обновление

        #region Работа с директориями

        //начальная директория, откуда стартовать
        string StartPath = GetDirectoryFromPath(Environment.GetCommandLineArgs()[0]);

        //определение директории, откуда будем работать с файлами
        public static string GetDirectoryFromPath(string path)
        {
            string trimmed = path.TrimEnd('\\');
            int index = trimmed.LastIndexOf('\\');
            if (index < 0)
            {
                return ""; // No directory, just a file name
            }
            return trimmed.Substring(0, index) + @"\";
        }

        #endregion Работа с директориями

        #region Группы

        //Конвертирование код группы из числового в текстовое значение и отображение
        public void StatusRole(int usergroup)
        {
            if (usergroup == 0)
            {
                //Гость
                //Конфигурация
                tol_Config.Visible = false;
                //Администирование
                tol_Admin.Visible = false;
            }

            if (usergroup == 1)
            {
                //Администратор
                //Градуировочные таблицы
                tol_Admin.Visible = true;
                //Конфигурация
                tol_Config.Visible = true;
            }

            if (usergroup == 2)
            {
                //КИПиА
                //Градуировочные таблицы
                tol_Admin.Visible = true;
                //Конфигурация
                tol_Config.Visible = true;
            }
        }

        #endregion Группы

        #region Функции SQL

        //Таблица, где находятся временные значения, которые мы выдернули
        DataTable dt_TempTable = new DataTable();

        public void SelectDataTable(string SQLText)
        {
            SqlDataAdapter daHosts = new SqlDataAdapter(SQLText, RegionConnect);
            dt_TempTable.Clear();
            daHosts.Fill(dt_TempTable);
        }

        public string GetCode(int number)
        {
            string code = string.Empty;
            string SQLText = @""
            + "DECLARE @NUMBER INT  "
            + "SET @NUMBER = " + number + ""
            + "SELECT  "
            + "    CODE=(CASE WHEN LEN(@NUMBER)>2   "
            + "    THEN  "
            + "    LEFT(@NUMBER,LEN(@NUMBER)-2)+ISNULL((SELECT TOP 1 LETTER FROM SKVCOD WHERE COD=RIGHT(@NUMBER,2)),'')  "
            + "    ELSE '0'  "
            + "END) ";

            SelectDataTable(SQLText);

            //Проанализируем список хостов, который будем обрабатывать
            foreach (DataRow dr in dt_TempTable.Rows)
            {
                code = dr["CODE"].ToString().Trim();
                return code;
            }

            return null;
        }

        public string GetClass(string code)
        {
            string code_name = string.Empty;
            string SQLText = @""
            + "DECLARE @CODE VARCHAR(30)  "
            + "SET @CODE = '" + code + "'  "
            + "SELECT CODE_NAME = NE_1 FROM CLASS WHERE CD_1 = @CODE    ";

            SelectDataTable(SQLText);

            //Проанализируем список хостов, который будем обрабатывать
            foreach (DataRow dr in dt_TempTable.Rows)
            {
                code = dr["CODE_NAME"].ToString().Trim();
                return code;
            }

            return null;
        }


        #endregion Функции SQL

        #region 

        private void frm_MainForm_Load(object sender, EventArgs e)
        {

        }

        private void tol_Transfer_Trends_Events_SKV_Click(object sender, EventArgs e)
        {
            frm_Transfer_Trends_Events_SKV Transfer_Trends_Events_SKV = new frm_Transfer_Trends_Events_SKV();
            Transfer_Trends_Events_SKV.RegionConnect = RegionConnect;
            Transfer_Trends_Events_SKV.ShowDialog();
        }



        private void tol_Function_DB_Click(object sender, EventArgs e)
        {
            Function_DB();
        }



        private void Function_DB()
        {
            String str;
            str = @"
             
                IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[GetCode]') AND XTYPE IN (N'FN', N'IF', N'TF'))
                DROP FUNCTION [dbo].[GetCode]

                SET ANSI_NULLS ON
                GO

                SET QUOTED_IDENTIFIER ON
                GO

                CREATE FUNCTION [dbo].[GetCode](@number int) 
                RETURNS varchar(max)
                AS
                BEGIN
	                DECLARE @code varchar(10)
                SELECT 
                @code=(CASE WHEN LEN(@number)>2 THEN
	                 LEFT(@number,LEN(@number)-2)+ISNULL((SELECT TOP 1 LETTER FROM SKVCOD WHERE COD=RIGHT(@number,2)),'')
	                else '0'
                END)
	                RETURN @code 
                END
                GO

                IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[GetRajon]') AND XTYPE IN (N'FN', N'IF', N'TF'))
                DROP FUNCTION [dbo].[GetRajon]

                SET ANSI_NULLS ON
                GO

                SET QUOTED_IDENTIFIER ON
                GO

                CREATE FUNCTION [dbo].[GetRajon](@RAJON varchar(20)) 
                RETURNS varchar(30)
                AS
                BEGIN
	                DECLARE @temp varchar(30)	
	                SELECT @temp=NE_1 FROM CLASS WHERE CD_1=@RAJON
	                RETURN @temp
                END

                GO   ";


            try
            {
                if (RegionConnect.State == System.Data.ConnectionState.Open)
                {
                    m_sqlCmd.CommandText = str;
                    m_sqlCmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frm_TrendPing TrendPing = new frm_TrendPing();
            //TrendPing.RegionConnect = RegionConnect;
            //TrendPing.ShowDialog();
        }


        #endregion

        int i_timer;
        void Timer()
        {
            i_timer = 300;
            lbl_Timer.Text = i_timer.ToString();
            tmr_Refresh.Interval = 1000;
            tmr_Refresh.Enabled = true;
            tmr_Refresh.Start();
            //Очищаем таблицу
            try
            {
                dgv_Data.Rows.Clear();
                dgv_Data.DataSource = null;
            }
            catch
            { }
            SPISOK_NASOSOV_TEK_ALARMS();
            COLOR_ALARM();
        }

        private void tmr_Refresh_Tick(object sender, EventArgs e)
        {
            lbl_Timer.Text = (ConvertToTime(--i_timer)).ToString();
            if (i_timer < 0)
            {
                tmr_Refresh.Stop();
                Timer();
            }
        }
    }
}