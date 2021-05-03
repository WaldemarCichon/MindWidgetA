using System;
using System.Collections.Generic;

namespace MindWidgetA.StateMachine
{
    public class StateTransitions
    {
        public States CurrentState { get; private set; }
        List<Transition> Transitions { get; set; }
        public StateTransitions(States currentState)
        {
            CurrentState = currentState;
            Transitions = new List<Transition>();
        }

        public StateTransitions Add(Transition transition)
        {
            Transitions.Add(transition);
            return this;
        }

        internal States PushEvent(Events _event)
        {
            foreach (var transition in Transitions)
            {
                if (transition.Event == _event && transition.IsValid)
                {
                    transition.UpdateUI();
                    return transition.ToState;
                } 
            }
            return States.NONE;
        }
    }
}
