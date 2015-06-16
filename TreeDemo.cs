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

            // June 11, 2015
            Console.WriteLine("In order traversal Iterative - method B");
            inOrderIterative_B(node1);

            // June 9, 2015 
            Console.WriteLine("Count of one child only node");
            node3.right = null;
            int count = countOneChildNode1(node1);
            int count2 = countOneChildNode1(null);

            int count3 = countOneChildNode(node1);

            // June 11, 2015
            var node11 = new Node();
            var node12 = new Node();
            var node13 = new Node();
            var node14 = new Node();
            var node15 = new Node();
            var node16 = new Node();
            var node17 = new Node();
            var node18 = new Node();
            var node19 = new Node();

            node11.value = 1;
            node12.value = 2;
            node13.value = 3;
            node14.value = 4;
            node15.value = 5;
            node16.value = 6;
            node18.value = 8;
            node19.value = 9;

            node15.left = node13;
            node15.right = node18;

            node13.left = node16;
            node13.right = node14;

            node18.left = node12;
            node18.right = node19;

            node16.left = node11;

            recoverTree(node15);

            // June 16 invert tree
            Node resultInvert = invertBinaryTree(node1);
            Node resultInvertIter = invertBinaryTreeIterative(node1);

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

        /**
         * Latest update: June 11, 2015
         * source code idea from the website: 
         * https://leetcodenotes.wordpress.com/2013/08/04/classic-%E6%A0%91%E7%9A%84%E5%89%8D%E5%BA%8F%E3%80%81%E4%B8%AD%E5%BA%8F%E3%80%81%E5%90%8E%E5%BA%8F%E7%9A%84iteration/
         * Very clear description in Chinese:
           中序 in order （左中右）：
           push root左支到死
           pop， 若pop的有right，则把right当root对待push它的左支到死。
           这样继续一边pop一边push。直到stack为空。
         */
        public static void inOrderIterative_B(Node root)
        {
            if (root == null)
                return;

            Stack s = new Stack();

            pushAllTheyWayAtLeft(s, root);

            while (s.Count > 0)
            {
                Node top = (Node)s.Pop();

                System.Console.WriteLine(top.value + ", ");

                if (top.right != null)
                    pushAllTheyWayAtLeft(s, top.right);
            }
        }

        private static void pushAllTheyWayAtLeft(Stack s, Node root)
        {
            while (root != null)
            {
                s.Push(root);
                root = root.left;
            }
        }

        /**
         * latest update; June 9, 2015
         * https://juliachenonsoftware.wordpress.com/2015/06/09/binary-tree-write-a-function-to-return-count-of-nodes-in-binary-tree-which-has-only-one-child/
         * Comment: the above code,  If node is null, the function crashes.
         */
        public static int countOneChildNode1(Node node)
        {
            if (node == null) return 0;

            if (node.left != null && node.right != null)
            {
                return countOneChildNode1(node.left) + countOneChildNode1(node.right);
            }
            else if (node.left != null)
            {
                return countOneChildNode1(node.left) + 1;
            }
            else if (node.right != null)
            {
                return countOneChildNode1(node.right) + 1;
            }
            else  // no left child, no right child
                return 0;
        }


        /**
         * https://juliachenonsoftware.wordpress.com/2015/06/09/binary-tree-write-a-function-to-return-count-of-nodes-in-binary-tree-which-has-only-one-child/
        *   Great arguments:
            1. Since first line is the discussion of “node==null”, there is no need to check node!=null before the function countOneChildNode call; which is redundant, waste of time, more code to maintain, and leave the check to the recursive function call, where the null checking is doing the job.
            2. How to express only one child?
            case 1: left child is not null; in other words, there is a left child: node.left!=null
            case 2: right child is not null; node.right!=null)
            case 3: node has two child
                    (node.left!=null) && (node.right!=null)
            case 4: node has only one child (A: left child only, B: right child only, one true, one false; left child existed != right child existed; cannot be both false or both true)
                    (node.left!=null)!=(node.right!=null)
            case 5: at least one child  (one child or two child)
                    (node.left!=null) || (node.right!=null)
            这道题非常好, 通过这道题, 你可以如何简化代码; 如果有一个出色的程序员, 有很强的逻辑思维能力, 想到只有一个孩子, 可以表示为一句话:　 (node.left!=null)!=(node.right!=null)
        */
        public static int countOneChildNode(Node node)
        {
            if (node == null) return 0;
            return (((node.left != null) != (node.right != null) ? 1 : 0) + countOneChildNode(node.left) + countOneChildNode(node.right));
        }

        /**
         * Leetcode: recover binary tree
         * Solution: 
         * We can use in-order traverse to find the swapped element. During the traverse, we can find the element that is smaller than the previous node. Using this method we can find the swapped node. Save it and swap them. Done.
         * http://www.lifeincode.net/programming/leetcode-recover-binary-search-tree-java/
         * 
         * using O(n) space 
         */
        static Node node1 = null;
        static Node node2 = null;
        public static void recoverTree(Node root)
        {
            inorderTraverse(root);

            int tmp = node1.value;
            node1.value = node2.value;
            node2.value = tmp;
        }

        static Node prev = null;
        private static void inorderTraverse(Node root)
        {
            if (root == null)
                return;
            inorderTraverse(root.left);

            if (prev != null)
            {
                if (root.value <= prev.value)
                {
                    if (node1 == null)
                        node1 = prev;
                    node2 = root;
                }
            }

            prev = root;
            inorderTraverse(root.right);
        }
        /* the end */

        /**
         * Latest update: on June 16, 2015
         * Leetcode: 
         * Invert a binary tree
         * Reference: 
         * http://www.zhihu.com/question/31202353
         * 
         * 7 lines of code - using recursion
         */
        public static Node invertBinaryTree(Node root)
        {
            if (root == null)
                return null;

            Node temp = root.left;
            root.left = root.right;
            root.right = temp;

            invertBinaryTree(root.left);
            invertBinaryTree(root.right);

            return root;
        }

        /**
         * Latest update: on June 16, 2015
         * Leetcode: Invert a binary tree
         * using iterative solution 
         */
        public static Node invertBinaryTreeIterative(Node root)
        {
            if (root == null)
                return null;

            Queue q = new Queue();
            q.Enqueue(root);

            /*
             * consider the queue: 
             */
            while (q.Count > 0)
            {
                Node nd = (Node)q.Dequeue();

                Node tmp = nd.left;
                nd.left = nd.right;
                nd.right = tmp;

                if (nd.left != null)
                    q.Enqueue(nd.left);
                if (nd.right != null)
                    q.Enqueue(nd.right);
            }

            return root;
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