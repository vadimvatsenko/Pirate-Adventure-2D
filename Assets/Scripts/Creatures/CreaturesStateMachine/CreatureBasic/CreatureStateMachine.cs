namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class CreatureStateMachine
    {
        public CreatureState CurrentState { get; private set; }
        public CreatureState PreviousState { get; private set; }

        private bool _canChangeState;

        public void Initialize(CreatureState initialState)
        {
            _canChangeState = true;
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(CreatureState newState)
        {
            if(!_canChangeState) return;
            
            PreviousState = CurrentState;
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void UpdateActiveState()
        {
            CurrentState.Update();
        }
        public void SwitchOffStateMachine() => _canChangeState = false;
    }
}