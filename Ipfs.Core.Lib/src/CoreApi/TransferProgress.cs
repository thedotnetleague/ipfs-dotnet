﻿using System;

namespace TheDotNetLeague.Ipfs.Core.Lib.CoreApi
{
    /// <summary>
    ///     Reports the <see cref="IProgress{T}">progress</see> of
    ///     a transfer operation.
    /// </summary>
    public class TransferProgress
    {
        /// <summary>
        ///     The cumuative number of bytes transfered for
        ///     the <see cref="Name" />.
        /// </summary>
        public ulong Bytes;

        /// <summary>
        ///     The name of the item being trasfered.
        /// </summary>
        /// <value>
        ///     Typically, a relative file path.
        /// </value>
        public string Name;
    }
}
