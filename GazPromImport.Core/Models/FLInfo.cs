using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace GazPromImport.Core.Models
{
    /// <summary>
    /// Информация по ФЛ
    /// </summary>
    public class FLInfo
    {
        /// <summary>
        /// Номер счёта
        /// </summary>
        [XmlAttribute("ChetNumber")]
        public int ChetNumber { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        [XmlAttribute("Region")]
        public string Region { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [XmlAttribute("City")]
        public string City { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [XmlAttribute("Street")]
        public string Street { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [XmlAttribute("House")]
        public string House { get; set; }

        /// <summary>
        /// Дополнительный дом
        /// </summary>
        [XmlAttribute("House2")]
        public string House2 { get; set; }

        /// <summary>
        /// Префикс дома
        /// </summary>
        [XmlAttribute("HouseCase")]
        public string HouseCase { get; set; }

        /// <summary>
        /// Апартамент
        /// </summary>
        [XmlAttribute("Apartment")]
        public string Apartment { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        [XmlAttribute("Room")]
        public string Room { get; set; }

        /// <summary>
        /// Наличие прибора
        /// </summary>
        [XmlAttribute("Pribor")]
        public bool IsPribor { get; set; }

        //private string _counter1;

        /// <summary>
        /// Счётчик
        /// </summary>
        [XmlAttribute("Counter1")]
        public string Counter1 { get; set; }
        //public string Counter1
        //{
        //    get
        //    {
        //        return _counter1;
        //    }
        //    set
        //    {
        //        _counter1 = GetOnlyNumbers(value);
        //    }
        //}

        /// <summary>
        /// Инфа по счётчику1
        /// </summary>
        [XmlAttribute("Counter1Info")]
        public string Counter1Info { get; set; }

        /// <summary>
        /// Счётчик
        /// </summary>
        [XmlAttribute("Counter2")]
        public string Counter2 { get; set; }

        /// <summary>
        /// Инфа по счётчику2
        /// </summary>
        [XmlAttribute("Counter2Info")]
        public string Counter2Info { get; set; }

        /// <summary>
        /// Счётчик
        /// </summary>
        [XmlAttribute("Counter3")]
        public string Counter3 { get; set; }

        /// <summary>
        /// Инфа по счётчику3
        /// </summary>
        [XmlAttribute("Counter3Info")]
        public string Counter3Info { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Сальдо
        /// </summary>
        [XmlAttribute("Saldo")]
        public string Saldo { get; set; }

        /// <summary>
        /// Заменяет символ № в неправильной кодировке
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetOnlyNumbers(string value)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(value);
            return match.Value;
        }
    }
}
