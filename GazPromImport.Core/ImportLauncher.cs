using GazPromImport.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LinqToDB.Data;
using System;
using GazPromImport.Core.Services;

namespace GazPromImport.Core
{
    /// <summary>
    /// Импорт ФЛ
    /// </summary>
    public class ImportLauncher
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        private readonly string connection = "ConnectionString";

        /// <summary>
        /// Запускает импорт
        /// </summary>
        /// <returns></returns>
        public int Start(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(FLInfoList));
                var arrayOfInfo = (FLInfoList)serializer.Deserialize(fileStream);

                List<SS_FlInfo> list = new List<SS_FlInfo>();
                ServiceLogger.Info("{info}", "начало мапинга");
                foreach (var item in arrayOfInfo.FLInfos)
                {
                    list.Add(CustomMapper(item));
                }
                ServiceLogger.Info("{info}", "конец мапинга");
                using (var db = new dbModel(connection))
                {
                    db.CommandTimeout = 1200000;
                    using (var tr = db.BeginTransaction())
                    {
                        try
                        {
                            ServiceLogger.Info("{info}", "таблица SS_FlInfo_Dupl перезапись начало");
                            db.AbonentInfoDelete();
                            ServiceLogger.Info("{info}", "таблица SS_FlInfo_Dupl перезапись конец");
                            ServiceLogger.Info("{info}", "таблица SS_FlInfo очищена");

                            ServiceLogger.Info("{info}", "таблица SS_FlInfo запись начало");
                            InsertBulk(list, db);
                            ServiceLogger.Info("{info}", "таблица SS_FlInfo запись окончание");

                            ServiceLogger.Info("{info}", "таблица SS_FlInfo обновление начало");
                            db.AbonentInfoUpdate();
                            ServiceLogger.Info("{info}", "таблица SS_FlInfo обновление конец");

                            tr.Commit();
                            ServiceLogger.Info("{info}", "импорт завершён");

                            return list.Count;
                        }
                        catch (Exception e)
                        {
                            ServiceLogger.Error("{error}", e.ToString());
                            return 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вставляет список записей в таблицу
        /// </summary>
        /// <returns></returns>
        private void InsertBulk(IEnumerable<SS_FlInfo> list, dbModel db)
        {
            try
            {
                db.BulkCopy(list);
            }
            catch (Exception e)
            {
                ServiceLogger.Error("{error}", e.ToString());
            }
        }

        /// <summary>
        /// Самодельный мапер
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private SS_FlInfo CustomMapper(FLInfo item)
        {
            Int32.TryParse(item.House, out int house);
            Int32.TryParse(item.Apartment, out int apartment);
            Int32.TryParse(item.House2, out int house2);
            Int32.TryParse(item.Counter1Info, out int counter1Info);
            Int32.TryParse(item.Counter2Info, out int counter2Info);
            Int32.TryParse(item.Counter3Info, out int counter3Info);
            
            return new SS_FlInfo
            {
                C_Guid = Guid.NewGuid(),
                N_ChetNumber = item.ChetNumber,
                C_Region = item.Region,
                C_City = item.City,
                C_Street = item.Street,
                C_House = item.House,
                N_House = GetNullableInteger(house),
                C_House2 = item.House2,
                N_House2 = GetNullableInteger(house2),
                C_HouseCase = item.HouseCase,
                C_Apartment = item.Apartment,
                N_Apartment = GetNullableInteger(apartment),
                C_Room = item.Room,
                B_Pribor = item.IsPribor,
                C_Counter1 = item.Counter1,
                N_Counter1_Info = GetNullableInteger(counter1Info),
                N_Counter2_Info = GetNullableInteger(counter2Info),
                N_Counter3_Info = GetNullableInteger(counter3Info),
                C_Saldo = item.Saldo
            };
        }

        /// <summary>
        /// Возвращает либо 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int? GetNullableInteger(int value)
        {
            int? result = null;
            if (value != 0 && value != -1)
            {
                result = value;
            }

            return result;
        }
    }
}
