using System;

namespace FSMTest
{
    public enum Location
    {
        Shack,
        Goldmine,
        Bank,
        Saloon
    }

    public class Miner : BaseEntity
    {
        private StateMachine<Miner> FSM;
        private int GoldCarried;
        private int Thirst;
        private int Fatigue;
        public Location m_Location { get; set; }
        public int MoneyInBank { get; set; }
               
        public Miner (int ID) : base(ID)
        {
            GoldCarried = 0;
            MoneyInBank = 0;
            Thirst = 0;
            Fatigue = 0;
            m_Location = Location.Shack;
            FSM = new StateMachine<Miner>(this);
            FSM.SetCurrentState(GoHomeAndSleepTilRested.Instance);
        }

        public void ChangeState(State<Miner> sm)
        {
            FSM.ChangeState(sm);
        }
        public void ReverttoPreviousState()
        {
            FSM.RevertToPreviousState();
        }

        public override bool HandleMessage(Telegram msg)
        {
            return FSM.HandleMessage(msg);
        }

        public override void Update()
        {
            Thirst++;
            FSM.Update();
        }

        public void AddToGoldCarried(int ammount) => GoldCarried += ammount;
        public void IncreaseFatigue() => Fatigue++;
        public void DecreaseFatigue() => Fatigue--;
        public bool isPocketsFull() => GoldCarried >= 3 ? true : false;
        public bool isThirsty() => Thirst >= 5 ? true : false;
        public bool isWealthy() => MoneyInBank >= 5 ? true : false;
        public bool isFatigued() => Fatigue >= 5 ? true : false;        
        public void DepositGoldInBank()
        {
            MoneyInBank += GoldCarried;
            GoldCarried = 0;
        }
        public void BuyDrinksandWhiskey()
        {
            MoneyInBank -= 2;
            Thirst = 0;
        }
        
    }
}
