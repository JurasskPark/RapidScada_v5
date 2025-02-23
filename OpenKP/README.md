	Драйвера для Rapid SCADA.
	Drivers  for Rapid SCADA.

![KpDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v5/v5.1.1.1/total)
![KpDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v5/KpDbImportPlus_v5.1.1.0/total)

Библиотека KpDbImportPlus была доработана и добавлен новый функционал:
- В конфигурационном файле теперь шифриуется не только пароль, но и строка подключения.
- Добавлен сбор данных через драйвера ODBC.
- Добавлен сбор данных из СУБД Firebird.
- Проверка подключения из формы драйвера.
- Проверка SQL-запроса из формы драйвера.
- Подсветка синтаксиса SQL.
- Реализован новый режим сбора данных, когда в первом столбце - название тега, во втором - значение тега, в третьем - время значения. (Работает историческая запись данных через срез.)

The KpDbImportPlus library has been improved and new functionality has been added:
- The configuration file now encrypts not only the password, but also the connection string.
- Added data collection via ODBC drivers.
- Added data collection from the Firebird DBMS.
- Checking the connection from the driver form.
- Checking the SQL query from the driver form.
- SQL syntax highlighting.
- A new data collection mode has been implemented, when the first column contains the tag name, the second column contains the tag value, and the third column contains the time of the value. (Historical data recording via slice works.)


## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
