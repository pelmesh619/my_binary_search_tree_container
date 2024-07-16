using MyBSTContainer;
using static System.Net.Mime.MediaTypeNames;

namespace MyBSTContainerTest;

[TestClass]
public class TraversalGeneratedTest
{
    [TestMethod]
    public void TestMethod1()
    {
        var tree = new TreeContainer<int>();

        tree.Add(84);
        tree.Add(21);
        tree.Add(22);
        tree.Add(15);
        tree.Add(32);
        tree.Add(64);
        tree.Add(89);
        tree.Add(35);
        tree.Add(29);
        tree.Add(5);


        int[] answer = { 22, 15, 5, 21, 64, 32, 29, 35, 84, 89 };
        int counter = 0;
        foreach (int i in tree.Preorder())
        {
            Assert.AreEqual(answer[counter++], i);
        }

        answer = [5, 15, 21, 22, 29, 32, 35, 64, 84, 89];
        counter = 0;
        foreach (int i in tree.Inorder())
        {
            Assert.AreEqual(answer[counter++], i);
        }

        answer = [5, 21, 15, 29, 35, 32, 89, 84, 64, 22];
        counter = 0;
        foreach (int i in tree.Postorder())
        {
            Assert.AreEqual(answer[counter++], i);
        }
    }

    [TestMethod]
    public void TestMethod2()
    {
        var tree = new TreeContainer<int>();

        tree.Add(88);
        tree.Add(76);
        tree.Add(77);
        tree.Add(72);
        tree.Add(97);
        tree.Add(32);
        tree.Add(4);
        tree.Add(29);
        tree.Add(50);
        tree.Add(74);
        tree.Add(51);
        tree.Add(82);
        tree.Add(48);
        tree.Add(6);
        tree.Add(34);

        int[] answer = { 97, 82, 88, 74, 76, 77, 51, 34, 48, 50, 29, 4, 6, 32, 72 };
        int counter = 0;
        foreach (int i in tree.ReversePreorder()) {
            Assert.AreEqual(answer[counter++], i);
        }

        answer = [97, 88, 82, 77, 76, 74, 72, 51, 50, 48, 34, 32, 29, 6, 4];
        counter = 0;
        foreach (int i in tree.ReverseInorder())
        {
            Assert.AreEqual(answer[counter++], i);
        }

        answer = [72, 77, 88, 97, 82, 76, 74, 32, 50, 51, 48, 34, 6, 29, 4];
        counter = 0;
        foreach (int i in tree.ReversePostorder())
        {
            Assert.AreEqual(answer[counter++], i);
        }
    }

}

