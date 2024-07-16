using System.Collections;


namespace MyBSTContainer
{
    public partial class TreeContainer<T> : ISet<T> where T : IComparable<T>
    {
        public TreeContainer() { }

        public int Count
        {
            get {
                if (root == null)
                {
                    return 0;
                }
                return root.Count; 
            }
        }
        public bool IsReadOnly { get; }

        public bool Add(T item)
        { 
            if (root == null)
            {
                root = new Node(item, this);
                return true;
            }
            return root.Insert(item);
            
        }

        public void Clear() {
            root = null;
        }

        public bool Contains(T item)
        {
            if (root == null)
            {
                return false;
            }
            return root.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            Inorder().CopyTo(array, arrayIndex);
        }
        public void ExceptWith(IEnumerable<T> other)
        {
            foreach (T item in other)
            {
                Remove(item);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            foreach (T item in this)
            {
                if (!other.Contains(item))
                    Remove(item);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (Count == other.Count())
            {
                return false;
            }
            foreach (T item in this)
            {
                if (Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (Count == other.Count())
            {
                return false;
            }
            foreach (T item in other)
            {
                if (!Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            foreach (T item in this)
            {
                if (Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            foreach (T item in other)
            {
                if (!Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            foreach (T item in this)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            if (Count != other.Count())
            {
                return false;
            }
            foreach (T item in other)
            {
                if (!Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            foreach (T item in other)
            {
                Add(item);
            }
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Remove(T item)
        {
            if (root == null)
            {
                return false;
            }
            bool is_removed_node_root = root.Value.CompareTo(item) == 0;
            Node? left_tree = root.LeftChild;
            Node? right_tree = root.RightChild;

            bool result = root.Delete(item);

            if (is_removed_node_root && result)
            {
                if (left_tree == null && right_tree == null)
                {
                    root = null;
                }
                else if (left_tree == null)
                {
                    root = right_tree;
                }
                else if (right_tree == null)
                {
                    root = left_tree;
                
                }
            }
            return result;


        }

        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T? this[int index] 
        {
            get { return root == null ? default : root.FindKElement(index + 1); }
        }

        public TraversalWrap Inorder()
        {
            return new InorderTraversalWrap(this);
        }

        public TraversalWrap ReverseInorder()
        {
            return new ReverseInorderTraversalWrap(this);
        }

        public TraversalWrap Preorder()
        {
            return new PreorderTraversalWrap(this);
        }

        public TraversalWrap ReversePreorder()
        {
            return new ReversePreorderTraversalWrap(this);
        }

        public TraversalWrap Postorder()
        {
            return new PostorderTraversalWrap(this);
        }

        public TraversalWrap ReversePostorder()
        {
            return new ReversePostorderTraversalWrap(this);
        }


    }
}
