using MyBSTContainer;

namespace MyBSTContainerTest;

[TestClass]
public class PreorderTraversalTest
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

        var enumerator = tree.Preorder().GetEnumerator();

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 10);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 1);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 100);

        Assert.IsFalse(enumerator.MoveNext());

        enumerator = tree.ReversePreorder().GetEnumerator();

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 100);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 1);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 10);

        Assert.IsFalse(enumerator.MoveNext());


    }

    [TestMethod]
    public void TestMethod2()
    {
        TreeContainer<int> tree = new TreeContainer<int>() { 5, 4, 3, 1, 6, 2 };

        Assert.AreEqual(tree.Count, 6);

        var enumerator = tree.Preorder().GetEnumerator();

        int[] answer = { 4, 2, 1, 3, 5, 6 };
        foreach (int i in answer)
        {
            Assert.IsFalse(!enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current, i);
        }
        Assert.IsFalse(enumerator.MoveNext());

        enumerator = tree.ReversePreorder().GetEnumerator();

        foreach (int i in answer.Reverse())
        {
            Assert.IsFalse(!enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current, i);
        }
        Assert.IsFalse(enumerator.MoveNext());
    }
}

