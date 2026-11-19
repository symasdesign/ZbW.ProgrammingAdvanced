using System;

public class Stack : ICloneable {
    private object?[] items;

    public int Count {
        get; private set;
    }

    public Stack(int length = 0) {
        items = new object[length == 0 ? 10 : length];
    }

    public void Push(object? item) {
        Grow();

        items[Count] = item;

        Count++;
    }

    public object? Peek() {
        if (Count == 0)
            throw new InvalidOperationException("No items on stack.");

        return items[Count - 1];
    }

    public object? Pop() {
        if (Count == 0)
            throw new InvalidOperationException("No items on stack.");

        var item = items[Count - 1];
        items[Count - 1] = null;
        Count--;

        return item;
    }

    public void Clear() {
        items = new object[10];
        Count = 0;
    }

    private void Grow() {
        // Überprüfen, ob noch Platz
        if (items.Length >= Count + 1)
            return;

        // Array-Kapazität verdoppeln
        var newLength = items.Length * 2;

        Array.Resize(ref items, newLength);
    }

    public object Clone() {
        // TODO: Kopieren Sie den belegten Inhalt in ein eigenes Array.
        return MemberwiseClone();
    }

    public static void TestStack() {
        twoEmptyStacksWithSameCapacityShouldBeEqual();
        twoEmptyStacksWithDifferentCapacitiesShouldBeEqual();
        stackIsEqualToSelf();
        stacksWithEqualElementsShouldBeEqual();
        cloningShouldResultInEqualStacks();
        cloningAndChangingAStackShouldNotBeEqual();
    }

    private static void twoEmptyStacksWithSameCapacityShouldBeEqual() {
        var stack1 = new Stack(5);
        var stack2 = new Stack(5);
        Console.WriteLine(stack1 == stack2);
    }

    private static void twoEmptyStacksWithDifferentCapacitiesShouldBeEqual() {
        var stack1 = new Stack(1);
        var stack2 = new Stack(2);
        Console.WriteLine(stack1 == stack2);
    }

    private static void stackIsEqualToSelf() {
        var stack1 = new Stack(1);
        var same = stack1;
        Console.WriteLine(stack1 == same);
    }

    private static void stacksWithEqualElementsShouldBeEqual() {
        var stack1 = new Stack(1);
        stack1.Push("ZbW");
        var stack2 = new Stack(1);
        stack2.Push("ZbW");
        Console.WriteLine(stack1 == stack2);
    }

    private static void cloningShouldResultInEqualStacks() {
        var stack1 = new Stack(2);
        stack1.Push("Hello");
        stack1.Push("ZbW");
        var stack2 = (Stack)stack1.Clone();
        Console.WriteLine(stack1 == stack2);
    }

    private static void cloningAndChangingAStackShouldNotBeEqual() {
        var stack1 = new Stack(2);
        var stack2 = (Stack)stack1.Clone();
        stack1.Push("Hello");
        Console.WriteLine(stack1 != stack2);
    }

}
