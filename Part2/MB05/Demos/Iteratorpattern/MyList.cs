using System.Collections;

namespace IteratorMotivation {
    public class MyList : IEnumerable {
        private object[] _data;
        private int _index;

        public MyList() {
            _data = new object[100];
        }

        public void Add(object obj) {
            if (_index == _data.Length) {
                Array.Resize(ref _data, _data.Length * 2);
            }
            _data[_index++] = obj;
        }

        public object this[int index] {
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

        public IEnumerator GetEnumerator() {
            return new MyListEnumerator(this);
        }

        private class MyListEnumerator : IEnumerator {
            private readonly MyList _list;
            public MyListEnumerator(MyList list) {
                this._list = list;
            }
            private int _currentIndex = -1;
            public object Current {
                get {
                    if (this._currentIndex < 0 || this._currentIndex >= this._list._index) {
                        throw new InvalidOperationException();
                    }
                    return _list._data[_currentIndex];
                }
            }

            public bool MoveNext() {
                if (_currentIndex < _list._index) {
                    _currentIndex++;
                }
                return _currentIndex < _list._index;
            }

            public void Reset() {
                this._currentIndex = -1;
            }
        }
    }
}
