# my_binary_search_tree_container

Originally this was one of laboratory works in C++ course at ITMO University in the 2nd semester that 
I wanted to publish in GitHub. But code in there was shameful to publish, so I decided to 
rewrite an entire thing on C# for more practice 👍👍👍

Original requirements were:

 * Create an assotiative template container based on binary search tree
 * Create a custom bidirectional iterator to iterate through the container via three different traversal ways: inorder, preorder, postorder
 * And make tests to cover all this

As C# has "enumerators" instead of "iterators", I made in total 6 different enumerators for this. 
Default enumerator for my container works as inorder.

Also my container implements `ISet<T>` interface

Also one of additional requirements was to make an AVL-tree, so this container should be pretty fast🤔




