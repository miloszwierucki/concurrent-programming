using Data;

namespace DataApiTest {

    [TestClass]
    public class DataAPITest {
        DataAbstractApi testBall;

        [TestMethod]
        public void TestBallGetValues() {
            testBall = DataAbstractApi.CreateBall(4, 5);
            Assert.IsNotNull(testBall);

            Assert.AreEqual(testBall.getXPosition(), 4);
            Assert.AreEqual(testBall.getYPosition(), 5);
        }

        [TestMethod]
        public void TestBallSetValues() {
            testBall = DataAbstractApi.CreateBall(7, 8);

            Assert.AreEqual(testBall.getXPosition(), 7);
            Assert.AreEqual(testBall.getYPosition(), 8);

            testBall.setXPosition(10);
            testBall.setYPosition(10);

            Assert.AreEqual(testBall.getXPosition(), 10);
            Assert.AreEqual(testBall.getYPosition(), 10);
        }
    }
}