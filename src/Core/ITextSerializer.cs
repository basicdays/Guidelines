namespace Guidelines.Core
{
    public interface ITextSerializer
    {
        string Serialize<T>(T obj);
    }
}
