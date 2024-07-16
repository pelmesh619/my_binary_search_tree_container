namespace MyBSTContainer
{
    partial class TreeContainer<T>
    {
        internal class Node(T value, TreeContainer<T> tree)
        {
            public T Value { get; private set; } = value;

            Node? parent = null;
            Node? left_child = null;
            Node? right_child = null;
            TreeContainer<T> tree = tree;

            int height = 1;
            int diff = 0;
            int children = 1;

            public int Count
            {
                get { return children; }
            }

            internal Node? LeftChild { get { return left_child; } }
            internal Node? RightChild { get { return right_child; } }


            private void Untie()
            {
                parent = null;
                left_child = null;
                right_child = null;
                height = 1;
                diff = 0;
                children = 1;
            }

            private void RecountChildren()
            {
                int new_value = 1;
                if (left_child != null)
                {
                    new_value += left_child.children;
                }
                if (right_child != null)
                {
                    new_value += right_child.children;
                }
                children = new_value;
                if (parent != null && children != new_value)
                {
                    parent.RecountChildren();
                }
            }

            private void Reheight()
            {
                int right_height = right_child != null ? right_child.height : 0;
                int left_height = left_child != null ? left_child.height : 0;

                height = 1 + (right_height > left_height ? right_height : left_height);
                diff = left_height - right_height;
            }


            internal T? FindKElement(int index)
            {
                if (left_child != null)
                {
                    if (left_child.children >= index)
                    {
                        return left_child.FindKElement(index);
                    }
                    index -= left_child.children;
                }
                if (index == 1)
                {
                    return Value;
                }
                else
                {
                    index -= 1;
                }
                if (right_child != null)
                {
                    return right_child.FindKElement(index);
                }
                return default;

            }

            internal bool Insert(T item)
            {
                int result = item.CompareTo(Value);
                if (result < 0)
                {
                    if (left_child == null)
                    {
                        left_child = new Node(item, tree) { parent = this };
                    }
                    else
                    {
                        bool insert_result = left_child.Insert(item);
                        if (insert_result)
                        {
                            Reheight();
                            RecountChildren();
                            Rebalance();
                        }
                        return insert_result;
                        
                    }
                }
                else if (result > 0)
                {
                    if (right_child == null)
                    {
                        right_child = new Node(item, tree) { parent = this };
                    }
                    else
                    {
                        bool insert_result = right_child.Insert(item);
                        if (insert_result)
                        {
                            Reheight();
                            RecountChildren();
                            Rebalance();
                        }
                        return insert_result;
                    }

                }
                else
                {
                    return false;
                }

                Reheight();
                RecountChildren();
                Rebalance();

                return true;
            }

            internal bool Delete(T item)
            {
                int result = item.CompareTo(Value);
                if (result < 0)
                {
                    if (left_child == null)
                    {
                        return false;
                    }
                    else
                    {
                        bool remove_result = left_child.Delete(item);
                        if (remove_result)
                        {
                            Reheight();
                            RecountChildren();
                            Rebalance();
                        }
                        return remove_result;
                    }
                }
                else if (result > 0)
                {
                    if (right_child == null)
                    {
                        return false;
                    }
                    else
                    {
                        bool remove_result = right_child.Delete(item);
                        if (remove_result)
                        {
                            Reheight();
                            RecountChildren();
                            Rebalance();
                        }
                        return remove_result;
                    }

                }
                else
                {
                    if (left_child == null && right_child == null)
                    {
                        DeleteNodeWithoutChildren();
                    }
                    else if (left_child == null)
                    {
                        DeleteNodeWithLeftChild();
                    }
                    else if (right_child == null)
                    {
                        DeleteNodeWithRightChild();
                    }
                    else
                    {
                        DeleteNodeWithChildren();
                    }
                    Reheight();
                    RecountChildren();
                    Rebalance();
                    return true;
                }

            }

            void DeleteNodeWithoutChildren()
            {
                if (parent != null)
                {
                    if (this.Equals(parent.left_child))
                    {
                        parent.left_child = null;
                    }
                    else if (this.Equals(parent.right_child))
                    {
                        parent.right_child = null;
                    }
                    parent.RecountChildren();
                }
                Untie();
            }
            void DeleteNodeWithLeftChild()
            {
                if (parent != null)
                {
                    if (this.Equals(parent.left_child))
                    {
                        parent.left_child = left_child;
                    }
                    else if (this.Equals(parent.right_child))
                    {
                        parent.right_child = left_child;
                    }
                    if (left_child != null)
                    {
                        left_child.parent = parent;
                    }
                    parent.RecountChildren();
                }
                Untie();
            }
            void DeleteNodeWithRightChild()
            {
                if (parent != null)
                {
                    if (this.Equals(parent.left_child))
                    {
                        parent.left_child = right_child;
                    }
                    else if (this.Equals(parent.right_child))
                    {
                        parent.right_child = right_child;
                    }
                    if (right_child != null)
                    {
                        right_child.parent = parent;
                    }
                    parent.RecountChildren();
                }
                Untie();
            }
            void DeleteNodeWithChildren()
            {
                Node? next_node = NextNode;
                if (next_node == null)
                {
                    return;
                }
                T value_of_next_node = next_node.Value;
                Delete(next_node.Value);
                Value = value_of_next_node;
            }

            internal Node? NextNode
            {
                get
                {
                    Node? node = this;
                    if (node.right_child != null)
                    {
                        node = node.right_child;
                        while (node.left_child != null)
                        {
                            node = node.left_child;
                        }
                        return node;
                    }

                    Node? parent = node.parent;
                    if (parent == null)
                    {
                        return null;
                    }
                    if (node.Equals(parent.left_child))
                    {
                        return parent;
                    }

                    do
                    {
                        node = parent;
                        parent = node.parent;
                    } while (parent != null && !node.Equals(parent.left_child));

                    if (parent == null)
                    {
                        return null;
                    }
                    return parent;

                }
            }
            internal Node? PreviousNode
            {
                get
                {
                    Node? node = this;
                    if (node.left_child != null)
                    {
                        node = node.left_child;
                        while (node.right_child != null)
                        {
                            node = node.right_child;
                        }
                        return node;
                    }

                    Node? parent = node.parent;
                    if (parent == null)
                    {
                        return null;
                    }
                    if (node.Equals(parent.right_child))
                    {
                        return parent;
                    }

                    do
                    {
                        node = parent;
                        parent = node.parent;
                    } while (parent != null && !node.Equals(parent.right_child));

                    if (parent == null)
                    {
                        return null;
                    }
                    return parent;

                }
            }

            internal Node LeastNode
            {
                get
                {
                    return left_child == null ? this : left_child.LeastNode;
                }
            }
            internal Node MostNode
            {
                get
                {
                    return right_child == null ? this : right_child.MostNode;
                }
            }
            internal Node LeastLeaf
            {
                get
                {
                    if (left_child != null)
                    {
                        return left_child.LeastLeaf;
                    }
                    if (right_child != null)
                    {
                        return right_child.LeastLeaf;
                    }
                    return this;
                }
            }
            internal Node MostLeaf
            {
                get
                {
                    if (right_child != null)
                    {
                        return right_child.MostLeaf;
                    }
                    if (left_child != null)
                    {
                        return left_child.MostLeaf;
                    }
                    return this;
                }
            }

            internal bool Contains(T item)
            {
                int result = item.CompareTo(Value);
                if (result < 0)
                {                    
                    return left_child != null && left_child.Contains(item);
                }
                else if (result > 0)
                { 
                    return right_child != null && right_child.Contains(item);
                }
                else
                {
                    return true;
                }

            }

            internal Node? NextPreorderNode
            {
                get
                {
                    if (left_child != null)
                    {
                        return left_child;
                    }
                    if (right_child != null)
                    {
                        return right_child;
                    }

                    Node? current_node = this;
                    Node? parent = current_node.parent;
                    while (parent != null)
                    {
                        if (current_node.Equals(parent.left_child) && parent.right_child != null)
                        { 
                            return parent.right_child;
                        }
                        current_node = parent;
                        parent = current_node.parent;
                    }
                    return null;
                }
            }
            internal Node? PreviousPreorderNode
            {
                get
                {
                    Node? current_node = this;
                    Node? parent = current_node.parent;
                    if (parent == null)
                    {
                        return null;
                    }
                    if (current_node.Equals(parent.left_child) || parent.left_child == null)
                    {
                        return parent;
                    }
                    
                    if (parent.left_child != null)
                    {
                        current_node = parent.left_child;
                    }

                    while (current_node != null)
                    {
                        if (current_node.right_child != null)
                        {
                            current_node = current_node.right_child;
                        }
                        else if (current_node.left_child != null)
                        {
                            current_node = current_node.left_child;
                        }
                        else
                        {
                            break;
                        }
                    }
                    return current_node;
                }
            }
            internal Node? NextPostorderNode
            {
                get
                {
                    Node? current_node = this;
                    Node? parent = this.parent;

                    if (parent == null)
                    {
                        return null;
                    }
                    if (current_node.Equals(parent.left_child))
                    {
                        current_node = parent;
                        if (current_node.right_child == null)
                        {
                            return current_node;
                        }
                        current_node = current_node.right_child;
                        while (current_node != null) 
                        {
                            if (current_node.left_child != null)
                            {
                                current_node = current_node.left_child;
                            }
                            else if (current_node.right_child != null)
                            {
                                current_node = current_node.right_child;
                            }
                            else
                            {
                                break;
                            }
                        }
                        return current_node;
                    }
                    return parent;
                }
            }
            internal Node? PreviousPostorderNode
            {
                get
                {
                    Node? current_node = this;
                    Node? parent = current_node.parent;

                    if (right_child != null)
                    {
                        return right_child;
                    }
                    if (left_child != null)
                    {
                        return left_child;
                    }
                    while (parent != null)
                    {
                        if (current_node.Equals(parent.right_child))
                        {
                            if (parent.left_child != null)
                            {
                                return parent.left_child;
                            }
                        }
                        current_node = parent;
                        parent = current_node.parent;
                    }
                    return null;
                }
            }

            void RotateLeft()
            {
                Node? b = right_child;
                if (b == null)
                {
                    return;
                }
                Node? child = b.left_child;
                b.left_child = this;
                if (parent != null)
                {
                    if (this.Equals(parent.left_child))
                    {
                        parent.left_child = b;
                    }
                    else if (this.Equals(parent.right_child))
                    {
                        parent.right_child = b;
                    }
                    b.parent = parent;
                }
                if (this.Equals(tree.root))
                {
                    tree.root = b;
                    b.parent = null;
                }
                parent = b;

                if (child != null)
                {
                    right_child = child;
                    child.parent = this;
                }
                else
                {
                    right_child = null;
                }
                Reheight();
                b.Reheight();
                RecountChildren();
                b.RecountChildren();
            }

            void RotateRight()
            {
                Node? b = left_child;
                if (b == null)
                {
                    return;
                }
                Node? child = b.right_child;
                b.right_child = this;
                if (parent != null)
                {
                    if (this.Equals(parent.left_child))
                    {
                        parent.left_child = b;
                    }
                    else if (this.Equals(parent.right_child))
                    {
                        parent.right_child = b;
                    }
                    b.parent = parent;
                }
                if (this.Equals(tree.root))
                {
                    tree.root = b;
                    b.parent = null;
                }
                parent = b;

                if (child != null)
                {
                    left_child = child;
                    child.parent = this;
                }
                else
                {
                    left_child = null;
                }
                Reheight();
                b.Reheight();
                RecountChildren();
                b.RecountChildren();
            }

            void BigRotateLeft()
            {
                if (right_child != null)
                {
                    right_child.RotateRight();
                    RotateLeft();
                }
            }

            void BigRotateRight()
            {
                if (left_child != null)
                {
                    left_child.RotateLeft();
                    RotateRight();
                }
            }

            void Rebalance()
            {
                if (diff < 2 && diff > -2)
                {
                    return;
                }
                Node? b = left_child;
                if (b != null)
                {
                    Node? d = b.right_child;
                    if (d != null && diff == 2 && b.diff == -1)
                    {
                        BigRotateRight();
                    }
                    if (diff == 2 && b.diff >= 0)
                    {
                        RotateRight();
                    }
                }
                Node? c = right_child;
                if (c != null)
                {
                    Node? d = c.left_child;
                    if (d != null && diff == -2 && c.diff == 1)
                    {
                        BigRotateLeft();
                    }
                    if (diff == -2 && c.diff <= 0)
                    {
                        RotateLeft();
                    }
                }
            }
        }
    }
}   
