using System.Collections.Generic;

namespace Arta.Util
{
    public class LruCache<K, V>
    {
        readonly Dictionary<K, V> _dict;
        readonly LinkedList<K> _queue = new LinkedList<K>();
        readonly object _syncRoot = new object();
        private readonly int _max;
        public LruCache(int capacity)
        {
            _dict = new Dictionary<K, V>(capacity);
            _max = capacity;
        }

        public void Add(K key, V value)
        {
            lock (_syncRoot)
            {
                CheckCapacity();
                _queue.AddLast(key);                            
                _dict[key] = value;                          
            }
        }

        private void CheckCapacity()
        {
            lock (_syncRoot)
            {
                var count = _dict.Count;                        
                if (count == _max)
                {
                    var node = _queue.First;
                    _dict.Remove(node.Value);                  
                    _queue.RemoveFirst();                       
                }
            }
        }

        public void Delete(K key)
        {
            lock (_syncRoot)
            {
                _dict.Remove(key);                              
                _queue.Remove(key);                             
            }
        }

        public V Get(K key)
        {
            lock (_syncRoot)
            {
                V ret;
                if (_dict.TryGetValue(key, out ret))         
                {
                    _queue.Remove(key);                         
                    _queue.AddLast(key);                       
                }
                return ret;
            }
        }
    }
}
