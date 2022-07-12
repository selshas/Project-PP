namespace PP
{
    public interface IAbility
    {
        bool IsAvailable();
        bool OnActive();
        void UseStock();
    }
}