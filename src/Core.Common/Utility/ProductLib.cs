using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using Core.Common.Utility;

namespace Core.Common.Net4.Utility
{
    [ComVisible(false)]
    public class ProductLib
    {
        private const int AlgorithmVersion = 2;

        private enum EnHwiDcodes
        {
            EnHWmotherboard = 6,
            EnHWbios = 1,
            EnHWhdd = 2,
            EnHWvideo = 3,
            EnHWlan = 4,
            EnHWcpu = 5
        };


        private readonly byte[] _bitarray = new byte[8]
		{
			1,2,4,8,16,32,64,128
		};


        private readonly SortedList _alphanumeric = new SortedList();


        private ProductLib()
        {
            Init();
        }


        internal static ProductLib GetInstance()
        {
            return new ProductLib();
        }


        private void Init()
        {
            var _alphanumeric = new char[32]
            {
                '2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','j','k','l','m','n','p','q','r','s','t','u','v','w','x','y','z'
            };

            foreach (var t in _alphanumeric)
            {
                this._alphanumeric.Add(t, t);
            }
        }


        // bitindex is from 0 to 7
        private bool Getbit(byte bte, int bitindex)
        {
            return (bte & _bitarray[bitindex]) != 0;
        }


        private void Setbit(bool bit, ref byte bte, int bitindex)
        {
            if (bit)
                bte = (byte)(bte | _bitarray[bitindex]);
            else
                bte = (byte)(bte & ~_bitarray[bitindex]);
        }
       
        internal void PackBits(ref byte[] buffer, ref int destbitoffset, byte[] bitdata, int sizebits)
        {


            // grow input bits array
            var len = buffer.Length + (destbitoffset + sizebits - 1) / 8;

            var destbyteoffset = buffer.Length - 1;

            if (destbitoffset == 0)
            {
                len++;
                destbyteoffset++;
            }

            if (len != buffer.Length)
            {
                var result = new byte[len];

                Array.Copy(buffer, 0, result, 0, buffer.Length);

                buffer = result;
            }

            var sourcebyteoffset = 0;
            var sourcebitoffset = 0;

            for (var i = 0; i < sizebits; i++)    // copy certain portion of it into the array
            {
                var bit = Getbit(bitdata[sourcebyteoffset], sourcebitoffset);

                Setbit(bit, ref buffer[destbyteoffset], destbitoffset);

                // move to next bit in destination
                destbitoffset++;

                if (destbitoffset == 8)
                {
                    destbitoffset = 0;

                    destbyteoffset++;
                }

                // move to next bit in source;
                sourcebitoffset++;

                if (sourcebitoffset == 8)
                {
                    sourcebitoffset = 0;

                    sourcebyteoffset++;
                }
            }
        }


        private void AddHwidComponent(EnHwiDcodes code, string data, ref byte[] blob, ref int bitoffset)
        {
            // add component code bits
            byte[] codeblob = new byte[1] { (byte)code };

            PackBits(ref blob, ref bitoffset, codeblob, 3);

            // find size of block in bits
            int sizebits = 0;

            switch (code)
            {
                case EnHwiDcodes.EnHWhdd:
                    sizebits = 15;
                    break;

                case EnHwiDcodes.EnHWlan:
                    sizebits = 15;
                    break;

                case EnHwiDcodes.EnHWbios:
                    sizebits = 7;
                    break;

                case EnHwiDcodes.EnHWcpu:
                    sizebits = 7;
                    break;

                case EnHwiDcodes.EnHWmotherboard:
                    sizebits = 7;
                    break;

                case EnHwiDcodes.EnHWvideo:
                    sizebits = 7;
                    break;
            }

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] databytes = Encoding.UTF8.GetBytes(data);

            byte[] hash = md5.ComputeHash(databytes);

            PackBits(ref blob, ref bitoffset, hash, sizebits);
        }


        internal byte[] DecodeAlphaNumeric(string data)
        {
            data = data.ToLower();

            var arr = new ArrayList();

            int bitcount = 0;
            byte encoded = 0;

            foreach (var t in data)
            {
                var character = (byte)_alphanumeric.IndexOfKey(t);

                for (var j = 0; j < 5; j++)
                {
                    var bit = Getbit(character, j);

                    Setbit(bit, ref encoded, bitcount);

                    bitcount++;

                    if (bitcount != 8) continue;
                    arr.Add(encoded);

                    bitcount = 0;
                    encoded = 0;
                }
            }

            return (byte[])arr.ToArray(typeof(byte));
        }


        internal string EncodeAlphaNumeric(byte[] data)
        {
            int bitcount = 0;
            byte encoded = 0;

            var output = new StringBuilder();

            foreach (var t in data)
            {
                for (int j = 0; j < 8; j++)
                {
                    var bit = Getbit(t, j);

                    Setbit(bit, ref encoded, bitcount);

                    bitcount++;

                    if (bitcount != 5) continue;
                    output.Append(_alphanumeric.GetByIndex(encoded));

                    bitcount = 0;
                    encoded = 0;
                }
            }

            return output.ToString().ToUpper();
        }


        static Hardware _hardware = null;


        private static void CheckWmiService()
        {
            var sc = new ServiceController("Winmgmt");

            // ensure service is running
            if (sc.Status != ServiceControllerStatus.Running)
            {
                if (sc.Status == ServiceControllerStatus.PausePending || sc.Status == ServiceControllerStatus.Paused)
                {
                    sc.Continue();
                }
                else
                {
                    try
                    {
                        sc.Start();
                    }
                    catch (System.Exception ex)
                    {
                        string s = ex.Message;

                        if (ex.InnerException != null)
                            s += ex.InnerException.Message;

                        throw new System.Exception(s, ex);
                    }
                }
            }

            sc.Refresh();

            while (sc.Status == ServiceControllerStatus.StartPending || sc.Status == ServiceControllerStatus.ContinuePending ||
                    sc.Status == ServiceControllerStatus.PausePending || sc.Status == ServiceControllerStatus.StopPending)
            {
                Debug.WriteLine(sc.Status.ToString());

                sc.Refresh();
            }
        }


        public static string Generate()
        {
            CheckWmiService();

            const string errorMsgNull = "Cannot access {0} component";




            var lib = new ProductLib();

            var hwidBlob = new byte[0];

            int bitoffset = 0;

            // add version code
            var codeblob = new byte[1] { (byte)AlgorithmVersion };

            lib.PackBits(ref hwidBlob, ref bitoffset, codeblob, 4);

            _hardware = new Hardware();

            string data = null;



            // ***** create fingerprints of individual components *****

            // motherboard serial            
            object product = _hardware.CPUProduct;
            object mbserial = _hardware.CPUSerialNo;

            data = (string)product + (string)mbserial;

            if (data == null)
            {
                throw new System.Exception(String.Format(errorMsgNull, "Motherboard"));
            }
            if (data.Length == 0)
            {
                data = "MBSER";
            }

            lib.AddHwidComponent(EnHwiDcodes.EnHWmotherboard, data, ref hwidBlob, ref bitoffset);


            // network card type/mac address            
            var caption = _hardware.AdapterCaption;
            var mac = _hardware.MACAddress;

            data = (string)caption + (string)mac;

            if (data == null)
            {
                throw new System.Exception(String.Format(errorMsgNull, "Network Card/MAC Address"));
            }
            if (data.Length == 0)
            {
                data = "NETMAC";
            }

            lib.AddHwidComponent(EnHwiDcodes.EnHWlan, data, ref hwidBlob, ref bitoffset);


            // HDD type & serial
            var model = _hardware.DiskModel;

            var serial = _hardware.DiskSerial;

            if (serial != null)     // NB: the serial number call can fail due to permissions...
            {
                data = (string)model + (string)serial;
            }
            else
            {
                Debug.WriteLine("HWID: HDD serial failed");

                data = "HDD FAIL";      // DO NOT CHANGE, this value is checked in CheckTolerence function and HDD check is skipped if found
            }

            if (data == null)
            {
                throw new System.Exception(String.Format(errorMsgNull, "Hard Disk"));
            }
            lib.AddHwidComponent(EnHwiDcodes.EnHWhdd, data, ref hwidBlob, ref bitoffset);


            // CPU type            
            var cpuname = _hardware.CPUName;

            data = (string)cpuname;

            if (data == null)
            {
                throw new System.Exception(String.Format(errorMsgNull, "CPU"));
            }
            if (data.Length == 0)
            {
                data = "CPUT";
            }

            lib.AddHwidComponent(EnHwiDcodes.EnHWcpu, data, ref hwidBlob, ref bitoffset);


            // BIOS serial            
            var biosserial = _hardware.BIOSSerial;

            data = (string)biosserial;

            if (data == null)
            {
                throw new System.Exception(String.Format(errorMsgNull, "BIOS"));
            }
            if (data.Length == 0)
            {
                data = "BIOS";
            }

            lib.AddHwidComponent(EnHwiDcodes.EnHWbios, data, ref hwidBlob, ref bitoffset);


            // videocard name            
            var vidname = _hardware.VideoCardName;

            data = (string)vidname;

            if (data == null)
            {
                throw new System.Exception(String.Format(errorMsgNull, "Videocard"));
            }
            if (data.Length == 0)
            {
                data = "VIDEO";
            }

            lib.AddHwidComponent(EnHwiDcodes.EnHWvideo, data, ref hwidBlob, ref bitoffset);


            // encode binary blob into string
            string result = lib.EncodeAlphaNumeric(hwidBlob);

            Debug.WriteLine("HWID generated: " + result);

            string tmp = string.Empty;

            const int dashLength = 4;
            var position = 0;

            // add dashes to the string
            while (result.Length - position > 0)
            {
                if (tmp.Length > 0)
                {
                    tmp += "-";
                }

                if (result.Length >= (position + dashLength))
                {
                    tmp += result.Substring(position, dashLength);
                }
                else
                {
                    tmp += result.Substring(position, result.Length - position);
                }

                position += dashLength;
            }

            Debug.WriteLine("HWID : " + tmp);

            return tmp;
        }


        /// <summary>
        /// Return the complete description of all the hardware components used.
        /// </summary>
        public static string DebugInfo()
        {
            try
            {
                return _hardware != null ? _hardware.GetDebugInfo() : "Call Generate() method before using DebugInfo method";
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }

     

      
        internal static int RunningInVm(out string components)
        {
            if (_hardware == null)
                Generate();

            components = "";

            // combine hardware values..
            if (_hardware.DiskModel != null)
                components += _hardware.DiskModel.ToString();

            if (_hardware.CPUProduct != null)
                components += _hardware.CPUProduct.ToString();

            if (_hardware.AdapterCaption != null)
                components += _hardware.AdapterCaption.ToString();

            if (_hardware.VideoCardName != null)
                components += _hardware.VideoCardName.ToString();

            if (_hardware.BIOSSerial != null)
                components += _hardware.BIOSSerial.ToString();

            // NB: only check for VMWare and Micrsoft VM

            const string msvm = "microsoft virtual machine";

            const string vmware = "vmware";

            if (components.Trim().ToLower().Contains(msvm))
                return 1;
            return components.Trim().ToLower().Contains(vmware) ? 2 : 0;
        }
    }
    public class HwidComponents
    {
        public int Version;
        public int Motherboard;
        public int Bios;
        public int Video;
        public int Lan;
        public int Hdd;
        public int Cpu;
    }
}
