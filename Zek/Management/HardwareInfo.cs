using System;

namespace Zek.Management
{
    #region HardwareInfo
    [Serializable]
    public enum HardwareInfo
    {
        //Win32_Processor
        ProcessorUniqueId = 0,
        ProcessorId,
        ProcessorName,
        ProcessorManufacturer,
        ProcessorMaxClockSpeed,

        //Win32_BIOS
        BiosManufacturer,
        BiosSMBIOSBIOSVersion,
        BiosIdentificationCode,
        BiosSerialNumber,
        BiosReleaseDate,
        BiosVersion,

        //Win32_DiskDrive
        DiskDriveModel,
        DiskDriveManufacturer,
        DiskDriveSignature,
        DiskDriveTotalHeads,

        //Win32_BaseBoard
        MotherboardModel,
        MotherboardManufacturer,
        MotherboardName,
        MotherboardSerialNumber,
        MotherboardProduct,

        //Win32_VideoController
        VideoControllerDriverVersion,
        VideoControllerName,
        VideoControllerCaption,

        PhysicalMediaSerialNumber,

        //Win32_NetworkAdapterConfiguration
        MACAddress,

        WindowsSerialNumber,
    }
    #endregion
}
