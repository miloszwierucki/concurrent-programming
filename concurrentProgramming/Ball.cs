// See https://aka.ms/new-console-template for more information

using System.Numerics;

namespace Data {
    public abstract class IBall {
        public abstract int ballID { get; }
        public abstract Vector2 position { get; set; }
        public abstract double radius { get; set; }
        public abstract double weight { get; set; }
        public abstract Vector2 speed { get; set; }

        public static IBall CreateInstance(int id, Vector2 p, double r, Vector2 v, double w)
        {
            return new Ball(id, p, r, v, w);
        }
    }

    internal class Ball: IBall {
        public override int ballID { get; }
        public override Vector2 position { get; set; }
        public override double radius { get; set; }
        public override double weight { get; set; }

        public override Vector2 speed { get; set; }

        public Ball(int id, Vector2 p, double r, Vector2 v, double weight) {
            ballID = id;
            this.position = p;
            this.radius = r;
            this.speed = v;
            this.weight = weight;
        }
    }
}