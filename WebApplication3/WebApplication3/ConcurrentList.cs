namespace WebApplication3
{
    public class ConcurrentList<T> : List<T>
    {
        private static List<T> values = new List<T>();
        private static object _Syncobject = new();
        public List<T> forAr
        {
            get;
            set;
        }
        public ConcurrentList()
        {
          List<T> sync = new List<T>();
          values = sync;
        }
        public void Add(T item)
        {
            lock (_Syncobject)
            {
                values.Add(item);
                forArray();
            }
        }
        public void Remove(T item)
        {
            lock (_Syncobject)
            {
                values.Remove(item);
                forArray();
            }
        }
        public void forArray()
        {
            lock (_Syncobject)
            {
                forAr = values;
            }
        }

    }
}
