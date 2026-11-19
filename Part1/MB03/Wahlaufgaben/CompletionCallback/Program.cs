using System;

namespace _1._4_CompletionCallback {
    class Program {
        static void Main(string[] args) {
            var myApp = new FileReaderClient();

            myApp.ActionRequiringFile(Path.Combine(AppContext.BaseDirectory, "HelloWorld1.txt"));
            myApp.ActionRequiringFile(Path.Combine(AppContext.BaseDirectory, "HelloWorld2.txt"));
            myApp.ActionRequiring2Files(Path.Combine(AppContext.BaseDirectory, "HelloWorld1.txt"), Path.Combine(AppContext.BaseDirectory, "HelloWorld2.txt"));

        }
    }
}
