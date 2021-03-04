using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;


namespace ViReLa
{
    /// <summary>
    /// Аттрибуты для установки тегов, которые используются на html странице
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class TagNameAttribute : Attribute
    {
        
        public TagNameAttribute(string tag0)
        {
            tagName = tag0;
        }
        public string tagName { get; set; }
    }

    /// <summary>
    /// Аттрибуты для установки тегов, которые используются на html странице
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class RussianNameAttribute : Attribute
    {

        public RussianNameAttribute(string _russianName)
        {
            russianName = _russianName;
        }
        public string russianName { get; set; }
    }

    /// <summary>
    /// Интерфейс, для ограничения работы с обобщениями
    /// </summary>
    public interface id
    {
        int id { get; set; }
    }

    /// <summary>
    /// Класс для описания роли пользователя
    /// </summary>
    [Description("Роль для пользователя")]
    public class Role : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Наименование роли")]
        [TagName("name")]
        [MaxLength(200)]
        public string name { get; set; }

    }

    /// <summary>
    /// Класс для описание специальности
    /// </summary>
    [Description("Специальность")]
    public class Specialty : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Наименование специальности")]
        [TagName("name")]
        [MaxLength(200)]
        public string name { get; set; }

        [Description("Дополнительная информация о специальности")]
        [TagName("info")]
        public string info { get; set; }
    }

    /// <summary>
    /// Класс для описание группы пользователя
    /// </summary>
    [Description("Группа студента")]
    public class Group : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Наименование группы")]
        [TagName("name")]
        [MaxLength(200)]
        public string name { get; set; }

        [Description("Идентификатор специальности")]
        [TagName("specialty_id")]
        public int specialty_id { get; set; }

        [Description("Специальность")]
        [TagName("specialty")]
        public virtual Specialty specialty { get; set; }
    }

    /// <summary>
    /// Класс для описания пользователя
    /// </summary>
    [Description("Пользователь")]
    public class User : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Имя, для обращения к пользователю")]
        [TagName("shortname")]
        [MaxLength(100)]
        public string nameS { get; set; }

        [Description("ФИО пользователя")]
        [TagName("fullname")]
        [MaxLength(500)]
        public string nameF { get; set; }

        [Description("Идентификатор пароля")]
        [TagName("password_id")]
        public int password_id { get; set; }

        [Description("Пароль")]
        [TagName("password")]
        public virtual Password password { get; set; }

        [Description("Идентификатор роли")]
        [TagName("role_id")]
        public int role_id { get; set; }

        [Description("Роль")]
        [TagName("role")]
        public virtual Role role { get; set; }

        [Description("Идентификатор группы")]
        [TagName("gole_id")]
        public int group_id { get; set; }

        [Description("Группа")]
        [TagName("group")]
        public virtual Group group { get; set; }

    }


    /// <summary>
    /// Класс для описания вида процесса 
    /// </summary>
    [Description("Вид процесса")]
    public class ViewProcess : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Наименование вида процесса")]
        [TagName("name")]
        [MaxLength(500)]
        public string name { get; set; }

        [Description("Дополнительная информация о виде процесса")]
        [TagName("info")]
        [MaxLength(5000)]
        public string info { get; set; }

    }

    /// <summary>
    /// Класс для описания типа предметной области
    /// </summary>
    [Description("Предметная область")]
    public class SubjectArea : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Наименование предметной области")]
        [TagName("name")]
        [MaxLength(500)]
        public string name { get; set; }

    }

    /// <summary>
    /// Класс для описания пути до приложения, которое выполняет процесс
    /// </summary>
    [Description("Путь до процесса")]
    public class PathToAppForProcess : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Путь до файла")]
        [TagName("path")]
        [MaxLength(255)]
        public string path { get; set; }

        [Description("Версия приложения")]
        [TagName("version")]
        [MaxLength(100)]
        public string version { get; set; }

        [Description("Описание приложения")]
        [TagName("info")]
        [MaxLength(5000)]
        public string info { get; set; }

        [Description("ИД передаваемых данных")]
        public int scheme_id { get; set; }

        [Description("Передаваемые данные")]
        public virtual SchemeDR scheme { get; set; }

    }

    /// <summary>
    /// Класс для описания передаваемых данных
    /// </summary>
    [Description("Данные и результаты")]
    public class SchemeDR : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Шаблон данных, которые необходимо заполнить в формате xml")]
        public string dataScheme { get; set; }

        [Description("Шаблон результатов процесса в формате xml")]
        public string resultScheme { get; set; }
    }


    /// <summary>
    /// Класс для описания типа процесса
    /// </summary>
    [Description("Типы процессов")]
    public class TypeProcess : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Наименование типа процесса")]
        [TagName("name")]
        [MaxLength(500)]
        public string name { get; set; }

        [Description("ИД пути до приложения")]
        public int pathToAppForProcess_id { get; set; }

        [Description("Путь до приложения")]
        public virtual PathToAppForProcess pathToAppForProcess { get; set; }

        [Description("ИД предметной области")]
        public int subjectArea_id { get; set; }

        [Description("Предметная область")]
        public virtual SubjectArea subjectArea { get; set; }

        [Description("ИД вида процесса")]
        public int viewProcess_id { get; set; }

        [Description("Вид процесса")]
        public virtual ViewProcess viewProcess { get; set; }

    }

    /// <summary>
    /// Класс для описания списка процессов
    /// </summary>
    [Description("Список процессов")]
    public class ListProcess : id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("ИД пользователь")]
        public int initiator_id { get; set; }

        [Description("Пользователь")]
        public virtual User initiator { get; set; }

        [Description("Данные для обработки в формате xml")]
        public string data { get; set; }

        [Description("Результат выполнения в формате xml")]
        public string result { get; set; }

        [Description("ИД типа процесса")]
        public int typeProcess_id { get; set; }

        [Description("Тип процесса")]
        public virtual TypeProcess typeProcess { get; set; }

    }

    /// <summary>
    /// Класс для хранения паролей
    /// </summary>
    [Description("Парол")]
    public class Password:id
    {
        [Description("Первичный ключ")]
        [TagName("pk")]
        [Key]
        public int id { get; set; }

        [Description("Пароль")]
        [TagName("pass")]
        [MaxLength(25)]
        public string password { get; set; }

    }
}
