using Data;

namespace Logic {
    public abstract class LogicAbstractApi {
        public event EventHandler<MovableBall>? PositionChangedEvent;

        public abstract void AddBalls(int quantity);

        public abstract void RemoveBalls(int quantity);

        public abstract IBall GetBall(int index);

        public abstract void Start();

        public abstract void Stop();
        public abstract int GetBallsCount();

        protected virtual void OnPositionChange(MovableBall ball) {
            PositionChangedEvent?.Invoke(this, ball);
        }


    }
}
