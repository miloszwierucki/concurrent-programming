using Data;
using System.Numerics;

namespace Logic {
    public class MovableBall {
        private IBall ball;
        public int id;

        private BallsLogic owner;
        public event EventHandler<MovableBall>? PositionChange;

        public double targetX;
        public double targetY;

        public MovableBall(IBall ball, int id, BallsLogic owner, double targetX, double targetY) {
            this.ball = ball;
            this.id = id;
            this.owner = owner;
            this.targetX = targetX;
            this.targetY = targetY;
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

                if (featurePosition.X < 0 || featurePosition.X + ball.radius > targetX) {
                    ball.speed = ball.speed * new Vector2(-1, 1);
                }

                if (featurePosition.Y < 0 || featurePosition.Y + ball.radius > targetY) {
                    ball.speed = ball.speed * new Vector2(1, -1);
                }

                ball.position = ball.position + ball.speed;
                PositionChange?.Invoke(this, this);

                await Task.Delay(20, owner.CancelSimulationSource.Token).ContinueWith(_ => { });
            }
        }
    }
}
