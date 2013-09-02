using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ChecklistManager.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var numbers = new int[] { 1, 2, 3, 4, 10 };
            Assert.IsTrue(IsSplitSum(numbers));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var numbers = new int[] { 1, 2, 3, 4, 11 };
            Assert.IsFalse(IsSplitSum(numbers));
        }

        [TestMethod]
        public void TestMethod3()
        {
            var numbers = new int[] { 11 , 2, 2, 3, 4};
            Assert.IsTrue(IsSplitSum(numbers));
        }

        internal static bool IsSplitSum(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return false;
            }
            double totalHalf = numbers.Sum() / 2.0;
            int splitSum = 0;
            return numbers.Any(n => (splitSum += n) == totalHalf);
        }
    }
}
