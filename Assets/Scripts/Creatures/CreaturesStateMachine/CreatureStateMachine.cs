namespace Creatures.CreaturesStateMachine
{
    public class CreatureStateMachine
    {
        public CreatureState CurrentState { get; private set; }

        public void Initialize(CreatureState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(CreatureState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}