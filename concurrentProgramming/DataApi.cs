namespace Data {
    public abstract class DataAbstractApi {
        public static DataAbstractApi CreateBall(double x, double y) {
            return new Ball(x, y);
        }

        public abstract double getXPosition();

        public abstract double getYPosition();

        public abstract void setXPosition(double x);

        public abstract void setYPosition(double y);
    }
}