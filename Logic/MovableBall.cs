using Data;
using System.Numerics;

namespace Logic {
    public class MovableBall {
        private IBall ball;
        public DataAbstractApi Balls { get; set; }
        public int id;

        private BallsLogic owner;
        public event EventHandler<MovableBall>? PositionChange;

        public double targetX;
        public double targetY;

        public MovableBall(IBall ball, int id, BallsLogic owner, double targetX, double targetY, DataAbstractApi Balls) {
            this.ball = ball;
            this.id = id;
            this.owner = owner;
            this.targetX = targetX;
            this.targetY = targetY;
            this.Balls = Balls;
        }

        public IBall GetBall() {
            return ball;
        }

        public Vector2 GetPosition() {
            return new Vector2(ball.position.X, ball.position.Y);
        }

        public async void Move() {
            while (!owner.CancelSimulationSource.Token.IsCancellationRequested) {
                Vector2 featurePosition = ball.position + ball.speed;

                // Obsługuje odbicie piłki od innych piłek
                lock (ball) {
                    for (int i = 0; i < Balls.GetBallsCount(); i++) {
                        IBall otherBall = Balls.GetBall(i);

                        if (ball.ballID == otherBall.ballID) {
                            continue;
                        }

                        if (Vector2.Distance(featurePosition, otherBall.position) < (this.ball.radius / 2 + otherBall.radius / 2)) {
                            Vector2 p = this.ball.speed;
                            this.ball.speed = otherBall.speed;
                            otherBall.speed = p;
                        }
                    }
                }

                Console.WriteLine("Pozycja: " + ball.position + " | Predkosc: " + ball.speed);
                Console.WriteLine("Funkcja: " + featurePosition);


                // Sprawdza i obsługuje kolizję z ścianami
                if (ball.position.X < 0 || ball.position.X > targetX - ball.radius) {
                    ball.speed = ball.speed * new Vector2(-1, 1);
                }

                if (ball.position.Y < 0 || ball.position.Y > targetY - ball.radius) {
                    ball.speed = ball.speed * new Vector2(1, -1);
                }

                ball.position = ball.position + ball.speed;
                PositionChange?.Invoke(this, this);
                await Task.Delay(20, owner.CancelSimulationSource.Token).ContinueWith(_ => { });
            }
        }
    }
}
