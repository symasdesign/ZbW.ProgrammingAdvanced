using System.Globalization;
using System.Text;
using Course.Reflection;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "inspect" or "construct" or "graph")) {
            Console.Error.WriteLine("Modus: all | inspect | construct | graph.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            action();
        }

        Run("inspect", ReflectionDemo.Inspect);
        Run("construct", ReflectionDemo.Construct);
        Run("graph", ReflectionDemo.Graph);
    }
}
