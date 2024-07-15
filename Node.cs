using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace MyBSTContainer
{
    partial class TreeContainer<T>
    {
        internal class Node(T value)
        {
            public T Value { get; private set; } = value;

            Node? parent = null;
            Node? left_child = null;
            Node? right_child = null;

            int height = 1;
            int diff = 0;
            int children = 1;

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
                        left_child = new Node(item) { parent = this };
                        return true;
                    }
                    else
                    {
                        return left_child.Insert(item);
                    }
                }
                else if (result > 0)
                {
                    if (right_child == null)
                    {
                        right_child = new Node(item) { parent = this };
                        return true;
                    }
                    else
                    {
                        return right_child.Insert(item);
                    }

                }
                else
                {
                    return false;
                }

                // TODO recalculate height
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
                        return left_child.Delete(item);
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
                        return right_child.Delete(item);
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
                Untie();
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
                    while (parent != null && parent.left_child == null)
                    {
                        current_node = parent;
                        parent = current_node.parent;
                    }
                    if (parent.left_child != null)
                    {
                        current_node = parent.left_child;
                    }

                    while (current_node.right_child != null || current_node.left_child != null)
                    {
                        if (current_node.right_child != null)
                        {
                            current_node = current_node.right_child;
                        }
                        else
                        {
                            current_node = current_node.left_child;
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
                        while (current_node.left_child != null || current_node.right_child != null)
                        {
                            if (current_node.left_child != null)
                            {
                                current_node = current_node.left_child;
                            }
                            else
                            {
                                current_node = current_node.right_child;
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
        }
    }
}   
