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
        public abstract void CreateLoggingTask(BlockingCollection<IBallLogData> logQueue);

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
        //private readonly string logPath = @"C:\Users\Mily\Desktop\Log.yaml";
        //private readonly string logPath = @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}/Log.yaml";
        private readonly string logPath = @$"{Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile)}/Desktop/Log.yaml";
        private bool newSession;
        private bool createThread = false;


        public Data() {
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
        public override void CreateLoggingTask(BlockingCollection<IBallLogData> logQueue) {
            if (createThread == false) {
                Thread thread = new Thread(() => CallLogger(logQueue));
                thread.Start();
                createThread = true;
            }
        }

        // Usuwa istniejący plik dziennika, jeśli istnieje i rozpoczyna nową sesję
        internal void FileMaker(string filename) {
            if (File.Exists(filename) && newSession) {
                newSession = false;
                File.Delete(filename);
            }
        }

        // Wywołuje zadanie logowania asynchronicznie
        internal async void CallLogger(BlockingCollection<IBallLogData> logQueue) {
            FileMaker(logPath);
            string diagnosticData;
            string log;

            try {
                foreach (var logObject in logQueue.GetConsumingEnumerable()) {
                    // Zapis do pliku w formacie YAML
                    diagnosticData = $"    BallID: {logObject.BallID}\n    BallPosition:\n      X: {logObject.Position.X}\n      Y: {logObject.Position.Y}\n    BallSpeed:\n      X: {logObject.Speed.X}\n      Y: {logObject.Speed.Y}";
                    log = String.Format("- Date: {0}\n  Info:\n{1}\n", logObject.Timestamp, diagnosticData);

                    File.AppendAllText(logPath, log);
                }
            } catch (Exception e) {

            }     
        }

    }
}