using Logic;
using System.Numerics;

namespace ModelApi {
    public class OnPositionChangeUiAdapterEventArgs : EventArgs {
        public readonly Vector2 position;
        public readonly int id;

        public OnPositionChangeUiAdapterEventArgs(Vector2 position, int id) {
            this.position = position;
            this.id = id;
        }
    }

    public class Model {
        public double tableWidth;
        public double tableHeight;
        public int ballsQuantity;

        public LogicAbstractApi? logicLayer;
        public event EventHandler<OnPositionChangeUiAdapterEventArgs>? BallPositionChange;


        public Model() {
            tableWidth = 600;
            tableHeight = 500;
            logicLayer = new BallsLogic(tableWidth, tableHeight);
            ballsQuantity = 0;

            logicLayer.PositionChangedEvent += (sender, b) => {
                BallPositionChange?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(b.GetBall().position, b.id));
            };
        }

        public void setBallCount(int quantity) {
            ballsQuantity = quantity;
        }

        public int getBallsCount() {
            return ballsQuantity;
        }

        public void start() {
            logicLayer.addBalls(ballsQuantity);
            logicLayer.start();
        }

        public void stop() {
            logicLayer.stop();

            logicLayer = new BallsLogic(tableWidth, tableHeight);
            logicLayer.PositionChangedEvent += (sender, b) => {
                BallPositionChange?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(b.GetBall().position, b.id));
            };
        }

        public void onBallPositionChange(OnPositionChangeUiAdapterEventArgs args) {
            BallPositionChange?.Invoke(this, args);
        }
    }
}
