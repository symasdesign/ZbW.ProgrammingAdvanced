public sealed class CityCollection : IEnumerable<string> {
    private readonly string[] cities = { "Bern", "Basel", "Zürich", "Rapperswil", "Genf" };
    public IEnumerator<string> GetEnumerator() => new CityEnumerator(cities);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
public sealed class CityEnumerator : IEnumerator<string> {
    private readonly string[] cities;
    private int position = -1;
    public CityEnumerator(string[] cities) => this.cities = cities;
    public string Current {
        get {
            if (position < 0 || position >= cities.Length)
                throw new InvalidOperationException("Kein aktuelles Element.");
            return cities[position];
        }
    }
    object System.Collections.IEnumerator.Current => Current;
    public bool MoveNext() {
        if (position < cities.Length)
            position++;
        return position < cities.Length;
    }
    public void Reset() => position = -1;
    public void Dispose() {
    }
}
