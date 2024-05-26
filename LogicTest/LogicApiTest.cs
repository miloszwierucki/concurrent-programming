using Moq;
using Logic;
using System.Numerics;

namespace Logic.Tests {
    [TestClass]
    public class LogicApiTest {

        [TestMethod]
        public void LogicRemoveBallTest() {
            LogicAbstractApi logicLayer = LogicAbstractApi.CreateInstance();
            logicLayer.AddBalls(5);
            Assert.AreEqual(logicLayer.GetBallsCount(), 5);

            logicLayer.RemoveBalls(2);
            Assert.AreEqual(logicLayer.GetBallsCount(), 3);
        }

        [TestMethod]
        public void BallManagerTest() {
            Mock<Data.IBall> ballMock = new Mock<Data.IBall>();
            ballMock.Setup(b => b.StopBall());
            ballMock.Setup(b => b.StartMoveBall());
            ballMock.Setup(b => b.Position).Returns(new Vector2(1, 1));
            ballMock.Setup(b => b.Speed).Returns(new Vector2(1, 1));
            ballMock.Setup(b => b.Radius).Returns(40);
            ballMock.Setup(b => b.Weight).Returns(10);

            Mock<Data.ITable> tableMock = new Mock<Data.ITable>();
            tableMock.Setup(b => b.Width).Returns(600);
            tableMock.Setup(b => b.Height).Returns(500);

            Mock<Data.DataAbstractApi> dataMock = new Mock<Data.DataAbstractApi>();
            dataMock.Setup(d => d.GetTableWidth()).Returns(600);
            dataMock.Setup(d => d.GetTableHeight()).Returns(500);
            dataMock.Setup(d => d.GetBallMaxSpeed()).Returns(2);
            dataMock.Setup(d => d.GetBallRadius()).Returns(40);
            dataMock.Setup(d => d.GetBallWeight()).Returns(10);


            List<Data.IBall> Balls = [ballMock.Object];
            Assert.IsNotNull(Balls);

            LogicAbstractApi logicLayer = LogicAbstractApi.CreateInstance(dataMock.Object, Balls, tableMock.Object);
            Assert.IsNotNull(logicLayer);
            Assert.IsTrue(logicLayer.GetBalls().Count == 1);

            logicLayer.Start();
            ballMock.Verify(b => b.StartMoveBall(), Times.Once);

            logicLayer.Stop();
            ballMock.Verify(b => b.StopBall(), Times.Once);

            logicLayer.Start();
            ballMock.Verify(b => b.StartMoveBall(), Times.Exactly(2));
        }
    }
}