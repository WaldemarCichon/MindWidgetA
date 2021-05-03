using System;
using System.Collections.Generic;
using System.Timers;
using Android.App;
using MindWidgetA.Texts;
using MindWidgetA.Tooling;
using Xamarin.Essentials;

namespace MindWidgetA.StateMachine
{
    public class StateMachine
    {
        private static Timer timer;
        private static States LastQuestionTaskState = States.TASK;

        public static States CurrentState { get; set; }
        public static StateMap StateMap;
        private static AbstractUI UI { get; set; }
        private static Affirmations affirmations;
        private static Questions questions;
        private static Tasks tasks;
        private static Statistics statistics;

        private StateTransitions GreetingsStateTransitions = new StateTransitions(States.GREETINGS).
            Add(new Transition(States.AFFIRMATION, Events.HappyButtonPressed, () => { return true; }, () => { statistics.IncrementMindState(Events.HappyButtonPressed) ; setAffirmationState(TimeConstants.DURATION_GOOD); })).
            Add(new Transition(States.AFFIRMATION, Events.NeutralButtonPressed, () => { return true; }, () => { statistics.IncrementMindState(Events.NeutralButtonPressed) ; setAffirmationState(TimeConstants.DURATION_MIDDLE); })).
            Add(new Transition(States.AFFIRMATION, Events.SadButtonPressed, () => { return true; }, () => { statistics.IncrementMindState(Events.SadButtonPressed); setAffirmationState(TimeConstants.DURATION_BAD); }));
        private StateTransitions AffirmationStateTransistions = new StateTransitions(States.AFFIRMATION).
            Add(new Transition(States.QUESTION, Events.TimeEllapsed, () => { return LastQuestionTaskState == States.TASK; }, () => { setQuestionState(); })).
            Add(new Transition(States.TASK, Events.TimeEllapsed, () => { return LastQuestionTaskState == States.QUESTION; }, () => { setTaskState(); })).
            Add(new Transition(States.INFO, Events.InfoButtonPressed, () => { return true; }, () => { setInfoState(); }));

        private StateTransitions TaskStateTransistions = new StateTransitions(States.TASK).
            Add(new Transition(States.AFFIRMATION, Events.YesButtonPressed, () => { return true; }, () => { setTaskAnswer(true); })).
            Add(new Transition(States.AFFIRMATION, Events.NoButtonPressed, () => { return true; }, () => { setTaskAnswer(false); })).
            Add(new Transition(States.CHOOSE_LATER, Events.ChooseLaterPressed, () => { return true; }, () => { }));
        private StateTransitions QuestionStateTransitions = new StateTransitions(States.QUESTION).
            Add(new Transition(States.AFFIRMATION, Events.YesButtonPressed, () => { return true; }, () => { setQuestionAnswer(true); })).
            Add(new Transition(States.AFFIRMATION, Events.NoButtonPressed, () => { return true; }, () => { setQuestionAnswer(false); })).
            Add(new Transition(States.CHOOSE_LATER, Events.ChooseLaterPressed, () => { return true; }, setChooseLaterState));
        private StateTransitions InfoStateTransisions = new StateTransitions(States.INFO).
            Add(new Transition(States.AFFIRMATION, Events.BackButtonPressed, () => { return true; }, () => { setAffirmationState(-1); })).
            Add(new Transition(States.QUESTION, Events.TimeEllapsed, () => { return LastQuestionTaskState == States.TASK; }, () => { setQuestionState(); })).
            Add(new Transition(States.TASK, Events.TimeEllapsed, () => { return LastQuestionTaskState == States.QUESTION; }, () => { setTaskState(); }));
        private static int currentTimerDuration;

        public StateMachine(AbstractUI ui)
        {
            CurrentState = States.GREETINGS;
            UI = ui;
            createTimer();
            InitStateMap();
            affirmations = new Affirmations();
            questions = new Questions();
            tasks = new Tasks();
            statistics = new Statistics();
        }

        private bool True()
        {
            return true;
        }

        private void InitStateMap()
        {
            StateMap = new StateMap();
            StateMap.Add(GreetingsStateTransitions);
            StateMap.Add(AffirmationStateTransistions);
            StateMap.Add(QuestionStateTransitions);
            StateMap.Add(TaskStateTransistions);
            StateMap.Add(InfoStateTransisions);
        }

        private void createTimer()
        {
            timer = new Timer();
            timer.Elapsed += (object o, ElapsedEventArgs a) => { MainThread.BeginInvokeOnMainThread(() => PushEvent(Events.TimeEllapsed)); timer.Interval = 0; };
        }

        private static void setTimer(int seconds)
        {
            currentTimerDuration = seconds;
            if (timer.Interval>0)
            {
                timer.Stop();
            }
            timer.AutoReset = false;
            timer.Interval = seconds * 1000;
            timer.Start();
        }

        private static void setAffirmationState(int seconds) 
        {
            if (seconds != -1)
            {
                setTimer(seconds);
            }
            UI.HappyButton.Visibility = Android.Views.ViewStates.Gone;
            UI.NeutralButton.Visibility = Android.Views.ViewStates.Gone;
            UI.SadButton.Visibility = Android.Views.ViewStates.Gone;
            UI.OkButton.Visibility = Android.Views.ViewStates.Gone;
            UI.NoButton.Visibility = Android.Views.ViewStates.Gone;
            UI.LaterButton.Visibility = Android.Views.ViewStates.Gone;
            UI.InfoButton.Visibility = Android.Views.ViewStates.Visible;
            UI.Background.SetImageResource(Resource.Drawable.affirmations);
            UI.MainText.Text = affirmations.Random;
        }


        private static void setQuestionTaskState(string random, int backgroundPicId)
        {
            UI.InfoButton.Visibility = Android.Views.ViewStates.Gone;
            UI.OkButton.Visibility = Android.Views.ViewStates.Visible;
            UI.NoButton.Visibility = Android.Views.ViewStates.Visible;
            UI.LaterButton.Visibility = Android.Views.ViewStates.Visible;
            UI.BackButton.Visibility = Android.Views.ViewStates.Gone;
            // UI.backgroundImageView.SetImageResource(backgroundPicId);
            UI.MainText.Text = random;
        }

        private static void setQuestionState()
        {
            setQuestionTaskState(questions.Random, Resource.Drawable.questions);
            LastQuestionTaskState = States.QUESTION;
            UI.Background.SetImageResource(Resource.Drawable.questions);
        }

        private static void setTaskState()
        {
            setQuestionTaskState(tasks.Random, Resource.Drawable.tasks);
            LastQuestionTaskState = States.TASK;
            UI.Background.SetImageResource(Resource.Drawable.tasks);
        }

        private static void setInfoState()
        {
            UI.MainText.Text = statistics.InfoText;
            UI.InfoButton.Visibility = Android.Views.ViewStates.Gone;
            UI.BackButton.Visibility = Android.Views.ViewStates.Visible;
            UI.Background.SetImageResource(Resource.Drawable.hg4);
        }

        private static void setTaskAnswer(bool answer)
        {
            statistics.IncrementTask(answer);
            setAffirmationState(currentTimerDuration);
        }

        private static void setQuestionAnswer(bool answer)
        {
            statistics.IncrementQuestion(answer);
            setAffirmationState(currentTimerDuration);
        }

        private static void setChooseLaterState()
        {

        }

        public void PushEvent(Events _event)
        {
            var newState = StateMap[CurrentState].PushEvent(_event);
            if (newState != States.NONE)
            {
                CurrentState = newState;
            }
        }

    }
}
