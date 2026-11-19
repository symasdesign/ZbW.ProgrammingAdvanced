using System;

namespace Integrator {
    public class MainClass {
        private const int STEPS = 100;
        private const double EPS = 0.001;

        public static void Main(string[] args) {
            IFunction f1 = new LineFunction();
            Console.WriteLine(f1.GetType().Name + ":" + Integrator.Integrate(f1, 0, 10, STEPS));
            IFunction f2 = new SquareFunction();
            Console.WriteLine(f2.GetType().Name + ":" + Integrator.Integrate(f2, 0, 10, STEPS));
            Console.WriteLine(f2.GetType().Name + ":" + Integrator.Integrate(f2, 0, 10, EPS));
        }
    }

    public class Integrator {

        public static double Integrate(IFunction func, double start, double end, int step) {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(step);
            var d = (end - start) / step;
            var sum = (func.F(start) + func.F(end)) / 2;
            for (var i = 1; i < step; i++) {
                sum += func.F(start + i * d);
            }

            return d * sum;
        }


        public static double Integrate(IFunction func, double start, double end, double eps) {
            if (!double.IsFinite(eps) || eps <= 0) {
                throw new ArgumentOutOfRangeException(nameof(eps));
            }
            var steps = 1;
            var previous = Integrate(func, start, end, steps);
            while (true) {
                steps = checked(steps * 2);
                var current = Integrate(func, start, end, steps);
                if (Math.Abs(current - previous) < eps) {
                    return current;
                }
                previous = current;
            }

        }
    }

    public interface IFunction {
        double F(double arg);
    }

    public class LineFunction : IFunction {
        public double F(double x) {
            return x;
        }
    }

    public class SquareFunction : IFunction {
        public double F(double x) {
            return x * x;
        }
    }
}
