﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Primitives;
using TheDotNetLeague.Ipfs.Abstractions;
using TheDotNetLeague.Ipfs.Core.Lib;

namespace TheDotNetLeague.Ipfs.Http.Gateway.Pages
{
    /// <summary>
    ///     An IPFS file or directory.
    /// </summary>
    public class IpfsModel : PageModel
    {
        private readonly ICoreApi ipfs;
        private IFileSystemNode node;

        /// <summary>
        ///     Creates a new instance of the controller.
        /// </summary>
        /// <param name="ipfs">
        ///     An object that implements the ICoreApi, typically an Ipfs.Core.Lib.
        /// </param>
        public IpfsModel(ICoreApi ipfs) { this.ipfs = ipfs; }

        /// <summary>
        ///     The IPFS path to a file or directry.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string Path { get; set; }

        /// <summary>
        ///     The parts of the <see cref="Path" />.
        /// </summary>
        public IEnumerable<string> PathParts
        {
            get
            {
                return Path
                   .Split('/')
                   .Where(p => !string.IsNullOrWhiteSpace(p));
            }
        }

        /// <summary>
        ///     The parent path.
        /// </summary>
        public string Parent
        {
            get
            {
                var parts = PathParts.ToList();
                parts.RemoveAt(parts.Count - 1);
                return string.Join('/', parts);
            }
        }

        /// <summary>
        ///     A sequence of files for the directory.
        /// </summary>
        public IEnumerable<IFileSystemLink> Files => node.Links;

        /// <summary>
        ///     A sequence of sub-directories for the directory.
        /// </summary>
        public IEnumerable<IFileSystemLink> Directories => node.Links;

        /// <summary>
        ///     Get the file or directory.
        /// </summary>
        /// <remarks>
        ///     Returns the contents of the file or a page listing the directory.
        /// </remarks>
        public async Task<IActionResult> OnGetAsync(string filename, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(Path)) return NotFound("The CID is missing.");

            try
            {
                node = await ipfs.FileSystem.ListFileAsync(Path, cancel);
            }
            catch (Exception e) when (e is ArgumentException || e is FormatException)
            {
                return NotFound(e.Message);
            }

            // If a directory, then display a page with directory contents or if
            // the directory contain "index.html", redirect to the page.
            if (node.IsDirectory)
            {
                if (!Path.EndsWith("/") && node.Links.Any(l => l.Name == "index.html"))
                    return Redirect($"/ipfs/{Path}/index.html");

                Path = Path.TrimEnd('/');
                return Page();
            }

            // If a file, send it.
            var etag = new EntityTagHeaderValue("\"" + node.Id + "\"", false);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filename ?? Path, out var contentType))
                contentType = "application/octet-stream";
            var stream = await ipfs.FileSystem.ReadFileAsync(node.Id, cancel);
            Response.Headers.Add("cache-control", new StringValues("public, max-age=31536000, immutable"));
            Response.Headers.Add("etag", new StringValues(etag.Tag));
            return File(stream, contentType);
        }
    }
}
