using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard
{
    public class VirtualList<T> : IList<T>
    {
        private IList<T> source;
        private VirtualListLoader<T> loader;

        public VirtualList(VirtualListLoader<T> loader)
        {
            this.loader = loader;
        }

        private IList<T> Source
        {
            get
            {
                if (this.source == null)
                    this.source = loader.Load();
                return this.source;
            }
        }

        public T this[int i]
        {
            get
            {
                return this.Source[i];
            }
            set
            {
                this.Source[i] = value;
            }
        }

        public int IndexOf(T item)
        {
            return this.Source.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.Source.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.Source.RemoveAt(index);
        }

        public void Add(T item)
        {
            this.Source.Add(item);
        }

        public void Clear()
        {
            this.Source.Clear();
        }

        public bool Contains(T item)
        {
            return this.Source.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Source.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.Source.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.Source.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return this.Source.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Source.GetEnumerator();
        }
    }
}
