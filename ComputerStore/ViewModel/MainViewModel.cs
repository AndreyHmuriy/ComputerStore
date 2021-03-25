using ComputerStore.ComputerStoreEntity;
using ComputerStore.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ComputerStore.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private DBQuery queries = new DBQuery();
        private IBaseDeviceType currentDeviceType;
        private BaseDevice currentDevice;
        private ObservableCollection<BaseDevice> devices
            = new ObservableCollection<BaseDevice>();
        private BitmapImage defaultDeviceTypeImage;
        private ObservableCollection<Model.DeviceCharacteristics> deviceCharacteristics
            = new ObservableCollection<DeviceCharacteristics>();
        private ObservableCollection<Object> filterList
            = new ObservableCollection<object>();

        public ObservableCollection<Object> FilterList
        {
            get => filterList;
            set
            {
                Set(nameof(FilterList), ref filterList, value);
            }
        }

        public ObservableCollection<DeviceCharacteristics> DeviceCharacteristics
        {
            get => deviceCharacteristics;
            set
            {
                Set(() => DeviceCharacteristics, ref deviceCharacteristics, value);
            }
        }

        public ObservableCollection<BaseDevice> Devices
        {
            get => devices;
            set
            {
                Set(() => Devices, ref devices, value);
            }
        }

        public BaseDevice CurrentDevice
        {
            get => currentDevice;
            set
            {
                Set(() => CurrentDevice, ref currentDevice, value);
                if (value != null)
                {
                    DeviceCharacteristics = value.DeviceCharacteristics;
                }
            }
        }

        /// <summary>
        /// Текущий выбранный тип устройства
        /// </summary>
        public IBaseDeviceType CurrentDeviceType
        {
            get => currentDeviceType;
            set
            {
                Set(() => CurrentDeviceType, ref currentDeviceType, value);
                if (value.ID != -1)
                {
                    Devices.Clear();
                    foreach (var item in queries.GetDeviceWithCharacteristics(value.ID))
                    {
                        Devices.Add(item);
                    }

                    List<DeviceCharacteristics> temp
                        = new List<DeviceCharacteristics>();
                    foreach (var _device in Devices)
                    {
                        foreach (var _characteristic in _device.DeviceCharacteristics)
                        {
                            temp.Add(_characteristic);
                        }
                    }

                    var stringfilterquery = temp.Where(x => x.Type == "string").GroupBy(x => x.Name);
                    foreach(var item in stringfilterquery)
                    {
                        ObservableCollection<CheckedElement> elements
                            = new ObservableCollection<CheckedElement>();
                        foreach(var elem in item)
                        {
                            elements.Add(new CheckedElement(elem.Value, true));
                        }
                        FilterList.Add(new StringFilter(item.Key, elements));
                    }

                    //FilterList.Add();

                    using (MemoryStream memory = new MemoryStream())
                    {
                        value.DefaultImage.Save(memory, ImageFormat.Png);
                        memory.Position = 0;
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        DefaultDeviceTypeImage = bitmapImage;
                    }
                }
                else
                {
                    Devices.Clear();
                }
            }
        }

        public BitmapImage DefaultDeviceTypeImage
        {
            get => defaultDeviceTypeImage;
            set
            {

                Set(() => DefaultDeviceTypeImage, ref defaultDeviceTypeImage, value);
            }
        }

        /// <summary>
        /// Список всех имеющихся в базе данных типов устройств
        /// </summary>
        public ObservableCollection<IBaseDeviceType> DeviceTypeList { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
                return;
            try
            {
                using (var context = new ComputerStoreEntity.ComputerStoreContext())
                {
                    context.CreateDataBase();
                }

                DeviceTypeList = new ObservableCollection<IBaseDeviceType>();
                DeviceTypeList.Add(new DeviceType(-1, "Выберите тип", null));
                var types = new ObservableCollection<IBaseDeviceType>(queries.GetDeviceTypes());
                foreach (var item in types)
                {
                    DeviceTypeList.Add(item);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show(exp.Message);
            }
            CurrentDeviceType = DeviceTypeList[0];

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}