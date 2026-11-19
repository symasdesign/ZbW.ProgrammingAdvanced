using System;


public struct Vektor {
    private double x, y, z;

    public Vektor(double x, double y, double z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vektor operator +(Vektor a, Vektor b) {
        return new Vektor(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vektor operator -(Vektor a, Vektor b) {
        return new Vektor(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vektor operator *(Vektor a, Vektor b) {
        return new Vektor(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
    }

    public static Vektor operator *(Vektor a, double d) {
        return new Vektor(a.x * d, a.y * d, a.z * d);
    }

    public static Vektor operator *(double d, Vektor a) {
        return new Vektor(a.x * d, a.y * d, a.z * d);
    }

    public double this[int idx] {
        get {
            switch (idx) {
                case 0:
                    return x;
                case 1:
                    return y;
                case 2:
                    return z;
                default:
                    throw new ArgumentOutOfRangeException(nameof(idx));
            }
        }
        set {
            switch (idx) {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                case 2:
                    z = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(idx));
            }
        }
    }

    public static implicit operator Vektor(double x) {
        return new Vektor(x, 0, 0);
    }

    public static explicit operator double(Vektor v) {
        return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
    }

    public override string ToString() {
        return "[" + x + " " + y + " " + z + "]";
    }

    public static bool operator ==(Vektor a, Vektor b) {
        return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z);
    }

    public static bool operator !=(Vektor a, Vektor b) {
        return !(a == b);
    }

    public override bool Equals(object? obj) => obj is Vektor other && this == other;

    public override int GetHashCode() => HashCode.Combine(x, y, z);

}
