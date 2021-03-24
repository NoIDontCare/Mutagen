﻿using System.IO;
using JetBrains.Annotations;
using Mutagen.Bethesda.Core.Pex.Interfaces;

namespace Mutagen.Bethesda.Core.Pex.DataTypes
{
    [PublicAPI]
    public class UserFlag : IUserFlag
    {
        public string? Name { get; set; }
        public byte FlagIndex { get; set; }
        public uint FlagMask => (uint) 1 << FlagIndex;

        private readonly PexFile _pexFile;
        
        public UserFlag(PexFile pexFile) { _pexFile = pexFile; }
        public UserFlag(BinaryReader br, PexFile pexFile) : this(pexFile) { Read(br); }
        
        public void Read(BinaryReader br)
        {
            Name = _pexFile.GetStringFromIndex(br.ReadUInt16());
            FlagIndex = br.ReadByte();
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(_pexFile.GetIndexFromString(Name));
            bw.Write(FlagIndex);
        }
    }
}
