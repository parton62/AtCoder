using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder
{
    class AVLTree<T>
    {


        class Node
        {
            private AVLTree<T> _tree;

            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }

            private int Bias()
            {
                return 0;
            }



        }

    }
}
