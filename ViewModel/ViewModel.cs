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
                if (BallsCount < 10) BallsCount += 1;
            });

            RemoveBallButton = new Commands(() => {
                if (BallsCount > 0) BallsCount -= 1;
            });

            StartButton = new Commands(() => {
                if (Balls != null) {
                    ModelApi.Start();
                }
                ModelApi.CreateBalls();
                Balls = ModelApi.GetBalls();
            });

            StopButton = new Commands(() => {
                ModelApi.Stop();
            });
        }
    }
}