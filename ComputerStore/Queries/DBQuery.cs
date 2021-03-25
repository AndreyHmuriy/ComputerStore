using ComputerStore.ComputerStoreEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore
{
    class DBQuery
    {
        public IEnumerable<ComputerStoreEntity.IBaseDeviceType> GetDeviceTypes()
        {
            using (var context = new ComputerStoreContext())
            {
                return context.DeviceTypes.ToList();
            }
        }

        public IEnumerable<BaseDevice> GetDevices(int deviceTypeID)
        {
            using (var context = new ComputerStoreContext())
            {
                IEnumerable<BaseDevice> result = context.Devices.Where(x => x.DeviceTypeID == deviceTypeID)
                            .Select(x => new BaseDevice(x.ID, x.Name))
                            .ToList();
                return result;
            }
        }

        public IEnumerable<Model.DeviceCharacteristics> GetDeviceCharacteristics(int deviceID)
        {
            using (var context = new ComputerStoreContext())
            {
                return context.Characteristics
                    .Where(x => x.DeviceID == deviceID)
                    .Join(context.CharacteristicTypes, x => x.CharacteristicTypeID, x => x.ID, (x, y) => new
                    {
                        y.Name,
                        y.ValueTypeID,
                        x.Value
                    })
                    .Join(context.ValueTypes, x => x.ValueTypeID, x => x.ID, (x, y) => new Model.DeviceCharacteristics()
                    {
                        Name = x.Name,
                        Value = (y.Name == "bool") ? "есть" : x.Value,
                        Type = y.Name
                    })
                    .OrderBy(x => x.Name)
                    .ToList();
            }
        }

        public ObservableCollection<BaseDevice> GetDeviceWithCharacteristics(int deviceTypeID)
        {
            using (var context = new ComputerStoreContext())
            {
                var enumerable = context.Devices.Where(x => x.DeviceTypeID == deviceTypeID)
                    .Join(context.Characteristics, x => x.ID, x => x.DeviceID, (x, y) => new
                    {
                        x.ID,
                        x.Name,
                        y.Value,
                        y.CharacteristicTypeID
                    })
                    .Join(context.CharacteristicTypes, x => x.CharacteristicTypeID, x => x.ID, (x, y) => new
                    {
                        DeviceID = x.ID,
                        DeviceName = x.Name,
                        CharacteristicValue = x.Value,
                        CharacteristicName = y.Name,
                        ValueTypeID = y.ValueTypeID
                    })
                    .Join(context.ValueTypes, x => x.ValueTypeID, x => x.ID, (x, y) => new
                    {
                        x.DeviceID,
                        x.DeviceName,
                        x.CharacteristicName,
                        x.CharacteristicValue,
                        ValueType = y.Name
                    })
                    .OrderBy(x=>x.CharacteristicName)
                    .ToList();
                var devices = enumerable.GroupBy(x => new { x.DeviceID, x.DeviceName });
                var deviceList = new ObservableCollection<BaseDevice>();
                foreach (var device in devices)
                {
                    BaseDevice baseDevice = new BaseDevice();
                    baseDevice.ID = device.Key.DeviceID;
                    baseDevice.Name = device.Key.DeviceName;
                    ObservableCollection<Model.DeviceCharacteristics> deviceCharacteristicsList
                        = new ObservableCollection<Model.DeviceCharacteristics>();
                    foreach (var c in device)
                    {
                        deviceCharacteristicsList.Add(
                            new Model.DeviceCharacteristics(c.CharacteristicName, c.CharacteristicValue, c.ValueType));
                    }
                    baseDevice.DeviceCharacteristics = deviceCharacteristicsList;
                    deviceList.Add(baseDevice);
                }
                return deviceList;
            }
        }
    }
}
