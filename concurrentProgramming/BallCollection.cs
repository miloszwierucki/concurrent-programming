﻿namespace Data {
    internal class BallsCollection : DataAbstractApi {
        private List<IBall> balls;
        public readonly object BallsLock = new object();

        public BallsCollection() {
            balls = new List<IBall>();
        }

        public override int GetBallsCount() {
            return balls.Count;
        }

        public override void AddBall(IBall ball) {
            balls.Add(ball);
        }

        public override bool RemoveBall(IBall ball) {
            int index = balls.IndexOf(ball);

            if (index < 0) return false;

            balls.RemoveAt(index);
            return true;
        }

        public override IBall GetBall(int index) {
            return balls[index];
        }
        public override object GetBallsLock() {
            return BallsLock;
        }
    }
}