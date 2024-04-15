// See https://aka.ms/new-console-template for more information

using System.Numerics;

namespace Data {
    public class Ball {
        public Vector2 position { get; set; }
        public double radius { get; set; }
        public Vector2 speed { get; set; }

        public Ball(Vector2 p, double r, Vector2 v) {
            position = p;
            radius = r;
            speed = v;
        }
    }
}