using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    //public class Data
    //{
    //    public List<ValueType> ValueTypesList = new List<ValueType>()
    //    {
    //        new ValueType("int"),
    //        new ValueType("string"),
    //        new ValueType("float"),
    //        new ValueType("bool")
    //    };

    //    public List<DeviceType> DeviceTypesList = new List<DeviceType>()
    //    {
    //        new DeviceType("Ноутбук")
    //    };

    //    public List<CharacteristicType> CharacteristicTypesList = new List<CharacteristicType>()
    //    {
    //        new CharacteristicType(1,"Веб-камера",4),
    //        new CharacteristicType(1,"Видеокарта",2),
    //        new CharacteristicType(1,"Время работы",1),
    //        new CharacteristicType(1,"Диагональ экрана",3),
    //        new CharacteristicType(1,"Накопитель",1),
    //        new CharacteristicType(1,"Оперативная память",1),
    //        new CharacteristicType(1,"Процессор",2)
    //    };

    //    public List<Device> DevicesList = new List<Device>()
    //    {
    //        new Device(1,"Samsung NC10"),
    //        new Device(1,"ASUS K50IN")
    //    };

    //    public List<Characteristic> CharacteristicsList = new List<Characteristic>()
    //    {
    //        //1 Samsung NC10
    //        new Characteristic(1,1,"true"),
    //        new Characteristic(1,2,"Intel GMA 952 (встроенная)"),
    //        new Characteristic(1,3,"6"),
    //        new Characteristic(1,4,"10,20"),
    //        new Characteristic(1,5,"80"),
    //        new Characteristic(1,6,"1"),
    //        new Characteristic(1,7,"Intel 945GSE"),

    //        //2 ASUS K50IN
    //        new Characteristic(2,1,"true"),
    //        new Characteristic(2,2,"NVIDIA GeForce G 102M (дискретная)"),
    //        new Characteristic(2,3,"6"),
    //        new Characteristic(2,4,"15,6"),
    //        new Characteristic(2,5,"250"),
    //        new Characteristic(2,6,"2")
    //    };
    //}

    class ComputerStoreContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Таблица ValueType
            modelBuilder.Entity<ValueType>().Property(p => p.Name)
                                            .IsRequired(true);
            modelBuilder.Entity<ValueType>().HasKey(k => k.ID)
                                            .HasName("PrimaryKey_ValueTypeID");


            //Таблица CharacteristicType
            modelBuilder.Entity<CharacteristicType>().Property(p => p.Name)
                                                     .IsRequired(true);
            modelBuilder.Entity<CharacteristicType>().HasKey(k => k.ID)
                                                     .HasName("PrimaryKey_CharacteristicTypeID");
            modelBuilder.Entity<CharacteristicType>().HasOne(o => o.ValueType)
                                                     .WithMany(m => m.CharacteristicTypes)
                                                     .HasForeignKey(fk => fk.ValueTypeID)
                                                     .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CharacteristicType>().HasOne(o => o.DeviceType)
                                                     .WithMany(m => m.CharacteristicTypes)
                                                     .HasForeignKey(fk => fk.DeviceTypeID)
                                                     .OnDelete(DeleteBehavior.Cascade);

            //Таблица DeviceType
            ComputerStore.Model.ImageConverter imageConverter = new Model.ImageConverter();
            var converter = new ValueConverter<System.Drawing.Image,byte[] >(
            x => imageConverter.ConvertImageToByte(x),
            x => imageConverter.ConverByteToImage(x));
            modelBuilder.Entity<DeviceType>()
                .Property(x => x.DefaultImage)
                .HasConversion(converter);
            modelBuilder.Entity<DeviceType>().Property(p => p.Name)
                                             .IsRequired(true);
            modelBuilder.Entity<DeviceType>().HasKey(k => k.ID)
                                             .HasName("PrimaryKey_DeviceTypeID");
            

            

            //Таблица Device
            modelBuilder.Entity<Device>().Property(p => p.Name)
                                         .IsRequired(true);
            modelBuilder.Entity<Device>().HasKey(p => p.ID)
                                         .HasName("PrimaryKey_DeviceID");
            modelBuilder.Entity<Device>().HasOne(o => o.DeviceType)
                                         .WithMany(m => m.Devices)
                                         .HasForeignKey(fk => fk.DeviceTypeID)
                                         .OnDelete(DeleteBehavior.ClientNoAction);

            //Таблица Characteristics
            modelBuilder.Entity<Characteristic>().Property(p => p.Value)
                                                 .IsRequired(true);
            modelBuilder.Entity<Characteristic>().HasKey(k => k.ID)
                                                 .HasName("PrimaryKey_CharacteristicID");
            modelBuilder.Entity<Characteristic>().HasOne(o => o.Device)
                                                 .WithMany(m => m.Characteristics)
                                                 .HasForeignKey(fk => fk.DeviceID)
                                                 .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Characteristic>().HasOne(o => o.CharacteristicType)
                                                 .WithMany(m => m.Characteristics)
                                                 .HasForeignKey(fk => fk.CharacteristicTypeID)
                                                 .OnDelete(DeleteBehavior.ClientNoAction);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=ComputerStore.db;");
        }

        public void CreateDataBase()
        {
            if (!System.IO.File.Exists("ComputerStore.db"))
            {
                Database.EnsureCreated();
                AddEntryes();
            }
        }

        private void AddEntryes()
        {
            #region ValueType
            ValueType valueIntType = new ValueType("int");
            ValueType valueStringType = new ValueType("string");
            ValueType valueFloatType = new ValueType("float");
            ValueType valueBoolType = new ValueType("bool");
            ValueTypes.Add(valueIntType);
            ValueTypes.Add(valueStringType);
            ValueTypes.Add(valueFloatType);
            ValueTypes.Add(valueBoolType);
            SaveChanges();
            #endregion

            #region DeviceType
            DeviceType deviceLaptopType = new DeviceType("Ноутбук",Properties.Resources.Laptop);
            DeviceTypes.Add(deviceLaptopType);
            SaveChanges();
            #endregion

            #region Devices
            Device device1 = new Device(deviceLaptopType.ID, "Samsung NC10");
            Device device2 = new Device(deviceLaptopType.ID, "ASUS K50IN");
            Devices.Add(device1);
            Devices.Add(device2);
            SaveChanges();
            #endregion

            #region CharacteristicType
            CharacteristicType characteristicType_Webcam = new CharacteristicType(deviceLaptopType.ID, "Веб-камера", valueBoolType.ID);
            CharacteristicType characteristicType_VideoCard = new CharacteristicType(deviceLaptopType.ID, "Видеокарта", valueStringType.ID);
            CharacteristicType characteristicType_WorkingHours = new CharacteristicType(deviceLaptopType.ID, "Время работы", valueIntType.ID);
            CharacteristicType characteristicType_Screen = new CharacteristicType(deviceLaptopType.ID, "Диагональ экрана", valueFloatType.ID);
            CharacteristicType characteristicType_Storage = new CharacteristicType(deviceLaptopType.ID, "Накопитель", valueIntType.ID);
            CharacteristicType characteristicType_RAM = new CharacteristicType(deviceLaptopType.ID, "Оперативная память", valueIntType.ID);
            CharacteristicType characteristicType_CPU = new CharacteristicType(deviceLaptopType.ID, "Процессор", valueStringType.ID);
            CharacteristicTypes.Add(characteristicType_Webcam);
            CharacteristicTypes.Add(characteristicType_VideoCard);
            CharacteristicTypes.Add(characteristicType_WorkingHours);
            CharacteristicTypes.Add(characteristicType_Screen);
            CharacteristicTypes.Add(characteristicType_Storage);
            CharacteristicTypes.Add(characteristicType_RAM);
            CharacteristicTypes.Add(characteristicType_CPU);
            SaveChanges();
            #endregion

            #region Characteristics
            //1 Samsung NC10
            Characteristic char1_Webcam = new Characteristic(device1.ID, characteristicType_Webcam.ID, "true");
            Characteristic char1_VideoCard = new Characteristic(device1.ID, characteristicType_VideoCard.ID, "Intel GMA 952 (встроенная)");
            Characteristic char1_WorkingHours = new Characteristic(device1.ID, characteristicType_WorkingHours.ID, "6");
            Characteristic char1_Screen = new Characteristic(device1.ID, characteristicType_Screen.ID, "10,20");
            Characteristic char1_Storage = new Characteristic(device1.ID, characteristicType_Storage.ID, "80");
            Characteristic char1_RAM = new Characteristic(device1.ID, characteristicType_RAM.ID, "1");
            Characteristic char1_CPU = new Characteristic(device1.ID, characteristicType_CPU.ID, "Intel 945GSE");
            Characteristics.Add(char1_Webcam);
            Characteristics.Add(char1_VideoCard);
            Characteristics.Add(char1_WorkingHours);
            Characteristics.Add(char1_Screen);
            Characteristics.Add(char1_Storage);
            Characteristics.Add(char1_RAM);
            Characteristics.Add(char1_CPU);
            //2 ASUS K50IN
            Characteristic char2_Webcam = new Characteristic(device2.ID, characteristicType_Webcam.ID, "true");
            Characteristic char2_VideoCard = new Characteristic(device2.ID, characteristicType_VideoCard.ID, "NVIDIA GeForce G 102M (дискретная)");
            Characteristic char2_WorkingHours = new Characteristic(device2.ID, characteristicType_WorkingHours.ID, "6");
            Characteristic char2_Screen = new Characteristic(device2.ID, characteristicType_Screen.ID, "15,6");
            Characteristic char2_Storage = new Characteristic(device2.ID, characteristicType_Storage.ID, "250");
            Characteristic char2_RAM = new Characteristic(device2.ID, characteristicType_RAM.ID, "2");
            Characteristics.Add(char2_Webcam);
            Characteristics.Add(char2_VideoCard);
            Characteristics.Add(char2_WorkingHours);
            Characteristics.Add(char2_Screen);
            Characteristics.Add(char2_Storage);
            Characteristics.Add(char2_RAM);
            SaveChanges();
            #endregion
        }

        public DbSet<ValueType> ValueTypes { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<CharacteristicType> CharacteristicTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
    }
}
