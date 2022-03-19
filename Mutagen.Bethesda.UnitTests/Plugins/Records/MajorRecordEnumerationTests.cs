using FluentAssertions;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins.Records;
using Noggog;
using System.Linq;
using Mutagen.Bethesda.Plugins.Aspects;
using Mutagen.Bethesda.Testing;
using Xunit;
using Ammunition = Mutagen.Bethesda.Skyrim.Ammunition;
using Cell = Mutagen.Bethesda.Skyrim.Cell;
using CellBlock = Mutagen.Bethesda.Skyrim.CellBlock;
using CellSubBlock = Mutagen.Bethesda.Skyrim.CellSubBlock;
using GroupTypeEnum = Mutagen.Bethesda.Skyrim.GroupTypeEnum;
using IAmmunition = Mutagen.Bethesda.Skyrim.IAmmunition;
using IAmmunitionGetter = Mutagen.Bethesda.Skyrim.IAmmunitionGetter;
using ICellGetter = Mutagen.Bethesda.Skyrim.ICellGetter;
using IFactionGetter = Mutagen.Bethesda.Skyrim.IFactionGetter;
using INpc = Mutagen.Bethesda.Skyrim.INpc;
using INpcGetter = Mutagen.Bethesda.Skyrim.INpcGetter;
using IOwner = Mutagen.Bethesda.Skyrim.IOwner;
using IOwnerGetter = Mutagen.Bethesda.Skyrim.IOwnerGetter;
using IPlaced = Mutagen.Bethesda.Skyrim.IPlaced;
using IPlacedGetter = Mutagen.Bethesda.Skyrim.IPlacedGetter;
using Npc = Mutagen.Bethesda.Skyrim.Npc;
using PlacedNpc = Mutagen.Bethesda.Skyrim.PlacedNpc;
using Worldspace = Mutagen.Bethesda.Skyrim.Worldspace;

namespace Mutagen.Bethesda.UnitTests.Plugins.Records;

public abstract class AMajorRecordEnumerationTests
{
    protected abstract ISkyrimModGetter ConvertMod(SkyrimMod mod);

    public abstract bool Getter { get; }

    [Fact]
    public void Empty()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var conv = ConvertMod(mod);
        Assert.Empty(conv.EnumerateMajorRecords());
        Assert.Empty(conv.EnumerateMajorRecords<IMajorRecord>());
        Assert.Empty(conv.EnumerateMajorRecords<IMajorRecordGetter>());
        Assert.Empty(conv.EnumerateMajorRecords<INpc>());
        Assert.Empty(conv.EnumerateMajorRecords<INpcGetter>());
        Assert.Empty(conv.EnumerateMajorRecords<Npc>());
    }

    [Fact]
    public void EnumerateAll()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        mod.Npcs.AddNew();
        mod.Ammunitions.AddNew();
        var conv = ConvertMod(mod);
        Assert.Equal(2, conv.EnumerateMajorRecords().Count());
    }

    [Fact]
    public void EnumerateAllViaGeneric()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        mod.Npcs.AddNew();
        mod.Ammunitions.AddNew();
        var conv = ConvertMod(mod);
        Assert.Equal(Getter ? 0 : 2, conv.EnumerateMajorRecords<IMajorRecord>().Count());
        Assert.Equal(2, conv.EnumerateMajorRecords<IMajorRecordGetter>().Count());
    }

    [Fact]
    public void EnumerateSpecificType_Matched()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        mod.Npcs.AddNew();
        mod.Ammunitions.AddNew();
        var conv = ConvertMod(mod);
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<INpc>().Count());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<Npc>().Count());
        Assert.Single(conv.EnumerateMajorRecords<INpcGetter>());
    }

    [Fact]
    public void EnumerateSpecificType_Unmatched()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        mod.Npcs.AddNew();
        var conv = ConvertMod(mod);
        Assert.Empty(conv.EnumerateMajorRecords<IAmmunition>());
        Assert.Empty(conv.EnumerateMajorRecords<Ammunition>());
        Assert.Empty(conv.EnumerateMajorRecords<IAmmunitionGetter>());
    }

    [Fact]
    public void EnumerateLinkInterface()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        mod.Factions.AddNew();
        var conv = ConvertMod(mod);
        Assert.NotEmpty(conv.EnumerateMajorRecords<IFactionGetter>());
        Assert.NotEmpty(conv.EnumerateMajorRecords<IOwnerGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<IOwner>().Count());
    }

    [Fact]
    public void EnumerateDeepLinkInterface()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var placed = new PlacedNpc(mod);
        mod.Cells.Records.Add(new CellBlock()
        {
            BlockNumber = 0,
            GroupType = GroupTypeEnum.InteriorCellBlock,
            LastModified = 4,
            SubBlocks =
            {
                new CellSubBlock()
                {
                    BlockNumber = 0,
                    GroupType = GroupTypeEnum.InteriorCellSubBlock,
                    LastModified = 4,
                    Cells =
                    {
                        new Cell(mod)
                        {
                            Persistent =
                            {
                                placed
                            }
                        }
                    }
                }
            }
        });
        var conv = ConvertMod(mod);
        Assert.NotEmpty(conv.EnumerateMajorRecords<ICellGetter>());
        Assert.NotEmpty(conv.EnumerateMajorRecords<IPlacedGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<IPlaced>().Count());
    }

    [Fact]
    public void EnumerateDeepLinkInterface2()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var placed = new PlacedNpc(mod);
        mod.Worldspaces.Add(new Worldspace(mod)
        {
            TopCell = 
                new Cell(mod)
                {
                    Temporary = new ExtendedList<IPlaced>()
                    {
                        placed
                    }
                }
        });
        var conv = ConvertMod(mod);
        Assert.NotEmpty(conv.EnumerateMajorRecords<ICellGetter>());
        Assert.NotEmpty(conv.EnumerateMajorRecords<ILocationTargetableGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<ILocationTargetable>().Count());
    }

    [Fact]
    public void EnumerateDeepMajorRecordType()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var placed = new PlacedNpc(mod);
        mod.Cells.Records.Add(new CellBlock()
        {
            BlockNumber = 0,
            GroupType = GroupTypeEnum.InteriorCellBlock,
            LastModified = 4,
            SubBlocks =
            {
                new CellSubBlock()
                {
                    BlockNumber = 0,
                    GroupType = GroupTypeEnum.InteriorCellSubBlock,
                    LastModified = 4,
                    Cells =
                    {
                        new Cell(mod)
                        {
                            Persistent =
                            {
                                placed
                            }
                        }
                    }
                }
            }
        });
        var conv = ConvertMod(mod);
        conv.EnumerateMajorRecords<ISkyrimMajorRecordGetter>().Any(p => p.FormKey == placed.FormKey).Should().BeTrue();
    }

    [Fact]
    public void EnumerateAspectInterface()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var light = mod.Lights.AddNew();
        light.Icons = new Icons()
        {
            LargeIconFilename = "Hello",
            SmallIconFilename = "World"
        };
        var conv = ConvertMod(mod);
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<IHasIcons>().Count());
        Assert.Single(conv.EnumerateMajorRecords<IHasIconsGetter>());
        var item = conv.EnumerateMajorRecords<IHasIconsGetter>().First();
        item.Icons.Should().NotBeNull();
        item.Icons!.LargeIconFilename.Should().Be("Hello");
        item.Icons!.SmallIconFilename.Should().Be("World");
    }

    [Fact]
    public void EnumerateNullableAspectInterfaceWithNpc()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var npc = mod.Npcs.AddNew();
        npc.Name = "Hello";
        var conv = ConvertMod(mod);
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<ITranslatedNamed>().Count());
        Assert.Single(conv.EnumerateMajorRecords<ITranslatedNamedGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<ITranslatedNamedRequired>().Count());
        Assert.Single(conv.EnumerateMajorRecords<ITranslatedNamedRequiredGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<INamed>().Count());
        Assert.Single(conv.EnumerateMajorRecords<INamedGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<INamedRequired>().Count());
        Assert.Single(conv.EnumerateMajorRecords<INamedRequiredGetter>());
        conv.EnumerateMajorRecords<INamedGetter>().First().Name.Should().Be("Hello");
    }

    [Fact]
    public void EnumerateNonNullableAspectInterfaceWithClass()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var classObj = mod.Classes.AddNew();
        classObj.Name = "Hello";
        var conv = ConvertMod(mod);
        Assert.Empty(conv.EnumerateMajorRecords<INamed>());
        Assert.Empty(conv.EnumerateMajorRecords<INamedGetter>());
        Assert.Empty(conv.EnumerateMajorRecords<ITranslatedNamed>());
        Assert.Empty(conv.EnumerateMajorRecords<ITranslatedNamedGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<INamedRequired>().Count());
        Assert.Single(conv.EnumerateMajorRecords<INamedRequiredGetter>());
        Assert.Equal(Getter ? 0 : 1, conv.EnumerateMajorRecords<ITranslatedNamedRequired>().Count());
        Assert.Single(conv.EnumerateMajorRecords<ITranslatedNamedRequiredGetter>());
        conv.EnumerateMajorRecords<INamedRequiredGetter>().First().Name.Should().Be("Hello");
    }

    [Fact]
    public void EnumerateNonMajorAspectInterfaceWithPackage()
    {
        var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimSE);
        var package = mod.Packages.AddNew();

        package.Data[4] = new PackageDataFloat()
        {
            Name = "Hello"
        };
        var conv = ConvertMod(mod);
        Assert.Empty(conv.EnumerateMajorRecords<INamed>());
        Assert.Empty(conv.EnumerateMajorRecords<INamedGetter>());
        Assert.Empty(conv.EnumerateMajorRecords<ITranslatedNamed>());
        Assert.Empty(conv.EnumerateMajorRecords<ITranslatedNamedGetter>());
        Assert.Empty(conv.EnumerateMajorRecords<INamedRequired>());
        Assert.Empty(conv.EnumerateMajorRecords<INamedRequiredGetter>());
        Assert.Empty(conv.EnumerateMajorRecords<ITranslatedNamedRequired>());
        Assert.Empty(conv.EnumerateMajorRecords<ITranslatedNamedRequiredGetter>());
    }
}

public class MajorRecordEnumerationDirectTests : AMajorRecordEnumerationTests
{
    public override bool Getter => false;

    protected override ISkyrimModGetter ConvertMod(SkyrimMod mod)
    {
        return mod;
    }
}

public class MajorRecordEnumerationOverlayTests : AMajorRecordEnumerationTests
{
    public override bool Getter => true;

    protected override ISkyrimModGetter ConvertMod(SkyrimMod mod)
    {
        var stream = new MemoryTributary();
        mod.WriteToBinary(stream);
        stream.Position = 0;
        return SkyrimMod.CreateFromBinaryOverlay(stream, SkyrimRelease.SkyrimSE, mod.ModKey);
    }
}