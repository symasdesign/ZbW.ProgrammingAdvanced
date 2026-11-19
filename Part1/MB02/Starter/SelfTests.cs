namespace Course;

public static class SelfTests {
    private static PhoneNumber Number() => new("123", "456", "7890");
    public static int Run() {
        Check.Run("Referenz ist gleich", () => {
            var a = Number();
            var b = a;
            Check.True(ReferenceEquals(a, b));
            Check.True(a == b);
        });
        Check.Run("Gleicher Wert, andere Instanz", () => {
            var a = Number();
            var b = Number();
            Check.True(!ReferenceEquals(a, b));
            Check.True(a.Equals(b));
        });
        Check.Run("Alle drei Felder zählen", () => {
            var a = Number();
            Check.True(!a.Equals(new PhoneNumber("999", "456", "7890")));
            Check.True(!a.Equals(new PhoneNumber("123", "999", "7890")));
            Check.True(!a.Equals(new PhoneNumber("123", "456", "9999")));
        });
        Check.Run("Equals object und Fremdtyp", () => {
            object a = Number();
            object b = Number();
            Check.True(a.Equals(b));
            Check.True(!a.Equals("123-456-7890"));
        });
        Check.Run("Null und Operatoren", () => {
            PhoneNumber? a = null;
            PhoneNumber? b = null;
            var c = Number();
            var d = Number();
            Check.True(a == b);
            Check.True(a != c);
            Check.True(c != a);
            Check.True(!c.Equals(null));
            Check.True(c == d);
            Check.True(!(c != d));
        });
        Check.Run("Äquivalenzrelation", () => {
            var a = Number();
            var b = Number();
            var c = Number();
            Check.True(a.Equals(a));
            Check.True(a.Equals(b) && b.Equals(a));
            Check.True(a.Equals(b) && b.Equals(c) && a.Equals(c));
        });
        Check.Run("Gleiche Werte gleicher Hash", () =>
            Check.Equal(Number().GetHashCode(), Number().GetHashCode()));
        Check.Run("Dictionary findet anderen gleichen Schlüssel", () => {
            var book = new Dictionary<PhoneNumber, string>();
            book.Add(Number(), "Gordon");
            Check.Equal("Gordon", book[Number()]);
        });
        Check.Run("Dictionary lehnt Duplikat ab", () => {
            var book = new Dictionary<PhoneNumber, string>();
            book.Add(Number(), "Gordon");
            Check.Throws<ArgumentException>(() => book.Add(Number(), "Samus"));
        });
        Check.Run("Kollision verliert keinen Schlüssel", () => {
            var book = new Dictionary<PhoneNumber, string>(new CollisionComparer());
            book.Add(Number(), "A");
            book.Add(new PhoneNumber("111", "222", "3333"), "B");
            Check.Equal(2, book.Count);
            Check.Equal("A", book[Number()]);
            Check.Equal("B", book[new PhoneNumber("111", "222", "3333")]);
        });
        Check.Run("Shallow teilt Adresse", () => {
            var a = new Person("Alex", 30, new Address(9000, "St. Gallen"));
            var b = a.ShallowCopy();
            b.Age = 31;
            b.Address.City = "Bern";
            Check.True(!ReferenceEquals(a, b));
            Check.True(ReferenceEquals(a.Address, b.Address));
            Check.Equal(30, a.Age);
            Check.Equal("Bern", a.Address.City);
        });
        Check.Run("Deep trennt Adresse", () => {
            var a = new Person("Alex", 30, new Address(9000, "St. Gallen"));
            var b = a.DeepCopy();
            b.Address.City = "Bern";
            b.Name = "Kim";
            Check.True(!ReferenceEquals(a.Address, b.Address));
            Check.Equal("St. Gallen", a.Address.City);
            Check.Equal("Alex", a.Name);
        });
        return Check.Finish();
    }
}
