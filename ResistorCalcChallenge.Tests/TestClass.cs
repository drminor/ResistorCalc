using NUnit.Framework;
using System;


namespace ResistorCalcChallenge.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestGoodValue1()
        {
            int rValue = new OhmValueCalculator().CalculateOhmValue("yellow", "violet", "red", "gold");

            Assert.AreEqual(4700, rValue);
            Assert.Pass("'yellow, violet, red, gold' produces 4700 ohms.");
        }

        [Test]
        public void TestGoodValue2()
        {
            int rValue = new OhmValueCalculator().CalculateOhmValue("black", "brown", "black", "gray");

            Assert.AreEqual(1, rValue);
            Assert.Pass("'black, brown, black, gray' produces 1 ohm.");
        }

        [Test]
        public void TestNullOkForTolerence()
        {
            int rValue = new OhmValueCalculator().CalculateOhmValue("yellow", "violet", "red", null);

            Assert.AreEqual(4700, rValue);
            Assert.Pass("'black, brown, black, null' produces 4700 ohms.");

        }

        [Test]
        public void UnrecognizedColorForSignficand()
        {
            Assert.Throws<ArgumentException>(() => GetRValue(), "'tan, violet, red, gold' should have thrown an ArgumentException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("tan", "violet", "red", "gold");
                return rValue;
            }
        }

        [Test]
        public void InvalidColorForSignficand()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GetRValue(), "'pink, violet, red, gold' should have thrown an ArgumentOutOfRangeException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("pink", "violet", "red", "gold");
                return rValue;
            }
        }

        [Test]
        public void UnrecognizedColorForMultiplier()
        {
            Assert.Throws<ArgumentException>(() => GetRValue(), "'yellow, violet, tan, gold' should have thrown an ArgumentException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("yellow", "violet", "tan", "gold");
                return rValue;
            }
        }

        [Test]
        public void UnrecognizedColorForTolerence()
        {
            Assert.Throws<ArgumentException>(() => GetRValue(), "'yellow, violet, red, tan' should have thrown an ArgumentException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("yellow", "violet", "red", "tan");
                return rValue;
            }
        }

        [Test]
        public void InvalidColorForTolerence()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GetRValue(), "'yellow, violet, red, black' should have thrown an ArgumentOutOfRangeException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("yellow", "violet", "red", "black");
                return rValue;
            }
        }

        [Test]
        public void ColorValuesProducesOverFlow()
        {
            Assert.Throws<InvalidOperationException>(() => GetRValue(), "'yellow, white, gray, gold' should have thrown an InvalidOperationException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("yellow", "violet", "gray", "gold");
                return rValue;
            }
        }

        [Test]
        public void ColorValuesProducesUnderFlow()
        {
            Assert.Throws<InvalidOperationException>(() => GetRValue(), "'black, brown, pink, gray' should have thrown an InvalidOperationException.");

            int GetRValue()
            {
                int rValue = new OhmValueCalculator().CalculateOhmValue("black", "brown", "pink", "gray");
                return rValue;
            }
        }
    }
}
