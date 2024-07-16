using System.Collections;

namespace MyBSTContainer
{
    public partial class TreeContainer<T>
    {
        internal class TreeContainerEnumerator : IEnumerator<T>
        {
            internal TreeContainerEnumerator(TreeContainer<T> tree)
            {
                this.tree = tree;
                Reset();
            }

            protected TreeContainer<T>.Node? current_node = null;
            protected TreeContainer<T> tree;
            protected bool is_beginning = true;
            public T Current
            {
                get
                {
                    if (current_node != null)
                    {
                        return current_node.Value;
                    }
                    throw new NullReferenceException();

                }
            }
            object IEnumerator.Current => (object)Current;
            public void Dispose() { }
            public virtual bool MoveNext() => throw new NotImplementedException();
            public virtual void Reset() { }
        }


        public class TraversalWrap : IEnumerable<T>
        {
            public TraversalWrap(TreeContainer<T> tree) { this.tree = tree; }
            protected TreeContainer<T> tree;

            public virtual IEnumerator<T> GetEnumerator() => throw new NotImplementedException();
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public void CopyTo(T[] array, int arrayIndex)
            {
                int counter = 0;
                foreach (T i in this)
                {
                    if (arrayIndex + counter < array.Count())
                    {
                        array[arrayIndex + counter++] = i;
                    }
                }
            }
        }

        public class InorderTraversalWrap : TraversalWrap, IEnumerable<T>
        {
            public InorderTraversalWrap(TreeContainer<T> tree) : base(tree) { }

            public override IEnumerator<T> GetEnumerator()
            {
                return new InorderTreeEnumerator(this.tree);
            }
        }
        internal class InorderTreeEnumerator : TreeContainerEnumerator, IEnumerator<T>
        {
            internal InorderTreeEnumerator(TreeContainer<T> tree) : base(tree) { }

            public override bool MoveNext()
            {
                if (is_beginning && current_node != null)
                {
                    is_beginning = false;
                    return true;
                }
                if (current_node == null)
                {
                    return false;
                }
                current_node = current_node.NextNode;
                return current_node != null;
            }

            public override void Reset()
            {
                is_beginning = true;
                current_node = tree.LeastNode;
            }
        }

        public class ReverseInorderTraversalWrap : TraversalWrap, IEnumerable<T>
        {
            public ReverseInorderTraversalWrap(TreeContainer<T> tree) : base(tree) { }
            public override IEnumerator<T> GetEnumerator()
            {
                return new ReverseInorderTreeEnumerator(this.tree);
            }
        }
        internal class ReverseInorderTreeEnumerator : TreeContainerEnumerator, IEnumerator<T>
        {
            internal ReverseInorderTreeEnumerator(TreeContainer<T> tree) : base(tree) { }

            public override bool MoveNext()
            {
                if (is_beginning && current_node != null)
                {
                    is_beginning = false;
                    return true;
                }
                if (current_node == null)
                {
                    return false;
                }
                current_node = current_node.PreviousNode;
                return current_node != null;
            }

            public override void Reset()
            {
                is_beginning = true;
                current_node = tree.MostNode;
            }
        }

        public class PreorderTraversalWrap : TraversalWrap, IEnumerable<T>
        {
            public PreorderTraversalWrap(TreeContainer<T> tree) : base(tree) { }
            public override IEnumerator<T> GetEnumerator()
            {
                return new PreorderTreeEnumerator(this.tree);
            }
        }
        internal class PreorderTreeEnumerator : TreeContainerEnumerator, IEnumerator<T>
        {
            internal PreorderTreeEnumerator(TreeContainer<T> tree) : base(tree) { }

            public override bool MoveNext()
            {
                if (is_beginning && current_node != null)
                {
                    is_beginning = false;
                    return true;
                }
                if (current_node == null)
                {
                    return false;
                }
                current_node = current_node.NextPreorderNode;
                return current_node != null;
            }

            public override void Reset()
            {
                is_beginning = true;
                current_node = tree.root;
            }
        }

        public class ReversePreorderTraversalWrap : TraversalWrap, IEnumerable<T>
        {
            public ReversePreorderTraversalWrap(TreeContainer<T> tree) : base(tree) { }
            public override IEnumerator<T> GetEnumerator()
            {
                return new ReversePreorderTreeEnumerator(this.tree);
            }
        }
        internal class ReversePreorderTreeEnumerator : TreeContainerEnumerator, IEnumerator<T>
        {
            internal ReversePreorderTreeEnumerator(TreeContainer<T> tree) : base(tree) { }

            public override bool MoveNext()
            {
                if (is_beginning && current_node != null)
                {
                    is_beginning = false;
                    return true;
                }
                if (current_node == null)
                {
                    return false;
                }
                current_node = current_node.PreviousPreorderNode;
                return current_node != null;
            }

            public override void Reset()
            {
                is_beginning = true;
                current_node = tree.MostLeaf;
            }
        }

        public class PostorderTraversalWrap : TraversalWrap, IEnumerable<T>
        {
            public PostorderTraversalWrap(TreeContainer<T> tree) : base(tree) { }
            public override IEnumerator<T> GetEnumerator()
            {
                return new PostorderTreeEnumerator(this.tree);
            }
        }
        internal class PostorderTreeEnumerator : TreeContainerEnumerator, IEnumerator<T>
        {
            internal PostorderTreeEnumerator(TreeContainer<T> tree) : base(tree) { }

            public override bool MoveNext()
            {
                if (is_beginning && current_node != null)
                {
                    is_beginning = false;
                    return true;
                }
                if (current_node == null)
                {
                    return false;
                }
                current_node = current_node.NextPostorderNode;
                return current_node != null;
            }

            public override void Reset()
            {
                is_beginning = true;
                current_node = tree.LeastLeaf;
            }
        }

        public class ReversePostorderTraversalWrap : TraversalWrap, IEnumerable<T>
        {
            public ReversePostorderTraversalWrap(TreeContainer<T> tree) : base(tree) { }
            public override IEnumerator<T> GetEnumerator()
            {
                return new ReversePostorderTreeEnumerator(this.tree);
            }
        }
        internal class ReversePostorderTreeEnumerator : TreeContainerEnumerator, IEnumerator<T>
        {
            internal ReversePostorderTreeEnumerator(TreeContainer<T> tree) : base(tree) { }

            public override bool MoveNext()
            {
                if (is_beginning && current_node != null)
                {
                    is_beginning = false;
                    return true;
                }
                if (current_node == null)
                {
                    return false;
                }
                current_node = current_node.PreviousPostorderNode;
                return current_node != null;
            }

            public override void Reset()
            {
                is_beginning = true;
                current_node = tree.root;
            }
        }

    }
}
