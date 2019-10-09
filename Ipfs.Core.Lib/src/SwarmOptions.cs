﻿using LibP2P;
using LibP2P.Cryptography;

namespace TheDotNetLeague.Ipfs.Core.Lib
{
    /// <summary>
    ///     Configuration options for communication with other peers.
    /// </summary>
    /// <seealso cref="IpfsEngineOptions" />
    public class SwarmOptions
    {
        /// <summary>
        ///     The key of the private network.
        /// </summary>
        /// <value>
        ///     The key must either <b>null</b> or 32 bytes (256 bits) in length.
        /// </value>
        /// <remarks>
        ///     When null, the public network is used.  Otherwise, the network is
        ///     considered private and only peers with the same key will be
        ///     communicated with.
        ///     <para>
        ///         When using a private network, the <see cref="DiscoveryOptions.BootstrapPeers" />
        ///         must also use this key.
        ///     </para>
        /// </remarks>
        /// <seealso href="https://github.com/libp2p/specs/blob/master/pnet/Private-Networks-PSK-V1.md" />
        public PreSharedKey PrivateNetworkKey { get; set; }

        /// <summary>
        ///     The low water mark for peer connections.
        /// </summary>
        /// <value>
        ///     Defaults to 0.
        /// </value>
        /// <remarks>
        ///     The <see cref="AutoDialer" /> is used to maintain at
        ///     least this number of connections.
        ///     <para>
        ///         This is an opt-feature.  The value must be positive to enable it.
        ///     </para>
        /// </remarks>
        public int MinConnections { get; set; } = 8;
    }
}
