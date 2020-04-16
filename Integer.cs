namespace GameTheGame
{
    class MInteger
    {
        MCircularList<bool> bitList;
        public MInteger() {
            bitList = new MCircularList<bool>();
        }
        public void Append(bool bit) {
            bitList.AddValue(bit);
        }
        public override string ToString()
        {
            string stringRepresentation =  "Integer: ";
            bool shouldIterate = true;
            MCircularListNode<bool> node = bitList.firstNode;
            if (node != null)
            {
                while (shouldIterate)
                {
                    if (node.nodeValue)
                    {
                        stringRepresentation += "1";
                    } else {
                        stringRepresentation += "0";
                    }
                    if (object.ReferenceEquals(node, bitList.lastNode))
                    {
                        shouldIterate = false;
                    } else {
                        node = node.nextNode;
                    }
                }  
            }
            return stringRepresentation;
        }
    }
}
