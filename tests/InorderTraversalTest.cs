using MyBSTContainer;

namespace MyBSTContainerTest;

[TestClass]
public class InorderTraversalTest
{
    [TestMethod]
    public void TestMethod1()
    {
        TreeContainer<int> tree = new TreeContainer<int>();

        tree.Add(100);
        tree.Add(10);
        tree.Add(1);

        var enumerator = tree.Inorder().GetEnumerator();

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 1);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 10);

        Assert.IsFalse(!enumerator.MoveNext());
        Assert.AreEqual(enumerator.Current, 100);

        Assert.IsFalse(enumerator.MoveNext());


    }

    [TestMethod]
    public void TestMethod2()
    {
        TreeContainer<int> tree = new TreeContainer<int>();

        tree.Add(5);
        tree.Add(4);
        tree.Add(3);
        tree.Add(1);
        tree.Add(6);
        tree.Add(2);

        var enumerator = tree.Inorder().GetEnumerator();

        for (int i = 1; i <= 6; i++) {
            Assert.IsFalse(!enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current, i);
        }
        Assert.IsFalse(enumerator.MoveNext());

        enumerator = tree.ReverseInorder().GetEnumerator();

        for (int i = 6; i >= 1; i--)
        {
            Assert.IsFalse(!enumerator.MoveNext());
            Assert.AreEqual(enumerator.Current, i);
        }
        Assert.IsFalse(enumerator.MoveNext());
    }
}