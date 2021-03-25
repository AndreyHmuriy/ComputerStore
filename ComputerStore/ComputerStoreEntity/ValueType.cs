using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    public class ValueType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ValueType() { }

        public ValueType(string name)
        {
            Name = name;
        }

        public ICollection<CharacteristicType> CharacteristicTypes { get; set; }
    }
}
