internal static class Program {
    private static void Main() {
        var a = new Vektor(1, 2, 3);
        var b = new Vektor(4, 5, 6);
        Console.WriteLine(a + b);
        Console.WriteLine(a - b);
        Console.WriteLine(a * 2);
        Console.WriteLine(a * b);
        Console.WriteLine(b * a);
        Console.WriteLine((double)a);
        Vektor e = 4;
        e[1] = 7;
        Console.WriteLine(e);
    }
}
