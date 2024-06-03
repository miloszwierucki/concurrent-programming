using Data;
using System.Collections.Concurrent;
using System.Numerics;
namespace Logic;

internal class BallManager : LogicAbstractApi {
    private DataAbstractApi Data;
    private List<IBall> Balls;
    private readonly ConcurrentQueue<IBall> queue;
    private ITable Table;

    private bool isRunning = false;
    private Object _locker = new Object();

    public BallManager(DataAbstractApi Data, List<IBall> Balls, ITable Table) {
        this.Balls = Balls;
        this.Data = Data;
        this.Table = Table;

        queue = new ConcurrentQueue<IBall>();
    }

    public override void AddBalls(int quantity) {
        Random random = new Random();

        if (Balls.Count == 0) {
            for (int i = 0; i < quantity; i++) {
                double radius = Data.GetBallRadius();
                double weight = Data.GetBallWeight();

                double x = (random.NextDouble() * (Table.Width - radius));
                double y = (random.NextDouble() * (Table.Height - radius));

                Vector2 speed = new Vector2(0, 0);
                double maxSpeed = Data.GetBallMaxSpeed();

                while (speed.X == 0) {
                    speed.X = (float)((random.NextDouble() * 2 * maxSpeed) - maxSpeed);
                }

                while (speed.Y == 0) {
                    speed.Y = (float)((random.NextDouble() * 2 * maxSpeed) - maxSpeed);
                }

                IBall ball = IBall.CreateInstance(i, new Vector2((float)x, (float)y), radius, speed, weight, false);

                Balls.Add(ball);
                ball.ChangedPosition += CheckCollisions;
            }
        }
    }

    public override void RemoveBalls(int quantity) {
        int count = Balls.Count();

        for (int i = 0; i < quantity; i++) {
            if (count > 0) {
                Balls.RemoveAt(i);
            };
        }
    }

    public override void Start() {

        if (!isRunning) {
            foreach (IBall ball in Balls) {
                ball.StartMoveBall(queue);
            }
            isRunning = true;

            Data.CreateLoggingTask(queue);
        }
    }

    public override void Stop() {
        if (isRunning) {
            foreach (IBall ball in Balls) {
                ball.StopBall();
            }
            isRunning = false;
        }
    }

    public override List<IMovabaleBall> GetBalls() {
        List<IMovabaleBall> ballPositions = new List<IMovabaleBall>();
        foreach (IBall Ball in Balls) {
            ballPositions.Add(IMovabaleBall.CreateInstance(Ball));
        }
        return ballPositions;
    }

    public override int GetBallsCount() {
        return Balls.Count;
    }

    private void CheckCollisions(object? sender, EventArgs e) {
        IBall Ball = (IBall)sender;

        lock (_locker) {
            Vector2 featurePosition = Ball.Position + Ball.Speed;

            // Sprawdza i obsługuje kolizję z ścianami
            if ((featurePosition.X <= 0 && Ball.Speed.X < 0) || (featurePosition.X >= Table.Width - Ball.Radius && Ball.Speed.X > 0)) {
                Ball.Speed *= new Vector2(-1, 1);
            }

            if ((featurePosition.Y <= 0 && Ball.Speed.Y < 0) || (featurePosition.Y >= Table.Height - Ball.Radius && Ball.Speed.Y > 0)) {
                Ball.Speed *= new Vector2(1, -1);
            }

            // Obsługuje odbicie piłki od innych piłek
            foreach (IBall OtherBall in Balls) {
                Vector2 featureOtherPosition = OtherBall.Position + OtherBall.Speed;

                if (OtherBall != Ball) {
                    Vector2 A = featurePosition + new Vector2((float)(Ball.Radius / 2));
                    Vector2 B = featureOtherPosition + new Vector2((float)(OtherBall.Radius / 2));

                    if (Vector2.Distance(A, B) <= (Ball.Radius / 2 + OtherBall.Radius / 2)) {
                        double m1 = Ball.Weight;
                        Vector2 v1 = Ball.Speed;

                        double m2 = OtherBall.Weight;
                        Vector2 v2 = OtherBall.Speed;

                        Vector2 u1 = Vector2.Multiply((Vector2.Multiply((float)(m1 - m2), v1) + Vector2.Multiply((float)(2 * m2), v2)), (float)(1 / (m1 + m2)));
                        Vector2 u2 = Vector2.Multiply((Vector2.Multiply((float)(2 * m1), v1) + Vector2.Multiply((float)(m2 - m1), v2)), (float)(1 / (m1 + m2)));

                        OtherBall.Speed = u2;
                        Ball.Speed = u1;
                    }
                }
            }

        }
    }
}
