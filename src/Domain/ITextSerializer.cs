namespace Guidelines.Domain
{
    public interface ITextSerializer
    {
        string Serialize<T>(T obj);
    }
}
