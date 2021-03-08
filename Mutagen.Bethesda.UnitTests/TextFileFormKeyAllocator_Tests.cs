using Mutagen.Bethesda.Core.Persistance;
using Mutagen.Bethesda.Oblivion;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Mutagen.Bethesda.UnitTests
{
    public class TextFileFormKeyAllocator_Tests : IPersistentFormKeyAllocator_Tests<TextFileFormKeyAllocator>
    {
        protected override TextFileFormKeyAllocator CreateFormKeyAllocator(IMod mod) => new(mod, tempFile.Value.File);

        [Fact]
        public void StaticExport()
        {
            TextFileFormKeyAllocator.WriteToFile(
                tempFile.Value.File.Path,
                new KeyValuePair<string, FormKey>[]
                {
                    new KeyValuePair<string, FormKey>(Utility.Edid1, Utility.Form1),
                    new KeyValuePair<string, FormKey>(Utility.Edid2, Utility.Form2),
                });

            var lines = File.ReadAllLines(tempFile.Value.File.Path);
            Assert.Equal(
                new string[]
                {
                    Utility.Edid1,
                    Utility.Form1.ToString(),
                    Utility.Edid2,
                    Utility.Form2.ToString(),
                },
                lines);
        }

        [Fact]
        public void TypicalImport()
        {
            using var file = tempFile.Value;
            File.WriteAllLines(
                file.File.Path,
                new string[]
                {
                    Utility.Edid1,
                    Utility.Form1.ToString(),
                    Utility.Edid2,
                    Utility.Form2.ToString(),
                });
            var mod = new OblivionMod(Utility.PluginModKey);
            using var allocator = new TextFileFormKeyAllocator(mod, file.File);
            var formID = allocator.GetNextFormKey(Utility.Edid1);
            Assert.Equal(Utility.PluginModKey, formID.ModKey);
            Assert.Equal(formID, Utility.Form1);
            formID = allocator.GetNextFormKey(Utility.Edid2);
            Assert.Equal(formID, Utility.Form2);
        }

        [Fact]
        public void TypicalReimport()
        {
            using var file = tempFile.Value;
            TextFileFormKeyAllocator.WriteToFile(
                file.File.Path,
                new KeyValuePair<string, FormKey>[]
                {
                    new KeyValuePair<string, FormKey>(Utility.Edid1, Utility.Form1),
                    new KeyValuePair<string, FormKey>(Utility.Edid2, Utility.Form2),
                });
            var mod = new OblivionMod(Utility.PluginModKey);
            using var allocator = new TextFileFormKeyAllocator(mod, file.File);
            var formID = allocator.GetNextFormKey(Utility.Edid1);
            Assert.Equal(Utility.PluginModKey, formID.ModKey);
            Assert.Equal(formID, Utility.Form1);
            formID = allocator.GetNextFormKey(Utility.Edid2);
            Assert.Equal(formID, Utility.Form2);
        }
    }
}
