﻿using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TheDotNetLeague.Ipfs.Core.Lib;
using TheDotNetLeague.Ipfs.Core.Lib.CoreApi;

namespace TheDotNetLeague.Ipfs.Http.Client.CoreApi
{

    class BlockRepositoryApi : IBlockRepositoryApi
    {
        IpfsClient ipfs;

        internal BlockRepositoryApi(IpfsClient ipfs)
        {
            this.ipfs = ipfs;
        }

        public async Task RemoveGarbageAsync(CancellationToken cancel = default(CancellationToken))
        {
            await ipfs.DoCommandAsync("repo/gc", cancel);
        }

        public Task<RepositoryData> StatisticsAsync(CancellationToken cancel = default(CancellationToken))
        {
            return ipfs.DoCommandAsync<RepositoryData>("repo/stat", cancel);
        }

        public async Task VerifyAsync(CancellationToken cancel = default(CancellationToken))
        {
            await ipfs.DoCommandAsync("repo/verify", cancel);
        }

        public async Task<string> VersionAsync(CancellationToken cancel = default(CancellationToken))
        {
            var json = await ipfs.DoCommandAsync("repo/version", cancel);
            var info = JObject.Parse(json);
            return (string)info["Version"];
        }
    }
}
