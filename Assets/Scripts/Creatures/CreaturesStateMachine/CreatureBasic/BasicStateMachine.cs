namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class BasicStateMachine
    {
        public BasicState CurrentState { get; private set; }
        public BasicState PreviousState { get; private set; }

        private bool _canChangeState;

        public void Initialize(BasicState initialState)
        {
            _canChangeState = true;
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(BasicState newState)
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