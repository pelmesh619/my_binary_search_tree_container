using MyBSTContainer;

namespace MyBSTContainerTest;

[TestClass]
public class PostorderTraversalTest
{

    [TestMethod]
    public void TestMethod1()
    {
        TreeContainer<int> tree = new TreeContainer<int>();

        Assert.AreEqual(tree.Count, 0);

        tree.Add(100);
        tree.Add(10);
        tree.Add(1);

        Assert.AreEqual(tree.Count, 3);

        var enumerator = tree.Postorder().GetEnumerator();

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 1);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 100);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 10);

        Assert.IsFalse(enumerator.MoveNext());

        enumerator = tree.ReversePostorder().GetEnumerator();

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 10);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 100);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 1);

        Assert.IsFalse(enumerator.MoveNext());


    }

    [TestMethod]
    public void TestMethod2()
    {
        TreeContainer<int> tree = new TreeContainer<int>() { 5, 4, 3, 1, 6, 2 };

        Assert.AreEqual(tree.Count, 6);

        var enumerator = tree.Postorder().GetEnumerator();

        int[] answer = { 1, 3, 2, 6, 5, 4 };
        foreach (int i in answer)
        {
            Assert.IsFalse(!enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current, i);
        }
        Assert.IsFalse(enumerator.MoveNext());

        enumerator = tree.ReversePostorder().GetEnumerator();

        foreach (int i in answer.Reverse())
        {
            Assert.IsFalse(!enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current, i);
        }
        Assert.IsFalse(enumerator.MoveNext());
    }
}

