using System.Numerics;

namespace Data {
    public abstract class DataAbstractApi {
        public abstract void AddBall(IBall b);
        public abstract bool RemoveBall(IBall b);
        public abstract IBall GetBall(int i);
        public abstract int GetBallsCount();
        public abstract object GetBallsLock();

        public static IBall CreateNewBall(int id, Vector2 p, double r, Vector2 s, double w) {
            return new Ball(id, p, r, s, w);
        }

        public static ITable CreateNewTable(double w, double h) {
            return new Table(w, h);
        }

        public static DataAbstractApi CreateBallCollection() {
            return new BallsCollection();
        }

    }
}