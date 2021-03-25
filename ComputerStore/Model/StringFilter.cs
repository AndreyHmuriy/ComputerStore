using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace ComputerStore.Model
{
    class CheckedElement: ObservableObject
    {
        private string value;
        private bool isChecked;

        public string Value
        {
            get => value;
            set
            {
                Set(nameof(Value), ref this.value, value);
            }
        }

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                Set(nameof(IsChecked), ref isChecked, value);
            }
        }

        public CheckedElement(string value, bool isChecked)
        {
            Value = value;
            IsChecked = isChecked;
        }
    }

    class StringFilter: ObservableObject
    {
        private string name;
        private ObservableCollection<CheckedElement> elements;
        
        public string Name
        {
            get => name;
            set
            {
                Set(nameof(Name), ref name, value);
            }
        }

        public ObservableCollection<CheckedElement> Elements
        {
            get => elements;
            set
            {
                Set(nameof(Elements), ref elements, value);
            }
        }

        public StringFilter(string name)
        {
            Name = name;
            Elements = new ObservableCollection<CheckedElement>();
        }

        public StringFilter(string name, ObservableCollection<CheckedElement> elements):this(name)
        {
            Elements = elements;
        }

    }
}
