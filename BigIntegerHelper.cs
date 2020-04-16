using System;
using System.Numerics;
public static class BigIntegerExtensions
{
    public static string ToBinaryString(this BigInteger big)
    {
        string representation = "";
        bool shouldIterate = true;
        byte remainder;
        while (shouldIterate) {
            remainder = (byte) (big % 2);
            if (remainder == 0)
            {
                representation += "0";
            } else {
                representation += "1";
            }
            big /= 2;
            if (big <= 0) {
                shouldIterate = false;
            }
        }
        return representation;
    }

}