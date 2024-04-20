using Data;
using System.Numerics;

namespace DataApiTest {

    [TestClass]
    public class DataAPITest {
        private BallsCollection collection;
        private Table testTable;
        private IBall testBall1, testBall2, testBall3;

        [TestMethod]
        public void BallTest() { 
            testBall1 = IBall.CreateInstance(new Vector2(1, 2), 1, new Vector2(0, 0));

            Assert.IsNotNull(testBall1);

            Assert.AreEqual(testBall1.position, new Vector2(1, 2));
            Assert.AreEqual(testBall1.radius, 1);
            Assert.AreEqual(testBall1.speed, new Vector2(0, 0));

            testBall1.position = new Vector2(10, 4);
            testBall1.radius = 10;
            testBall1.speed = new Vector2(2, 4);

            Assert.AreEqual(testBall1.position, new Vector2(10, 4));
            Assert.AreEqual(testBall1.radius, 10);
            Assert.AreEqual(testBall1.speed, new Vector2(2, 4));
        }


        [TestMethod]
        public void TableTest()
        {
            testTable = new Table(10, 10);
            Assert.IsNotNull(testTable);

            Assert.AreEqual(testTable.height, 10);
            Assert.AreEqual(testTable.width, 10);
        }

        [TestMethod]
        public void BallsCollectionTest()
        {
            collection = new BallsCollection();
            testBall1 = IBall.CreateInstance(new Vector2(5, 7), 2, new Vector2(0, 0));
            testBall2 = IBall.CreateInstance(new Vector2(3, 73), 2, new Vector2(0, 0));
            testBall3 = IBall.CreateInstance(new Vector2(5, 55), 2, new Vector2(0, 0));


            Assert.AreEqual(collection.GetBallsCount(), 0);
            collection.AddBall(testBall1);
            Assert.AreEqual(collection.GetBallsCount(), 1);
            collection.AddBall(testBall2);
            Assert.AreEqual(collection.GetBallsCount(), 2);
            collection.AddBall(testBall3);
            Assert.AreEqual(collection.GetBallsCount(), 3);

            Assert.AreEqual(testBall1, collection.GetBall(0));
            Assert.AreEqual(testBall2, collection.GetBall(1));
            Assert.AreEqual(testBall3, collection.GetBall(2));
        }
    }
}