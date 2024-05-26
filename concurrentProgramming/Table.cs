using System.Numerics;

namespace Data {
    public abstract class ITable {
        public abstract double Width { get; }
        public abstract double Height { get; }
        public static ITable CreateInstance(double w, double h) {
            return new Table(w, h);
        }
    }

    internal class Table: ITable {
        public override double Width { get; }
        public override double Height { get; }

        public Table(double w, double h) {
            Width = w;
            Height = h;
        }
    }
}