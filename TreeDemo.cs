using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*  May 26, 2015 http://arachnode.net/blogs/programming_challenges/archive/2009/09/25/recursive-tree-traversal-orders.aspx   
 * https://github.com/yuzhangcmu/LeetCode/blob/master/tree/TreeDemo.java
 
 */
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

            node1.value = 1;
            node2.value = 2;
            node3.value = 3;
            node4.value = 4;
            node5.value = 5;
            node6.value = 6;
            node7.value = 7;

            node1.left = node2;
            node1.right = node3;

            node2.left = node4;
            node2.right = node5;

            node3.left = node6;
            node3.right = node7;

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

            // June 2, 2015 
            Console.WriteLine("Post order traversal Iterative");
            postorderTraversal_Iterative(node1);



            // June 3, 2015
            Console.WriteLine("In order traversal Iterative");
            inorderTraversalIterative(node1);

            Console.ReadLine();


        }

        private static void PreorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine(node.value.ToString());

            PreorderTraversal(node.left);
            PreorderTraversal(node.right);
        }

        private static void InorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            InorderTraversal(node.left);

            Console.WriteLine(node.value);

            InorderTraversal(node.right);
        }

        private static void PostorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            PostorderTraversal(node.left);

            PostorderTraversal(node.right);

            Console.WriteLine(node.value);
        }

        private static void BreadthFirstTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            Queue.Enqueue(node.left);

            Queue.Enqueue(node.right);

            Console.WriteLine(node.value);

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
                Console.WriteLine(tmpNode.value);

                Node left = tmpNode.left;
                Node right = tmpNode.right;

                if (left != null)
                    Queue2.Enqueue(left);

                if (right != null)
                    Queue2.Enqueue(right);
            }
        }


        private static ArrayList preorderTraversalIterative(Node root)
        {
            ArrayList result = new ArrayList();
            Node p = new Node();

            Stack s = new Stack();

            p = root;
            if (p != null) s.Push(p);

            while (s.Count > 0)
            {
                p = (Node)s.Peek();

                s.Pop();

                result.Add(p.value);

                if (p.right != null) s.Push(p.right);
                if (p.left != null) s.Push(p.left);
            }

            return result;
        }

        /** 
         * https://github.com/yuzhangcmu/LeetCode/blob/master/tree/TreeDemo.java
     *  后序遍历迭代解法 
     *  
     *  从左到右的后序 与从右到左的前序的逆序是一样的，所以就简单喽！ 哈哈
     *  用另外一个栈进行翻转即可喽 
     
     */
        public static void postorderTraversal_Iterative(Node root)
        {
            if (root == null)
            {
                return;
            }

            Stack s = new Stack();
            Stack outS = new Stack();

            s.Push(root);
            while (s.Count > 0)
            {
                Node cur = (Node)s.Pop();
                outS.Push(cur);

                if (cur.left != null)
                {
                    s.Push(cur.left);
                }
                if (cur.right != null)
                {
                    s.Push(cur.right);
                }
            }

            while (outS.Count > 0)
            {
                Node cur = (Node)outS.Pop();
                Console.WriteLine(cur.value + " ");
            }
        }


        /** 
         * https://github.com/yuzhangcmu/LeetCode/blob/master/tree/TreeDemo.java
         * http://blog.csdn.net/fightforyourdream/article/details/16843303
        * 中序遍历迭代解法 ，用栈先把根节点的所有左孩子都添加到栈内， 
        * 然后输出栈顶元素，再处理栈顶元素的右子树 
        *  
        *  
        * 还有一种方法能不用递归和栈，基于线索二叉树的方法，较麻烦以后补上 
        * http://www.geeksforgeeks.org/inorder-tree-traversal-without-recursion-and-without-stack/ 
           * * *  Julia comment on June 1, 2015: 
           *  如何思考这个问题:
           *  1. 第一个点应该访问, 就是从根节点出发,　找左孩子,　直到最左边的点; 
           *  2. 如何找到第一个点 N1?
           *     2.1 从根节点出发，设为当前点;　     　　　 
           *     2.2 把非空的当前点入堆栈,　更新当前点为左的孩子,循环,　直到左边节点为空, 节点 N1; 
         　*     2.3 出栈,　该点输出；　
           *     2.3 接下来, 把右孩子设为当前点; (对第一个点N1来说, 左孩子为空, 右孩子入栈; 所有的孩子都考虑了!)
         *    3. 如何想到设计一个栈,　一个当前节点，能够解决问题?
        */
        public static void inorderTraversalIterative(Node root)
        {
            if (root == null)
            {
                return;
            }

            Stack s = new Stack();

            Node cur = root;

            while (true)
            {
                // 把当前节点的左节点都push到栈中.
                while (cur != null)
                {
                    s.Push(cur);
                    cur = cur.left;
                }

                if (s.Count == 0)
                {
                    break;
                }

                // 因为此时已经没有左孩子了，所以输出栈顶元素 
                cur = (Node)s.Pop();
                Console.WriteLine(cur.value + " ");

                // 准备处理右子树  
                cur = cur.right;
            }
        }


    }

    internal class Node
    {
        public int value { get; set; }
        public Node left { get; set; }
        public Node right { get; set; }
        public override string ToString()
        {
            return value.ToString();
        }
    }
}