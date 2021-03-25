using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    public class Characteristic
    {
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public int CharacteristicTypeID { get; set; }
        public string Value { get; set; }

        public Characteristic() { }

        public Characteristic( int deviceID, int characteristicTypeID, string value)
        {
            DeviceID = deviceID;
            CharacteristicTypeID = characteristicTypeID;
            Value = value;
        }

        public Device Device { get; set; }
        public CharacteristicType CharacteristicType { get; set; }
    }
}
