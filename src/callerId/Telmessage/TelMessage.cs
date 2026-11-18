using System.Collections.Generic;

namespace callerId.Telmessage
{

    public struct PhoneStatu
    {
        public int Id;
        public bool IsRing;
        public string PhoneNum;
        public string HookStatu;
    }

    public interface ITelMessage
    {
        int GetCount();

        List<PhoneStatu> GetTelNum();

        bool IsSw(int type);

        void FreeCID();
    }
}