using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;

namespace Data {
    public abstract class DataAbstractApi {
        public abstract double GetTableWidth();
        public abstract double GetTableHeight();
        public abstract double GetBallRadius();
        public abstract double GetBallMaxSpeed();
        public abstract double GetBallWeight();
        public abstract Task CreateLoggingTask(ConcurrentQueue<IBall> logQueue);

        public static IBall CreateNewBall(int id, Vector2 p, double r, Vector2 s, double w) {
            return new Ball(id, p, r, s, w, true);
        }

        public static ITable CreateNewTable(double w, double h) {
            return new Table(w, h);
        }

        public static DataAbstractApi CreateInstance() {
            return new Data();
        }
    }

    internal class Data : DataAbstractApi {
        private readonly double width = 600;
        private readonly double height = 500;
        private readonly double ballRadius = 40;
        private readonly double ballMaxSpeed = 2;
        private readonly double ballWeight = 10;
        private readonly string logPath = @"C:\Users\Mily\Desktop\Log.yaml";
        private readonly Stopwatch stopWatch;
        private bool newSession;

        public Data() {
            stopWatch = new Stopwatch();
            newSession = true;
        }

        public override double GetBallRadius() {
            return ballRadius;
        }

        public override double GetBallMaxSpeed() {
            return ballMaxSpeed;
        }

        public override double GetBallWeight() {
            return ballWeight;
        }

        public override double GetTableHeight() {
            return height;
        }

        public override double GetTableWidth() {
            return width;
        }

        // Tworzy zadanie logowania z określoną kolejką piłek z zapisanym stanem
        public override Task CreateLoggingTask(ConcurrentQueue<IBall> logQueue) {
            return CallLogger(logQueue);
        }

        // Usuwa istniejący plik dziennika, jeśli istnieje i rozpoczyna nową sesję
        internal void FileMaker(string filename) {
            if (File.Exists(filename) && newSession) {
                newSession = false;
                File.Delete(filename);
            }
        }

        // Wywołuje zadanie logowania asynchronicznie
        internal async Task CallLogger(ConcurrentQueue<IBall> logQueue) {
            FileMaker(logPath);
            string diagnosticData;
            string timestamp;
            string log;
            ManualResetEvent queueNotEmpty = new ManualResetEvent(false);

            while (true) {
                stopWatch.Reset();
                stopWatch.Start();
                IBall logObject;

                if (!logQueue.IsEmpty) {
                    while (!logQueue.TryDequeue(out logObject)) {
                        queueNotEmpty.Reset();
                        queueNotEmpty.WaitOne();
                    }

                    // Zapis do pliku w formacie YAML
                    timestamp = DateTime.Now.ToString("MM/DD/YYYY HH:MM:SS.FFF");
                    diagnosticData = $"    BallID: {logObject.BallID}\n    BallPosition:\n      X: {logObject.Position.X}\n      Y: {logObject.Position.Y}\n    BallSpeed:\n      X: {logObject.Speed.X}\n      Y: {logObject.Speed.Y}";
                    log = String.Format("- Date: {0}\n  Info:\n{1}\n", timestamp, diagnosticData);

                    lock (this) {
                        File.AppendAllText(logPath, log);
                    }

                    stopWatch.Stop();
                    await Task.Delay((int)(stopWatch.ElapsedMilliseconds));
                } else {
                    queueNotEmpty.Set(); // Ustawienie zdarzenie, które umożliwić kontynuację oczekujących wątków
                    await Task.Delay(100); // Opóźnienie przed ponownym sprawdzeniem kolejki logQueue
                }
            }
        }

    }
}