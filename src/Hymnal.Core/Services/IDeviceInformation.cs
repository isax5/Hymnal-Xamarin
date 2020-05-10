using System;
using Hymnal.Core.Models;

namespace Hymnal.Core.Services
{
    public interface IDeviceInformation
    {
        string Model { get; }
        string Manufacturer { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        Platform Platform { get; }
        Idiom Idiom { get; }
        DeviceType DeviceType { get; }
    }
}
