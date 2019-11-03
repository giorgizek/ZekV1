using System;

namespace Zek.Security
{
    [Serializable]
    public enum AlgorithmTypes
    {
        GuidDateTimeTicks,
        BitConverterToInt64,
        //DateTimeHashCode,
        //GUIDHashCode,
        Random,
        RNGCrypto,
    }
}
