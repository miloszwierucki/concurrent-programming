namespace Data {
    internal class BallsCollection : DataAbstractApi {
        private List<IBall> balls;

        public BallsCollection() {
            balls = new List<IBall>();
        }

        public override int GetBallsCount() {
            return balls.Count;
        }

        public override void AddBall(IBall ball) {
            balls.Add(ball);
        }

        public override void RemoveBall() {
            balls.Remove(balls[balls.Count - 1]);
        }

        public override IBall GetBall(int index) {
            return balls[index];
        }
    }
}