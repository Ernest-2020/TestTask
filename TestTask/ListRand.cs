namespace TestTask
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream stream)
        {
            WriteData(AddNodesAtList(Head), stream);
        }

        private List<ListNode> AddNodesAtList(ListNode head)
        {
            List<ListNode> nodes = new List<ListNode>();
            ListNode currentNode = head;

            while (currentNode != null)
            {
                nodes.Add(currentNode);
                currentNode = currentNode.Next;
            }

            return nodes;
        }

        private void WriteData(List<ListNode> nodes, Stream stream)
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                foreach (var node in nodes)
                {
                    streamWriter.WriteLine($"{node.Data}:{nodes.IndexOf(node.Rand)}");
                }
            }
        }

        public void Deserialize(Stream stream)
        {
            List<ListNode> nodes = ReadData(stream);

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Rand = nodes[Convert.ToInt32(nodes[i].Data.Split(':')[1])];
                nodes[i].Data = nodes[i].Data.Split(':')[0];
            }
        }

        private List<ListNode> ReadData(Stream stream)
        {
            List<ListNode> nodes = new List<ListNode>();
            var currentNode = new ListNode();
            Count = 0;

            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    currentNode.Data = streamReader.ReadLine();
                    var next = new ListNode();
                    currentNode.Next = next;
                    nodes.Add(currentNode);
                    next.Prev = currentNode;
                    currentNode = next;
                    Count++;
                }
            }
            Head = nodes[0];
            Tail = currentNode.Prev;
            Tail.Next = null;

            return nodes;
        }
    }
}
