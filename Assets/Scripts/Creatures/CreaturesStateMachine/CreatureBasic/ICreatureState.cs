namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public interface ICreatureState
    {
        void Enter();
        void Update();
        void Exit();
    }
}