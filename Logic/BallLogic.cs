using Data;
using System.Numerics;

namespace Logic {
    public class BallsLogic : LogicAbstractApi {
        public DataAbstractApi Balls { get; set; }
        public ITable Table { get; set; }

        public CancellationTokenSource CancelSimulationSource { get; private set; }


        public BallsLogic(double w, double h) {
            Table = ITable.CreateInstance(w, h);
            Balls = DataAbstractApi.CreateBallCollection();

            CancelSimulationSource = new CancellationTokenSource();
        }

        protected override void onPositionChange(MovableBall args) {
            base.onPositionChange(args);
        }

        public override void addBalls(int quantity) {
            Random random = new Random();
            int count = Balls.GetBallsCount();

            for (int i = count; i < quantity + count; i++) {

                bool contain = true;
                bool licz;
                while (contain) {
                    int radius = 40;
                    double weight = radius;

                    double x = (random.NextDouble() * (Table.width - radius));
                    double y = (random.NextDouble() * (Table.height - radius));

                    Vector2 speed = new Vector2(0, 0);
                    while (speed.X == 0)
                    {
                        speed.X = (float)(random.Next(-5, 5) + random.NextDouble());
                    }
                    while (speed.Y == 0)
                    {
                        speed.Y = (float)(random.Next(-5, 5) + random.NextDouble());
                    }


                    IBall ball = IBall.CreateInstance(i, new Vector2((float)x, (float)y), radius, speed, weight);
                    this.Balls.AddBall(ball);
                    licz = false;

                    for (int j = 0; j < i; j++) {
                        // Sprawdza kolizję między piłkami
                        if (Balls.GetBall(i).position.X <= Balls.GetBall(j).position.X + Balls.GetBall(j).radius && Balls.GetBall(i).position.X + Balls.GetBall(i).radius >= Balls.GetBall(j).position.X) {
                            if (Balls.GetBall(i).position.Y <= Balls.GetBall(j).position.Y + Balls.GetBall(j).radius && Balls.GetBall(i).position.Y + Balls.GetBall(i).radius >= Balls.GetBall(j).position.Y) {
                                // Jeśli występuje kolizja, usuwa nowo utworzoną piłkę
                                licz = true;
                                Balls.RemoveBall(Balls.GetBall(i));
                                break;
                            }
                        }
                    }
                    if (!licz)
                    {
                        contain = false;
                    }
                }
            }

        }

        public override void removeBalls(int quantity) {
            int count = Balls.GetBallsCount();

            for (int i = 0; i < quantity; i++) {
                if (count > 0) {
                    Balls.RemoveBall(Balls.GetBall(count - i - 1));
                };
            }
        }

        public override int getBallsCount() {
            return Balls.GetBallsCount();
        }

        public override IBall getBall(int index) {
            return Balls.GetBall(index);
        }


        public override void start() {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();

            for (var i = 0; i < Balls.GetBallsCount(); i++) {
                var ball = new MovableBall(Balls.GetBall(i), i, this, Table.width, Table.height, Balls);

                ball.PositionChange += (_, args) => onPositionChange(ball);
                Task.Factory.StartNew(ball.Move, CancelSimulationSource.Token);
            }

        }

        public override void stop() {
            this.CancelSimulationSource.Cancel();
        }

        
    }
}
