using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMTest
{
    class WifeGlobalState : State<Wife>
    {
        Random random = new Random();
        public static WifeGlobalState Instance { get; }
        static WifeGlobalState() { Instance = new WifeGlobalState(); }

        public override void Enter(Wife wife){}

        public override void Execute(Wife wife)
        {
            if(random.Next(1,10) <= 1)
                wife.GetFSM().ChangeState(VisitBathroom.Instance);
        }

        public override void Exit(Wife wife){}

        public override bool OnMessage(Wife wife, Telegram msg)
        {
            switch(msg.Msg)
            {
                case (int)msg_type.MSG_HIHONEYIMHOME:
                    {
                        Console.WriteLine("Message handled by " + EntityType.GetEntityName(wife.ID) + " at time: " + CrudeTimer.Instance.GetCurrentTime());
                        Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Hi honey. Let me make you some of mah fine country stew");
                        wife.GetFSM().ChangeState(CookStew.Instance);
                        return true;
                    }    
            }
            return false;
        }
    }
    class DoHousework : State<Wife>
    {
        Random random = new Random();
        public static DoHousework Instance { get; }
        static DoHousework() { Instance = new DoHousework(); }

        public override void Enter(Wife wife)
        {
            if (wife.m_pLocation != Locations.house)
            {
                Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Gonna do some cleanin'.");
                wife.m_pLocation = Locations.house;
            }
        }

        public override void Execute(Wife wife)
        {
            switch(random.Next(0,2))
            {
                case 0:
                    {
                        Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Moppin' the floor.");
                        return;
                    }
                case 1:
                    {
                        Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Washin' the dishes.");
                        return;
                    }
                case 2:
                    {
                        Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Makin' the bed.");
                        return;
                    }                    
                default:
                    return;
            }
        }

        public override void Exit(Wife wife)
        {
            Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Thats enough cleanin' for now.");
        }

        public override bool OnMessage(Wife wife, Telegram msg)
        {
            return false;
        }
    }

    class VisitBathroom : State<Wife>
    {
        public static VisitBathroom Instance { get; }

        static VisitBathroom() { Instance = new VisitBathroom(); }

        public override void Enter(Wife wife)
        {
            if (wife.m_pLocation != Locations.bathroom)
            {
                Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Walkin' to the can. Need to powda mah pretty li'l nose.");
                wife.m_pLocation = Locations.bathroom;
            }
        }

        public override void Execute(Wife wife)
        {
            Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Ahhhhhh! Sweet relief!");
            wife.ReverttoPreviousState();
        }

        public override void Exit(Wife wife)
        {
            Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Leavin' the john");
        }
        public override bool OnMessage(Wife wife, Telegram msg)
        {
            return false;
        }
    }

    class CookStew : State<Wife>
    {
        Random random = new Random();

        public static CookStew Instance { get; }
        static CookStew() { Instance = new CookStew(); }
        public override void Enter(Wife wife)
        {
            if(!wife.Cooking())
            {
                Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": Putting the stew in the oven");
                MessageDispatcher.Instance.DispatchMessage(1.5, wife.ID, wife.ID, msg_type.MSG_STEWREADY, MessageDispatcher.NO_ADDITIONAL_INFO);
                wife.SetCooking(true);
            }
        }
        public override void Execute(Wife wife)
        {
            Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": : Fussin' over food.");
        }
        public override void Exit(Wife wife)
        {
            Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": : Puttin' the stew on the table.");
        }
        public override bool OnMessage(Wife wife, Telegram msg)
        {
            switch(msg.Msg)
            {
                case msg_type.MSG_STEWREADY:
                    Console.WriteLine("Message received by " + EntityType.GetEntityName(wife.ID) + "at time " + CrudeTimer.Instance.GetCurrentTime());
                    Console.WriteLine(EntityType.GetEntityName(wife.ID) + ": StewReady! Lets eat.");
                    MessageDispatcher.Instance.DispatchMessage(MessageDispatcher.SEND_MSG_IMMEDIATELY, wife.ID, (int)EntityNameType.MINER, msg_type.MSG_STEWREADY, MessageDispatcher.NO_ADDITIONAL_INFO);
                    wife.SetCooking(false);
                    wife.GetFSM().ChangeState(DoHousework.Instance);
                    return true;

            }
            return false;
        }
    }
}
