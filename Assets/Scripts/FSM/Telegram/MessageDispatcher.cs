using System;
using System.Collections.Generic;

namespace FSMTest
{
    class MessageDispatcher
    {
        public const float SEND_MSG_IMMEDIATELY = 0.0f;
        public const int NO_ADDITIONAL_INFO = 0;

        public static MessageDispatcher Instance { get; }
        static MessageDispatcher()
        {
            Instance = new MessageDispatcher();
        }

        EntityManager EntityMgr = EntityManager.Instance;
        private SortedSet<Telegram> PriorityQ = new SortedSet<Telegram>();        

        private void Discharge(BaseEntity pReciever, Telegram msg)
        {
            if (!pReciever.HandleMessage(msg))
                Console.WriteLine("Message not handled!");
        }

        public void DispatchMessage(double delay, int sender, int reciever, msg_type msg, object ExtraInfo)
        {
            BaseEntity pSender = EntityMgr.GetEntityFromID(sender);
            BaseEntity pReciever = EntityMgr.GetEntityFromID(reciever);

            if (pReciever == null)
                Console.WriteLine("Warning! No Reciever with ID " + reciever + " is found!");

            Telegram telegram = new Telegram(0, sender, reciever, msg, ExtraInfo);

            if (delay <= 0.0f)
            {
                Console.WriteLine("Instant telegram dispatched at time: " + string.Format(" {0:HH:mm:ss tt}", DateTime.Now) + " by " + EntityType.GetEntityName(pSender.ID)
                                    + " for " + EntityType.GetEntityName(pReciever.ID) + ". Message is: " + MsgType.GetMsgName(msg));
                Discharge(pReciever, telegram);
            }
            else
            {
                double currenttime = CrudeTimer.Instance.GetCurrentTime();
                telegram.DispatchTime = currenttime + delay;
                PriorityQ.Add(telegram);
                Console.WriteLine("Delayed telegram from " + EntityType.GetEntityName(pSender.ID) + " recorded at time " + string.Format(" {0:HH:mm:ss tt}", CrudeTimer.Instance.GetCurrentTime()) + " for " + EntityType.GetEntityName(pReciever.ID)
                                    + ". Message is: " + MsgType.GetMsgName(msg));
            }
        }

        public void DispatchDelayedMessage()
        {
            double CurrentTime = CrudeTimer.Instance.GetCurrentTime();
            var numerator = PriorityQ.GetEnumerator(); 

            while(numerator.MoveNext())
            {
                Telegram telegram = numerator.Current;
                BaseEntity pReciever = EntityMgr.GetEntityFromID(telegram.Reciever);

                Console.WriteLine("Queued telegram ready for dispatch: Sent to " + EntityType.GetEntityName(pReciever.ID) + ". Msg is " + MsgType.GetMsgName(telegram.Msg));

                Discharge(pReciever, telegram);
            }
            PriorityQ.Clear();
        }            
    }
}
