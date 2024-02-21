namespace Infrastructure.Interfaces
{
    public interface IInputService
    {
        bool IsSwiping { get; }
        int Direction { get; }
    }
}