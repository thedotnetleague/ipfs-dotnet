﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace TheDotNetLeague.Ipfs.Cli.Commands
{
    [Command(Description = "Resolve any type of name")]
    internal class ResolveCommand : CommandBase
    {
        [Argument(0, "name", "The IPFS/IPNS/... name")]
        [Required]
        public string Name { get; set; }

        [Option("-r|--recursive", Description = "Resolve until the result is an IPFS name")]
        public bool Recursive { get; set; }

        private Program Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var result = await Parent.CoreApi.Generic.ResolveAsync(Name, Recursive);
            app.Out.Write(result);
            return 0;
        }
    }
}
