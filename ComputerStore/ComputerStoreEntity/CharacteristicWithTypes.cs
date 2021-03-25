using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.ComputerStoreEntity
{
    public class CharacteristicWithTypes
    {
        public string CharacteristicType { get; set; }
        public string ValueType { get; set; }
        public string Value { get; set; }

        public CharacteristicWithTypes()
        {

        }

        public CharacteristicWithTypes(string chararcteristicType, string valueType, string value):this()
        {
            CharacteristicType = chararcteristicType;
            ValueType = valueType;
            Value = value;
        }
    }
}
