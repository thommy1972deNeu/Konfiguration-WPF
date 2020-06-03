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
            key = Registry.CurrentUser.CreateSubKey(@"Computerservice Blasius Thomas");
            key.SetValue(name, value);
            key.Close();
        }

        public static string Registry_Lesen(string value)
        {
         
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Computerservice Blasius Thomas\", true);
                if(key != null)
                return key.GetValue(value).ToString();

                return null;

        }

        public static string Encryption_Key()
        {

            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                OleDbCommand cmd_key = new OleDbCommand("SELECT EncryptionKey FROM config", con);
                con.Open();
                object key = cmd_key.ExecuteScalar();
                con.Close();
                cmd_key.Dispose();
                return key.ToString();
            }
            catch (Exception)
            {
                return null;
      
            }    
         }


        public static string Mail_Key()
        {

            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                OleDbCommand cmd_key = new OleDbCommand("SELECT email_pass FROM config", con);
                con.Open();
                object key = cmd_key.ExecuteScalar();
                con.Close();
                cmd_key.Dispose();
                return key.ToString();
            }
            catch (Exception)
            {
                return null;

            }
        }

        public static string Mail_Server()
        {

            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                OleDbCommand cmd_key = new OleDbCommand("SELECT mail_server FROM config", con);
                con.Open();
                object key = cmd_key.ExecuteScalar();
                con.Close();
                cmd_key.Dispose();
                return key.ToString();
            }
            catch (Exception)
            {
                return null;

            }
        }

        public static string Mail_User()
        {

            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                OleDbCommand cmd_key = new OleDbCommand("SELECT mail_user FROM config", con);
                con.Open();
                object key = cmd_key.ExecuteScalar();
                con.Close();
                cmd_key.Dispose();
                return key.ToString();
            }
            catch (Exception)
            {
                return null;

            }
        }
        public static int Mail_Port()
        {
            try
            {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                OleDbCommand cmd_key = new OleDbCommand("SELECT mail_port FROM config", con);
                con.Open();
                object key = cmd_key.ExecuteScalar();
                con.Close();
                cmd_key.Dispose();
                return Convert.ToInt32(key);
            }
            catch (Exception)
            {
                return 0;

            }
        }

        public static string CPU_NAME()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
           
        }


        public static string COMPUTER_SYSTEM_UUID()
        {
            try
            {
                var name = String.Empty;
                ManagementObjectSearcher cpu = new ManagementObjectSearcher("select * from Win32_ComputerSystemProduct");
                ManagementObjectCollection moc = cpu.Get();
                foreach (ManagementObject mo in moc)
                {
                    name = mo["UUID"].ToString();
                }
                return name;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static int CPU_BIT()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }


        


        public static string CPU_ID()
        {
        try
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
        catch (Exception)
        {

            throw;
        }
        }

        public static string SOCKET()
        {
        try
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
        catch (Exception)
        {

            throw;
        }
        }


        public static string Hersteller()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string Modell1()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string Modell2()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public static int RAM_ANZ()
        {
            try
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
            catch (Exception)
            {

                throw;
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
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string RAM_SPEED()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string SERIENNUMMER()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string GRAFIK1()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }
        public static string GRAFIK1_RESOLUTION()
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }


        public static string MAINBOARD()
        {
            var mb_herst = String.Empty;
            var mb_mod = String.Empty;
            var alles = String.Empty;

            try
            {
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string LW1()
        {
            var lw = String.Empty;
            try
            {
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string LW2()
        {
            var lw = String.Empty;
            try
            {
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
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetWindwosClientVersion()
        {
            try
            {
                string test = new ComputerInfo().OSFullName;
                return test;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
