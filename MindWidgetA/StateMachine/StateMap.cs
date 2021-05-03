using System;
using System.Collections.Generic;

namespace MindWidgetA.StateMachine
{
    public class StateMap: Dictionary<States, StateTransitions>
    {
        public StateMap()
        {

        }

        public void Add(StateTransitions stateTransitions)
        {
            this.Add(stateTransitions.CurrentState, stateTransitions);
        }
    }
}
