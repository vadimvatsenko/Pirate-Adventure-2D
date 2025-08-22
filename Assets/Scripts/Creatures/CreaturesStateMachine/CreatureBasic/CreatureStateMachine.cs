namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class CreatureStateMachine
    {
        public CreatureState CurrentState { get; private set; }
        public CreatureState PreviousState { get; private set; }

        public void Initialize(CreatureState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(CreatureState newState)
        {
            PreviousState = CurrentState;
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}