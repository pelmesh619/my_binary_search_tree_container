using MyBSTContainer;
using static System.Net.Mime.MediaTypeNames;

namespace MyBSTContainerTest;

[TestClass]
public class FindTest
{
    [TestMethod]
    public void TestMethod1()
    {
        var tree = new TreeContainer<String>();

        tree.Add("fuf");
        tree.Add("kiki");
        tree.Add("foobar");
        tree.Add("papapa");
        tree.Add("lalala");

        Assert.IsTrue(tree.Contains("fuf"));
        Assert.IsFalse(tree.Contains("gugugu"));
        Assert.IsTrue(tree.Contains("papapa"));

    }

}
