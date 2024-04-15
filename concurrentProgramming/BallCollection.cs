namespace Data {
    public class BallsCollection : DataAbstractApi {
        private List<Ball> balls;

        public BallsCollection() {
            balls = new List<Ball>();
        }

        public override int GetBallsCount() {
            return balls.Count;
        }

        public override void AddBall(Ball ball) {
            balls.Add(ball);
        }

        public override void RemoveBall() {
            balls.Remove(balls[balls.Count - 1]);
        }

        public override Ball GetBall(int index) {
            return balls[index];
        }
    }
}