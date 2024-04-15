using Data;
using System.Numerics;

namespace Logic {
    public class BallsLogic : LogicAbstractApi {
        public BallsCollection Balls { get; set; }
        public Table Table { get; set; }

        public CancellationTokenSource CancelSimulationSource { get; private set; }


        public BallsLogic(double w, double h) {
            Table = new Table(w, h);
            Balls = new BallsCollection();

            CancelSimulationSource = new CancellationTokenSource();
        }

        protected override void onPositionChange(MovableBall args) {
            base.onPositionChange(args);
        }

        public override void addBalls(int quantity) {
            Random random = new Random();

            for (int i = 0; i < quantity; i++) {

                int radius = 40;
                double x = (random.NextDouble() * (Table.width - radius));
                double y = (random.NextDouble() * (Table.height - radius));
                double speedX = (random.NextDouble() * 10);
                double speedY = (random.NextDouble() * 10);

                this.Balls.AddBall(new Ball(new Vector2((float)x, (float)y), radius, new Vector2((float)speedX, (float)speedY)));
            }

        }

        public override void removeBalls(int quantity) {
            for (int i = 0; i < quantity; i++) {
                Balls.RemoveBall();
            }
        }

        public override int getBallsCount() {
            return Balls.GetBallsCount();
        }

        public override Ball getBall(int index) {
            return Balls.GetBall(index);
        }


        public override void start() {
            if (CancelSimulationSource.IsCancellationRequested) return;

            CancelSimulationSource = new CancellationTokenSource();

            for (var i = 0; i < Balls.GetBallsCount(); i++) {
                var ball = new MovableBall(Balls.GetBall(i), i, this, Table.width, Table.height);

                ball.PositionChange += (_, args) => onPositionChange(ball);
                Task.Factory.StartNew(ball.Move, CancelSimulationSource.Token);
            }

        }

        public override void stop() {
            this.CancelSimulationSource.Cancel();
        }

        
    }
}
