namespace FSMTest
{
    class StateMachine<T>
    {
        private T m_pOwner;
        private State<T> m_pCurrentState;
        private State<T> m_pPreviousState;
        private State<T> m_pGlobalState;

        public StateMachine(T owner)
        {
            m_pOwner = owner;
            m_pCurrentState = null;
            m_pPreviousState = null;
            m_pGlobalState = null;
        }

        public void SetCurrentState(State<T> s) { m_pCurrentState = s; }
        public void SetPreviousState(State<T> s) { m_pPreviousState = s; }
        public void SetGlobalState(State<T> s) { m_pGlobalState = s; }

        public void Initialise(T owner, State<T> InitialState)
        {
            m_pOwner = owner;
            ChangeState(InitialState);
        }

        public void Update()
        {
            m_pGlobalState?.Execute(m_pOwner);
            m_pCurrentState?.Execute(m_pOwner);
        }

        public bool HandleMessage(Telegram msg)
        {
            if (m_pCurrentState != null && m_pCurrentState.OnMessage(m_pOwner, msg))
                return true;
            if (m_pGlobalState != null && m_pGlobalState.OnMessage(m_pOwner, msg))
                return true;
            return false;
        }

        public void ChangeState(State<T> pNewState)
        {
            m_pPreviousState = m_pCurrentState;
            m_pCurrentState?.Exit(m_pOwner);
            m_pCurrentState = pNewState;
            m_pCurrentState?.Enter(m_pOwner);
        }
        public void RevertToPreviousState()
        {
            if(m_pPreviousState != null)
                m_pCurrentState = m_pPreviousState;
            else throw new System.ArgumentException("Parameter cannot be null", "original");
        }
    }
}
