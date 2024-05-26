// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Numerics;

namespace Data {
    public abstract class IBall {
        public abstract event EventHandler? ChangedPosition;
        public abstract void StopBall();
        public abstract void StartMoveBall();
        public abstract Vector2 Position { get; protected set; }
        public abstract Vector2 Speed { get; set; }
        public abstract double Radius { get; }
        public abstract double Weight { get; }
        public static IBall CreateInstance(Vector2 p, double r, Vector2 v, double m, bool isRunning) {
            return new Ball(p, r, v, m, isRunning);
        }
    }


    internal class Ball : IBall {
        public override Vector2 Position { get; protected set; }
        public override double Radius { get; }
        public override double Weight { get; }

        public override Vector2 Speed { get; set; }
        private bool isRunning;
        public override event EventHandler? ChangedPosition;

        public Ball(Vector2 p, double r, Vector2 v, double m, bool isRunning) {
            Position = p;
            Radius = r;
            Speed = v;
            Weight = m;

            this.isRunning = isRunning;
            new Thread(new ThreadStart(Move)).Start();
        }

        public void OnChangedPosition() {
            ChangedPosition?.Invoke(this, new EventArgs());
        }

        private async void Move() {
            Stopwatch timer = new Stopwatch();
            float multiplier = 0;

            while (true) {
                timer.Restart();

                if (isRunning) {
                    Position += Speed * multiplier;
                    OnChangedPosition();
                }

                await Task.Delay(5);
                timer.Stop();
                multiplier = ((float)timer.Elapsed.TotalMilliseconds) / 5;
            }
        }

        public override void StopBall() {
            isRunning = false;
        }

        public override void StartMoveBall() {
            isRunning = true;
        }

    }
}