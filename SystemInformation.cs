using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Management;


namespace Konfiguration_WPF
{
    public static class SystemInformation
    {

        public static string Encryption_Key()
        {
            object queryResult;
            using (var oleDbConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                using (var cmdKey = new OleDbCommand("SELECT EncryptionKey FROM Config", oleDbConnection))
                {
                    oleDbConnection.Open();
                    queryResult = cmdKey.ExecuteScalar();
                }

                oleDbConnection.Close();
            }

            return queryResult.ToString();
        }



        public static string Email_Password()
        {
            object queryResult;
            using (var oleDbConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                using (var cmdKey = new OleDbCommand("SELECT Email_Password FROM Config", oleDbConnection))
                {
                    oleDbConnection.Open();
                    queryResult = cmdKey.ExecuteScalar();
                }

                oleDbConnection.Close();
            }

            return queryResult.ToString();
        }



        public static string CPU_NAME()
        {
            var cpuName = string.Empty;
            ManagementObjectCollection managementObjectCollection;

            using (var managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_processor"))
            {
                managementObjectCollection = managementObjectSearcher.Get();
            }

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                cpuName = managementObject["Name"].ToString();
            }

            return cpuName;
        }




        public static int CPU_BIT()
        {
            var bit = 0;

            using (var cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor"))
            {
                using (var managementObjectCollection = cpu.Get())
                {
                    foreach (ManagementObject managementObject in managementObjectCollection)
                    {
                        bit = Convert.ToInt32(managementObject["AddressWidth"]);
                    }
                }

            }

            return bit;
        }


        public static string ProcessorId()
        {
            var processorId = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_processor"))
            {
                using (var managementObjectCollection = managementObjectSearcher.Get())
                {

                    foreach (ManagementObject managementObject in managementObjectCollection)
                    {
                        processorId = managementObject["ProcessorId"].ToString();
                    }
                }

            }
          
            return processorId;
        }

        public static string SocketDesignation()
        {
            var socketDesignation = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_processor"))
            {
                using (var managementObjectCollection = managementObjectSearcher.Get())
                {

                    foreach (ManagementObject managementObject in managementObjectCollection)
                    {
                        socketDesignation = managementObject["SocketDesignation"].ToString();
                    }
                }

            }

            return socketDesignation;
        }


        public static string Manufacturer()
        {
            var manufacturer = string.Empty;
            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        manufacturer = Conversions.ToString(current["Manufacturer"]);
                    }
                }

                return manufacturer;
            }
        }

        public static string Modell1()
        {
            var modell1 = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        modell1 = Conversions.ToString(current["Product"]);
                    }
                }

                return modell1;
            }
        }

        public static string Modell2()
        {
            var modell2 = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        modell2 = Conversions.ToString(current["Caption"]);
                    }
                }

                return modell2;
            }
        }

        public static int PhysicalMemoryCount()
        {
            var physicalMemoryCount = 0;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        physicalMemoryCount++;
                    }
                }

                return physicalMemoryCount;
            }
        }

        public static string RAM_TYP()
        {
            var memoryType = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;

                        switch (Conversions.ToString(current["MemoryType"]))
                        {
                            case "0":
                                memoryType = "Unbekannt";
                                break;
                            case "1":
                                memoryType = "Andere";
                                break;
                            case "2":
                                memoryType = "DRAM";
                                break;
                            case "3":
                                memoryType = "Synchronous DRAM";
                                break;
                            case "4":
                                memoryType = "Cache DRAM";
                                break;
                            case "5":
                                memoryType = "EDO";
                                break;
                            case "6":
                                memoryType = "EDRAM ";
                                break;
                            case "7":
                                memoryType = "VRAM";
                                break;
                            case "8":
                                memoryType = "SRAM";
                                break;
                            case "9":
                                memoryType = "RAM";
                                break;
                            case "10":
                                memoryType = "ROM";
                                break;
                            case "11":
                                memoryType = "Flash";
                                break;
                            case "12":
                                memoryType = "EEPROM";
                                break;
                            case "13":
                                memoryType = "FEPROM";
                                break;
                            case "14":
                                memoryType = "EPROM";
                                break;
                            case "15":
                                memoryType = "CDRAM";
                                break;
                            case "16":
                                memoryType = "3DRAM";
                                break;
                            case "17":
                                memoryType = "SDRAM";
                                break;
                            case "18":
                                memoryType = "SGRAM";
                                break;
                            case "19":
                                memoryType = "RDRAM";
                                break;
                            case "20":
                                memoryType = "DDR1";
                                break;
                            case "21":
                                memoryType = "DDR2";
                                break;
                            case "22":
                                memoryType = "DDR2 FB-DIMM";
                                break;
                            case "23":
                                break;
                            case "24":
                                memoryType = "DDR3";
                                break;
                            case "25":
                                memoryType = "FBD2";
                                break;

                        }
                       
                    }
                }

                return memoryType;
            }
        }

        public static double TotalPhysicalMemory()
        {
            var mem = string.Empty;
            double mem2 = 0;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        mem = Conversions.ToString(current["Capacity"]);
                        mem2 += Convert.ToDouble(mem);
                    }
                }

                mem2 = mem2 / 1073741824;
                return mem2;
            }
        }

        public static string RAM_SPEED()
        {
            var speed = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        speed = Conversions.ToString(current["Speed"]);
                    }
                }

                return speed;
            }
        }

        public static string SerialNumber()
        {
            var serial = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        serial = Conversions.ToString(current["SerialNumber"]);
                    }
                }

                return serial;
            }
        }

        public static string GraphicCard()
        {
            var graphicCard = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        graphicCard = Conversions.ToString(current["Name"]);
                    }
                }

                return graphicCard;
            }
        }
        public static string GraphicCardResolution()
        {
            var graphicCardResolution = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        graphicCardResolution = Conversions.ToString(current["VideoModeDescription"]);
                    }
                }

                return graphicCardResolution;
            }
        }


        public static string MAINBOARD()
        {
            var mainBoardManufacturer = string.Empty;
            var mainBoardProduct           = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        mainBoardManufacturer = Conversions.ToString(current["Manufacturer"]);
                        mainBoardProduct      = Conversions.ToString(current["Product"]);
                    }
                }

                return $"{mainBoardManufacturer} {mainBoardProduct}";
            }
        }

        public static string RomDrive1()
        {
            var romDrive = string.Empty;
            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive WHERE SCSILogicalUnit = 0"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        romDrive = Conversions.ToString(current["name"]);
                    }
                }
            }
            return romDrive;
        }

        public static string RomDrive2()
        {
            var romDrive2 = string.Empty;
            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive WHERE SCSILogicalUnit = 1"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        romDrive2 = Conversions.ToString(current["name"]);
                    }
                }
            }
            if (romDrive2 == "")
                romDrive2 = "---";

            return romDrive2;
        }

        public static string HDD2()
        {
            var HDD2String = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE1'"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        HDD2String = "Bezeichnung: " + Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));

                    }
                }
            }
            return HDD2String;
        }

        public static string HDD3()
        {
            var HDD3String = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE2'"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        HDD3String = "Bezeichnung: " + Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));

                    }
                }
            }
            return HDD3String;
        }

        public static string HDD2_String()
        {
            var HDD2 = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE1'"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    HDD2 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                    HDD2 += Conversions.ToString(Operators.AddObject(HDD2, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                }
            }
            return HDD2;
        }

        //public void HDD3()
        //{
        //    ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
        //    using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE2'"))
        //    {
        //        enumerator = managementObjectSearcher.Get().GetEnumerator();
        //        while (enumerator.MoveNext())
        //        {
        //            ManagementObject current = (ManagementObject)enumerator.Current;
        //            lbl_HDD3.Content = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
        //            Label lblHDD1 = this.lbl_HDD3;
        //            Label label = lblHDD1;
        //            lbl_HDD3.Content = Conversions.ToString(Operators.AddObject(label.Content, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
        //        }
        //    }
        //}

        public static string HDD3_String()
        {
            var hdd3 = string.Empty;

            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE2'"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        hdd3 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                        hdd3 += Conversions.ToString(Operators.AddObject(hdd3, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                    }
                }
            }
            return hdd3;
        }

        //public void HDD1()
        //{
        //    ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
        //    using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE0'"))
        //    {
        //        enumerator = managementObjectSearcher.Get().GetEnumerator();
        //        while (enumerator.MoveNext())
        //        {
        //            ManagementObject current = (ManagementObject)enumerator.Current;
        //            lbl_HDD1.Content = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
        //            Label lblHDD1 = this.lbl_HDD1;
        //            Label label = lblHDD1;
        //            lbl_HDD1.Content = Conversions.ToString(Operators.AddObject(label.Content, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
        //        }
        //    }
        //}

        public static string HDD1()
        {
            var HDD1String = string.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE0'"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var current = (ManagementObject)enumerator.Current;
                    HDD1String = "Bezeichnung: " + Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00")), " TB"), Environment.NewLine));

                }
            }
            return HDD1String;
        }


        public static string HDD1_String()
        {
            var HDD1 = string.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE0'"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    HDD1 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
                    HDD1 += Conversions.ToString(Operators.AddObject(HDD1, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
                }
                return HDD1;
            }
        }


        //public void HDD2()
        //{
        //    ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
        //    using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE DeviceID LIKE '%DRIVE1'"))
        //    {
        //        enumerator = managementObjectSearcher.Get().GetEnumerator();
        //        while (enumerator.MoveNext())
        //        {
        //            ManagementObject current = (ManagementObject)enumerator.Current;
        //            lbl_HDD2.Content = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(current["Caption"], " @ "), Microsoft.VisualBasic.Strings.Format(Conversions.ToDouble(current["Size"]) / 1000 / 1000 / 1000 / 1000, ".00").ToString()), " TB"), Environment.NewLine));
        //            Label lblHDD1 = this.lbl_HDD2;
        //            Label label = lblHDD1;
        //            lbl_HDD2.Content = Conversions.ToString(Operators.AddObject(label.Content, Operators.ConcatenateObject("Serial:", current["SerialNumber"])));
        //        }
        //    }
        //}

        public static string SystemTyp_String()
        {
            using (var managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (ManagementObject)enumerator.Current;
                        var pcSystemTypeId = current["PCSystemType"];

                        switch (pcSystemTypeId)
                        {
                            case "0":
                                return "Unbekannt";
                            case "1":
                                return "Desktop-PC";
                            case "2":
                                return "Laptop";
                            case "3":
                                return "Desktop-PC";
                            case "4":
                                return "Enterprise-Server";
                            case "5":
                                return "SOHO Server";
                            case "6":
                                return "SOHO Server";
                            case "7":
                                return "Performance Server";
                            case "8":
                                return "Slate";
                            case "9":
                                return "Maximum";

                        }

                    }
                }

                return null;
            }

        }


        public static int Letzte_Serial()
        {
            var oleDbConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            var cmdLetzte = new OleDbCommand("SELECT Eigene_Serial FROM Rechner ORDER BY Eigene_Serial DESC", oleDbConnection);
            oleDbConnection.Open();

            object letz = cmdLetzte.ExecuteScalar();
            oleDbConnection.Close();
            cmdLetzte.Dispose();
            return Convert.ToInt32(letz) + 1;
        }

        public static string GetWindowsClientVersion() => new ComputerInfo().OSFullName;
    }
}
