﻿using Noggog;
using System;
using System.IO;

namespace Mutagen.Bethesda.Binary
{
    public class DateTimeBinaryTranslation : PrimitiveBinaryTranslation<DateTime>
    {
        public readonly static DateTimeBinaryTranslation Instance = new DateTimeBinaryTranslation();
        public override ContentLength? ExpectedLength => new ContentLength(4);

        protected override DateTime ParseValue(MutagenFrame reader)
        {
            reader.Reader.ReadBytes(4);
            return DateTime.Now;
        }

        protected override void WriteValue(MutagenWriter writer, DateTime item)
        {
            writer.Write(new byte[4]);
        }
    }
}