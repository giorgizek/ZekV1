using System;
using System.Management;

namespace Zek.Management
{

    public class CpuInfo
    {
        /// <summary>
        /// return Volume Serial Number from hard drive
        /// </summary>
        /// <param name="driveLetter">[optional] Drive letter</param>
        /// <returns>[string] VolumeSerialNumber</returns>
        public static string GetVolumeSerial(string driveLetter)
        {
            if (string.IsNullOrEmpty(driveLetter)) driveLetter = "C";
            var disk = new ManagementObject($"win32_logicaldisk.deviceid=\"{driveLetter}:\"");
            disk.Get();
            return disk["VolumeSerialNumber"].ToString();
        }

        private static string _hardwareID;
        /// <summary>
        /// Hardware ID
        /// </summary>
        /// <returns>აბრუნებს GetCpuID()+GetBiosID()+GetDiskID()+GetMotherboardID().</returns>
        public static string GetHardwareID()
        {
            return _hardwareID ?? (_hardwareID = GetCpuID() + GetBiosID() + GetDiskID() + GetMotherboardID());
        }

        public static string GetHardwareID( bool useProcessorID,
                                            bool useBaseBoardManufacturer,
                                            bool useBaseBoardProduct,
                                            bool useDiskDriveSignature,
                                            bool useVideoControllerCaption,
                                            bool usePhysicalMediaSerialNumber,
                                            bool useBiosManufacturer,
                                            bool useBiosVersion,
                                            bool useWindowsSerialNumber)
        {
            var info = string.Empty;

            if (useProcessorID) info += GetHardwareInfo(HardwareInfo.ProcessorId);
            if (useBaseBoardManufacturer) info += GetHardwareInfo(HardwareInfo.MotherboardManufacturer);
            if (useBaseBoardProduct) info += GetHardwareInfo(HardwareInfo.MotherboardProduct);
            if (useDiskDriveSignature) info += GetHardwareInfo(HardwareInfo.DiskDriveSignature);
            if (useVideoControllerCaption) info += GetHardwareInfo(HardwareInfo.VideoControllerCaption);
            if (usePhysicalMediaSerialNumber) info += GetHardwareInfo(HardwareInfo.PhysicalMediaSerialNumber);
            if (useBiosManufacturer) info += GetHardwareInfo(HardwareInfo.BiosManufacturer);
            if (useBiosVersion) info += GetHardwareInfo(HardwareInfo.BiosVersion);
            if (useWindowsSerialNumber) info += GetHardwareInfo(HardwareInfo.WindowsSerialNumber);

            return info;
        }

        /// <summary>
        /// Processor ID.
        /// </summary>
        /// <returns>აბრუნებს პროცესორის ID-ს.</returns>
        private static string GetCpuID()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as very time consuming
            var result = GetHardwareInfo(HardwareInfo.ProcessorUniqueId);

            //If no UniqueID, use ProcessorID
            if (result.Length == 0)
            {
                result = GetHardwareInfo(HardwareInfo.ProcessorId);

                //If no ProcessorId, use Name
                if (result.Length == 0)
                {
                    result = GetHardwareInfo(HardwareInfo.ProcessorName);

                    //If no Name, use Manufacturer
                    if (result.Length == 0) result = GetHardwareInfo(HardwareInfo.ProcessorManufacturer);

                    //Add clock speed for extra security
                    result += GetHardwareInfo(HardwareInfo.ProcessorMaxClockSpeed);
                }
            }

            return result;
        }
        /// <summary>
        /// BIOS Identifier.
        /// </summary>
        /// <returns>აბრუნებს ბიოსის იდენთიფიკატორს.</returns>
        public static string GetBiosID()
        {
            return GetHardwareInfo(HardwareInfo.BiosManufacturer) +
                    GetHardwareInfo(HardwareInfo.BiosSMBIOSBIOSVersion) +
                    GetHardwareInfo(HardwareInfo.BiosIdentificationCode) +
                    GetHardwareInfo(HardwareInfo.BiosSerialNumber) +
                    GetHardwareInfo(HardwareInfo.BiosReleaseDate) +
                    GetHardwareInfo(HardwareInfo.BiosVersion);
        }
        /// <summary>
        /// Main physical hard drive ID.
        /// </summary>
        /// <returns>აბრუნებს ვინჩესტერის ID-ს.</returns>
        public static string GetDiskID()
        {
            return GetHardwareInfo(HardwareInfo.DiskDriveModel) +
                    GetHardwareInfo(HardwareInfo.DiskDriveManufacturer) +
                    GetHardwareInfo(HardwareInfo.DiskDriveSignature) +
                    GetHardwareInfo(HardwareInfo.DiskDriveTotalHeads);
        }
        /// <summary>
        /// Motherboard ID.
        /// </summary>
        /// <returns>აბრუნებს დედაპლატის ID-ს.</returns>
        public static string GetMotherboardID()
        {
            return GetHardwareInfo(HardwareInfo.MotherboardModel) +
                  GetHardwareInfo(HardwareInfo.MotherboardManufacturer) +
                  GetHardwareInfo(HardwareInfo.MotherboardName) +
                  GetHardwareInfo(HardwareInfo.MotherboardSerialNumber);
        }

        /// <summary>
        /// Return a hardware identifier
        /// </summary>
        /// <param name="info">Info name</param>
        /// <returns>Hardware identifier</returns>
        public static string GetHardwareInfo(HardwareInfo info)
        {
            switch (info)
            {
                case HardwareInfo.ProcessorUniqueId:
                    return Identifier("Win32_Processor", "UniqueId");
                case HardwareInfo.ProcessorId:
                    return Identifier("Win32_Processor", "ProcessorId");
                case HardwareInfo.ProcessorName:
                    return Identifier("Win32_Processor", "Name");
                case HardwareInfo.ProcessorManufacturer:
                    return Identifier("Win32_Processor", "Manufacturer");
                case HardwareInfo.ProcessorMaxClockSpeed:
                    return Identifier("Win32_Processor", "MaxClockSpeed");

                case HardwareInfo.BiosManufacturer:
                    return Identifier("Win32_BIOS", "Manufacturer");
                case HardwareInfo.BiosSMBIOSBIOSVersion:
                    return Identifier("Win32_BIOS", "SMBIOSBIOSVersion");
                case HardwareInfo.BiosIdentificationCode:
                    return Identifier("Win32_BIOS", "IdentificationCode");
                case HardwareInfo.BiosSerialNumber:
                    return Identifier("Win32_BIOS", "SerialNumber");
                case HardwareInfo.BiosReleaseDate:
                    return Identifier("Win32_BIOS", "ReleaseDate");
                case HardwareInfo.BiosVersion:
                    return Identifier("Win32_BIOS", "Version");

                case HardwareInfo.DiskDriveModel:
                    return Identifier("Win32_DiskDrive", "Model");
                case HardwareInfo.DiskDriveManufacturer:
                    return Identifier("Win32_DiskDrive", "Manufacturer");
                case HardwareInfo.DiskDriveSignature:
                    return Identifier("Win32_DiskDrive", "Signature");
                case HardwareInfo.DiskDriveTotalHeads:
                    return Identifier("Win32_DiskDrive", "TotalHeads");

                case HardwareInfo.MotherboardModel:
                    return Identifier("Win32_BaseBoard", "Model");
                case HardwareInfo.MotherboardManufacturer:
                    return Identifier("Win32_BaseBoard", "Manufacturer");
                case HardwareInfo.MotherboardName:
                    return Identifier("Win32_BaseBoard", "Name");
                case HardwareInfo.MotherboardSerialNumber:
                    return Identifier("Win32_BaseBoard", "SerialNumber");
                case HardwareInfo.MotherboardProduct:
                    return Identifier("Win32_BaseBoard", "Product");

                case HardwareInfo.VideoControllerDriverVersion:
                    return Identifier("Win32_VideoController", "DriverVersion");
                case HardwareInfo.VideoControllerName:
                    return Identifier("Win32_VideoController", "Name");
                case HardwareInfo.VideoControllerCaption:
                    return Identifier("Win32_VideoController", "Caption");

                case HardwareInfo.PhysicalMediaSerialNumber:
                    return Identifier("Win32_PhysicalMedia", "SerialNumber");

                case HardwareInfo.MACAddress:
                    return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");

                case HardwareInfo.WindowsSerialNumber:
                    return Identifier("Win32_OperatingSystem", "SerialNumber");

                default:
                    throw new Exception("Error while getting hardware information");
            }
        }

        /// <summary>
        /// Return a hardware identifier
        /// </summary>
        /// <param name="wmiClass">WMI Class name</param>
        /// <param name="wmiProperty">WMI Property name</param>
        /// <param name="wmiMustBeTrue">WMI Property name</param>
        /// <returns>Hardware identifier</returns>
        private static string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            var result = "";

            var mc = new ManagementClass(wmiClass);
            var moc = mc.GetInstances();

            foreach (var mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch { }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Return a hardware identifier
        /// </summary>
        /// <param name="wmiClass">WMI Class name</param>
        /// <param name="wmiProperty">WMI Property name</param>
        /// <returns>Hardware identifier</returns>
        private static string Identifier(string wmiClass, string wmiProperty)
        {
            var result = string.Empty;
            var mc = new ManagementClass(wmiClass);
            var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch { }
                }
            }

            return result;
        }
    }
}
