using System;

namespace FSMTest
{
    class CrudeTimer
    {
        public static CrudeTimer Instance { get; }
        static CrudeTimer()
        {
            Instance = new CrudeTimer();
        }

        private double m_dStartTime;

        private CrudeTimer()
        {
            m_dStartTime = DateTime.Now.Ticks * 0.001;
        }

        public double GetCurrentTime()
        {
            return DateTime.Now.Ticks * 0.001 - m_dStartTime;
        }
    }
}
