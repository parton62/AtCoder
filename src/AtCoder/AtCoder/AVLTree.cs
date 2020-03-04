using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder
{
    class AVLTree<T>
    {


        class Node<T>
        {
            private AVLTree<T> _tree;

            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }
            public Node<T> Parent { get; set; }

            private int Bias()
            {
                return 0;
            }



        }

    }
}
