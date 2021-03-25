using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ComputerStore.ComputerStoreEntity
{
    public interface IBaseDeviceType
    {
        int ID { get; set; }
        string Name { get; set; }
        Image DefaultImage { get; set; }
    }

     public class DeviceType:IBaseDeviceType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Image DefaultImage { get; set; }

        public DeviceType()
        {

        }

        public DeviceType(string name)
        {
            Name = name;
        }
        public DeviceType(string name,Image defaultImage):this(name)
        {
            DefaultImage = defaultImage;
        }

        public DeviceType(int id, string name, Image defaultImage) : this(name,defaultImage)
        {
            ID = id;
        }

        public ICollection<CharacteristicType> CharacteristicTypes { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
