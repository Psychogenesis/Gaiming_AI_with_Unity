using System;
using System.ComponentModel.DataAnnotations;

namespace FSMTest
{
    public enum msg_type
    {
        [Display(Name = "Hi Honey, I'm home!")]
        MSG_HIHONEYIMHOME,
        [Display(Name = "Stew is ready!")]
        MSG_STEWREADY
    }
    public class MsgType
    {
        public static string GetMsgName(msg_type msg)
        {
            switch (msg)
            {
                case msg_type.MSG_HIHONEYIMHOME:
                    return "Hi Honey, I'm home!";
                case msg_type.MSG_STEWREADY:
                    return "Stew is ready!";
                default:
                    return "UNKNOWN!";
            }
        }
    }


    public class Telegram
    {
        public int Sender;
        public int Reciever;
        public msg_type Msg;
        public double DispatchTime;
        public object ExtraInfo;

        public Telegram (double time, int sender, int reciever, msg_type msg, object info)
        {
            DispatchTime = time;
            Sender = sender;
            Reciever = reciever;
            Msg = msg;
            ExtraInfo = info;
        }

        public const double SMALLESTDELAY = 0.25;
    }
}
