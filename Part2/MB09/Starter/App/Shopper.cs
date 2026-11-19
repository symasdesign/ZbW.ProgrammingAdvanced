namespace Course.Shop;

public interface ITerminal {
    string Charge(decimal amount, string cardName);
}

public sealed class EftTerminal : ITerminal {
    public string Charge(decimal amount, string cardName) =>
        $"{cardName}: {amount:0.00} CHF bezahlt";
}

public interface ICreditCard {
    string Pay(decimal amount);
}

public sealed class Visa(ITerminal terminal) : ICreditCard {
    public string Pay(decimal amount) => terminal.Charge(amount, "Visa");
}

public sealed class MasterCard(ITerminal terminal) : ICreditCard {
    public string Pay(decimal amount) => terminal.Charge(amount, "MasterCard");
}

public sealed class Shopper(ICreditCard card) {
    public string Checkout(decimal amount) => card.Pay(amount);
}
