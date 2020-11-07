using Spectrum.Repository.Abstractions;
using System.IO;

namespace Spectrum.Login.DeviceServices
{
    public class DatabaseDeviceLocation : IDatabaseDeviceLocation
    {
        public string GetPath(string fileName = "Spectrum.db3")
        {
            var basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, fileName);
        }
    }
}