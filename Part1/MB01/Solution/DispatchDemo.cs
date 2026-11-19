namespace Course;

public static class DispatchDemo {
    private class Vehicle {
        public virtual string Move() => "moving";
    }
    private class Boat : Vehicle {
        public override string Move() => "swimming";
    }
    private class HiddenBoat : Vehicle {
        public new string Move() => "hidden";
    }
    public static void Run() {
        Vehicle a = new Boat();
        Vehicle b = new HiddenBoat();
        Console.WriteLine(a.Move()); // swimming: virtueller Aufruf
        Console.WriteLine(b.Move()); // moving: neue Methode verdeckt nur
        Console.WriteLine(((HiddenBoat)b).Move()); // hidden
    }
}
