using System;
namespace MindWidgetA.StateMachine
{
    public class Transition
    {
        public States ToState { get; private set; }
        private Func<bool> Condition { get; set; }
        public Action UpdateUI { get; private set; }
        public Events Event { get; private set; }

        public Transition(States toState, Events _event, Func<bool> condition, Action updateUI)
        {
            ToState = toState;
            Condition = condition;
            UpdateUI = updateUI;
            Event = _event;
        }

        public bool IsValid
        {
            get
            {
                return Condition();
            }
        }
    }
}
