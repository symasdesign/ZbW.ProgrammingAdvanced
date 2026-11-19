namespace IteratorMotivation {
    internal class Program {
        static void Main(string[] args) {
            var list = new MyList();
            list.Add("42");
            list.Add("def");
            list.Add("Ada");
            list.Add("ZbW");

            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext()) {
                Console.WriteLine(enumerator.Current);
            }
            foreach (var item in list) {
                Console.WriteLine(item);
            }
            foreach (var item in list) {
                Console.WriteLine(item);
            }

            var genericList = new MyList<string>();

            genericList.Add("42");
            genericList.Add("def");
            genericList.Add("Ada");
            genericList.Add("ZbW");

            using var genericEnumerator = genericList.GetEnumerator();
            while (genericEnumerator.MoveNext()) {
                Console.WriteLine(genericEnumerator.Current);
            }
            foreach (var item in genericList) {
                Console.WriteLine(item);
            }
            foreach (var item in genericList) {
                Console.WriteLine(item);
            }
        }
    }

    public class MyNaiveList {
        public object[] _data;
        private int _index = 0;

        public MyNaiveList() {
            _data = new object[100];
        }

        public void Add(object obj) {
            if (_index == _data.Length) {
                Array.Resize(ref _data, _data.Length * 2);
            }
            _data[_index++] = obj;
        }

    }
}
