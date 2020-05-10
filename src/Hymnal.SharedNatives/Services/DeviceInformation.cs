using System;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using Xamarin.Essentials;

namespace Hymnal.SharedNatives.Services
{
    public class DeviceInformation : IDeviceInformation
    {
        public string Model => DeviceInfo.Model;
        public string Manufacturer => DeviceInfo.Manufacturer;
        public string Name => DeviceInfo.Name;
        public string VersionString => DeviceInfo.VersionString;
        public Version Version => DeviceInfo.Version;
        public Platform Platform
        {
            get
            {
                DevicePlatform currentPlatform = DeviceInfo.Platform;

                if (currentPlatform == DevicePlatform.Android)
                {
                    return Platform.Android;
                }
                else if (currentPlatform == DevicePlatform.iOS)
                {
                    return Platform.iOS;
                }
                else if (currentPlatform == DevicePlatform.tvOS)
                {
                    return Platform.tvOS;
                }
                else if (currentPlatform == DevicePlatform.Tizen)
                {
                    return Platform.Tizen;
                }
                else if (currentPlatform == DevicePlatform.UWP)
                {
                    return Platform.UWP;
                }
                else if (currentPlatform == DevicePlatform.watchOS)
                {
                    return Platform.watchOS;
                }

                return Platform.Unknown;
            }
        }
        public Idiom Idiom
        {
            get
            {
                DeviceIdiom currentIdiom = DeviceInfo.Idiom;

                if (currentIdiom == DeviceIdiom.Phone)
                {
                    return Idiom.Phone;
                }
                else if (currentIdiom == DeviceIdiom.Tablet)
                {
                    return Idiom.Tablet;
                }
                else if (currentIdiom == DeviceIdiom.Desktop)
                {
                    return Idiom.Desktop;
                }
                else if (currentIdiom == DeviceIdiom.TV)
                {
                    return Idiom.TV;
                }
                else if (currentIdiom == DeviceIdiom.Watch)
                {
                    return Idiom.Watch;
                }

                return Idiom.Unknown;
            }
        }
        public Core.Models.DeviceType DeviceType
        {
            get
            {
                switch (DeviceInfo.DeviceType)
                {
                    case Xamarin.Essentials.DeviceType.Physical:
                        return Core.Models.DeviceType.Physical;
                    case Xamarin.Essentials.DeviceType.Virtual:
                        return Core.Models.DeviceType.Virtual;
                    default:
                        return Core.Models.DeviceType.Unknown;
                }
            }
        }
    }
}
