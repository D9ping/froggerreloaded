using Frogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace FroggerTest
{
    
    
    /// <summary>
    ///This is a test class for GameEngineTest and is intended
    ///to contain all GameEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameEngineTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for UpdatePositionMovingObjects
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Frogger.exe")]
        public void UpdatePositionMovingObjectsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GameEngine_Accessor target = new GameEngine_Accessor(param0); // TODO: Initialize to an appropriate value
            target.UpdatePositionMovingObjects();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DetectCollision
        ///</summary>
        [TestMethod()]
        public void DetectCollisionTest()
        {
            int level = 0; // TODO: Initialize to an appropriate value
            FrmGame frmgame = null; // TODO: Initialize to an appropriate value
            Niveau tier = new Niveau(); // TODO: Initialize to an appropriate value
            GameEngine target = new GameEngine(level, frmgame, tier); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DetectCollision();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CheckGameTime
        ///</summary>
        [TestMethod()]
        public void CheckGameTimeTest()
        {
            int level = 0; // TODO: Initialize to an appropriate value
            FrmGame frmgame = null; // TODO: Initialize to an appropriate value
            Niveau tier = new Niveau(); // TODO: Initialize to an appropriate value
            GameEngine target = new GameEngine(level, frmgame, tier); // TODO: Initialize to an appropriate value
            int min = 0; // TODO: Initialize to an appropriate value
            target.CheckGameTime(min);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        /// A test for CreateCarRandomColor methode
        /// Because the color of the car is random we need ALL possible
        ///  random objects to be compared with the 1 expected object.
        /// If 1 is the same than this test is passed.
        ///</summary>
        [TestMethod()]
        public void CreateCarRandomColorTest()
        {
            //Initialize to an appropriate value
            int level = 1;
            Niveau tier = Niveau.easy;
            Direction direction = Direction.East;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, level, tier);
            //create target
            GameEngine target = new GameEngine(level, frmgame, tier);

            for (int times = 0; times < 10; times++)
            {
                //Create object from methode.
                MovingObject actual = target.CreateCarRandomColor(1, direction, 0, new Random());
                //Test: are object created.
                Assert.IsNotNull(actual, "cannot create object from methode.");
            }

        }

        /// <summary>
        ///A test for CreateTreeTrunk
        ///</summary>
        [TestMethod()]
        public void CreateTreeTrunkTest()
        {
            //Initialize to an appropriate value
            int level = 1;
            int vel = 1;
            Niveau tier = Niveau.easy;
            Direction direction = Direction.East;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, level, tier);
            //create target
            GameEngine target = new GameEngine(level, frmgame, tier);

            MovingObject actual = target.CreateTreeTrunk(vel, direction, 0);
            Assert.IsNotNull(actual, "cannot create object from methode.");
        }

        /// <summary>
        ///A test for CreateFrog
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Frogger.exe")]
        public void CreateFrogTest()
        {
            //Initialize to an appropriate value
            int level = 1;
            Niveau tier = Niveau.easy;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, level, tier);
            //create target
            GameEngine target = new GameEngine(level, frmgame, tier);

            MovingObject testfrog = target.CreateFrog();
            Assert.IsNotNull(testfrog, "cannot create a test frog.");
            int exceptedPosX = (frmgame.ClientRectangle.Width / 2) - (testfrog.Width / 2);
            int exceptedPosY = (frmgame.ClientRectangle.Height - testfrog.Height - 5); // target.frogbottommargin
            if ((testfrog.Location.X != exceptedPosX) && (testfrog.Location.Y == exceptedPosY))
            {
                Assert.Fail("Position test frog is not what is excepted.");
            }
        }

        /// <summary>
        ///A test for StopEngine
        ///</summary>
        [TestMethod()]
        public void StopEngineTest()
        {
            int level = 1;
            Niveau tier = Niveau.easy;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, level, tier);

            GameEngine target = new GameEngine(level, frmgame, tier); // TODO: Initialize to an appropriate value
            target.StopEngine();
            if (target.GameUpdateStatus != false)
            {
                Assert.Fail("Game Engine has not stopped.");
            }
        }

        /// <summary>
        ///A test for CheckLives
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Frogger.exe")]
        public void CheckLivesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GameEngine_Accessor target = new GameEngine_Accessor(param0); // TODO: Initialize to an appropriate value
            target.CheckLives();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
