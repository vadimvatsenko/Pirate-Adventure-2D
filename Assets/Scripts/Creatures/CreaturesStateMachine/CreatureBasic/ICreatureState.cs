namespace Creatures.Player
{
    public interface ICreatureState
    {
        void Enter();
        void Update();
        void Exit();
    }
}