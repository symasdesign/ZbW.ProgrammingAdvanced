namespace Course.Shop;

public static class CompositionRoot {
    public static Shopper CreateShopper(ICreditCard card) {
        return new Shopper(card);
    }
}
