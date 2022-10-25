﻿using AutoFixture;
using AutoFixture.Xunit2;
using Mutagen.Bethesda.Testing.AutoData;

namespace Mutagen.Bethesda.UnitTests.AutoData;

public class MutagenModAutoDataAttribute : AutoDataAttribute
{
    public MutagenModAutoDataAttribute(
        GameRelease Release = GameRelease.SkyrimSE,
        bool UseMockFileSystem = true)
        : base(() =>
        {
            var ret = new Fixture();
            ret.Customize(new MutagenDefaultCustomization(
                useMockFileSystem: UseMockFileSystem,
                configureMembers: false,
                release: Release));
            ret.Customize(new MutagenConcreteModsCustomization(release: Release));
            return ret;
        })
    {
    }
}