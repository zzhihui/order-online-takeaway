using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace callerId.Telmessage
{
    public class RlRecoder2: ITelMessage
    {
        private readonly List<string> _phoneList = new List<string>();

        [DllImport("RLRecoder.dll")]
        private static extern bool SW_Init();

        [DllImport("RLRecoder.dll")]
        private static extern int SW_GetCount();

        [DllImport("RLRecoder.dll")]
        private static extern int SW_CheckOffHook(int i);

        [DllImport("RLRecoder.dll")]
        private static extern int SW_ReceiveCID(int i, [Out] byte[] c);

        [DllImport("RLRecoder.dll")]
        private static extern int SW_SetType(int i);

        public int GetCount()
        {
            return RlRecoder2.SW_GetCount();
        }

        public List<PhoneStatu> GetTelNum()
        {
            int count = RlRecoder2.SW_GetCount();
            this._phoneList.Clear();
            List<PhoneStatu> list = new List<PhoneStatu>();
            for (int i = 0; i < count; ++i)
            {
                PhoneStatu phoneStatu = new PhoneStatu();
                byte[] c = new byte[16];
                StringBuilder stringBuilder = new StringBuilder();
                if (RlRecoder2.SW_ReceiveCID(i, c) > 0)
                {
                    foreach (char ch in c)
                        stringBuilder.Append(ch);
                }
                phoneStatu.Id = i;
                string str = stringBuilder.ToString();
                if (this._phoneList.Contains(str))
                {
                    phoneStatu.PhoneNum = "";
                    phoneStatu.HookStatu = "0";
                }
                else
                {
                    phoneStatu.PhoneNum = str;
                    this._phoneList.Add(str);
                    phoneStatu.HookStatu = RlRecoder2.SW_CheckOffHook(i).ToString();
                }
                phoneStatu.IsRing = false;
                list.Add(phoneStatu);
            }
            return list;
        }

        public bool IsSw(int type)
        {
            try
            {
                bool flag = RlRecoder2.SW_Init();
                if (RlRecoder2.SW_Init())
                    RlRecoder2.SW_SetType(Convert.ToInt32(type));
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public void FreeCID()
        {
        }
    }
}