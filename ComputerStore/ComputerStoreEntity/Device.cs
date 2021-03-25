using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    public interface IBaseDevice
    {
        int ID { get; set; }
        string Name { get; set; }
    }

    public class BaseDevice
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Model.DeviceCharacteristics> DeviceCharacteristics { get; set; }
        public BaseDevice() { }
        public BaseDevice(int id, string name):this()
        {
            ID = id;
            Name = name;
        }

        public BaseDevice(int id, string name, ObservableCollection<Model.DeviceCharacteristics> deviceCharacteristics):this(id,name)
        {
            DeviceCharacteristics = deviceCharacteristics;
        }
    }

    public class Device: IBaseDevice
    {
        public int ID { get; set; }
        public int DeviceTypeID { get; set; }
        public string Name { get; set; }

        public Device() { }

        public Device(int deviceTypeID, string name)
        {
            DeviceTypeID = deviceTypeID;
            Name = name;
        }

        public DeviceType DeviceType { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
    }
}
