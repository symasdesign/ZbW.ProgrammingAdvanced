using Course.Reflection;
using Course.Shop;

internal class Program {
    private static void Main(string[] args) {
        var mode = args.FirstOrDefault() ?? "inspect";
        switch (mode) {
            case "inspect":
                ReflectionDemo.Inspect();
                break;
            case "construct":
                ReflectionDemo.Construct();
                break;
            case "shop":
                ReflectionDemo.Shop();
                break;
            default:
                Console.WriteLine("Aufruf: inspect | construct | shop");
                break;
        }
    }
}
