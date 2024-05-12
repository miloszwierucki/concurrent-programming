// See https://aka.ms/new-console-template for more information

using System.Numerics;

namespace Data {
    public abstract class IBall {
        public abstract int BallID { get; }
        public abstract Vector2 Position { get; set; }
        public abstract double Radius { get; set; }
        public abstract double Weight { get; set; }
        public abstract Vector2 Speed { get; set; }

        public static IBall CreateInstance(int id, Vector2 p, double r, Vector2 v, double w)
        {
            return new Ball(id, p, r, v, w);
        }
    }

    internal class Ball(int id, Vector2 p, double r, Vector2 v, double weight) : IBall {
        public override int BallID { get; } = id;
        public override Vector2 Position { get; set; } = p;
        public override double Radius { get; set; } = r;
        public override double Weight { get; set; } = weight;

        public override Vector2 Speed { get; set; } = v;
    }
}