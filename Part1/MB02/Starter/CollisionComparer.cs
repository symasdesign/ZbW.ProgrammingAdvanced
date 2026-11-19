namespace Course;
// Bewusst schlechte Verteilung; trotzdem konsistent mit Equals.
// Nur um Kollisionen deterministisch zu zeigen, nie für Produktivcode.
public sealed class CollisionComparer : IEqualityComparer<PhoneNumber> {
    public bool Equals(PhoneNumber? x, PhoneNumber? y) => x == y;
    public int GetHashCode(PhoneNumber obj) => 0;
}
