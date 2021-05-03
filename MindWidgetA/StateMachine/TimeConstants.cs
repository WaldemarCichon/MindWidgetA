using System;
namespace MindWidgetA.StateMachine
{
    public class TimeConstants
    {
        const bool is_test = true;


        public const int MAIN_UNIT = is_test ? 10 : 60 * 60;
        public const int START = 1;
        public const int MIDDAY = 14;
        public const int END = 21;
        public const int DURATION_BAD = (int) 3 * MAIN_UNIT / 2;
        public const int DURATION_MIDDLE = MAIN_UNIT * 2;
        public const int DURATION_GOOD = 3 * MAIN_UNIT;
        public const int WAIT_TIME = is_test ? 20 : 60 * 20;
        
        private TimeConstants()
        {
        }
    }
}
