using Data;

namespace Logic {
    public abstract class LogicAbstractApi {
        public event EventHandler<MovableBall>? PositionChangedEvent;

        public abstract void addBalls(int quantity);

        public abstract void removeBalls(int quantity);

        public abstract Ball getBall(int index);

        public abstract void start();

        public abstract void stop();
        public abstract int getBallsCount();

        protected virtual void onPositionChange(MovableBall ball) {
            PositionChangedEvent?.Invoke(this, ball);
        }


    }
}
