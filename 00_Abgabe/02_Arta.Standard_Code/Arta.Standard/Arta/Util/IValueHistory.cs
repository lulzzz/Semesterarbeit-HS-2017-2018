namespace Arta.Util
{
    public interface IValueHistory<T>
    {
        T Get(int index);
        void Add(T item);
        int Size();
    }
}
