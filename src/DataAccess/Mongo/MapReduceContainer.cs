namespace Guidelines.DataAccess.Mongo
{
    public class MapReduceContainer<TKey, TValue>
    {
        public TKey Id { get; set; }
        public TValue value { get; set; }
    }
}