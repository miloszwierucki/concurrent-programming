using System.Numerics;

namespace Data {
    public abstract class DataAbstractApi {
        public abstract double GetTableWidth();
        public abstract double GetTableHeight();
        public abstract double GetBallRadius();
        public abstract double getBallMaxSpeed();
        public abstract double getBallWeight();

        public static IBall CreateNewBall(Vector2 p, double r, Vector2 s, double w) {
            return new Ball(p, r, s, w, true);
        }

        public static ITable CreateNewTable(double w, double h) {
            return new Table(w, h);
        }

        public static DataAbstractApi CreateInstance() {
            return new Data();
        }
    }

    internal class Data : DataAbstractApi {
        private readonly double width = 600;
        private readonly double height = 500;
        private readonly double ballRadius = 40;
        private readonly double ballMaxSpeed = 2;
        private readonly double ballWeight = 10;

        public override double GetBallRadius() {
            return ballRadius;
        }

        public override double getBallMaxSpeed() {
            return ballMaxSpeed;
        }

        public override double getBallWeight() {
            return ballWeight;
        }

        public override double GetTableHeight() {
            return height;
        }

        public override double GetTableWidth() {
            return width;
        }
    }
}