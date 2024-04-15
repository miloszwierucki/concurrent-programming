using Logic;
using System.Numerics;

namespace LogicApiTest {

    [TestClass]
    public class LogicApiTest {
        [TestMethod]
        public void LogicRemoveBallTest()
        {
            LogicAbstractApi logicLayer = new BallsLogic(300, 300);
            logicLayer.addBalls(5);
            Assert.AreEqual(logicLayer.getBallsCount(), 5);

            logicLayer.removeBalls(2);
            Assert.AreEqual(logicLayer.getBallsCount(), 3);
        }
    }
}