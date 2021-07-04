﻿using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Environments.DI;
using Path = System.IO.Path;

namespace Mutagen.Bethesda.Plugins.Order.DI
{
    public interface ICreationClubListingsProvider : IListingsProvider
    {
        /// <summary>
        /// Parses the typical plugins file to retrieve all ModKeys in expected plugin file format,
        /// </summary>
        /// <returns>Enumerable of ModKeys representing a load order</returns>
        /// <exception cref="InvalidDataException">Line in plugin file is unexpected</exception>
        public IEnumerable<IModListingGetter> Get(bool throwIfMissing);
    }

    public class CreationClubListingsProvider : ICreationClubListingsProvider
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDataDirectoryContext _dataDirectoryContext;
        private readonly ICreationClubListingsPathProvider _pluginListingsPathProvider;
        private readonly ICreationClubRawListingsReader _reader;

        public CreationClubListingsProvider(
            IFileSystem fileSystem,
            IDataDirectoryContext dataDirectoryContext,
            ICreationClubListingsPathProvider pluginListingsPathProvider,
            ICreationClubRawListingsReader reader)
        {
            _fileSystem = fileSystem;
            _dataDirectoryContext = dataDirectoryContext;
            _pluginListingsPathProvider = pluginListingsPathProvider;
            _reader = reader;
        }

        public IEnumerable<IModListingGetter> Get()
        {
            return Get(throwIfMissing: true);
        }
        
        public IEnumerable<IModListingGetter> Get(bool throwIfMissing)
        {
            var path = _pluginListingsPathProvider.Path;
            if (path == null) return Enumerable.Empty<IModListingGetter>();
            if (!_fileSystem.File.Exists(path.Value))
            {
                if (throwIfMissing)
                {
                    throw new FileNotFoundException("Could not locate ccc plugin file", path.Value.Path);   
                }
                else
                {
                    return Enumerable.Empty<IModListingGetter>();
                }
            }

            return _reader.Read(_fileSystem.File.OpenRead(path.Value))
                .Where(x => _fileSystem.File.Exists(Path.Combine(_dataDirectoryContext.Path, x.ModKey.FileName)))
                .ToList();
        }
    }
}