// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Numerics;

namespace Data {
    public abstract class IBallLogData {
        public abstract int BallID { get; }
        public abstract Vector2 Position { get; }
        public abstract Vector2 Speed { get; }
        public abstract string Timestamp { get; }
    }

    internal class BallLogData : IBallLogData {
        public override int BallID { get; }
        public override Vector2 Position { get; }
        public override Vector2 Speed { get; }
        public override string Timestamp { get; }

        public BallLogData(int id, Vector2 p, Vector2 v) {
            BallID = id;
            Position = p;
            Speed = v;
            Timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.FFF");
        }
    }
}