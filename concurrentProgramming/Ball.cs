// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;

namespace Data {
    public abstract class IBall {
        public abstract event EventHandler? ChangedPosition;
        public abstract void StopBall();
        public abstract void StartMoveBall(ConcurrentQueue<IBall> queue);
        public abstract int BallID { get; }
        public abstract Vector2 Position { get; protected set; }
        public abstract Vector2 Speed { get; set; }
        public abstract double Radius { get; }
        public abstract double Weight { get; }
        public static IBall CreateInstance(int id, Vector2 p, double r, Vector2 v, double m, bool isRunning) {
            return new Ball(id, p, r, v, m, isRunning);
        }
    }

    internal class Ball : IBall {
        public override Vector2 Position { get; protected set; }
        public override double Radius { get; }
        public override double Weight { get; }
        public override int BallID { get; }

        public override Vector2 Speed { get; set; }
        public override event EventHandler? ChangedPosition;

        private bool isRunning;
        private bool createThread = false;
        private readonly object locker = new object();

        public Ball(int id, Vector2 p, double r, Vector2 v, double m, bool isRunning) {
            BallID = id;
            Position = p;
            Radius = r;
            Speed = v;
            Weight = m;

            this.isRunning = isRunning;
            //new Thread(new ThreadStart(Move).Start();
        }

        public void OnChangedPosition() {
            ChangedPosition?.Invoke(this, new EventArgs());
        }

        private async void Move(ConcurrentQueue<IBall> queue) {
            Stopwatch timer = new Stopwatch();
            float multiplier = 0;
            double startTime = 0;
            double endTime = 0;
            timer.Start();

            while (true) {
                startTime = timer.Elapsed.TotalMilliseconds;

                if (isRunning) {
                    lock (locker)
                    {
                        Position += Speed * multiplier;
                        OnChangedPosition();
                        saveBall(queue);
                    }
                }

                await Task.Delay(5);
                endTime = timer.Elapsed.TotalMilliseconds;
                multiplier = (float)((endTime - startTime) / 5);
            }
        }

        public override void StopBall() {
            isRunning = false;
        }

        public override void StartMoveBall(ConcurrentQueue<IBall> queue) {
            isRunning = true;

            if (createThread == false){
                //new Thread(new ThreadStart(Move).Start();
                Thread thread = new Thread(() => Move(queue));
                thread.Start();
                createThread = true;
            }
        }

        private void saveBall(ConcurrentQueue<IBall> queue) {
            // Umieszcza w kolejce nowy obiekt Ball z zapisanym bieżącym stanem
            queue.Enqueue(new Ball(BallID, Position, Radius, Speed, Weight, isRunning));
        }
    }
}