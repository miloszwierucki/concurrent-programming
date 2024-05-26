//using Data;
//using System.Numerics;
//using Moq;

//namespace DataApiTest {

//    [TestClass]
//    public class DataAPITest {
//        private DataAbstractApi collection;
//        private ITable testTable;
//        private IBall testBall1, testBall2, testBall3;

//        [TestMethod]
//        public void BallTest() { 
//            testBall1 = IBall.CreateInstance(1, new Vector2(1, 2), 1, new Vector2(0, 0), 40);

//            Assert.IsNotNull(testBall1);

//            Assert.AreEqual(testBall1.Position, new Vector2(1, 2));
//            Assert.AreEqual(testBall1.Radius, 1);
//            Assert.AreEqual(testBall1.Speed, new Vector2(0, 0));
//            Assert.AreEqual(testBall1.Weight, 40);

//            testBall1.Radius = 10;
//            testBall1.Speed = new Vector2(2, 4);
//            testBall1.Weight = 20;

//            Assert.AreEqual(testBall1.Radius, 10);
//            Assert.AreEqual(testBall1.Speed, new Vector2(2, 4));
//            Assert.AreEqual(testBall1.Weight, 20);
//        }


//        [TestMethod]
//        public void TableTest()
//        {
//            testTable = ITable.CreateInstance(10, 10);
//            Assert.IsNotNull(testTable);

//            Assert.AreEqual(testTable.Height, 10);
//            Assert.AreEqual(testTable.Width, 10);
//        }

//        [TestMethod]
//        public void BallsCollectionTest()
//        {
//            collection = DataAbstractApi.CreateBallCollection();
//            testBall1 = IBall.CreateInstance(1, new Vector2(5, 7), 2, new Vector2(0, 0), 40);
//            testBall2 = IBall.CreateInstance(2, new Vector2(3, 73), 2, new Vector2(0, 0), 40);
//            testBall3 = IBall.CreateInstance(3, new Vector2(5, 55), 2, new Vector2(0, 0), 40);


//            Assert.AreEqual(collection.GetBallsCount(), 0);
//            collection.AddBall(testBall1);
//            Assert.AreEqual(collection.GetBallsCount(), 1);
//            collection.AddBall(testBall2);
//            Assert.AreEqual(collection.GetBallsCount(), 2);
//            collection.AddBall(testBall3);
//            Assert.AreEqual(collection.GetBallsCount(), 3);

//            Assert.AreEqual(testBall1, collection.GetBall(0));
//            Assert.AreEqual(testBall2, collection.GetBall(1));
//            Assert.AreEqual(testBall3, collection.GetBall(2));
//        }
//    }
//}