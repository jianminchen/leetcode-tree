using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*  May 26, 2015 http://arachnode.net/blogs/programming_challenges/archive/2009/09/25/recursive-tree-traversal-orders.aspx   */
namespace TreeTraversal
{
    internal class Program
    {
        private static readonly Queue<Node> Queue = new Queue<Node>();

        private static readonly Queue<Node> Queue2 = new Queue<Node>();

        private static void Main()
        {
            var node1 = new Node();
            var node2 = new Node();
            var node3 = new Node();
            var node4 = new Node();
            var node5 = new Node();
            var node6 = new Node();
            var node7 = new Node();

            node1.Value = 1;
            node2.Value = 2;
            node3.Value = 3;
            node4.Value = 4;
            node5.Value = 5;
            node6.Value = 6;
            node7.Value = 7;

            node1.Left = node2;
            node1.Right = node3;

            node2.Left = node4;
            node2.Right = node5;

            node3.Left = node6;
            node3.Right = node7;

            Console.WriteLine("PreorderTraversal");

            PreorderTraversal(node1);

            Console.WriteLine("PreorderTraversalIterative");
            ArrayList list = preorderTraversalIterative(node1);
            foreach (Object s in list)
            {
                int tmp = (int)s;
                Console.WriteLine(tmp); 
            }

            Console.WriteLine("InorderTraversal");

            InorderTraversal(node1);

            Console.WriteLine("PostorderTraversal");

            PostorderTraversal(node1);

            Console.WriteLine("BreathFirstTraversal");

            BreadthFirstTraversal(node1);

            Console.WriteLine("BreathFirstTraversalIterative");

            BreadthFirstTraversalIterative(node1);

            Console.ReadLine();


        }

        private static void PreorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine(node.Value.ToString());

            PreorderTraversal(node.Left);
            PreorderTraversal(node.Right);
        }

        private static void InorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            InorderTraversal(node.Left);

            Console.WriteLine(node.Value);

            InorderTraversal(node.Right);
        }

        private static void PostorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            PostorderTraversal(node.Left);

            PostorderTraversal(node.Right);

            Console.WriteLine(node.Value);
        }

        private static void BreadthFirstTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            Queue.Enqueue(node.Left);

            Queue.Enqueue(node.Right);

            Console.WriteLine(node.Value);

            if (Queue.Count != 0)
            {
                BreadthFirstTraversal(Queue.Dequeue());
            }
        }

        private static void BreadthFirstTraversalIterative(Node node)
        {
            if (node == null)
            {
                return;
            }

            Queue2.Enqueue(node);             

            while (Queue2.Count != 0)
            {
                //BreadthFirstTraversal(Queue.Dequeue());
                Node tmpNode = Queue2.Dequeue();

                // visit the node
                Console.WriteLine(tmpNode.Value);

                Node left = tmpNode.Left;
                Node right = tmpNode.Right;

                if (left != null)
                    Queue2.Enqueue(left);

                if (right != null)
                    Queue2.Enqueue(right);
            }
        }


        private static ArrayList preorderTraversalIterative(Node root) {
            ArrayList result = new ArrayList();
            Node p = new Node();

            Stack s = new Stack();

            p = root;
            if (p != null) s.Push(p);

            while (s.Count > 0) {
                p = (Node)s.Peek();

                s.Pop();

                result.Add(p.Value);

                if (p.Right != null) s.Push(p.Right);
                if (p.Left != null) s.Push(p.Left);
            }

            return result;
        }
    }

    internal class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
