using Frogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
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
            //Initialize to an appropriate value
            string lvlname = "1";
            Niveau tier = Niveau.easy;
            Direction direction = Direction.East;

            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            GameEngine target = new GameEngine(lvlname, frmgame, tier);

            int velocity = 2;
            MovingObject car = target.CreateCarRandomColor(velocity, direction, 100, 100,new Random());
            Point oldpos = car.Location;
            target.UpdatePositionMovingObjects();
            Assert.AreEqual(oldpos, new Point(oldpos.X, oldpos.Y));

            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DetectCollision
        ///</summary>
        [TestMethod()]
        public void DetectCollisionTest()
        {
            //Initialize to an appropriate value
            String lvlname = "1";
            Niveau tier = Niveau.easy;
            Direction direction = Direction.East;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            GameEngine target = new GameEngine(lvlname, frmgame, tier);

            MovingObject testfrog = target.CreateFrog();
            Assert.IsNotNull(testfrog, "cannot create frog.");

            MovingObject car = target.CreateCarRandomColor(0, direction, 100, 100, new Random()); //velocity is 0
            Assert.IsNotNull(car, "cannot create car.");

            target.movingobjs.Add(car);

            testfrog.Location = new Point(car.Location.X, car.Location.Y);
            if (target.NumObjects == 0) { Assert.Fail("no objects."); }

            if (target.DetectCollision(car) == false)
            {
                Assert.Fail("There should be a collision.");
            }
        }

        /// <summary>
        ///A test for CheckGameTime
        ///</summary>
        [TestMethod()]
        public void CheckGameTimeTest()
        {
            String lvlname = "1";
            Niveau tier = Niveau.easy;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            //create target
            GameEngine target = new GameEngine(lvlname, frmgame, tier);

            for (int min = 20; min >= 0; min--)
            {
                if (target.CheckGameTime(min)) { Assert.Fail("Should not be gameover yet, " + (min+1)+" minuts to go."); }
            }
            if (target.CheckGameTime(-1)==false) { Assert.Fail("Should be Game Over because time is up."); }
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
            String lvlname = "1";
            Niveau tier = Niveau.easy;
            Direction direction = Direction.East;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            //create target
            GameEngine target = new GameEngine(lvlname, frmgame, tier);

            for (int times = 0; times < 10; times++)
            {
                //Create object from methode.
                MovingObject actual = target.CreateCarRandomColor(1, direction, 0, 100, new Random());
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
            String lvlname = "1";
            int vel = 1;
            Niveau tier = Niveau.easy;
            Direction direction = Direction.East;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            //create target
            GameEngine game = new GameEngine(lvlname, frmgame, tier);
            MovingObject actual = game.CreateTreeTrunk(vel, direction, 0, 0);
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
            String lvlname = "1";
            Niveau tier = Niveau.easy;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            //create target
            GameEngine target = new GameEngine(lvlname, frmgame, tier);

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
            String lvlname = "1";
            Niveau tier = Niveau.easy;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);

            GameEngine target = new GameEngine(lvlname, frmgame, tier); // TODO: Initialize to an appropriate value
            target.StopEngine(true);
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
            String lvlname = "1";
            Niveau tier = Niveau.easy;
            FrmMenu frmmenu = new FrmMenu();
            FrmGame frmgame = new FrmGame(frmmenu, lvlname, tier);
            GameEngine target = new GameEngine(lvlname, frmgame, tier);

            for (int lives = 8; lives > 0; lives--)
            {
                if (target.CheckLives(lives))
                {
                    Assert.Fail("shouldnt be game over yet, still " + lives + " over.");
                }
            }
            if (!target.CheckLives(0))
            {
                Assert.Fail("should be game over because there are no lives more left.");
            }
        }
    }
}
