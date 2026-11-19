using System;
using System.Diagnostics;

namespace _1._1_Shapes {
    // TODO:
    // Definieren Sie einen Delegate-Typ Comparer
    // mit derselben Signatur wie die untenstehenden Funktionen
    // CompareFraction bzw CompareString
    // TODO: Ersetzen Sie Comparison<object> in Sort durch Ihren eigenen Delegate-Typ.

    /// <summary>
    /// Einfacher Referenztyp für das Rechnen mit Bruechen
    /// </summary>
    class Fraction {
        public int a, b;

        public Fraction(int a, int b) {
            this.a = a;
            this.b = b;
        }

        // Überladene Methode System.Object.ToString
        public override string ToString() {
            return a + " / " + b;
        }
    }

    class Program {
        // Statische Methode zum Vergleichen zweier Brueche x und y
        // Resultat 0: x = y; Resultat -1: x < y; Resultat +1: x > y
        // Signatur muss mit Delegate Comparer übereinstimmen
        static int CompareFraction(object x, object y) {
            // TODO: Vergleichen Sie die Werte der beiden Brüche.
            throw new NotImplementedException();
        }

        // Statische Methode zum Vergleichen zweier strings x und y
        // Resultat 0: x = y; Resultat -1: x < y; Resultat +1: x > y
        // Signatur muss mit Delegate Comparer übereinstimmen
        static int CompareString(object x, object y) {
            // TODO: Vergleichen Sie die beiden Zeichenketten alphabetisch.
            throw new NotImplementedException();
        }

        // Generische Sort-Methode zum Sortieren von beliebigen Arrays a
        // compare ist eine Delegate-Instanz und enthaelt die Referenz (=Funktionszeiger)
        // auf eine Compare-Funktion fuer den aktuellen Elementtyp des Arrays
        static void Sort(object[] a, Comparison<object> compare) {
            // TODO:
            // Überpruefen Sie folgende Vorbedingung
            // compare muss genau eine Methode referenzieren (kein Multicast)
            // Sie koennen dazu die Methode Debug.Assert verwenden (siehe Help)
            //Debug.Assert ...

            // Selection sort
            for (var i = 0; i < a.Length - 1; i++) {
                var min = i;
                for (var j = i + 1; j < a.Length; j++) {
                    if (compare(a[j], a[min]) < 0)
                        min = j;
                }

                if (min != i) {
                    var x = a[i];
                    a[i] = a[min];
                    a[min] = x;
                }
            }
        }

        public static void Main() {
            var a = new Fraction[] { new Fraction(1, 2), new Fraction(3, 4), new Fraction(4, 8), new Fraction(8, 3) };
            var b = new string[] { "pears", "apples", "oranges", "bananas", "plums" };

            // TODO: Sortieren Sie den Array a mit der Sort-Methode
            // Sort(...);

            // Ausgabe des sortierten Arrays a
            foreach (var f in a)
                Console.Write(f + " ");
            Console.WriteLine();

            // TODO: Sortieren Sie den Array b mit der Sort-Methode
            // Sort(...);

            // Ausgabe des sortierten Arrays b
            foreach (var s in b)
                Console.Write(s + " ");
            Console.WriteLine();

        }
    }
}
