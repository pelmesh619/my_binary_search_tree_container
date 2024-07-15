using System;
using System.Collections.Generic;

namespace MyBSTContainer
{
    public partial class TreeContainer<T> : ISet<T>
    {

        Node? root = null;
        private UIntPtr size = 0;

        Node? LeastNode
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                return root.LeastNode;
            }
        }
        Node? MostNode
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                return root.MostNode;
            }
        }
        Node? LeastLeaf
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                return root.LeastLeaf;
            }
        }
        Node? MostLeaf
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                return root.MostLeaf;
            }
        }
    }
}
