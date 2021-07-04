using DynamicData;
using Mutagen.Bethesda.Plugins.Records;
using Noggog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData.Kernel;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Plugins.Exceptions;
using Mutagen.Bethesda.Plugins.Implicit;
using Mutagen.Bethesda.Plugins.Implicit.DI;
using Mutagen.Bethesda.Plugins.Order.DI;
using Mutagen.Bethesda.Plugins.Records.DI;

namespace Mutagen.Bethesda.Plugins.Order
{
    /// <summary>
    /// A static class with LoadOrder related utility functions
    /// </summary>
    public static class LoadOrder
    {
        private static TimestampAligner Aligner = new(IFileSystemExt.DefaultFilesystem);
        private static OrderListings Orderer = new();
        private static readonly TimestampAligner TimestampAligner = new(IFileSystemExt.DefaultFilesystem);
        
        #region Timestamps

        /// <summary>
        /// Returns whether given game needs timestamp alignment for its load order
        /// </summary>
        /// <param name="game">Game to check</param>
        /// <returns>True if file located</returns>
        public static bool NeedsTimestampAlignment(GameCategory game) => Aligner.NeedsTimestampAlignment(game);

        /// <summary>
        /// Constructs a load order from a list of mods and a data folder.
        /// Load Order is sorted to the order the game will load the mod files: by file's date modified timestamp.
        /// </summary>
        /// <param name="incomingLoadOrder">Mods to include</param>
        /// <param name="dataPath">Path to data folder</param>
        /// <param name="throwOnMissingMods">Whether to throw and exception if mods are missing</param>
        /// <returns>Enumerable of modkeys in load order, excluding missing mods</returns>
        /// <exception cref="MissingModException">If throwOnMissingMods true and file is missing</exception>
        public static IEnumerable<IModListingGetter> AlignToTimestamps(
            IEnumerable<IModListingGetter> incomingLoadOrder,
            DirectoryPath dataPath,
            bool throwOnMissingMods = true)
        {
            return Aligner.AlignToTimestamps(incomingLoadOrder, dataPath, throwOnMissingMods: throwOnMissingMods);
        }

        /// <summary>
        /// Constructs a load order from a list of mods and a data folder.
        /// Load Order is sorted to the order the game will load the mod files: by file's date modified timestamp.
        /// </summary>
        /// <param name="incomingLoadOrder">Mods and their write timestamps</param>
        /// <returns>Enumerable of modkeys in load order, excluding missing mods</returns>
        public static IEnumerable<ModKey> AlignToTimestamps(IEnumerable<(ModKey ModKey, DateTime Write)> incomingLoadOrder)
        {
            return Aligner.AlignToTimestamps(incomingLoadOrder);
        }

        /// <summary>
        /// Modifies time stamps of files to match the given ordering
        /// <param name="loadOrder">Order to conform files to</param>
        /// <param name="dataPath">Path to data folder</param>
        /// <param name="throwOnMissingMods">Whether to throw and exception if mods are missing</param>
        /// <param name="startDate">Date to give the first file</param>
        /// <param name="interval">Time interval to space between each file's date</param>
        /// <exception cref="MissingModException">If throwOnMissingMods true and file is missing</exception>
        /// </summary>
        public static void AlignTimestamps(
            IEnumerable<ModKey> loadOrder,
            DirectoryPath dataPath,
            bool throwOnMissingMods = true,
            DateTime? startDate = null,
            TimeSpan? interval = null)
        {
            Aligner.AlignTimestamps(
                loadOrder,
                dataPath,
                throwOnMissingMods: throwOnMissingMods,
                startDate: startDate,
                interval: interval);
        }
        #endregion

        /// <summary>
        /// Returns a load order listing from the usual sources
        /// </summary>
        /// <param name="game">Game type</param>
        /// <param name="dataPath">Path to game's data folder</param>
        /// <param name="throwOnMissingMods">Whether to throw and exception if mods are missing</param>
        /// <returns>Enumerable of modkeys representing a load order</returns>
        /// <exception cref="ArgumentException">Line in plugin file is unexpected</exception>
        /// <exception cref="FileNotFoundException">If plugin file not located</exception>
        /// <exception cref="MissingModException">If throwOnMissingMods true and file is missing</exception>
        public static IEnumerable<IModListingGetter> GetListings(
            GameRelease game,
            DirectoryPath dataPath,
            bool throwOnMissingMods = true)
        {
            var gameContext = new GameReleaseInjection(game);
            var categoryContext = new GameCategoryInjection(game.ToCategory());
            var pluginPath = new PluginListingsPathProvider(gameContext);
            var dataDir = new DataDirectoryInjection(dataPath);
            var pluginProvider = PluginListingsProvider(
                dataDir,
                gameContext,
                pluginPath,
                throwOnMissingMods);

            return new LoadOrderListingsProvider(
                    Orderer,
                    new ImplicitListingsProvider(
                        IFileSystemExt.DefaultFilesystem,
                        new DataDirectoryInjection(dataPath),
                        new ImplicitListingModKeyProvider(gameContext)),
                    pluginProvider,
                    new CreationClubListingsProvider(
                        IFileSystemExt.DefaultFilesystem,
                        dataDir,
                        new CreationClubListingsPathProvider(
                            categoryContext,
                            new CreationClubEnabledProvider(
                                categoryContext),
                            dataDir),
                        new CreationClubRawListingsReader(
                            IFileSystemExt.DefaultFilesystem,
                            dataDir)))
                .Get();
        }

        public static IEnumerable<IModListingGetter> GetListings(
            GameRelease game,
            FilePath pluginsFilePath,
            FilePath? creationClubFilePath,
            DirectoryPath dataPath,
            bool throwOnMissingMods = true)
        {
            var gameContext = new GameReleaseInjection(game);
            var pluginPath = new PluginListingsPathInjection(pluginsFilePath);
            var dataDir = new DataDirectoryInjection(dataPath);
            var pluginProvider = PluginListingsProvider(
                dataDir,
                gameContext,
                pluginPath,
                throwOnMissingMods);

            return new LoadOrderListingsProvider(
                    Orderer,
                    new ImplicitListingsProvider(
                        IFileSystemExt.DefaultFilesystem,
                        new DataDirectoryInjection(dataPath),
                        new ImplicitListingModKeyProvider(gameContext)),
                    pluginProvider,
                    new CreationClubListingsProvider(
                        IFileSystemExt.DefaultFilesystem,
                        dataDir,
                        new CreationClubListingsPathInjection(creationClubFilePath),
                        new CreationClubRawListingsReader(
                            IFileSystemExt.DefaultFilesystem,
                            dataDir)))
                .Get();
        }

        public static IEnumerable<T> OrderListings<T>(IEnumerable<T> e, Func<T, ModKey> selector)
        {
            return Orderer.Order(e, selector);
        }

        public static IEnumerable<T> OrderListings<T>(
            IEnumerable<T> implicitListings,
            IEnumerable<T> pluginsListings,
            IEnumerable<T> creationClubListings,
            Func<T, ModKey> selector)
        {
            return Orderer.Order(implicitListings, pluginsListings, creationClubListings, selector);
        }

        public static IObservable<IChangeSet<IModListingGetter>> GetLiveLoadOrder(
            GameRelease game,
            DirectoryPath dataFolderPath,
            out IObservable<ErrorResponse> state,
            bool throwOnMissingMods = true)
        {
            var dataDir = new DataDirectoryInjection(dataFolderPath);
            var gameRelease = new GameReleaseInjection(game);
            var pluginPath = new PluginListingsPathProvider(gameRelease);
            var gameCategoryInjection = new GameCategoryInjection(game.ToCategory());
            var cccPath = new CreationClubListingsPathProvider(
                gameCategoryInjection,
                new CreationClubEnabledProvider(
                    gameCategoryInjection),
                dataDir);
            return GetLiveLoadOrder(
                game,
                pluginPath.Path,
                dataFolderPath,
                out state,
                cccPath.Path,
                throwOnMissingMods);
        }

        public static IObservable<IChangeSet<IModListingGetter>> GetLiveLoadOrder(
            IObservable<GameRelease> game,
            IObservable<DirectoryPath> dataFolderPath,
            out IObservable<ErrorResponse> state,
            bool throwOnMissingMods = true)
        {
            var obs = Observable.CombineLatest(
                    game,
                    dataFolderPath,
                    (gameVal, dataFolderVal) =>
                    {
                        var lo = GetLiveLoadOrder(
                            game: gameVal,
                            dataFolderPath: dataFolderVal,
                            loadOrderFilePath: PluginListings.GetListingsPath(gameVal),
                            cccLoadOrderFilePath: CreationClubListings.GetListingsPath(gameVal.ToCategory(), dataFolderVal),
                            state: out var state,
                            throwOnMissingMods: throwOnMissingMods);
                        return (LoadOrder: lo, State: state);
                    })
                .Replay(1)
                .RefCount();
            state = obs.Select(x => x.State)
                .Switch();
            return obs.Select(x => x.LoadOrder)
                .Switch();
        }

        public static IObservable<IChangeSet<IModListingGetter>> GetLiveLoadOrder(
            GameRelease game,
            FilePath loadOrderFilePath,
            DirectoryPath dataFolderPath,
            out IObservable<ErrorResponse> state,
            FilePath? cccLoadOrderFilePath = null,
            bool throwOnMissingMods = true)
        {
            var dataDir = new DataDirectoryInjection(dataFolderPath);
            var gameRelease = new GameReleaseInjection(game);
            var pluginPath = new PluginListingsPathInjection(loadOrderFilePath);
            var cccPath = new CreationClubListingsPathInjection(cccLoadOrderFilePath);
            var pluginListingsProv = PluginListingsProvider(
                dataDir,
                gameRelease,
                pluginPath,
                throwOnMissingMods);
            var creationClubRawListingsReader = new CreationClubRawListingsReader(
                IFileSystemExt.DefaultFilesystem,
                dataDir);
            var cccListingsProv = new CreationClubListingsProvider(
                IFileSystemExt.DefaultFilesystem,
                dataDir,
                cccPath,
                creationClubRawListingsReader);
            return new LiveLoadOrderProvider(
                new PluginLiveLoadOrderProvider(
                    IFileSystemExt.DefaultFilesystem,
                    pluginListingsProv,
                    pluginPath),
                new CreationClubLiveLoadOrderProvider(
                    new CreationClubLiveListingsFileReader(
                        IFileSystemExt.DefaultFilesystem,
                        creationClubRawListingsReader,
                        cccPath),
                    new CreationClubLiveLoadOrderFolderWatcher(
                        IFileSystemExt.DefaultFilesystem,
                        dataDir)),
                new LoadOrderListingsProvider(
                    Orderer,
                    new ImplicitListingsProvider(
                        IFileSystemExt.DefaultFilesystem,
                        dataDir,
                        new ImplicitListingModKeyProvider(gameRelease)),
                    pluginListingsProv,
                    cccListingsProv))
                .Get(out state);
        }

        public static IObservable<IChangeSet<IModListingGetter>> GetLiveLoadOrder(
            IObservable<GameRelease> game,
            IObservable<FilePath> loadOrderFilePath,
            IObservable<DirectoryPath> dataFolderPath,
            out IObservable<ErrorResponse> state,
            IObservable<FilePath?>? cccLoadOrderFilePath = null,
            bool throwOnMissingMods = true)
        {
            var obs = Observable.CombineLatest(
                    game,
                    dataFolderPath,
                    loadOrderFilePath,
                    cccLoadOrderFilePath ?? Observable.Return(default(FilePath?)),
                    (gameVal, dataFolderVal, loadOrderFilePathVal, cccVal) =>
                    {
                        var lo = GetLiveLoadOrder(
                            game: gameVal,
                            dataFolderPath: dataFolderVal,
                            loadOrderFilePath: loadOrderFilePathVal,
                            cccLoadOrderFilePath: cccVal,
                            state: out var state,
                            throwOnMissingMods: throwOnMissingMods);
                        return (LoadOrder: lo, State: state);
                    })
                .Replay(1)
                .RefCount();
            state = obs.Select(x => x.State)
                .Switch();
            return obs.Select(x => x.LoadOrder)
                .Switch();
        }

        /// <summary>
        /// Constructs a load order filled with mods constructed
        /// </summary>
        /// <param name="dataFolder">Path data folder containing mods</param>
        /// <param name="loadOrder">Unique list of listings to import</param>
        /// <param name="gameRelease">GameRelease associated with the mods to create<br/>
        /// This may be unapplicable to some games with only one release, but should still be passed in.
        /// </param>
        public static ILoadOrder<IModListing<TMod>> Import<TMod>(
            DirectoryPath dataFolder,
            IEnumerable<IModListingGetter> loadOrder,
            GameRelease gameRelease)
            where TMod : class, IModGetter
        {
            return new LoadOrderImporter<TMod>(
                    IFileSystemExt.DefaultFilesystem,
                    new DataDirectoryInjection(dataFolder),
                    new LoadOrderListingsInjector(loadOrder),
                    new ModImporter<TMod>(
                        IFileSystemExt.DefaultFilesystem,
                        new GameReleaseInjection(gameRelease)))
                .Import();
        }

        /// <summary>
        /// Constructs a load order filled with mods constructed
        /// </summary>
        /// <param name="dataFolder">Path data folder containing mods</param>
        /// <param name="loadOrder">Unique list of mod keys to import</param>
        /// <param name="gameRelease">GameRelease associated with the mods to create<br/>
        /// This may be unapplicable to some games with only one release, but should still be passed in.
        /// </param>
        public static ILoadOrder<IModListing<TMod>> Import<TMod>(
            DirectoryPath dataFolder,
            IEnumerable<ModKey> loadOrder,
            GameRelease gameRelease)
            where TMod : class, IModGetter
        {
            return new LoadOrderImporter<TMod>(
                    IFileSystemExt.DefaultFilesystem,
                    new DataDirectoryInjection(dataFolder),
                    new LoadOrderListingsInjector(
                        loadOrder.Select(x => new ModListing(x, true))),
                    new ModImporter<TMod>(
                        IFileSystemExt.DefaultFilesystem,
                        new GameReleaseInjection(gameRelease)))
                .Import();
        }

        /// <summary>
        /// Constructs a load order filled with mods constructed by given importer func
        /// </summary>
        /// <param name="dataFolder">Path data folder containing mods</param>
        /// <param name="loadOrder">Unique list of mod keys to import</param>
        /// <param name="factory">Func to use to create a new mod from a path</param>
        public static ILoadOrder<IModListing<TMod>> Import<TMod>(
            DirectoryPath dataFolder,
            IEnumerable<ModKey> loadOrder,
            Func<ModPath, TMod> factory)
            where TMod : class, IModGetter
        {
            return new LoadOrderImporter<TMod>(
                    IFileSystemExt.DefaultFilesystem,
                    new DataDirectoryInjection(dataFolder),
                    new LoadOrderListingsInjector(
                        loadOrder.Select(x => new ModListing(x, true))),
                    new ModImporterWrapper<TMod>(factory))
                .Import();
        }

        /// <summary>
        /// Constructs a load order filled with mods constructed by given importer func
        /// </summary>
        /// <param name="dataFolder">Path data folder containing mods</param>
        /// <param name="loadOrder">Unique list of listings to import</param>
        /// <param name="factory">Func to use to create a new mod from a path</param>
        public static ILoadOrder<IModListing<TMod>> Import<TMod>(
            DirectoryPath dataFolder,
            IEnumerable<IModListingGetter> loadOrder,
            Func<ModPath, TMod> factory)
            where TMod : class, IModGetter
        {
            return new LoadOrderImporter<TMod>(
                    IFileSystemExt.DefaultFilesystem,
                    new DataDirectoryInjection(dataFolder),
                    new LoadOrderListingsInjector(loadOrder),
                    new ModImporterWrapper<TMod>(factory))
                .Import();
        }

        public static void Write(
            FilePath path, 
            GameRelease release, 
            IEnumerable<IModListingGetter> loadOrder,
            bool removeImplicitMods = true)
        {
            var rel = new GameReleaseInjection(release);
            new LoadOrderWriter(
                    IFileSystemExt.DefaultFilesystem,
                    new HasEnabledMarkersProvider(rel),
                    new ImplicitListingModKeyProvider(rel))
                .Write(path, loadOrder, removeImplicitMods);
        }
        
        private static PluginListingsProvider PluginListingsProvider(
            IDataDirectoryContext dataDirectory,
            IGameReleaseContext gameContext,
            IPluginListingsPathProvider listingsPathProvider, 
            bool throwOnMissingMods)
        {
            var fs = IFileSystemExt.DefaultFilesystem;
            var pluginListingParser = new PluginListingsParser(
                new ModListingParser(
                    new HasEnabledMarkersProvider(gameContext)));
            var provider = new PluginListingsProvider(
                gameContext,
                new TimestampedPluginListingsProvider(
                    new TimestampAligner(fs),
                    new TimestampedPluginListingsPreferences() {ThrowOnMissingMods = throwOnMissingMods},
                    new PluginRawListingsReader(
                        fs,
                        pluginListingParser),
                    dataDirectory,
                    listingsPathProvider),
                new EnabledPluginListingsProvider(
                    new PluginRawListingsReader(
                        fs,
                        pluginListingParser),
                    listingsPathProvider));
            return provider;
        }
    }

    public interface ILoadOrderGetter : IDisposable
    {
        /// <summary>
        /// Number of listings on the Load Order
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Listings in the order they were listed
        /// </summary>
        IEnumerable<ModKey> ListedOrder { get; }

        /// <summary>
        /// Listings in priority order, where the mod with the highest priority comes first.  (Reverse of ListedOrder)
        /// </summary>
        IEnumerable<ModKey> PriorityOrder { get; }

        /// <summary>
        /// Whether the load order contains a listing with the given key
        /// </summary>
        bool ContainsKey(ModKey key);
    }

    public interface ILoadOrderGetter<out TListing> : ILoadOrderGetter, IReadOnlyList<Noggog.IKeyValue<TListing, ModKey>>, IReadOnlyCache<TListing, ModKey>
        where TListing : IModKeyed
    {
        new TListing this[int index] { get; }
        
        TListing? TryGetAtIndex(int index);

        /// <summary>
        /// Listings in the order they were listed
        /// </summary>
        new IEnumerable<TListing> ListedOrder { get; }

        /// <summary>
        /// Listings in priority order, where the mod with the highest priority comes first.  (Reverse of ListedOrder)
        /// </summary>
        new IEnumerable<TListing> PriorityOrder { get; }

        /// <summary>
        /// Number of listings on the Load Order
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Whether the load order contains a listing with the given key
        /// </summary>
        new bool ContainsKey(ModKey key);

        /// <summary>
        /// Locates index of an item with given key
        /// </summary>
        /// <param name="key">Key to query</param>
        /// <returns>Index of item on list with key. -1 if not located</returns>
        int IndexOf(ModKey key);
    }

    public interface ILoadOrder<TListing> : ILoadOrderGetter<TListing>
        where TListing : IModKeyed
    {
        /// <summary>
        /// Adds an item to the end of the load order
        /// </summary>
        /// <param name="item">Item to put at end of load order</param>
        /// <exception cref="ArgumentException">If an item with same ModKey exists already</exception>
        void Add(TListing item);

        /// <summary>
        /// Adds items to the end of the load order
        /// </summary>
        /// <param name="items">Items to put at end of load order</param>
        /// <exception cref="ArgumentException">If an item with same ModKey exists already</exception>
        void Add(IEnumerable<TListing> items);

        /// <summary>
        /// Adds an item at the given index in load order, with the given ModKey
        /// </summary>
        /// <param name="item">Item to put at end of load order</param>
        /// <param name="index">Index to insert at</param>
        /// <exception cref="ArgumentException">If an item with same ModKey exists already</exception>
        void Add(TListing item, int index);

        /// <summary>
        /// Clears load order of all items
        /// </summary>
        void Clear();

        bool RemoveKey(ModKey modKey);

        void RemoveAt(int index);

        void Set(TListing listing);

        void Set(IEnumerable<TListing> items);
    }

    /// <summary>
    /// A container for objects with in a specific load order, that are associated with ModKeys.
    /// LoadOrder does not need to be disposed for proper use, but rather can optionally be disposed of which will dispose any contained items that implement IDisposable
    /// </summary>
    public class LoadOrder<TListing> : ILoadOrder<TListing>
        where TListing : IModKeyed
    {
        private readonly List<ItemContainer> _byLoadOrder = new();
        private readonly Dictionary<ModKey, ItemContainer> _byModKey = new();
        private readonly bool _disposeItems;

        /// <inheritdoc />
        public int Count => _byLoadOrder.Count;

        /// <inheritdoc />
        public TListing this[int index] => _byLoadOrder[index].Item;

        IEnumerable<TListing> IReadOnlyCache<TListing, ModKey>.Items => ListedOrder;

        /// <inheritdoc />
        public IEnumerable<ModKey> Keys => _byModKey.Keys;

        /// <inheritdoc />
        public IEnumerable<TListing> ListedOrder => _byLoadOrder.Select(i => i.Item);

        /// <inheritdoc />
        public IEnumerable<TListing> PriorityOrder => ((IEnumerable<ItemContainer>)_byLoadOrder).Reverse().Select(i => i.Item);

        IEnumerable<ModKey> ILoadOrderGetter.ListedOrder => _byLoadOrder.Select(x => x.Item.ModKey);

        IEnumerable<ModKey> ILoadOrderGetter.PriorityOrder => _byLoadOrder.Select(x => x.Item.ModKey).Reverse();

        Noggog.IKeyValue<TListing, ModKey> IReadOnlyList<Noggog.IKeyValue<TListing, ModKey>>.this[int index]
        {
            get
            {
                var cont = _byLoadOrder[index];
                return new KeyValue<TListing, ModKey>(cont.Item.ModKey, cont.Item);
            }
        }

        /// <inheritdoc />
        public TListing this[ModKey key] => _byModKey[key].Item;

        public LoadOrder(bool disposeItems = true)
        {
            _disposeItems = disposeItems;
        }

        public LoadOrder(IEnumerable<TListing> items, bool disposeItems = true)
        {
            _disposeItems = disposeItems;
            int index = 0;
            _byLoadOrder.AddRange(items.Select(i => new ItemContainer(i, index++)));
            foreach (var item in _byLoadOrder)
            {
                try
                {
                    _byModKey.Add(item.Item.ModKey, item);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException($"ModKey was already present: {item.Item.ModKey}");
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve an item given a ModKey
        /// </summary>
        /// <param name="key">ModKey to query for</param>
        /// <param name="value">Result reference to the item</param>
        /// <returns>True if matching key located</returns>
        public bool TryGetValue(ModKey key, [MaybeNullWhen(false)] out TListing value)
        {
            if (_byModKey.TryGetValue(key, out var container))
            {
                value = container.Item;
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Attempts to retrieve an item given a ModKey
        /// </summary>
        /// <param name="key">ModKey to query for</param>
        /// <returns>Result reference to the item, or null if not found</returns>
        public TListing? TryGetValue(ModKey key)
        {
            if (_byModKey.TryGetValue(key, out var container))
            {
                return container.Item;
            }
            return default;
        }

        /// <summary>
        /// Attempts to retrieve an item given an index
        /// </summary>
        /// <param name="index">Index to retrieve</param>
        /// <returns>TryGet result of the item</returns>
        public TListing? TryGetAtIndex(int index)
        {
            if (!_byLoadOrder.InRange(index))
            {
                return default;
            }

            return _byLoadOrder[index].Item;
        }

        /// <inheritdoc />
        public void Add(TListing item)
        {
            var index = _byLoadOrder.Count;
            var container = new ItemContainer(item, index);
            try
            {
                _byModKey.Add(item.ModKey, container);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"ModKey was already present: {item.ModKey}");
            }
            _byLoadOrder.Add(container);
        }

        /// <inheritdoc />
        public void Add(IEnumerable<TListing> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <inheritdoc />
        public void Add(TListing item, int index)
        {
            if (!_byLoadOrder.InRange(index))
            {
                throw new ArgumentException("Tried to insert at an out of range index.");
            }
            var container = new ItemContainer(item, index);
            try
            {
                _byModKey.Add(item.ModKey, container);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"ModKey was already present: {item.ModKey}");
            }
            _byLoadOrder.Add(container);
            for (int i = index + 1; i < _byLoadOrder.Count; i++)
            {
                _byLoadOrder[i].Index += 1;
            }
        }

        /// <inheritdoc />
        public bool ContainsKey(ModKey key)
        {
            return IndexOf(key) != -1;
        }

        /// <inheritdoc />
        public int IndexOf(ModKey key)
        {
            if (!_byModKey.TryGetValue(key, out var container))
            {
                return -1;
            }
            return container.Index;
        }

        /// <inheritdoc />
        public void Clear()
        {
            Dispose();
            this._byLoadOrder.Clear();
            this._byModKey.Clear();
        }

        public bool RemoveKey(ModKey key)
        {
            if (!_byModKey.TryGetValue(key, out var registry)) return false;
            _byLoadOrder.RemoveAt(registry.Index);
            for (int i = registry.Index; i < _byLoadOrder.Count; i++)
            {
                _byLoadOrder[i].Index--;
            }

            _byModKey.Remove(key);
            return true;
        }

        public void RemoveAt(int index)
        {
            var item = _byLoadOrder[index];
            _byLoadOrder.RemoveAt(index);
            _byModKey.Remove(item.Item.ModKey);
            for (int i = index; i < _byLoadOrder.Count; i++)
            {
                _byLoadOrder[i].Index--;
            }

            if (_disposeItems && item.Item is IDisposable disp)
            {
                disp.Dispose();
            }
        }

        public void Set(TListing listing)
        {
            if (!_byModKey.TryGetValue(listing.ModKey, out var existing))
            {
                Add(listing);
                return;
            }

            var old = existing.Item;
            existing.Item = listing;

            if (_disposeItems && old is IDisposable disp)
            {
                disp.Dispose();
            }
        }

        public void Set(IEnumerable<TListing> items)
        {
            foreach (var item in items)
            {
                Set(item);
            }
        }

        IEnumerator<Noggog.IKeyValue<TListing, ModKey>> IEnumerable<Noggog.IKeyValue<TListing, ModKey>>.GetEnumerator()
        {
            return ListedOrder.Select(x => new KeyValue<TListing, ModKey>(x.ModKey, x)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Disposes all contained items that implement IDisposable
        /// </summary>
        public void Dispose()
        {
            if (!_disposeItems) return;
            foreach (var item in _byLoadOrder)
            {
                if (item.Item is IDisposable disp)
                {
                    disp.Dispose();
                }
            }
        }

        public IEnumerator<TListing> GetEnumerator()
        {
            foreach (var item in _byLoadOrder)
            {
                yield return item.Item;
            }
        }

        private class ItemContainer
        {
            public TListing Item;
            public int Index;

            public ItemContainer(TListing item, int index)
            {
                Item = item;
                Index = index;
            }
        }
    }
}
