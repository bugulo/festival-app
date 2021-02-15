using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExampleTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.IsFalse(false, "I am false");
        }
    }
}
