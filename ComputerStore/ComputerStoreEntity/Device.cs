using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    class Device
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
