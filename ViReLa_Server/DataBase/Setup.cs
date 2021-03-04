using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Data.Common;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.Entity.Validation;
using System.Configuration;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using Sgml;
using System.Net;
using System.Text.RegularExpressions;


namespace ViReLa
{
    public class DbContextViReLa : DbContext
    {
        [TagName("Информация"), RussianName("Пользователи")]
        public DbSet<User> User { get; set; }
        [RussianName("Специальности")]
        public DbSet<Specialty> Specialty { get; set; }

        [TagName("Группа"), RussianName("Группы")]
        public DbSet<Group> Group { get; set; }
        [RussianName("Роли")]
        public DbSet<Role> Role { get; set; }
        [RussianName("Виды процессов")]
        public DbSet<ViewProcess> ViewProcess { get; set; }
        [RussianName("Предметы")]
        public DbSet<SubjectArea> SubjectArea { get; set; }
        [RussianName("Приложения")]
        public DbSet<PathToAppForProcess> PathToAppForProcess { get; set; }
        [RussianName("Типы процессов")]
        public DbSet<TypeProcess> TypeProcess { get; set; }

        [TagName("Лабораторные работы"), RussianName("Список процессов")]
        public DbSet<ListProcess> ListProcess { get; set; }
        [RussianName("Пароли")]
        public DbSet<Password> Password { get; set; }
        [RussianName("Схемы")]
        public DbSet<SchemeDR> SchemeDR { get; set; }

        /// <summary>
        /// Конструктор контекста
        /// </summary>
        public DbContextViReLa(): base("DbConnection")
        {
            //Обновление description в database
            Database.SetInitializer<DbContextViReLa>(new MyInitializer());
        }

        /// <summary>
        /// Добавляем описание к модели данных в SQL Server
        /// </summary>
        public class MyInitializer : CreateDatabaseIfNotExists<DbContextViReLa>
        {
            protected override void Seed(DbContextViReLa context)
            {
                DbDescriptionUpdater<DbContextViReLa> updater = new DbDescriptionUpdater<DbContextViReLa>(context);
                updater.UpdateDatabaseDescriptions();
            }
        }

        /// <summary>
        /// Описание схемы базы данных
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("VARCHAR"));

            modelBuilder.Entity<Group>()
                        .HasRequired(x => x.specialty)
                        .WithMany()
                        .HasForeignKey(y => y.specialty_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                        .HasRequired(x => x.role)
                        .WithMany()
                        .HasForeignKey(y => y.role_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                        .HasRequired(x => x.group)
                        .WithMany()
                        .HasForeignKey(y => y.group_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                        .HasRequired(x => x.password)
                        .WithMany()
                        .HasForeignKey(y => y.password_id)
                        .WillCascadeOnDelete(true);


            modelBuilder.Entity<ListProcess>()
                        .HasRequired(x => x.initiator)
                        .WithMany()
                        .HasForeignKey(y => y.initiator_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<ListProcess>()
                        .HasRequired(x => x.typeProcess)
                        .WithMany()
                        .HasForeignKey(y => y.typeProcess_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<TypeProcess>()
                        .HasRequired(x => x.pathToAppForProcess)
                        .WithMany()
                        .HasForeignKey(y => y.pathToAppForProcess_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<TypeProcess>()
                        .HasRequired(x => x.subjectArea)
                        .WithMany()
                        .HasForeignKey(y => y.subjectArea_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<TypeProcess>()
                        .HasRequired(x => x.viewProcess)
                        .WithMany()
                        .HasForeignKey(y => y.viewProcess_id)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<PathToAppForProcess>()
                        .HasRequired(x => x.scheme)
                        .WithMany()
                        .HasForeignKey(y => y.scheme_id)
                        .WillCascadeOnDelete(true);
        }

        /// <summary>
        /// Парсинг файла FillStandart.xml для создание первичных данных в базе данных
        /// </summary>
        public void FillStandart()
        {
            string alltext = File.ReadAllText("./FillStandart.xml"); 
            var headTables = XElement.Parse(alltext).XPathSelectElement("//standart");
            foreach (var propertie in this.GetType().GetProperties().Where(x=>x.PropertyType.Name == "DbSet`1"))
            {
                var xmlElements = headTables.XPathSelectElements("./"+propertie.Name+"/element");
                foreach (var xmlElement in xmlElements)
                {
                    var newElement = Activator.CreateInstance(propertie.PropertyType.GenericTypeArguments.ToArray().FirstOrDefault());
                    var element = xmlElement.Elements();
                    foreach (var item in element)
                    {
                        switch (newElement.GetType().GetProperty(item.Name.ToString()).PropertyType.FullName)
                        {
                            case "System.String":
                                var val = item.Elements().Count() == 0 ? item.Value.Trim(): String.Join("", item.Elements());
                                var currentValue = (string)newElement.GetType().GetProperty(item.Name.ToString()).GetValue(newElement) + " " + val;
                                newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, currentValue);
                                break;
                            case "System.Int32":
                                newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, int.Parse(item.Value));
                                break;
                            case "System.Int64":
                                if (String.IsNullOrWhiteSpace(item.Value))
                                    newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, null);
                                else
                                    newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, long.Parse(item.Value));
                                break;
                            case "System.DateTime":
                                newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, DateTime.Parse(item.Value));
                                break;
                            case "System.Nullable`1[System.DateTime]":
                                if (String.IsNullOrWhiteSpace(item.Value))
                                    newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, null);
                                else
                                    newElement.GetType().GetProperty(item.Name.ToString()).SetValue(newElement, DateTime.Parse(item.Value));
                                break;
                            default:
                                throw new ArgumentException("Не известный тип в файле для стандартной загрузки");
                        }
                    }
                    object[] arrayObj = new object[1];
                    arrayObj[0] = newElement;
                    this.GetType().GetProperty(propertie.Name).GetValue(this).GetType().GetMethod("Add").Invoke(this.GetType().GetProperty(propertie.Name).GetValue(this), arrayObj);
                }
            }
        }
    }
}
