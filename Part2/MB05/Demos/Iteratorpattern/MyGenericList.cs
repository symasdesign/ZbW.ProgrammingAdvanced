using System.Collections;

namespace IteratorMotivation {
    public class MyList<T> : IEnumerable<T> {
        private T[] _data;
        private int _index;

        public MyList() {
            _data = new T[100];
        }

        public void Add(T obj) {
            if (_index == _data.Length) {
                Array.Resize(ref _data, _data.Length * 2);
            }
            _data[_index++] = obj;
        }

        public T this[int index] {
            get {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _index);
                return _data[index];
            }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _index);
                _data[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return new MyListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private class MyListEnumerator : IEnumerator<T> {
            private readonly MyList<T> _list;
            public MyListEnumerator(MyList<T> list) {
                this._list = list;
            }
            private int _currentIndex = -1;
            public T Current {
                get {
                    if (this._currentIndex < 0 || this._currentIndex >= this._list._index) {
                        throw new InvalidOperationException();
                    }
                    return _list._data[_currentIndex];
                }
            }

            object IEnumerator.Current => ((IEnumerator<T>)this).Current!;

            public bool MoveNext() {
                if (_currentIndex < _list._index) {
                    _currentIndex++;
                }
                return _currentIndex < _list._index;
            }

            public void Reset() {
                this._currentIndex = -1;
            }

            public void Dispose() {

            }
        }
    }
}
