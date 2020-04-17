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
            MInteger result = new MInteger(firstInteger);
            MInteger counter = new MInteger();
            while (!counter.Equals(secondInteger))
            {
                result.Next();
                counter.Next();
            }
            return result;
        }
        public byte GetByte(MInteger index) {
            if (IsNull())
            {
                return 0;
            }
            MCircularListNode<bool> node = list.firstNode;
            MInteger counter = new MInteger();
            bool iterate = true;
            while (iterate)
            {
                if (counter.Equals(index))
                {
                    iterate = false;
                }
                else
                {
                    if (object.ReferenceEquals(node, list.lastNode))
                    {
                        return 0;
                    }
                    node = node.nextNode;
                }
            }
            MInteger limit = new MInteger("8");
            counter = new MInteger();
            byte result = 0;
            byte adder = 1;
            while (true)
            {
                if (counter.Equals(index))
                {
                    return result;
                }
                if (object.ReferenceEquals(node, list.lastNode))
                {
                    return result;
                }
                if (node.nodeValue)
                {
                    result += adder;
                }
                adder *= 2;
                node = node.nextNode;
            }
        }
        public MInteger Next() {
            if (IsNull())
            {
                return Append(true);
            }
            MCircularListNode<bool> node = list.firstNode;
            while (true)
            {
                if (node.nodeValue)
                {
                    node.nodeValue = false;
                } 
                else
                {
                    node.nodeValue = true;
                    return this;
                }
                if (object.ReferenceEquals(node, list.lastNode))
                {
                    return Append(true);
                }
                node = node.nextNode;
            }
        }
        public MInteger AddOneToLast() {
            if (!IsNull())
            {
                if (list.lastNode.nodeValue)
                {
                    list.lastNode.nodeValue = false;
                }
                else
                {
                    list.lastNode.nodeValue = true;
                    return this;
                }
            }
            return Append(true);
        }
        public static MInteger MayAddOneToLast(MInteger integer, bool addOneToLast) {
            if (addOneToLast)
            {
                return (new MInteger(integer)).AddOneToLast();
            }
            return new MInteger(integer);
        }
        public static MInteger AddOptimized(MInteger firstInteger, MInteger secondInteger, bool carry) {
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
            return (new MInteger(bit)).Append(AddOptimized(firstInteger.RemoveFirst(), secondInteger.RemoveFirst(), resultCarry));
        }
        public bool FirstBit() {
            return list.firstNode.nodeValue;
        }
        public bool LastBit() {
            return list.lastNode.nodeValue;
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
                if (object.ReferenceEquals(node, integer.list.lastNode)) {
                    return this;
                }
                node = node.nextNode;
            }
        }
        public bool EqualsInteger(MInteger integer) {
            if (IsNull()) {
                if (integer.IsNull()) {
                   return true;
                }
                return false;
            }
            if (integer.IsNull()) {
                   return false;
            }
            if (FirstBit() == integer.FirstBit())
            {
                return RemoveFirst().EqualsInteger(integer.RemoveFirst());
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return EqualsInteger((MInteger)obj);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
