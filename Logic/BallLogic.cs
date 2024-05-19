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

        protected override void OnPositionChange(MovableBall args) {
            base.OnPositionChange(args);
        }

        public override void AddBalls(int quantity) {
            Random random = new Random();
            int count = Balls.GetBallsCount();

            for (int i = count; i < quantity + count; i++) {
                int radius = 40;
                double weight = 60;

                double x = (random.NextDouble() * (Table.Width - radius));
                double y = (random.NextDouble() * (Table.Height - radius));

                Vector2 speed = new Vector2(0, 0);
                while (speed.X == 0) {
                    speed.X = (float)(random.Next(-5, 5) + random.NextDouble());
                }

                while (speed.Y == 0) {
                    speed.Y = (float)(random.Next(-5, 5) + random.NextDouble());
                }

                IBall ball = IBall.CreateInstance(i, new Vector2((float)x, (float)y), radius, speed, weight);
                this.Balls.AddBall(ball);
            }

        }

        public override void RemoveBalls(int quantity) {
            int count = Balls.GetBallsCount();

            for (int i = 0; i < quantity; i++) {
                if (count > 0) {
                    Balls.RemoveBall(Balls.GetBall(count - i - 1));
                };
            }
        }

        public override int GetBallsCount() {
            return Balls.GetBallsCount();
        }

        public override IBall GetBall(int index) {
            return Balls.GetBall(index);
        }

        public override void Start() {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();

            for (var i = 0; i < Balls.GetBallsCount(); i++) {
                var ball = new MovableBall(Balls.GetBall(i), i, this, Table.Width, Table.Height, Balls);

                Balls.GetBall(i).StartMoveBall();
                ball.PositionChange += (_, args) => OnPositionChange(ball);
                Task.Factory.StartNew(ball.Move, CancelSimulationSource.Token);
            }

        }

        public override void Stop() {
            this.CancelSimulationSource.Cancel();
        }

        
    }
}
