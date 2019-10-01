namespace Konfiguration_WPF
{
    public class ConfiguratorViewModel
    {
        public string ProcessorID             { get; set; } = string.Empty;

        public string CpuName                 { get; set; } = string.Empty;

        public string SerialNumber            { get; set; } = string.Empty;
                                              
        public string Manufacturer            { get; set; } = string.Empty;

        public string Modell1                 { get; set; } = string.Empty;
                                              
        public string Modell2                 { get; set; } = string.Empty;

        public int CPUBit                     { get; set; } = 0;

        public string RamType                 { get; set; } = string.Empty;

        public double TotalPhysicalMemory     { get; set; } = 0;

        public int PhysicalMemoryCount        { get; set; } = 0;

        public string RamSpeed                { get; set; } = string.Empty;

        public string HDD1                    { get; set; } = string.Empty;
                                              
        public string HDD2                    { get; set; } = string.Empty;
                                              
        public string HDD3                    { get; set; } = string.Empty;

        public string GraphicCard             { get; set; } = string.Empty;

        public string GraphicCardResolution   { get; set; } = string.Empty;

        public string Mainboard               { get; set; } = string.Empty;
                                              
        public string RomDrive1               { get; set; } = string.Empty;
                                              
        public string RomDrive2               { get; set; } = string.Empty;
                                              
        public string OsVersion               { get; set; } = string.Empty;
                                              
        public string SystemTyp               { get; set; } = string.Empty;

        public string Email_PasswordEncrypted { get; set; } = string.Empty;

        public static ConfiguratorViewModel BuildConfiguratorViewModel()
        {
            return new ConfiguratorViewModel
            {
                SystemTyp             = SystemInformation.SystemTyp_String(),
                CpuName               = SystemInformation.CPU_NAME(),
                CPUBit                = SystemInformation.CPU_BIT(),
                Manufacturer          = SystemInformation.Manufacturer(),
                Modell1               = SystemInformation.Modell1(),
                Modell2               = SystemInformation.Modell2(),
                HDD1                  = SystemInformation.HDD1(),
                HDD2                  = SystemInformation.HDD2(),
                HDD3                  = SystemInformation.HDD3(),
                RamType               = SystemInformation.RAM_TYP(),
                TotalPhysicalMemory   = SystemInformation.TotalPhysicalMemory(),
                RamSpeed              = SystemInformation.RAM_SPEED(),
                SerialNumber          = SystemInformation.SerialNumber(),
                GraphicCard           = SystemInformation.GraphicCard(),
                GraphicCardResolution = SystemInformation.GraphicCardResolution(),
                RomDrive1             = SystemInformation.RomDrive1(),
                RomDrive2             = SystemInformation.RomDrive2(),
                OsVersion             = SystemInformation.GetWindowsClientVersion()
            };
        }
    }
}
