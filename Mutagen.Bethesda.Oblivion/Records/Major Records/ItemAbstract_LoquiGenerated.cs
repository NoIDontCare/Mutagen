/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loqui;
using Noggog;
using Noggog.Notifying;
using Mutagen.Bethesda.Oblivion.Internals;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Internals;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Noggog.Xml;
using Loqui.Xml;
using System.Diagnostics;
using Mutagen.Bethesda.Binary;

namespace Mutagen.Bethesda.Oblivion
{
    #region Class
    public abstract partial class ItemAbstract : MajorRecord, IItemAbstract, ILoquiObject<ItemAbstract>, ILoquiObjectSetter, IEquatable<ItemAbstract>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => ItemAbstract_Registration.Instance;
        public new static ItemAbstract_Registration Registration => ItemAbstract_Registration.Instance;

        #region Ctor
        public ItemAbstract()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #endregion


        #region Loqui Getter Interface

        protected override object GetNthObject(ushort index) => ItemAbstractCommon.GetNthObject(index, this);

        protected override bool GetNthObjectHasBeenSet(ushort index) => ItemAbstractCommon.GetNthObjectHasBeenSet(index, this);

        protected override void UnsetNthObject(ushort index, NotifyingUnsetParameters cmds) => ItemAbstractCommon.UnsetNthObject(index, this, cmds);

        #endregion

        #region Loqui Interface
        protected override void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            ItemAbstractCommon.SetNthObjectHasBeenSet(index, on, this);
        }

        #endregion

        IMask<bool> IEqualsMask<ItemAbstract>.GetEqualsMask(ItemAbstract rhs) => ItemAbstractCommon.GetEqualsMask(this, rhs);
        IMask<bool> IEqualsMask<IItemAbstractGetter>.GetEqualsMask(IItemAbstractGetter rhs) => ItemAbstractCommon.GetEqualsMask(this, rhs);
        #region To String
        public override string ToString()
        {
            return ItemAbstractCommon.ToString(this, printMask: null);
        }

        public string ToString(
            string name = null,
            ItemAbstract_Mask<bool> printMask = null)
        {
            return ItemAbstractCommon.ToString(this, name: name, printMask: printMask);
        }

        public override void ToString(
            FileGeneration fg,
            string name = null)
        {
            ItemAbstractCommon.ToString(this, fg, name: name, printMask: null);
        }

        #endregion

        IMask<bool> ILoquiObjectGetter.GetHasBeenSetMask() => this.GetHasBeenSetMask();
        public new ItemAbstract_Mask<bool> GetHasBeenSetMask()
        {
            return ItemAbstractCommon.GetHasBeenSetMask(this);
        }
        #region Equals and Hash
        public override bool Equals(object obj)
        {
            if (!(obj is ItemAbstract rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(ItemAbstract rhs)
        {
            if (rhs == null) return false;
            if (!base.Equals(rhs)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int ret = 0;
            ret = ret.CombineHashCode(base.GetHashCode());
            return ret;
        }

        #endregion


        #region XML Translation
        #region XML Copy In
        public override void CopyIn_XML(
            XElement root,
            NotifyingFireParameters cmds = null)
        {
            LoquiXmlTranslation<ItemAbstract, ItemAbstract_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: false,
                mask: out var errorMask,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(
            XElement root,
            out ItemAbstract_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            LoquiXmlTranslation<ItemAbstract, ItemAbstract_ErrorMask>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                doMasks: true,
                mask: out errorMask,
                cmds: cmds);
        }

        public void CopyIn_XML(
            string path,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(path).Root;
            this.CopyIn_XML(
                root: root,
                cmds: cmds);
        }

        public void CopyIn_XML(
            string path,
            out ItemAbstract_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(path).Root;
            this.CopyIn_XML(
                root: root,
                errorMask: out errorMask,
                cmds: cmds);
        }

        public void CopyIn_XML(
            Stream stream,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(stream).Root;
            this.CopyIn_XML(
                root: root,
                cmds: cmds);
        }

        public void CopyIn_XML(
            Stream stream,
            out ItemAbstract_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(stream).Root;
            this.CopyIn_XML(
                root: root,
                errorMask: out errorMask,
                cmds: cmds);
        }

        public override void CopyIn_XML(
            XElement root,
            out MajorRecord_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            this.CopyIn_XML(
                root: root,
                errorMask: out ItemAbstract_ErrorMask errMask,
                cmds: cmds);
            errorMask = errMask;
        }

        #endregion

        #region XML Write
        public virtual void Write_XML(
            XElement node,
            out ItemAbstract_ErrorMask errorMask,
            bool doMasks = true,
            string name = null)
        {
            errorMask = this.Write_XML_Internal(
                node: node,
                name: name,
                doMasks: doMasks) as ItemAbstract_ErrorMask;
        }

        public virtual void Write_XML(
            string path,
            out ItemAbstract_ErrorMask errorMask,
            bool doMasks = true,
            string name = null)
        {
            XElement topNode = new XElement("topnode");
            Write_XML(
                node: topNode,
                name: name,
                errorMask: out errorMask,
                doMasks: doMasks);
            topNode.Elements().First().Save(path);
        }

        public virtual void Write_XML(
            Stream stream,
            out ItemAbstract_ErrorMask errorMask,
            bool doMasks = true,
            string name = null)
        {
            XElement topNode = new XElement("topnode");
            Write_XML(
                node: topNode,
                name: name,
                errorMask: out errorMask,
                doMasks: doMasks);
            topNode.Elements().First().Save(stream);
        }

        protected override object Write_XML_Internal(
            XElement node,
            bool doMasks,
            string name = null)
        {
            ItemAbstractCommon.Write_XML(
                item: this,
                doMasks: doMasks,
                node: node,
                name: name,
                errorMask: out var errorMask);
            return errorMask;
        }
        #endregion

        protected static void Fill_XML_Internal(
            ItemAbstract item,
            XElement root,
            string name,
            Func<ItemAbstract_ErrorMask> errorMask)
        {
            switch (name)
            {
                default:
                    MajorRecord.Fill_XML_Internal(
                        item: item,
                        root: root,
                        name: name,
                        errorMask: errorMask);
                    break;
            }
        }

        #endregion

        #region Mutagen
        public new static readonly RecordType GRUP_RECORD_TYPE = ItemAbstract_Registration.TRIGGERING_RECORD_TYPE;
        #endregion

        #region Binary Translation
        #region Binary Write
        public virtual void Write_Binary(
            MutagenWriter writer,
            out ItemAbstract_ErrorMask errorMask,
            bool doMasks = true)
        {
            errorMask = this.Write_Binary_Internal(
                writer: writer,
                recordTypeConverter: null,
                doMasks: doMasks) as ItemAbstract_ErrorMask;
        }

        public virtual void Write_Binary(
            string path,
            out ItemAbstract_ErrorMask errorMask,
            bool doMasks = true)
        {
            using (var writer = new MutagenWriter(path))
            {
                Write_Binary(
                    writer: writer,
                    errorMask: out errorMask,
                    doMasks: doMasks);
            }
        }

        public virtual void Write_Binary(
            Stream stream,
            out ItemAbstract_ErrorMask errorMask,
            bool doMasks = true)
        {
            using (var writer = new MutagenWriter(stream))
            {
                Write_Binary(
                    writer: writer,
                    errorMask: out errorMask,
                    doMasks: doMasks);
            }
        }

        protected override object Write_Binary_Internal(
            MutagenWriter writer,
            RecordTypeConverter recordTypeConverter,
            bool doMasks)
        {
            ItemAbstractCommon.Write_Binary(
                item: this,
                doMasks: doMasks,
                writer: writer,
                recordTypeConverter: recordTypeConverter,
                errorMask: out var errorMask);
            return errorMask;
        }
        #endregion

        #endregion

        public ItemAbstract Copy(
            ItemAbstract_CopyMask copyMask = null,
            IItemAbstractGetter def = null)
        {
            return ItemAbstract.Copy(
                this,
                copyMask: copyMask,
                def: def);
        }

        public static ItemAbstract Copy(
            IItemAbstract item,
            ItemAbstract_CopyMask copyMask = null,
            IItemAbstractGetter def = null)
        {
            ItemAbstract ret = (ItemAbstract)System.Activator.CreateInstance(item.GetType());
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public static ItemAbstract Copy_ToLoqui(
            IItemAbstractGetter item,
            ItemAbstract_CopyMask copyMask = null,
            IItemAbstractGetter def = null)
        {
            ItemAbstract ret = (ItemAbstract)System.Activator.CreateInstance(item.GetType());
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public void CopyFieldsFrom(
            IItemAbstractGetter rhs,
            ItemAbstract_CopyMask copyMask,
            IItemAbstractGetter def = null,
            NotifyingFireParameters cmds = null)
        {
            this.CopyFieldsFrom(
                rhs: rhs,
                def: def,
                doMasks: false,
                errorMask: out var errMask,
                copyMask: copyMask,
                cmds: cmds);
        }

        public void CopyFieldsFrom(
            IItemAbstractGetter rhs,
            out ItemAbstract_ErrorMask errorMask,
            ItemAbstract_CopyMask copyMask = null,
            IItemAbstractGetter def = null,
            NotifyingFireParameters cmds = null,
            bool doMasks = true)
        {
            ItemAbstract_ErrorMask retErrorMask = null;
            Func<IErrorMask> maskGetter = !doMasks ? default(Func<IErrorMask>) : () =>
            {
                if (retErrorMask == null)
                {
                    retErrorMask = new ItemAbstract_ErrorMask();
                }
                return retErrorMask;
            };
            ItemAbstractCommon.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                doMasks: true,
                errorMask: maskGetter,
                copyMask: copyMask,
                cmds: cmds);
            errorMask = retErrorMask;
        }

        protected override void SetNthObject(ushort index, object obj, NotifyingFireParameters cmds = null)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthObject(index, obj, cmds);
                    break;
            }
        }

        public override void Clear(NotifyingUnsetParameters cmds = null)
        {
            CallClearPartial_Internal(cmds);
            ItemAbstractCommon.Clear(this, cmds);
        }


        protected new static void CopyInInternal_ItemAbstract(ItemAbstract obj, KeyValuePair<ushort, object> pair)
        {
            if (!EnumExt.TryParse(pair.Key, out ItemAbstract_FieldIndex enu))
            {
                CopyInInternal_MajorRecord(obj, pair);
            }
            switch (enu)
            {
                default:
                    throw new ArgumentException($"Unknown enum type: {enu}");
            }
        }
        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, ItemAbstract obj)
        {
            ILoquiObjectExt.CopyFieldsIn(obj, fields, def: null, skipProtected: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public partial interface IItemAbstract : IItemAbstractGetter, IMajorRecord, ILoquiClass<IItemAbstract, IItemAbstractGetter>, ILoquiClass<ItemAbstract, IItemAbstractGetter>
    {
    }

    public partial interface IItemAbstractGetter : IMajorRecordGetter
    {

    }

    #endregion

}

namespace Mutagen.Bethesda.Oblivion.Internals
{
    #region Field Index
    public enum ItemAbstract_FieldIndex
    {
        MajorRecordFlags = 0,
        FormID = 1,
        Version = 2,
        EditorID = 3,
        RecordType = 4,
    }
    #endregion

    #region Registration
    public class ItemAbstract_Registration : ILoquiRegistration
    {
        public static readonly ItemAbstract_Registration Instance = new ItemAbstract_Registration();

        public static ProtocolKey ProtocolKey => ProtocolDefinition_Oblivion.ProtocolKey;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_Oblivion.ProtocolKey,
            msgID: 73,
            version: 0);

        public const string GUID = "028aca3c-7f36-4a5f-80cd-42aedbbc606b";

        public const ushort FieldCount = 0;

        public static readonly Type MaskType = typeof(ItemAbstract_Mask<>);

        public static readonly Type ErrorMaskType = typeof(ItemAbstract_ErrorMask);

        public static readonly Type ClassType = typeof(ItemAbstract);

        public static readonly Type GetterType = typeof(IItemAbstractGetter);

        public static readonly Type SetterType = typeof(IItemAbstract);

        public static readonly Type CommonType = typeof(ItemAbstractCommon);

        public const string FullName = "Mutagen.Bethesda.Oblivion.ItemAbstract";

        public const string Name = "ItemAbstract";

        public const string Namespace = "Mutagen.Bethesda.Oblivion";

        public const byte GenericCount = 0;

        public static readonly Type GenericRegistrationType = null;

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                default:
                    return null;
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.GetNthIsEnumerable(index);
            }
        }

        public static bool GetNthIsLoqui(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.GetNthIsLoqui(index);
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.GetNthIsSingleton(index);
            }
        }

        public static string GetNthName(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.GetNthName(index);
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.IsNthDerivative(index);
            }
        }

        public static bool IsProtected(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.IsProtected(index);
            }
        }

        public static Type GetNthType(ushort index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecord_Registration.GetNthType(index);
            }
        }

        public static readonly RecordType AMMO_HEADER = new RecordType("AMMO");
        public static readonly RecordType TRIGGERING_RECORD_TYPE = AMMO_HEADER;
        public const int NumStructFields = 0;
        public const int NumTypedFields = 0;
        #region Interface
        ProtocolKey ILoquiRegistration.ProtocolKey => ProtocolKey;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        int ILoquiRegistration.FieldCount => FieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.SetterType => SetterType;
        Type ILoquiRegistration.GetterType => GetterType;
        Type ILoquiRegistration.CommonType => CommonType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        string ILoquiRegistration.Namespace => Namespace;
        byte ILoquiRegistration.GenericCount => GenericCount;
        Type ILoquiRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? ILoquiRegistration.GetNameIndex(StringCaseAgnostic name) => GetNameIndex(name);
        bool ILoquiRegistration.GetNthIsEnumerable(ushort index) => GetNthIsEnumerable(index);
        bool ILoquiRegistration.GetNthIsLoqui(ushort index) => GetNthIsLoqui(index);
        bool ILoquiRegistration.GetNthIsSingleton(ushort index) => GetNthIsSingleton(index);
        string ILoquiRegistration.GetNthName(ushort index) => GetNthName(index);
        bool ILoquiRegistration.IsNthDerivative(ushort index) => IsNthDerivative(index);
        bool ILoquiRegistration.IsProtected(ushort index) => IsProtected(index);
        Type ILoquiRegistration.GetNthType(ushort index) => GetNthType(index);
        #endregion

    }
    #endregion

    #region Extensions
    public static partial class ItemAbstractCommon
    {
        #region Copy Fields From
        public static void CopyFieldsFrom(
            IItemAbstract item,
            IItemAbstractGetter rhs,
            IItemAbstractGetter def,
            bool doMasks,
            Func<IErrorMask> errorMask,
            ItemAbstract_CopyMask copyMask,
            NotifyingFireParameters cmds = null)
        {
            MajorRecordCommon.CopyFieldsFrom(
                item,
                rhs,
                def,
                doMasks,
                errorMask,
                copyMask,
                cmds);
        }

        #endregion

        public static void SetNthObjectHasBeenSet(
            ushort index,
            bool on,
            IItemAbstract obj,
            NotifyingFireParameters cmds = null)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    MajorRecordCommon.SetNthObjectHasBeenSet(index, on, obj);
                    break;
            }
        }

        public static void UnsetNthObject(
            ushort index,
            IItemAbstract obj,
            NotifyingUnsetParameters cmds = null)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    MajorRecordCommon.UnsetNthObject(index, obj);
                    break;
            }
        }

        public static bool GetNthObjectHasBeenSet(
            ushort index,
            IItemAbstract obj)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecordCommon.GetNthObjectHasBeenSet(index, obj);
            }
        }

        public static object GetNthObject(
            ushort index,
            IItemAbstractGetter obj)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return MajorRecordCommon.GetNthObject(index, obj);
            }
        }

        public static void Clear(
            IItemAbstract item,
            NotifyingUnsetParameters cmds = null)
        {
        }

        public static ItemAbstract_Mask<bool> GetEqualsMask(
            this IItemAbstractGetter item,
            IItemAbstractGetter rhs)
        {
            var ret = new ItemAbstract_Mask<bool>();
            FillEqualsMask(item, rhs, ret);
            return ret;
        }

        public static void FillEqualsMask(
            IItemAbstractGetter item,
            IItemAbstractGetter rhs,
            ItemAbstract_Mask<bool> ret)
        {
            if (rhs == null) return;
            MajorRecordCommon.FillEqualsMask(item, rhs, ret);
        }

        public static string ToString(
            this IItemAbstractGetter item,
            string name = null,
            ItemAbstract_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            item.ToString(fg, name, printMask);
            return fg.ToString();
        }

        public static void ToString(
            this IItemAbstractGetter item,
            FileGeneration fg,
            string name = null,
            ItemAbstract_Mask<bool> printMask = null)
        {
            if (name == null)
            {
                fg.AppendLine($"{nameof(ItemAbstract)} =>");
            }
            else
            {
                fg.AppendLine($"{name} ({nameof(ItemAbstract)}) =>");
            }
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }

        public static bool HasBeenSet(
            this IItemAbstractGetter item,
            ItemAbstract_Mask<bool?> checkMask)
        {
            return true;
        }

        public static ItemAbstract_Mask<bool> GetHasBeenSetMask(IItemAbstractGetter item)
        {
            var ret = new ItemAbstract_Mask<bool>();
            return ret;
        }

        public static ItemAbstract_FieldIndex? ConvertFieldIndex(MajorRecord_FieldIndex? index)
        {
            if (!index.HasValue) return null;
            return ConvertFieldIndex(index: index.Value);
        }

        public static ItemAbstract_FieldIndex ConvertFieldIndex(MajorRecord_FieldIndex index)
        {
            switch (index)
            {
                case MajorRecord_FieldIndex.MajorRecordFlags:
                    return (ItemAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.FormID:
                    return (ItemAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.Version:
                    return (ItemAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.EditorID:
                    return (ItemAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.RecordType:
                    return (ItemAbstract_FieldIndex)((int)index);
                default:
                    throw new ArgumentException($"Index is out of range: {index.ToStringFast_Enum_Only()}");
            }
        }

        #region XML Translation
        #region XML Write
        public static void Write_XML(
            XElement node,
            IItemAbstractGetter item,
            bool doMasks,
            out ItemAbstract_ErrorMask errorMask,
            string name = null)
        {
            ItemAbstract_ErrorMask errMaskRet = null;
            Write_XML_Internal(
                node: node,
                name: name,
                item: item,
                errorMask: doMasks ? () => errMaskRet ?? (errMaskRet = new ItemAbstract_ErrorMask()) : default(Func<ItemAbstract_ErrorMask>));
            errorMask = errMaskRet;
        }

        private static void Write_XML_Internal(
            XElement node,
            IItemAbstractGetter item,
            Func<ItemAbstract_ErrorMask> errorMask,
            string name = null)
        {
            try
            {
                var elem = new XElement(name ?? "Mutagen.Bethesda.Oblivion.ItemAbstract");
                node.Add(elem);
                if (name != null)
                {
                    elem.SetAttributeValue("type", "Mutagen.Bethesda.Oblivion.ItemAbstract");
                }
            }
            catch (Exception ex)
            when (errorMask != null)
            {
                errorMask().Overall = ex;
            }
        }
        #endregion

        #endregion

        #region Binary Translation
        #region Binary Write
        public static void Write_Binary(
            MutagenWriter writer,
            ItemAbstract item,
            RecordTypeConverter recordTypeConverter,
            bool doMasks,
            out ItemAbstract_ErrorMask errorMask)
        {
            ItemAbstract_ErrorMask errMaskRet = null;
            Write_Binary_Internal(
                writer: writer,
                item: item,
                recordTypeConverter: recordTypeConverter,
                errorMask: doMasks ? () => errMaskRet ?? (errMaskRet = new ItemAbstract_ErrorMask()) : default(Func<ItemAbstract_ErrorMask>));
            errorMask = errMaskRet;
        }

        private static void Write_Binary_Internal(
            MutagenWriter writer,
            ItemAbstract item,
            RecordTypeConverter recordTypeConverter,
            Func<ItemAbstract_ErrorMask> errorMask)
        {
            try
            {
                MajorRecordCommon.Write_Binary_Embedded(
                    item: item,
                    writer: writer,
                    errorMask: errorMask);
                MajorRecordCommon.Write_Binary_RecordTypes(
                    item: item,
                    writer: writer,
                    recordTypeConverter: recordTypeConverter,
                    errorMask: errorMask);
            }
            catch (Exception ex)
            when (errorMask != null)
            {
                errorMask().Overall = ex;
            }
        }
        #endregion

        #endregion

    }
    #endregion

    #region Modules

    #region Mask
    public class ItemAbstract_Mask<T> : MajorRecord_Mask<T>, IMask<T>, IEquatable<ItemAbstract_Mask<T>>
    {
        #region Ctors
        public ItemAbstract_Mask()
        {
        }

        public ItemAbstract_Mask(T initialValue)
        {
        }
        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (!(obj is ItemAbstract_Mask<T> rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(ItemAbstract_Mask<T> rhs)
        {
            if (rhs == null) return false;
            if (!base.Equals(rhs)) return false;
            return true;
        }
        public override int GetHashCode()
        {
            int ret = 0;
            ret = ret.CombineHashCode(base.GetHashCode());
            return ret;
        }

        #endregion

        #region All Equal
        public override bool AllEqual(Func<T, bool> eval)
        {
            if (!base.AllEqual(eval)) return false;
            return true;
        }
        #endregion

        #region Translate
        public new ItemAbstract_Mask<R> Translate<R>(Func<T, R> eval)
        {
            var ret = new ItemAbstract_Mask<R>();
            this.Translate_InternalFill(ret, eval);
            return ret;
        }

        protected void Translate_InternalFill<R>(ItemAbstract_Mask<R> obj, Func<T, R> eval)
        {
            base.Translate_InternalFill(obj, eval);
        }
        #endregion

        #region Clear Enumerables
        public override void ClearEnumerables()
        {
            base.ClearEnumerables();
        }
        #endregion

        #region To String
        public override string ToString()
        {
            return ToString(printMask: null);
        }

        public string ToString(ItemAbstract_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            ToString(fg, printMask);
            return fg.ToString();
        }

        public void ToString(FileGeneration fg, ItemAbstract_Mask<bool> printMask = null)
        {
            fg.AppendLine($"{nameof(ItemAbstract_Mask<T>)} =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }
        #endregion

    }

    public class ItemAbstract_ErrorMask : MajorRecord_ErrorMask, IErrorMask<ItemAbstract_ErrorMask>
    {
        #region IErrorMask
        public override object GetNthMask(int index)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return base.GetNthMask(index);
            }
        }

        public override void SetNthException(int index, Exception ex)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthException(index, ex);
                    break;
            }
        }

        public override void SetNthMask(int index, object obj)
        {
            ItemAbstract_FieldIndex enu = (ItemAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthMask(index, obj);
                    break;
            }
        }

        public override bool IsInError()
        {
            if (Overall != null) return true;
            return false;
        }
        #endregion

        #region To String
        public override string ToString()
        {
            var fg = new FileGeneration();
            ToString(fg);
            return fg.ToString();
        }

        public override void ToString(FileGeneration fg)
        {
            fg.AppendLine("ItemAbstract_ErrorMask =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
                if (this.Overall != null)
                {
                    fg.AppendLine("Overall =>");
                    fg.AppendLine("[");
                    using (new DepthWrapper(fg))
                    {
                        fg.AppendLine($"{this.Overall}");
                    }
                    fg.AppendLine("]");
                }
                ToString_FillInternal(fg);
            }
            fg.AppendLine("]");
        }
        protected override void ToString_FillInternal(FileGeneration fg)
        {
            base.ToString_FillInternal(fg);
        }
        #endregion

        #region Combine
        public ItemAbstract_ErrorMask Combine(ItemAbstract_ErrorMask rhs)
        {
            var ret = new ItemAbstract_ErrorMask();
            return ret;
        }
        public static ItemAbstract_ErrorMask Combine(ItemAbstract_ErrorMask lhs, ItemAbstract_ErrorMask rhs)
        {
            if (lhs != null && rhs != null) return lhs.Combine(rhs);
            return lhs ?? rhs;
        }
        #endregion

    }
    public class ItemAbstract_CopyMask : MajorRecord_CopyMask
    {
    }
    #endregion




    #endregion

}
