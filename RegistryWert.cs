using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Management;


namespace Konfiguration_WPF
{
    class RegistryWert
    {

        public static void Registry_Eintrag(string name, string value)
        {
            RegistryKey key;
            key = Registry.CurrentConfig.CreateSubKey("Computerservice Blasius Thomas");
            key.SetValue(name, value);
            key.Close();
        }


        public static string Encryption_Key()
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            OleDbCommand cmd_key = new OleDbCommand("SELECT EncryptionKey FROM Config", con);
            con.Open();
            object key = cmd_key.ExecuteScalar();
            con.Close();
            cmd_key.Dispose();
            return key.ToString();
        }



        public static string Email_Passwort()
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            OleDbCommand cmd_key = new OleDbCommand("SELECT Email_Passwort FROM Config", con);
            con.Open();
            object key = cmd_key.ExecuteScalar();
            con.Close();
            cmd_key.Dispose();
            return key.ToString();
        }



        public static string CPU_NAME()
        {
            var name = String.Empty;
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor");
            ManagementObjectCollection moc = cpu.Get();
            foreach (ManagementObject mo in moc)
            {
                name = mo["Name"].ToString();
            }
            return name;
        }




        public static int CPU_BIT()
        {
            int Bit = 0;
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor");
            ManagementObjectCollection moc = cpu.Get();
            foreach (ManagementObject mo in moc)
            {
                Bit = Convert.ToInt32(mo["AddressWidth"]);
            }
            cpu.Dispose();
            return Bit;
        }


        public static string CPU_ID()
        {
            var ProzId = String.Empty;
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor");
            ManagementObjectCollection moc = cpu.Get();
            foreach (ManagementObject mo in moc)
            {
                ProzId = mo["ProcessorId"].ToString();
            }
            return ProzId;
        }

        public static string SOCKET()
        {
            var Socket = String.Empty;
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor");
            ManagementObjectCollection moc = cpu.Get();
            foreach (ManagementObject mo in moc)
            {
                Socket = mo["SocketDesignation"].ToString();
            }
            cpu.Dispose();
            return Socket;
        }


        public static string Hersteller()
        {
            var Hersteller = String.Empty;
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    Hersteller = Conversions.ToString(current["Manufacturer"]);
                }
                return Hersteller;
            }
        }

        public static string Modell1()
        {
            var Modell1 = String.Empty;
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    Modell1 = Conversions.ToString(current["Product"]);
                }
                return Modell1;
            }
        }

        public static string Modell2()
        {
            var Modell2 = String.Empty;
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    Modell2 = Conversions.ToString(current["Caption"]);
                }
                return Modell2;
            }
        }

        public static int RAM_ANZ()
        {
            var Modell2 = String.Empty;
            int anz = 0;
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    anz++;

                }
                return anz;
            }
        }
        public static string RAM_TYP()
        {
            var ram_typ = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    if (Conversions.ToString(current["MemoryType"]) == "0")
                        ram_typ = "Unbekannt";
                    if (Conversions.ToString(current["MemoryType"]) == "1")
                        ram_typ = "Andere";
                    if (Conversions.ToString(current["MemoryType"]) == "2")
                        ram_typ = "DRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "3")
                        ram_typ = "Synchronous DRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "4")
                        ram_typ = "Cache DRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "5")
                        ram_typ = "EDO";
                    if (Conversions.ToString(current["MemoryType"]) == "6")
                        ram_typ = "EDRAM ";
                    if (Conversions.ToString(current["MemoryType"]) == "7")
                        ram_typ = "VRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "8")
                        ram_typ = "SRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "9")
                        ram_typ = "RAM";
                    if (Conversions.ToString(current["MemoryType"]) == "10")
                        ram_typ = "ROM";
                    if (Conversions.ToString(current["MemoryType"]) == "11")
                        ram_typ = "Flash";
                    if (Conversions.ToString(current["MemoryType"]) == "12")
                        ram_typ = "EEPROM";
                    if (Conversions.ToString(current["MemoryType"]) == "13")
                        ram_typ = "FEPROM";
                    if (Conversions.ToString(current["MemoryType"]) == "14")
                        ram_typ = "EPROM";
                    if (Conversions.ToString(current["MemoryType"]) == "15")
                        ram_typ = "CDRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "16")
                        ram_typ = "3DRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "17")
                        ram_typ = "SDRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "18")
                        ram_typ = "SGRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "19")
                        ram_typ = "RDRAM";
                    if (Conversions.ToString(current["MemoryType"]) == "20")
                        ram_typ = "DDR1";
                    if (Conversions.ToString(current["MemoryType"]) == "21")
                        ram_typ = "DDR2";
                    if (Conversions.ToString(current["MemoryType"]) == "22")
                        ram_typ = "DDR2 FB-DIMM";
                    if (Conversions.ToString(current["MemoryType"]) == "24")
                        ram_typ = "DDR3";
                    if (Conversions.ToString(current["MemoryType"]) == "25")
                        ram_typ = "FBD2";


                }
                return ram_typ;
            }
        }

        public static double RAM_TOTAL()
        {
            var mem = String.Empty;
            double mem2 = 0;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    mem = Conversions.ToString(current["Capacity"]);
                    mem2 += Convert.ToDouble(mem.ToString());
                }
                mem2 = mem2 / 1073741824;
                return mem2;
            }
        }

        public static string RAM_SPEED()
        {
            var speed = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    speed = Conversions.ToString(current["Speed"]);

                }
                return speed;
            }
        }

        public static string SERIENNUMMER()
        {
            var serial = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    serial = Conversions.ToString(current["SerialNumber"]);

                }
                return serial;
            }
        }

        public static string GRAFIK1()
        {
            var graka1 = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    graka1 = Conversions.ToString(current["Name"]);

                }
                return graka1;
            }
        }
        public static string GRAFIK1_RESOLUTION()
        {
            var res = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    res = Conversions.ToString(current["VideoModeDescription"]);

                }
                return res;
            }
        }


        public static string MAINBOARD()
        {
            var mb_herst = String.Empty;
            var mb_mod = String.Empty;
            var alles = String.Empty;

            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    mb_herst = Conversions.ToString(current["Manufacturer"]);
                    mb_mod = Conversions.ToString(current["Product"]);
                }
                alles = mb_herst + " " + mb_mod;
                return alles;
            }
        }

        public static string LW1()
        {
            var lw = String.Empty;
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive WHERE SCSILogicalUnit = 0"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    lw = Conversions.ToString(current["name"]);
                }
            }
            return lw;
        }

        public static string LW2()
        {
            var lw = String.Empty;
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive WHERE SCSILogicalUnit = 1"))
            {
                enumerator = managementObjectSearcher.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    lw = Conversions.ToString(current["name"]);
                }
            }
            if (lw == "")
                lw = "---";

            return lw;
        }

        public static string GetWindwosClientVersion()
        {
            string test = new ComputerInfo().OSFullName;
            return test;
        }

    }
}
