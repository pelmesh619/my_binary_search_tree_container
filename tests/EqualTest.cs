using MyBSTContainer;

namespace MyBSTContainerTest;

[TestClass]
 public class EqualTest
{

    [TestMethod]
    public void TestMethod1()
    {
        var tree1 = new TreeContainer<int>();

        tree1.Add(5);
        tree1.Add(4);
        tree1.Add(3);
        tree1.Add(1);
        tree1.Add(6);
        tree1.Add(2);

        var tree2 = new TreeContainer<int>();

        tree2.Add(4);
        tree2.Add(1);
        tree2.Add(2);
        tree2.Add(6);
        tree2.Add(5);
        tree2.Add(3);

        Assert.IsTrue(tree1.SetEquals(tree2));

    }


    [TestMethod]
    public void TestMethod2()
    {
        var tree1 = new TreeContainer<int>();

        tree1.Add(5);
        tree1.Add(4);
        tree1.Add(3);
        tree1.Add(1);
        tree1.Add(6);
        tree1.Add(2);

        var tree2 = new TreeContainer<int>();

        tree2.Add(4);
        tree2.Add(1);
        tree2.Add(2);
        tree2.Add(6);
        tree2.Add(5);
        tree2.Add(9);

        Assert.IsFalse(tree1.SetEquals(tree2));

    }

    [TestMethod]
    public void TestMethod3()
    {
        var tree1 = new TreeContainer<int>();

        tree1.Add(5);
        tree1.Add(4);
        tree1.Add(3);
        tree1.Add(1);
        tree1.Add(6);
        tree1.Add(2);

        var tree2 = tree1; 
        var tree3 = new TreeContainer<int>();

        Assert.IsTrue(tree3.Count == 0);
        tree3 = tree1;

        Assert.IsTrue(tree1.SetEquals(tree2));
        Assert.IsTrue(tree1.SetEquals(tree3));
        Assert.IsTrue(tree1.Count == 6);

    }

}
