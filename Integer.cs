using System.Numerics;
namespace GameTheGame
{
    class MInteger
    {
        MCircularList<bool> list;
        public MInteger() {
            CreateBitList();
        }
        public MInteger(MInteger integer) {
            CreateBitList();
            Append(integer);
        }
        public MInteger(bool bit) {
            CreateBitList();
            Append(bit);
        }
        public MInteger(string representation) {
            CreateBitList();
            BigInteger big = BigInteger.Parse(representation);
            string binaryString = BigIntegerExtensions.ToBinaryString(big);
            AppendBinaryString(binaryString);            
        }
        public void CreateBitList() {
            list = new MCircularList<bool>();
        }
        public static (bool carry, bool result) AddBits(bool b1, bool b2, bool carry)
        {
            if(b1)
                if(b2)
                    if (carry)
                        return (true, true);
                    else
                        return (true, false);
                else 
                    if (carry)
                        return (true, false);
                    else 
                        return (false, true);
            else
                if(b2)
                    if (carry)
                        return (true, false);
                    else
                        return (false, true);
                else 
                    if (carry)
                        return (false, true);
                    else 
                        return (false, false);
        }
        public void AppendBinaryString(string binaryString) {
            foreach (char binaryCharacter in binaryString)
            {
                if (binaryCharacter == '0')
                {
                    Append(false);
                } 
                else
                {
                    Append(true);
                }
            }
        }
        public static MInteger Add(MInteger firstInteger, MInteger secondInteger) {
            return Add(firstInteger, secondInteger, false);
        }
        public MInteger AddOneToLast() {
            list.lastNode.nodeValue = false;
            return Append(true);
        }
        public static MInteger MayAddOneToLast(MInteger integer, bool carry) {
            if (carry)
            {
                return (new MInteger(integer)).AddOneToLast();
            }
            return new MInteger(integer);
        }
        public static MInteger Add(MInteger firstInteger, MInteger secondInteger, bool carry) {
            if(firstInteger.IsNull() || secondInteger.IsNull())
            {
                if (firstInteger.IsNull()) 
                {
                    return MayAddOneToLast(secondInteger, carry);
                } 
                else
                {
                    return MayAddOneToLast(firstInteger, carry);
                }
                    
            }
            (bool resultCarry, bool bit) = AddBits(firstInteger.FirstBit(), secondInteger.FirstBit(), carry);
            return (new MInteger(bit)).Append(Add(firstInteger.RemoveFirst(), secondInteger.RemoveFirst(), resultCarry));
        }
        public bool FirstBit() {
            return list.firstNode.nodeValue;
        }
        public MInteger RemoveFirst() {
            if (IsNull())
            {
                return new MInteger();
            }
            if (list.firstNode == list.lastNode)
            {
                return new MInteger();
            }
            MInteger result = new MInteger(this);
            result.list.lastNode = list.lastNode;
            result.list.firstNode = list.firstNode.nextNode;
            return result;
        }
        public MInteger Append(bool bit) {
            list.AddValue(bit);
            return this;
        }
        public bool IsNull() {
            if (list.firstNode == null) {
                return true;
            }
            return false;
        }
        public MInteger Append(MInteger integer) {
            if (integer.IsNull())
            {
                return this;
            }
            MCircularListNode<bool> node = integer.list.firstNode;
            while (true)
            {
                Append(node.nodeValue);
                if (object.Equals(node, integer.list.lastNode)) {
                    return this;
                }
                node = node.nextNode;
            }
        }
        public override string ToString()
        {
            string representation =  "Integer: ";
            MCircularListNode<bool> node = list.firstNode;
            if (node != null)
            {
                while (true)
                {
                    if (node.nodeValue)
                    {
                        representation += "1";
                    } 
                    else 
                    {
                        representation += "0";
                    }
                    if (object.ReferenceEquals(node, list.lastNode))
                    {
                        return representation;
                    } 
                    else 
                    {
                        node = node.nextNode;
                    }
                }  
            }
            return representation;
        }
    }
}
