using Moq;
using Logic;

namespace Logic.Tests
{
    [TestClass]
    public class LogicApiTest
    {
        [TestMethod]
        public void LogicRemoveBallTest()
        {
            LogicAbstractApi logicLayer = new BallsLogic(300, 300);
            logicLayer.AddBalls(5);
            Assert.AreEqual(logicLayer.GetBallsCount(), 5);

            logicLayer.RemoveBalls(2);
            Assert.AreEqual(logicLayer.GetBallsCount(), 3);
        }
        
        [TestMethod]
        public void Start_Calls_Move_Method_For_Each_Ball()
        {
            // Arrange
            var ballsApiMock = new Mock<Data.DataAbstractApi>();
            ballsApiMock.Setup(api => api.GetBallsCount()).Returns(3); // Assuming there are 3 balls
            var ballMock1 = new Mock<Data.IBall>();
            var ballMock2 = new Mock<Data.IBall>();
            var ballMock3 = new Mock<Data.IBall>();
            ballsApiMock.Setup(api => api.GetBall(0)).Returns(ballMock1.Object);
            ballsApiMock.Setup(api => api.GetBall(1)).Returns(ballMock2.Object);
            ballsApiMock.Setup(api => api.GetBall(2)).Returns(ballMock3.Object);

            var tableMock = new Mock<Data.ITable>();

            var logic = new BallsLogic(100, 100);
            logic.Balls = ballsApiMock.Object;
            logic.Table = tableMock.Object;

            // Act
            logic.Start();

            // Assert
            ballMock1.Verify(m => m.Position, Times.AtLeastOnce);
            ballMock2.Verify(m => m.Position, Times.AtLeastOnce);
            ballMock3.Verify(m => m.Position, Times.AtLeastOnce);
        }
    }
}
