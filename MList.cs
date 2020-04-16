namespace GameTheGame
{
    class MCircularListNode<Type> {
        MCircularListNode<Type> nextNode;
        MCircularListNode<Type> previousNode;
        Type nodeValue;
        public MCircularListNode(Type nodeValue)
        {
            this.nodeValue = nodeValue;
        }
    }
    class MCircularList<Type>
    {
        MCircularListNode<Type> firstNode;
        MCircularListNode<Type> lastNode;
        public MCircularList()
        {
        }
        public void AddNode(MCircularListNode<Type> node) {
            if (firstNode == null)
            {
                firstNode = node;
                lastNode = node;
            } else if (Object.ReferenceEquals(firstNode, lastNode)) {
                
            }
        }
        public void AddValue(Type nodeValue) {
            MCircularListNode<Type> node = new MCircularListNode<Type>(nodeValue);
            AddNode(node);
        }
    }
}
