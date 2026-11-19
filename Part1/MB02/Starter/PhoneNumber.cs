namespace Course;
// Werte bleiben stabil, solange die Instanz als Dictionary-Schlüssel lebt.
// Modell aus der alten Aufgabe: AreaCode, Exchange, SubscriberNumber.
// Es erfolgt KEINE Normalisierung internationaler Telefonnummern.
public sealed class PhoneNumber : IEquatable<PhoneNumber> {
    public string AreaCode {
        get;
    }
    public string Exchange {
        get;
    }
    public string SubscriberNumber {
        get;
    }
    public PhoneNumber(string areaCode, string exchange, string subscriberNumber) {
        ArgumentNullException.ThrowIfNull(areaCode);
        ArgumentNullException.ThrowIfNull(exchange);
        ArgumentNullException.ThrowIfNull(subscriberNumber);
        AreaCode = areaCode;
        Exchange = exchange;
        SubscriberNumber = subscriberNumber;
    }
    public bool Equals(PhoneNumber? other) =>
        ReferenceEquals(this, other); // TODO: Wertgleichheit ergänzen.
    public override bool Equals(object? obj) =>
        obj is PhoneNumber number && Equals(number);
    public override int GetHashCode() =>
        base.GetHashCode(); // TODO: Zu Equals passend implementieren.
    public static bool operator ==(PhoneNumber? left, PhoneNumber? right) =>
        ReferenceEquals(left, right) ||
        (left is not null && left.Equals(right));
    public static bool operator !=(PhoneNumber? left, PhoneNumber? right) =>
        !(left == right);
    public override string ToString() => $"{AreaCode}-{Exchange}-{SubscriberNumber}";
}
