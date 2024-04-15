using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;
using ModelApi;

namespace ViewModelApi {
    public class BallProps : VM {
        private Vector2 position;
        public float X {
            get { 
                return position.X; 
            }

            set { 
                position.X = value; OnPropertyChanged();
            }
        }

        public float Y {
            get { 
                return position.Y;
            }

            set { 
                position.Y = value; OnPropertyChanged(); 
            }
        }

        public int r { get; set; }

        public BallProps() {
            X = 0;
            Y = 0;
            r = 40;
        }

        public void ChangePosition(Vector2 position) {
            X = position.X;
            Y = position.Y;
        }
    }


    public class AsyncObservableCollection<T> : ObservableCollection<T> {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public AsyncObservableCollection() {}

        public AsyncObservableCollection(IEnumerable<T> list): base(list) {}

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (SynchronizationContext.Current == _synchronizationContext) {
                RaiseCollectionChanged(e);

            } else {
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param) {
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (SynchronizationContext.Current == _synchronizationContext) {
                RaisePropertyChanged(e);
            } else {
                _synchronizationContext.Send(RaisePropertyChanged, e);
            }
        }

        private void RaisePropertyChanged(object param) {
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }


    public class ViewModel : VM {
        private Model model;
        public AsyncObservableCollection<BallProps> Circles { get; set; }

        public int BallsCount {
            get { 
                return model.getBallsCount(); 
            }

            set {
                if (value >= 0) {
                    model.setBallCount(value);
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddBallButton { get; }
        public ICommand RemoveBallButton { get; }
        public ICommand StartButton { get; }
        public ICommand StopButton { get; }

        public ViewModel() {
            Circles = new AsyncObservableCollection<BallProps>();
            model = new Model();
            BallsCount = 1;

            AddBallButton = new Commands(() => {
                BallsCount += 1;
            });

            RemoveBallButton = new Commands(() => {
                BallsCount -= 1;
            });

            StartButton = new Commands(() => {
                model.setBallCount(BallsCount);

                for (int i = 0; i < BallsCount; i++) {
                    Circles.Add(new BallProps());
                }

                model.BallPositionChange += (sender, argv) => {
                    if (Circles.Count > 0)
                        Circles[argv.id].ChangePosition(argv.position);
                };
                model.start();
            });

            StopButton = new Commands(() => {
                model.stop();
                Circles.Clear();

                model.setBallCount(BallsCount);

            });
        }
    }
}


