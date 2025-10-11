namespace Creatures.Interfaces
{
    public interface IFacingDirection
    {
        int FacingDirection { get; }
        void Flip();
    }
}