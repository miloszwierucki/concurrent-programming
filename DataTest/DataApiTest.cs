using Data;
using System.Numerics;

namespace DataApiTest {

    [TestClass]
    public class DataAPITest {
        private DataAbstractApi testData;
        private ITable testTable;
        private IBall testBall1;

        [TestMethod]
        public void BallTest() { 
            testBall1 = IBall.CreateInstance(0, new Vector2(1, 2), 1, new Vector2(0, 0), 40, false);

            Assert.IsNotNull(testBall1);

            Assert.AreEqual(testBall1.BallID, 0);
            Assert.AreEqual(testBall1.Position, new Vector2(1, 2));
            Assert.AreEqual(testBall1.Radius, 1);
            Assert.AreEqual(testBall1.Speed, new Vector2(0, 0));
            Assert.AreEqual(testBall1.Weight, 40);

            testBall1.Speed = new Vector2(2, 4);

            Assert.AreEqual(testBall1.Speed, new Vector2(2, 4));
        }


        [TestMethod]
        public void TableTest() {
            testTable = ITable.CreateInstance(10, 10);
            Assert.IsNotNull(testTable);

            Assert.AreEqual(testTable.Height, 10);
            Assert.AreEqual(testTable.Width, 10);
        }

        [TestMethod]
        public void DataApiTest() {
            testData = DataAbstractApi.CreateInstance();
            Assert.IsTrue(testData.GetTableWidth() == 600);
            Assert.IsTrue(testData.GetTableHeight() == 500);
            Assert.IsTrue(testData.GetBallMaxSpeed() == 2);
            Assert.IsTrue(testData.GetBallRadius() == 40);
            Assert.IsTrue(testData.GetBallWeight() == 10);
        }
    }
}