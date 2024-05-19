// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Numerics;

namespace Data {
    public abstract class IBall {
        public abstract int BallID { get; }
        public abstract Vector2 Position { get; protected set; }
        public abstract double Radius { get; set; }
        public abstract double Weight { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract event EventHandler? ChangedPosition;

        public abstract void StopBall();
        public abstract void StartMoveBall();
        public abstract void OnChangedPosition();

        public static IBall CreateInstance(int id, Vector2 p, double r, Vector2 v, double w, bool isRunning = true) {
            return new Ball(id, p, r, v, w, isRunning);
        }
    }

    internal class Ball : IBall {
        public override int BallID { get; }
        public override Vector2 Position { get; protected set; }
        public override double Radius { get; set; }
        public override double Weight { get; set; }

        public override Vector2 Speed { get; set; }
        private bool isRunning;
        public override event EventHandler? ChangedPosition;

        public Ball(int id, Vector2 p, double r, Vector2 v, double weight, bool isRunning) {
            this.BallID = id;
            Position = p;
            Radius = r;
            Speed = v;
            Weight = weight;

            this.isRunning = isRunning;
            new Thread(new ThreadStart(Move)).Start();
        }

        private async void Move() {
            Stopwatch timer = new Stopwatch();

            while (true) {
                if (isRunning) {
                    timer.Start();
                    Position += Speed;
                    OnChangedPosition();
                    timer.Stop();
                }

                await Task.Delay(TimeSpan.FromMilliseconds((float)Math.Sqrt(Speed.X * Speed.X + Speed.Y * Speed.Y) * (timer.ElapsedMilliseconds + 5) / 5));
            }

        }
        public override void OnChangedPosition() {
            ChangedPosition?.Invoke(this, new EventArgs());
        }

        public override void StopBall() {
            isRunning = false;
        }

        public override void StartMoveBall() {
            isRunning = true;
        }
    }
}