﻿using System.Collections.Generic;
using LibP2P;
using TheDotNetLeague.MultiFormats.MultiHash;

namespace TheDotNetLeague.Ipfs.Core.Lib.CoreApi
{
    /// <summary>
    ///     The statistics for <see cref="IStatsApi.BitswapAsync" />.
    /// </summary>
    public class BitswapData
    {
        /// <summary>
        ///     The number of blocks sent by other peers.
        /// </summary>
        public ulong BlocksReceived;

        /// <summary>
        ///     The number of blocks sent to other peers.
        /// </summary>
        public ulong BlocksSent;

        /// <summary>
        ///     The number of bytes sent by other peers.
        /// </summary>
        public ulong DataReceived;

        /// <summary>
        ///     The number of bytes sent to other peers.
        /// </summary>
        public ulong DataSent;

        /// <summary>
        ///     The number of duplicate blocks sent by other peers.
        /// </summary>
        /// <remarks>
        ///     A duplicate block is a block that is already stored in the
        ///     local repository.
        /// </remarks>
        public ulong DupBlksReceived;

        /// <summary>
        ///     The number of duplicate bytes sent by other peers.
        /// </summary>
        /// <remarks>
        ///     A duplicate block is a block that is already stored in the
        ///     local repository.
        /// </remarks>
        public ulong DupDataReceived;

        /// <summary>
        ///     The known peers.
        /// </summary>
        public IEnumerable<MultiHash> Peers;

        /// <summary>
        ///     TODO: Unknown.
        /// </summary>
        public int ProvideBufLen;

        /// <summary>
        ///     The content that is wanted.
        /// </summary>
        public IEnumerable<Cid> Wantlist;
    }
}
