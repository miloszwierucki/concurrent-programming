using Data;
using System.Data;

namespace Logic {
    public abstract class LogicAbstractApi {
        public abstract void AddBalls(int quantity);
        public abstract void RemoveBalls(int quantity);
        public abstract void Start();
        public abstract void Stop();

        public abstract List<IMovabaleBall> GetBalls();

        public abstract int GetBallsCount();

        public static LogicAbstractApi CreateInstance(DataAbstractApi? Data = default, List<IBall>? Balls = default, ITable? Table = default) {
            DataAbstractApi data = DataAbstractApi.CreateInstance();
            return new BallManager(Data ?? data, Balls ?? new List<IBall>(), Table ?? ITable.CreateInstance(w: data.GetTableWidth(), data.GetTableHeight()));
        }
    }
}