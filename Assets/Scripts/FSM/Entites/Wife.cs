using System;

namespace FSMTest
{
    public enum Locations
    {
        bathroom,
        house,
        kitchen
    }
    class Wife : BaseEntity
    {
        private StateMachine<Wife> FSM;
        private bool isCooking;
        public Locations m_pLocation;
        public Wife(int ID) : base(ID)
        {
            m_pLocation = Locations.house;
            isCooking = false;
            FSM = new StateMachine<Wife>(this);
            FSM.SetCurrentState(DoHousework.Instance);
            FSM.SetGlobalState(WifeGlobalState.Instance);
        }

        public StateMachine<Wife> GetFSM() { return FSM; }
        public void ChangeState(State<Wife> sm)
        {
            FSM.ChangeState(sm);
        }
        public void ReverttoPreviousState()
        {
            FSM.RevertToPreviousState();
        }
        public override void Update()
        {
            FSM.Update();
        }

        public override bool HandleMessage(Telegram msg)
        {
            return FSM.HandleMessage(msg);
        }
        public bool Cooking() { return isCooking; }
        public void SetCooking(bool val) { isCooking = val; }
    }
}
