using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrum.Repository.Abstractions
{
    public interface IDatabaseDeviceLocation
    {
        string GetPath(string fileName);
    }
}
