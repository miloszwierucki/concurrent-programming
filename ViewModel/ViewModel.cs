using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModelApi {
    public class ViewModel : VM {
        ModelApi ModelApi = ModelApi.CreateInstance();
        private ObservableCollection<object>? _balls;

        public ObservableCollection<object>? Balls {
            get => _balls;
            set {
                _balls = value;
                OnPropertyChanged();
            }
        }

        public int BallsCount {
            get => ModelApi.BallsCount;
            set {
                ModelApi.BallsCount = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddBallButton { get; }
        public ICommand RemoveBallButton { get; }
        public ICommand StartButton { get; }
        public ICommand StopButton { get; }

        public ViewModel() {
            AddBallButton = new Commands(() => {
                if (BallsCount < 10) {
                    BallsCount += 1;
                    ModelApi.CreateBalls();
                };
            });

            RemoveBallButton = new Commands(() => {
                if (BallsCount > 0) {
                    BallsCount -= 1;
                    ModelApi.RemoveBalls();
                };
            });

            StartButton = new Commands(() => {
                Balls = ModelApi.GetBalls();

                if (Balls != null) {
                    ModelApi.Start();
                }
            });

            StopButton = new Commands(() => {
                ModelApi.Stop();
                Balls.Clear();
            });
        }
    }
}