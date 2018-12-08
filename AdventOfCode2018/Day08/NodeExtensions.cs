using System.Linq;

namespace Day08
{
    public static class NodeExtensions
    {
        public static int CalcValue(this Node node)
        {
            if (node.Header.ChildrenCount == 0)
            {
                return node.Metadata.Sum();
            }

            int value = 0;
            foreach (int index in node.Metadata)
            {
                if (node.Children.ElementAtOrDefault(index - 1) != null)
                {
                    value += CalcValue(node.Children[index - 1]);
                }
            }

            return value;
        }

        public static int SumMetadata(this Node node)
        {
            int sum = 0;
            foreach (var child in node.Children)
            {
                sum += SumMetadata(child);
            }

            sum += node.Metadata.Sum();

            return sum;
        }
    }
}
