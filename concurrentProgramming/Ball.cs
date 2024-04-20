// See https://aka.ms/new-console-template for more information

using System.Numerics;

namespace Data {
    public abstract class IBall {
        public abstract Vector2 position { get; set; }
        public abstract double radius { get; set; }
        public abstract Vector2 speed { get; set; }

        public static IBall CreateInstance(Vector2 p, double r, Vector2 v)
        {
            return new Ball(p, r, v);
        }
    }

    internal class Ball: IBall {
        public override Vector2 position { get; set; }
        public override double radius { get; set; }
        public override Vector2 speed { get; set; }

        public Ball(Vector2 p, double r, Vector2 v) {
            position = p;
            radius = r;
            speed = v;
        }
    }
}