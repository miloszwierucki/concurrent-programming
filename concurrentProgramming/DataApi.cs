﻿using System.Numerics;

namespace Data {
    public abstract class DataAbstractApi {
        public abstract void AddBall(Ball b);
        public abstract void RemoveBall();
        public abstract Ball GetBall(int i);
        public abstract int GetBallsCount();

        public static Ball CreateNewBall(Vector2 p, double r, Vector2 s) {
            return new Ball(p, r, s);
        }

        public static Table CreateNewTable(double w, double h) {
            return new Table(w, h);
        }

        public static DataAbstractApi CreateBallCollection() {
            return new BallsCollection();
        }

    }
}