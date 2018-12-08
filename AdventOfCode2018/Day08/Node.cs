using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public class Node
    {
        public (int ChildrenCount, int MetadataCount) Header { get; set; }
        public List<Node> Children { get; set; }
        public List<int> Metadata { get; set; }

        public static (Node Node, int currentIndex) CreateNode(List<int> license, int currentIndex)
        {
            var node = new Node
            {
                Header = (license[currentIndex], license[currentIndex + 1]),
                Children = new List<Node>(),
                Metadata = new List<int>()
            };

            for (int i = 0; i < node.Header.ChildrenCount; i++)
            {
                var child = CreateNode(license, currentIndex + 2);
                node.Children.Add(child.Node);
                currentIndex = child.currentIndex;
            }

            node.Metadata = license.Skip(currentIndex + 2).Take(node.Header.MetadataCount).ToList();
            currentIndex += node.Header.MetadataCount;

            return (node, currentIndex);
        }
    }
}
