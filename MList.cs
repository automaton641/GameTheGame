namespace GameTheGame
{
    class MCircularListNode<Type> {
        public MCircularListNode<Type> nextNode;
        public MCircularListNode<Type> previousNode;
        public Type nodeValue;
        public MCircularListNode(Type nodeValue)
        {
            this.nodeValue = nodeValue;
        }
    }
    class MCircularList<Type>
    {
        public MCircularListNode<Type> firstNode;
        public MCircularListNode<Type> lastNode;
        public MCircularList()
        {
        }
        public void AddNode(MCircularListNode<Type> node) {
            if (firstNode == null && lastNode == null)
            {
                firstNode = node;
                lastNode = node;
                node.nextNode = node;
                node.previousNode = node;
            } else if (object.ReferenceEquals(firstNode, lastNode)) {
                lastNode = node;
                firstNode.nextNode = lastNode;
                firstNode.previousNode = lastNode;
                lastNode.nextNode = firstNode;
                lastNode.previousNode = firstNode;
            } else {
                node.previousNode = lastNode;
                node.nextNode = firstNode;
                firstNode.previousNode = node;
                lastNode.nextNode = node;
                lastNode = node;
            }
        }
        public void AddValue(Type nodeValue) {
            MCircularListNode<Type> node = new MCircularListNode<Type>(nodeValue);
            AddNode(node);
        }
    }
}
