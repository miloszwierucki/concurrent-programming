using System.Numerics;

namespace Data {
    public abstract class ITable {
        public abstract double width { get; }
        public abstract double height { get; }
        public static ITable CreateInstance(double w, double h) {
            return new Table(w, h);
        }
    }
    internal class Table: ITable {
        public override double width { get; }
        public override double height { get; }

        public Table(double w, double h) {
            width = w;
            height = h;
        }
    }
}