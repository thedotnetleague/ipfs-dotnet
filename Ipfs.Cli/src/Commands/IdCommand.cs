﻿using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using TheDotNetLeague.MultiFormats.MultiHash;

namespace TheDotNetLeague.Ipfs.Cli.Commands
{
    [Command(Description = "Show info on an IPFS peer")]
    internal class IdCommand : CommandBase
    {
        [Argument(0, "peerid", "The IPFS peer ID")]
        public string PeerId { get; set; }

        private Program Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var id = PeerId == null ? null : new MultiHash(PeerId);
            var peer = await Parent.CoreApi.Generic.IdAsync(id);
            using (JsonWriter writer = new JsonTextWriter(app.Out))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("ID");
                writer.WriteValue(peer.Id.ToBase58());
                writer.WritePropertyName("PublicKey");
                writer.WriteValue(peer.PublicKey);
                writer.WritePropertyName("Adddresses");
                writer.WriteStartArray();
                foreach (var a in peer.Addresses)
                    if (a != null)
                        writer.WriteValue(a.ToString());
                writer.WriteEndArray();
                writer.WritePropertyName("AgentVersion");
                writer.WriteValue(peer.AgentVersion);
                writer.WritePropertyName("ProtocolVersion");
                writer.WriteValue(peer.ProtocolVersion);
                writer.WriteEndObject();
            }

            return 0;
        }
    }
}
