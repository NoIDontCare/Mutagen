﻿using Mutagen.Bethesda.Internals;
using Noggog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mutagen.Bethesda.Binary
{
    public class MutagenBinaryReadStream : BinaryReadStream, IMutagenReadStream
    {
        public long OffsetReference { get; }
        public GameConstants MetaData { get; }
        public MasterReferenceReader? MasterReferences { get; set; }

        public MutagenBinaryReadStream(
            string path, 
            GameConstants metaData, 
            MasterReferenceReader? masterReferences = null,
            int bufferSize = 4096, 
            long offsetReference = 0)
            : base(path, bufferSize)
        {
            this.MetaData = metaData;
            this.MasterReferences = masterReferences;
            this.OffsetReference = offsetReference;
        }

        public MutagenBinaryReadStream(
            Stream stream, 
            GameConstants metaData,
            MasterReferenceReader? masterReferences = null,
            int bufferSize = 4096, 
            bool dispose = true, 
            long offsetReference = 0)
            : base(stream, bufferSize, dispose)
        {
            this.MetaData = metaData;
            this.MasterReferences = masterReferences;
            this.OffsetReference = offsetReference;
        }

        public IMutagenReadStream ReadAndReframe(int length)
        {
            var offset = this.OffsetReference + this.Position;
            return new MutagenMemoryReadStream(this.ReadBytes(length), this.MetaData, this.MasterReferences, offsetReference: offset);
        }
    }
}
