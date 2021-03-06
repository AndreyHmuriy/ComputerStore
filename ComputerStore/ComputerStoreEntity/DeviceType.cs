using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    class DeviceType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DeviceType()
        {

        }

        public DeviceType(string name)
        {
            Name = name;
        }

        public ICollection<CharacteristicType> CharacteristicTypes { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
