using MyBSTContainer;

namespace MyBSTContainerTest;

[TestClass]
public class CreationTest
{
    [TestMethod]
    public void TestMethod1()
    {
        TreeContainer<int> tree = new TreeContainer<int>();

        tree.Add(100);
        tree.Add(10);
        tree.Add(1);

        
    }
}