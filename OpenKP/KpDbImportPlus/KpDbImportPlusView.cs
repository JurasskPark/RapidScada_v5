/*
 * Copyright 2020 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : KpDbImportPlus
 * Summary  : Device library user interface
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2018
 * Modified : 2022
 */

using Scada.Comm.Devices.DbImportPlus.Configuration;
using Scada.Comm.Devices.DbImportPlus.UI;
using Scada.Data.Configuration;
using System.IO;

namespace Scada.Comm.Devices
{
    /// <summary>
    /// Device library user interface.
    /// <para>Пользовательский интерфейс библиотеки КП.</para>
    /// </summary>
    public class KpDbImportPlusView : KPView
    {
        /// <summary>
        /// The driver version.
        /// </summary>
        internal const string KpVersion = "5.1.1.0";


        /// <summary>
        /// Initializes a new instance of the class. Designed for general configuring.
        /// </summary>
        public KpDbImportPlusView()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class. Designed for configuring a particular device.
        /// </summary>
        public KpDbImportPlusView(int number)
            : base(number)
        {
            CanShowProps = number > 0;
        }


        /// <summary>
        /// Gets the driver description.
        /// </summary>
        public override string KPDescr
        {
            get
            {
                return Localization.UseRussian ?
                    "Автор:  Михаил Ширяев\n" +
                    "Доработка: Юрий Прадиус\n" +
                    "Импорт из сторонней базы данных.\n\n" +
                    "Версия 5.1.1.0 (14.08.2022)\n" +
                    "Добавлено:\n" +
                    "[+] Подключение через ODBC.\n" +
                    "[+] Подключение к базе данных Firebird.\n" +
                    "[+] Проверка подключения к базе данных.\n" +
                    "[+] Проверка SQL-запроса.\n" +
                    "[+] Второй способ получения данных, когда тег является значением в строке.\n" +
                    "[v] Строка подключения в конфигурационном файле теперь шифруется.\n"
                    :

                    "The author: Mikhail Shiryaev\n" +
                    "Revision: Yuri Pradius\n" +
                    "Import from a third-party database.\n\n" +
                    "Version 5.1.0.0 (11.08.2022)\n" +
                    "Added:\n" +
                    "[+] Connection via ODBC.\n" +
                    "[+] Connecting to the Firebird database.\n" +
                    "[+] Checking the connection to the database.\n" +
                    "[+] Checking the SQL query.\n" +
                    "[+] The second way to get data is when the tag is a value in a string.\n" +
                    "[v] The connection string in the configuration file is now encrypted.\n";
            }
        }

        /// <summary>
        /// Gets the driver version.
        /// </summary>
        public override string Version
        {
            get
            {
                return KpVersion;
            }
        }

        /// <summary>
        /// Gets the default request parameters.
        /// </summary>
        public override KPReqParams DefaultReqParams
        {
            get
            {
                return new KPReqParams(0, 500);
            }
        }

        /// <summary>
        /// Gets the default channel prototypes.
        /// </summary>
        public override KPCnlPrototypes DefaultCnls
        {
            get
            {
                // load configuration
                KpConfig config = new KpConfig();
                string fileName = KpConfig.GetFileName(AppDirs.ConfigDir, Number);

                if (!File.Exists(fileName))
                    return null;
                else if (!config.Load(fileName, out string errMsg))
                    throw new ScadaException(errMsg);

                // create channel prototypes
                KPCnlPrototypes prototypes = new KPCnlPrototypes();
                string[] tagNames = KpDbImportPlusLogic.GetTagNames(config);
                int signal = 1;

                foreach (string tagName in tagNames)
                {
                    prototypes.InCnls.Add(new InCnlPrototype(tagName, BaseValues.CnlTypes.TI) { Signal = signal++ });
                }

                return prototypes;
            }
        }


        /// <summary>
        /// Shows the driver properties.
        /// </summary>
        public override void ShowProps()
        {
            new FrmConfig(AppDirs, Number).ShowDialog();
        }
    }
}
