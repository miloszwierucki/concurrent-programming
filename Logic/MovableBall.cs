using Data;
using System.Numerics;

namespace Logic
{
    public class MovableBall
    {
        private readonly IBall ball;
        public DataAbstractApi Balls { get; set; }
        public int id;

        private readonly BallsLogic owner;
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
            return new Vector2(ball.Position.X, ball.Position.Y);
        }


        public async void Move() {
            while (!owner.CancelSimulationSource.Token.IsCancellationRequested) {
                Vector2 featurePosition = ball.Position + ball.Speed;

                // Obsługuje odbicie piłki od innych piłek
                lock (owner) {
                    for (int i = 0; i < Balls.GetBallsCount(); i++) {
                        IBall otherBall = Balls.GetBall(i);

                        if (ball.BallID != otherBall.BallID) {
                            //Console.WriteLine("Ty: " + ball.BallID + " | Sasiada: " + otherBall.BallID);

                            lock (otherBall) {
                                Vector2 A = featurePosition + new Vector2((float)(ball.Radius / 2));
                                Vector2 B = otherBall.Position + new Vector2((float)(otherBall.Radius / 2));

                                if (Vector2.Distance(A, B) <= (ball.Radius / 2 + otherBall.Radius / 2)) {
                                    double m1 = ball.Weight;
                                    Vector2 v1 = ball.Speed;

                                    double m2 = otherBall.Weight;
                                    Vector2 v2 = otherBall.Speed;

                                    Vector2 u1 = Vector2.Multiply((Vector2.Multiply((float)(m1 - m2), v1) + Vector2.Multiply((float)(2 * m2), v2)), (float)(1 / (m1 + m2)));
                                    Vector2 u2 = Vector2.Multiply((Vector2.Multiply((float)(2 * m1), v1) + Vector2.Multiply((float)(m2 - m1), v2)), (float)(1 / (m1 + m2)));

                                    otherBall.Speed = u2;
                                    ball.Speed = u1;
                                }
                            }
                        }
                    }
                }

                //Console.WriteLine("Pozycja: " + ball.Position + " | Predkosc: " + ball.Speed);
                //Console.WriteLine("Funkcja: " + featurePosition);


                // Sprawdza i obsługuje kolizję z ścianami
                if (featurePosition.X <= 0 || featurePosition.X >= targetX - ball.Radius) {
                    ball.Speed *= new Vector2(-1, 1);
                }

                if (featurePosition.Y <= 0 || featurePosition.Y >= targetY - ball.Radius) {
                    ball.Speed *= new Vector2(1, -1);
                }

                ball.Position += ball.Speed;
                PositionChange?.Invoke(this, this);
                await Task.Delay(20, owner.CancelSimulationSource.Token).ContinueWith(_ => { });
            }
        }
    }
}
