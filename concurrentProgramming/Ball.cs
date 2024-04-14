// See https://aka.ms/new-console-template for more information

namespace Data {
    internal class Ball : DataAbstractApi {
        private double positionX;
        private double positionY;

        public Ball(double x, double y) {
            positionX = x;
            positionY = y;
        }

        public override double getXPosition() {
            return positionX;
        }

        public override double getYPosition() {
            return positionY;
        }

        public override void setXPosition(double newX) {
            positionX = newX;
        }

        public override void setYPosition(double newY) {
            positionY = newY;
        }
    }
}