using UnityEngine;

public class ScenarioStateMachine : MonoBehaviour
{

    public enum ScenarioState
    {
        Idle,
        Active,
        Paused,
        Completed
    }



    public ScenarioState currentState;

    void Start()
    {
        TransitionToState(ScenarioState.Idle);
    }

    public void HandleEvent(string evt)
    {
        switch (currentState)
        {
            case ScenarioState.Idle:
                if (evt == "Start")
                    TransitionToState(ScenarioState.Active);
                break;

            case ScenarioState.Active:
                if (evt == "Pause")
                    TransitionToState(ScenarioState.Paused);
                else if (evt == "Finish")
                    TransitionToState(ScenarioState.Completed);
                break;

            case ScenarioState.Paused:
                if (evt == "Resume")
                    TransitionToState(ScenarioState.Active);
                break;
        }
    }

    void TransitionToState(ScenarioState newState)
    {
        currentState = newState;
    }
}
