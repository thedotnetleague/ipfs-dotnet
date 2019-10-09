﻿using ProtoBuf;

namespace TheDotNetLeague.Ipfs.Core.Lib.Cryptography.Proto
{
    internal enum KeyType
    {
        RSA = 0,
        Ed25519 = 1,
        Secp256k1 = 2,
        ECDH = 4
    }

    [ProtoContract]
    internal class PublicKey
    {
        [ProtoMember(2, IsRequired = true)]
        public byte[] Data;

        [ProtoMember(1, IsRequired = true)]
        public KeyType Type;
    }

    // PrivateKey message is not currently used.  Hopefully it never will be
    // because it could introduce a huge security hole.
#if false
    [ProtoContract]
    class PrivateKey
    {
        [ProtoMember(1, IsRequired = true)]
        public KeyType Type;
        [ProtoMember(2, IsRequired = true)]
        public byte[] Data;
    }
#endif
}
