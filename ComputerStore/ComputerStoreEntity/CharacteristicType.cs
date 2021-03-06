using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    class CharacteristicType
    {
        public int ID { get; set; }
        public int DeviceTypeID { get; set; }
        public string Name { get; set; }
        public int ValueTypeID { get; set; }
        public bool Seach { get; set; }

        public CharacteristicType()
        {

        }

        public CharacteristicType(int deviceTypeID, string name, int valueTypeID, bool seach)
        {
            DeviceTypeID = deviceTypeID;
            Name = name;
            ValueTypeID = valueTypeID;
            Seach = seach;
        }

        public ValueType ValueType { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
    }
}
