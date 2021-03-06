using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    internal class Data
    {
        public List<ValueType> ValueTypesList = new List<ValueType>()
        {
            new ValueType("int"),
            new ValueType("string"),
            new ValueType("float"),
            new ValueType("bool")
        };

        public List<DeviceType> DeviceTypesList = new List<DeviceType>()
        {
            new DeviceType("Ноутбук")
        };

        public List<CharacteristicType> CharacteristicTypesList = new List<CharacteristicType>()
        {
            new CharacteristicType(1,"Веб-камера",4,false),
            new CharacteristicType(1,"Видеокарта",2,false),
            new CharacteristicType(1,"Время работы",1,true),
            new CharacteristicType(1,"Диагональ экрана",3,false),
            new CharacteristicType(1,"Накопитель",1,true),
            new CharacteristicType(1,"Оперативная память",1,true),
            new CharacteristicType(1,"Процессор",2,false)
        };

        public List<Device> DevicesList = new List<Device>()
        {
            new Device(1,"Samsung NC10")
        };

        public List<Characteristic> CharacteristicsList = new List<Characteristic>()
        {
            new Characteristic(1,1,"true"),
            new Characteristic(1,2,"Intel GMA 952 (встроенная)"),
            new Characteristic(1,3,"6"),
            new Characteristic(1,4,"10,20"),
            new Characteristic(1,5,"80"),
            new Characteristic(1,6,"1"),
            new Characteristic(1,7,"Intel 945GSE")
        };
    }

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
                                                     .HasForeignKey(fk=>fk.ValueTypeID)
                                                     .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CharacteristicType>().HasOne(o => o.DeviceType)
                                                     .WithMany(m => m.CharacteristicTypes)
                                                     .HasForeignKey(fk=>fk.DeviceTypeID)
                                                     .OnDelete(DeleteBehavior.Cascade);

            //Таблица DeviceType
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
                                         .HasForeignKey(fk=>fk.DeviceTypeID)
                                         .OnDelete(DeleteBehavior.ClientNoAction);

            //Таблица Characteristics
            modelBuilder.Entity<Characteristic>().Property(p => p.Value)
                                                 .IsRequired(true);
            modelBuilder.Entity<Characteristic>().HasKey(k => k.ID)
                                                 .HasName("PrimaryKey_CharacteristicID");
            modelBuilder.Entity<Characteristic>().HasOne(o => o.Device)
                                                 .WithMany(m => m.Characteristics)
                                                 .HasForeignKey(fk=>fk.DeviceID)
                                                 .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Characteristic>().HasOne(o => o.CharacteristicType)
                                                 .WithMany(m => m.Characteristics)
                                                 .HasForeignKey(fk=>fk.CharacteristicTypeID)
                                                 .OnDelete(DeleteBehavior.ClientNoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=ComputerStore.db;");
        }

        public void CreateDataBase()
        {
            //if(System.IO.File.Exists("ComputerStore.db"))
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
                AddEntryes();
            }
        }

        private void AddEntryes()
        {
            Data data = new Data();
            ValueTypes.AddRange(data.ValueTypesList);
            SaveChanges();

            DeviceTypes.AddRange(data.DeviceTypesList);
            SaveChanges();

            Devices.AddRange(data.DevicesList);
            SaveChanges();

            CharacteristicTypes.AddRange(data.CharacteristicTypesList);
            SaveChanges();

            Characteristics.AddRange(data.CharacteristicsList);
            SaveChanges();
        }

        public DbSet<ValueType> ValueTypes { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<CharacteristicType> CharacteristicTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
    }
}
