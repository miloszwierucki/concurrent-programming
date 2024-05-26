
using Logic;
using System.Collections.ObjectModel;

namespace Model {
    public abstract class ModelApi {
        public abstract void CreateBalls();
        public abstract void Start();
        public abstract void Stop();
        public abstract ObservableCollection<object> GetBalls();
        public abstract int BallsCount { get; set; }

        public static ModelApi CreateInstance() {
            return new Model();
        }
    }

    internal class Model : ModelApi {
        private ObservableCollection<object> _balls = new ObservableCollection<object>();
        private LogicAbstractApi LogicApi = LogicAbstractApi.CreateInstance();
        private int _ballQuantity = 0;
        public override int BallsCount { get => _ballQuantity; set => _ballQuantity = value; }

        public override void CreateBalls() {
            LogicApi.AddBalls(_ballQuantity);
        }

        public override ObservableCollection<object> GetBalls() {
            foreach (object ball in LogicApi.GetBalls()) {
                _balls.Add(ball);
            }
            return _balls;
        }

        public override void Start() {
            LogicApi.Start();
        }

        public override void Stop() {
            LogicApi.Stop();
        }
    }
}