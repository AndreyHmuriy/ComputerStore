using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.Model
{
    public class DeviceCharacteristics
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public DeviceCharacteristics()
        {

        }

        public DeviceCharacteristics(string name, string value,string type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
}
