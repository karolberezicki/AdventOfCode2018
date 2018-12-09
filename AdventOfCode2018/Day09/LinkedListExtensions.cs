using System.Collections.Generic;

namespace Day09
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<T> CircleForward<T>(this LinkedListNode<T> node, LinkedList<T> list)
        {
            return node.Next ?? list.First;
        }

        public static LinkedListNode<T> CircleBackward<T>(this LinkedListNode<T> node, LinkedList<T> list)
        {
            return node.Previous ?? list.Last;
        }

        public static LinkedListNode<T> CircleBackward<T>(this LinkedListNode<T> node, LinkedList<T> list, int times)
        {
            for (int i = 0; i < times; i++)
            {
                node = node.CircleBackward(list);
            }

            return node;
        }
    }
}
