using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management;

namespace Core.Common.Utility
{
    public class Hardware
    {
        #region Contructor
        /// <summary>
        /// Constructor will generate the HWID fingerprint of each component/
        /// </summary>
        public Hardware()
        {
            GetValues();	// get all the hardware component values required...
        }


        #endregion

        #region Attribs

        object _model;


        public object DiskModel
        {
            get { return _model; }
        }


        object _diskSerial;


        public object DiskSerial
        {
            get { return _diskSerial; }
        }


        object _adapterCaption;

        public object AdapterCaption
        {
            get { return _adapterCaption; }
        }


        object _mac;

        public object MACAddress
        {
            get { return _mac; }
        }


        object _cpuProduct;


        public object CPUProduct
        {
            get { return _cpuProduct; }
        }


        object _mbserial;

        public object CPUSerialNo
        {
            get { return _mbserial; }
        }


        object _biosSerial;

        public object BIOSSerial
        {
            get { return _biosSerial; }
        }

        object _vidname;

        public object VideoCardName
        {
            get { return _vidname; }
        }

        object _cpuName;

        public object CPUName
        {
            get { return _cpuName; }
        }

        public bool HddValuesOk = true;


        // error event...
        public delegate void ExceptionHandler(System.Exception ex);

        public static event ExceptionHandler Error;

        // keep the various WMI objects in memory to access debug data...
        ManagementObjectCollection _win32DiskDrives = null;
        ManagementObjectCollection _win32MediaCollection = null;
        ManagementObjectCollection _networkAdaptersConfig = null;
        ManagementObjectCollection _networkAdapters = null;
        ManagementObjectCollection _win32Baseboards = null;
        ManagementObjectCollection _win32Bios = null;
        ManagementObjectCollection _processors = null;
        ManagementObjectCollection _videoControllers = null;


        #endregion

        #region Methods

        /// <summary>
        /// Returns a string containing all the details of the various hardware components.
        /// </summary>
        public string GetDebugInfo()
        {
            string debugInfo = "";

            debugInfo = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n";

            debugInfo += Environment.OSVersion.Version + "\r\n\r\n";

            // foreach mo in the collection iterate through the properties of that mo
            debugInfo += "\r\nDisk drives\r\n\r\n";

            if (_win32DiskDrives != null)
            {
                debugInfo = _win32DiskDrives.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            debugInfo += "\r\nMedia\r\n\r\n";

            if (_win32MediaCollection != null)
            {
                debugInfo = _win32MediaCollection.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            debugInfo += "\r\nNetwork adpaters\r\n\r\n";

            if (_networkAdaptersConfig != null)
            {
                debugInfo = _networkAdaptersConfig.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }
            else if (_networkAdapters != null)
            {
                debugInfo = _networkAdapters.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            debugInfo += "\r\nMotherboards\r\n\r\n";

            if (_win32Baseboards != null)
            {
                debugInfo = _win32Baseboards.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            debugInfo += "\r\nBios\r\n\r\n";

            if (_win32Bios != null)
            {
                debugInfo = _win32Bios.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            debugInfo += "\r\nProcessors\r\n\r\n";

            if (_processors != null)
            {
                debugInfo = _processors.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            debugInfo += "\r\nVideo Controllers\r\n\r\n";

            if (_videoControllers != null)
            {
                debugInfo = _videoControllers.Cast<ManagementBaseObject>().Aggregate(debugInfo, (current, mo) => current + PrintWmiObjectProperties(mo));
            }

            return debugInfo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        private string PrintWmiObjectProperties(ManagementBaseObject mo)
        {
            string tmp = "";

            // list the properties...
            var propEn = mo.Properties.GetEnumerator();

            for (var j = 0; j < mo.Properties.Count; j++)
            {
                propEn.MoveNext();

                tmp += propEn.Current.Name;

                if (propEn.Current.Value != null)
                {
                    if (propEn.Current.IsArray)
                    {
                        tmp += ": ";

                        switch (propEn.Current.Type)
                        {
                            case CimType.UInt16:
                            {
                                var values = (UInt16[])propEn.Current.Value;

                                tmp = values.Aggregate(tmp, (current, i) => current + ("[" + i.ToString() + "]\t"));
                            }
                                break;
                            case CimType.UInt32:
                            {
                                var values = (UInt32[])propEn.Current.Value;

                                tmp = values.Aggregate(tmp, (current, i) => current + ("[" + i.ToString() + "]\t"));
                            }
                                break;
                            case CimType.String:
                            {
                                var values = (String[])propEn.Current.Value;

                                tmp = values.Aggregate(tmp, (current, s) => current + ("[" + s + "]\t"));
                            }
                                break;
                            default:
                                tmp += "Array type: " + propEn.Current.Type.ToString();
                                break;
                        }
                    }
                    else
                    {
                        tmp += ": " + propEn.Current.Value;
                    }
                }
                else
                {
                    tmp += ": Null";
                }

                tmp += "\r\n";
            }


            tmp += "\r\n--------------------------------------------------------------\r\n";

            return tmp;
        }



        /// <summary>
        /// Gets all the hardware values we need using WMI.
        /// </summary>
        private void GetValues()
        {
            ManagementClass mc = null;
            ManagementObjectCollection.ManagementObjectEnumerator en = null;

            // hard disk
            try
            {
                mc = new ManagementClass("Win32_DiskDrive");

                _win32DiskDrives = mc.GetInstances();

                en = _win32DiskDrives.GetEnumerator();

                en.MoveNext();

                _model = en.Current.Properties["Model"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _model = "";
            }

            try
            {
                mc = new ManagementClass("Win32_PhysicalMedia");

                _win32MediaCollection = mc.GetInstances();

                en = _win32MediaCollection.GetEnumerator();

                en.MoveNext();

                _diskSerial = en.Current.Properties["SerialNumber"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _diskSerial = null;

                _model = "";
            }

            //
            // network card id & the mac address
            //

            // Vista and Longhorn allow us to identify physical network adapters, use this property if OS is either of these...
            if (Environment.OSVersion.Version.Major < 6)
            {
                try
                {
                    mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

                    _networkAdaptersConfig = mc.GetInstances();

                    en = _networkAdaptersConfig.GetEnumerator();

                    // loop through the set of network adapters, try and find a ethernet type adapter...
                    for (int i = 0; i < _networkAdaptersConfig.Count; i++)
                    {
                        en.MoveNext();

                        ManagementBaseObject mo = en.Current;

                        if ((bool)mo.Properties["IPEnabled"].Value == true)		// use IPEnabled property to find the ethernet card..
                        {
                            _adapterCaption = mo.Properties["Caption"].Value;

                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("HWID: " + ex.ToString());

                    if (Error != null)
                    {
                        Error(ex);
                    }

                    _adapterCaption = "";
                }
            }
            else
            {
                try         // check for physical adapter...
                {
                    mc = new ManagementClass("Win32_NetworkAdapter");

                    _networkAdapters = mc.GetInstances();

                    en = _networkAdapters.GetEnumerator();

                    // loop through the set of network adapters, try and find a ethernet type adapter...
                    for (int i = 0; i < _networkAdapters.Count; i++)
                    {
                        en.MoveNext();

                        ManagementBaseObject mo = en.Current;

                        if ((bool)mo.Properties["PhysicalAdapter"].Value == true)		// use IPEnabled property to find the ethernet card..
                        {
                            Debug.WriteLine("HWID: Physical Adapter = true");

                            _adapterCaption = mo.Properties["Caption"].Value;

                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("HWID: " + ex.ToString());

                    if (Error != null)
                    {
                        Error(ex);
                    }

                    _adapterCaption = "";
                }
            }

            //
            // ...now the mac address of the network adapter
            //
            try
            {
                _mac = en.Current.Properties["MACAddress"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _mac = "";
            }

            //
            // motherboard serial
            //
            try
            {
                mc = new ManagementClass("Win32_BaseBoard");

                _win32Baseboards = mc.GetInstances();

                en = _win32Baseboards.GetEnumerator();

                en.MoveNext();

                _cpuProduct = en.Current.Properties["Product"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _cpuProduct = "";
            }

            try
            {
                _mbserial = en.Current.Properties["SerialNumber"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _mbserial = "";
            }

            //
            //	BIOS serial
            //
            try
            {
                mc = new ManagementClass("Win32_BIOS");

                _win32Bios = mc.GetInstances();

                en = _win32Bios.GetEnumerator();

                string serial = string.Empty, version = string.Empty, release_date = string.Empty;

                string desc = string.Empty;

                en.MoveNext();

                ManagementBaseObject mo = en.Current;

                try
                {
                    if (en.Current.Properties["SerialNumber"].Value != null)
                    {
                        serial = en.Current.Properties["SerialNumber"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("HWID: " + ex.ToString());

                    if (Error != null)
                    {
                        Error(ex);
                    }
                }

                try
                {
                    if (en.Current.Properties["Version"].Value != null)
                    {
                        version = en.Current.Properties["Version"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("HWID: " + ex.ToString());

                    if (Error != null)
                    {
                        Error(ex);
                    }
                }

                try
                {
                    if (en.Current.Properties["ReleaseDate"].Value != null)
                    {
                        release_date = en.Current.Properties["ReleaseDate"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("HWID: " + ex.ToString());

                    if (Error != null)
                    {
                        Error(ex);
                    }
                }

                _biosSerial = serial + version + release_date;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _biosSerial = "";
            }

            //
            // videocard name
            //
            try
            {
                mc = new ManagementClass("Win32_VideoController");

                _videoControllers = mc.GetInstances();

                en = _videoControllers.GetEnumerator();
                en.MoveNext();

                _vidname = en.Current.Properties["Name"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _vidname = "";
            }

            //
            // CPU type
            //
            try
            {
                mc = new ManagementClass("Win32_Processor");

                _processors = mc.GetInstances();

                en = _processors.GetEnumerator();

                en.MoveNext();

                _cpuName = en.Current.Properties["Caption"].Value;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HWID: " + ex.ToString());

                if (Error != null)
                {
                    Error(ex);
                }

                _cpuName = "";
            }
        }



        #endregion 
    }
}