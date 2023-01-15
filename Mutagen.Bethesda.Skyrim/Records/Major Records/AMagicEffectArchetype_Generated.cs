/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
#region Usings
using Loqui;
using Loqui.Interfaces;
using Loqui.Internal;
using Mutagen.Bethesda.Binary;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Headers;
using Mutagen.Bethesda.Plugins.Binary.Overlay;
using Mutagen.Bethesda.Plugins.Binary.Streams;
using Mutagen.Bethesda.Plugins.Binary.Translations;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Exceptions;
using Mutagen.Bethesda.Plugins.Internals;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Internals;
using Mutagen.Bethesda.Plugins.Records.Mapping;
using Mutagen.Bethesda.Skyrim.Internals;
using Mutagen.Bethesda.Translations.Binary;
using Noggog;
using Noggog.StructuredStrings;
using Noggog.StructuredStrings.CSharp;
using RecordTypeInts = Mutagen.Bethesda.Skyrim.Internals.RecordTypeInts;
using RecordTypes = Mutagen.Bethesda.Skyrim.Internals.RecordTypes;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
#endregion

#nullable enable
namespace Mutagen.Bethesda.Skyrim
{
    #region Class
    /// <summary>
    /// Implemented by: [MagicEffectArchetype, MagicEffectLightArchetype, MagicEffectBoundArchetype, MagicEffectSummonCreatureArchetype, MagicEffectGuideArchetype, MagicEffectSpawnHazardArchetype, MagicEffectCloakArchetype, MagicEffectWerewolfArchetype, MagicEffectVampireArchetype, MagicEffectEnhanceWeaponArchetype, MagicEffectPeakValueModArchetype]
    /// </summary>
    public abstract partial class AMagicEffectArchetype :
        IAMagicEffectArchetype,
        IEquatable<IAMagicEffectArchetypeGetter>,
        ILoquiObjectSetter<AMagicEffectArchetype>
    {
        #region Ctor
        public AMagicEffectArchetype()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #endregion


        #region To String

        public virtual void Print(
            StructuredStringBuilder sb,
            string? name = null)
        {
            AMagicEffectArchetypeMixIn.Print(
                item: this,
                sb: sb,
                name: name);
        }

        #endregion

        #region Equals and Hash
        public override bool Equals(object? obj)
        {
            if (obj is not IAMagicEffectArchetypeGetter rhs) return false;
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)this).CommonInstance()!).Equals(this, rhs, equalsMask: null);
        }

        public bool Equals(IAMagicEffectArchetypeGetter? obj)
        {
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)this).CommonInstance()!).Equals(this, obj, equalsMask: null);
        }

        public override int GetHashCode() => ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)this).CommonInstance()!).GetHashCode(this);

        #endregion

        #region Mask
        public class Mask<TItem> :
            IEquatable<Mask<TItem>>,
            IMask<TItem>
        {
            #region Ctors
            public Mask(TItem ActorValue)
            {
                this.ActorValue = ActorValue;
            }

            #pragma warning disable CS8618
            protected Mask()
            {
            }
            #pragma warning restore CS8618

            #endregion

            #region Members
            public TItem ActorValue;
            #endregion

            #region Equals
            public override bool Equals(object? obj)
            {
                if (!(obj is Mask<TItem> rhs)) return false;
                return Equals(rhs);
            }

            public bool Equals(Mask<TItem>? rhs)
            {
                if (rhs == null) return false;
                if (!object.Equals(this.ActorValue, rhs.ActorValue)) return false;
                return true;
            }
            public override int GetHashCode()
            {
                var hash = new HashCode();
                hash.Add(this.ActorValue);
                return hash.ToHashCode();
            }

            #endregion

            #region All
            public virtual bool All(Func<TItem, bool> eval)
            {
                if (!eval(this.ActorValue)) return false;
                return true;
            }
            #endregion

            #region Any
            public virtual bool Any(Func<TItem, bool> eval)
            {
                if (eval(this.ActorValue)) return true;
                return false;
            }
            #endregion

            #region Translate
            public Mask<R> Translate<R>(Func<TItem, R> eval)
            {
                var ret = new AMagicEffectArchetype.Mask<R>();
                this.Translate_InternalFill(ret, eval);
                return ret;
            }

            protected void Translate_InternalFill<R>(Mask<R> obj, Func<TItem, R> eval)
            {
                obj.ActorValue = eval(this.ActorValue);
            }
            #endregion

            #region To String
            public override string ToString() => this.Print();

            public string Print(AMagicEffectArchetype.Mask<bool>? printMask = null)
            {
                var sb = new StructuredStringBuilder();
                Print(sb, printMask);
                return sb.ToString();
            }

            public void Print(StructuredStringBuilder sb, AMagicEffectArchetype.Mask<bool>? printMask = null)
            {
                sb.AppendLine($"{nameof(AMagicEffectArchetype.Mask<TItem>)} =>");
                using (sb.Brace())
                {
                    if (printMask?.ActorValue ?? true)
                    {
                        sb.AppendItem(ActorValue, "ActorValue");
                    }
                }
            }
            #endregion

        }

        public class ErrorMask :
            IErrorMask,
            IErrorMask<ErrorMask>
        {
            #region Members
            public Exception? Overall { get; set; }
            private List<string>? _warnings;
            public List<string> Warnings
            {
                get
                {
                    if (_warnings == null)
                    {
                        _warnings = new List<string>();
                    }
                    return _warnings;
                }
            }
            public Exception? ActorValue;
            #endregion

            #region IErrorMask
            public virtual object? GetNthMask(int index)
            {
                AMagicEffectArchetype_FieldIndex enu = (AMagicEffectArchetype_FieldIndex)index;
                switch (enu)
                {
                    case AMagicEffectArchetype_FieldIndex.ActorValue:
                        return ActorValue;
                    default:
                        throw new ArgumentException($"Index is out of range: {index}");
                }
            }

            public virtual void SetNthException(int index, Exception ex)
            {
                AMagicEffectArchetype_FieldIndex enu = (AMagicEffectArchetype_FieldIndex)index;
                switch (enu)
                {
                    case AMagicEffectArchetype_FieldIndex.ActorValue:
                        this.ActorValue = ex;
                        break;
                    default:
                        throw new ArgumentException($"Index is out of range: {index}");
                }
            }

            public virtual void SetNthMask(int index, object obj)
            {
                AMagicEffectArchetype_FieldIndex enu = (AMagicEffectArchetype_FieldIndex)index;
                switch (enu)
                {
                    case AMagicEffectArchetype_FieldIndex.ActorValue:
                        this.ActorValue = (Exception?)obj;
                        break;
                    default:
                        throw new ArgumentException($"Index is out of range: {index}");
                }
            }

            public virtual bool IsInError()
            {
                if (Overall != null) return true;
                if (ActorValue != null) return true;
                return false;
            }
            #endregion

            #region To String
            public override string ToString() => this.Print();

            public virtual void Print(StructuredStringBuilder sb, string? name = null)
            {
                sb.AppendLine($"{(name ?? "ErrorMask")} =>");
                using (sb.Brace())
                {
                    if (this.Overall != null)
                    {
                        sb.AppendLine("Overall =>");
                        using (sb.Brace())
                        {
                            sb.AppendLine($"{this.Overall}");
                        }
                    }
                    PrintFillInternal(sb);
                }
            }
            protected virtual void PrintFillInternal(StructuredStringBuilder sb)
            {
                {
                    sb.AppendItem(ActorValue, "ActorValue");
                }
            }
            #endregion

            #region Combine
            public ErrorMask Combine(ErrorMask? rhs)
            {
                if (rhs == null) return this;
                var ret = new ErrorMask();
                ret.ActorValue = this.ActorValue.Combine(rhs.ActorValue);
                return ret;
            }
            public static ErrorMask? Combine(ErrorMask? lhs, ErrorMask? rhs)
            {
                if (lhs != null && rhs != null) return lhs.Combine(rhs);
                return lhs ?? rhs;
            }
            #endregion

            #region Factory
            public static ErrorMask Factory(ErrorMaskBuilder errorMask)
            {
                return new ErrorMask();
            }
            #endregion

        }
        public class TranslationMask : ITranslationMask
        {
            #region Members
            private TranslationCrystal? _crystal;
            public readonly bool DefaultOn;
            public bool OnOverall;
            public bool ActorValue;
            #endregion

            #region Ctors
            public TranslationMask(
                bool defaultOn,
                bool onOverall = true)
            {
                this.DefaultOn = defaultOn;
                this.OnOverall = onOverall;
                this.ActorValue = defaultOn;
            }

            #endregion

            public TranslationCrystal GetCrystal()
            {
                if (_crystal != null) return _crystal;
                var ret = new List<(bool On, TranslationCrystal? SubCrystal)>();
                GetCrystal(ret);
                _crystal = new TranslationCrystal(ret.ToArray());
                return _crystal;
            }

            protected virtual void GetCrystal(List<(bool On, TranslationCrystal? SubCrystal)> ret)
            {
                ret.Add((ActorValue, null));
            }

            public static implicit operator TranslationMask(bool defaultOn)
            {
                return new TranslationMask(defaultOn: defaultOn, onOverall: defaultOn);
            }

        }
        #endregion

        #region Mutagen
        public virtual IEnumerable<IFormLinkGetter> EnumerateFormLinks() => AMagicEffectArchetypeCommon.Instance.EnumerateFormLinks(this);
        public virtual void RemapLinks(IReadOnlyDictionary<FormKey, FormKey> mapping) => AMagicEffectArchetypeSetterCommon.Instance.RemapLinks(this, mapping);
        #endregion

        #region Binary Translation
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected virtual object BinaryWriteTranslator => AMagicEffectArchetypeBinaryWriteTranslation.Instance;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        object IBinaryItem.BinaryWriteTranslator => this.BinaryWriteTranslator;
        void IBinaryItem.WriteToBinary(
            MutagenWriter writer,
            TypedWriteParams translationParams = default)
        {
            ((AMagicEffectArchetypeBinaryWriteTranslation)this.BinaryWriteTranslator).Write(
                item: this,
                writer: writer,
                translationParams: translationParams);
        }
        #endregion

        void IPrintable.Print(StructuredStringBuilder sb, string? name) => this.Print(sb, name);

        void IClearable.Clear()
        {
            ((AMagicEffectArchetypeSetterCommon)((IAMagicEffectArchetypeGetter)this).CommonSetterInstance()!).Clear(this);
        }

        internal static AMagicEffectArchetype GetNew()
        {
            throw new ArgumentException("New called on an abstract class.");
        }

    }
    #endregion

    #region Interface
    /// <summary>
    /// Implemented by: [MagicEffectArchetype, MagicEffectLightArchetype, MagicEffectBoundArchetype, MagicEffectSummonCreatureArchetype, MagicEffectGuideArchetype, MagicEffectSpawnHazardArchetype, MagicEffectCloakArchetype, MagicEffectWerewolfArchetype, MagicEffectVampireArchetype, MagicEffectEnhanceWeaponArchetype, MagicEffectPeakValueModArchetype]
    /// </summary>
    public partial interface IAMagicEffectArchetype :
        IAMagicEffectArchetypeGetter,
        IFormLinkContainer,
        ILoquiObjectSetter<IAMagicEffectArchetype>
    {
        new ActorValue ActorValue { get; set; }
    }

    /// <summary>
    /// Implemented by: [MagicEffectArchetype, MagicEffectLightArchetype, MagicEffectBoundArchetype, MagicEffectSummonCreatureArchetype, MagicEffectGuideArchetype, MagicEffectSpawnHazardArchetype, MagicEffectCloakArchetype, MagicEffectWerewolfArchetype, MagicEffectVampireArchetype, MagicEffectEnhanceWeaponArchetype, MagicEffectPeakValueModArchetype]
    /// </summary>
    public partial interface IAMagicEffectArchetypeGetter :
        ILoquiObject,
        IBinaryItem,
        IFormLinkContainerGetter,
        ILoquiObject<IAMagicEffectArchetypeGetter>
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        object CommonInstance();
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        object? CommonSetterInstance();
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        object CommonSetterTranslationInstance();
        static ILoquiRegistration StaticRegistration => AMagicEffectArchetype_Registration.Instance;
        ActorValue ActorValue { get; }

    }

    #endregion

    #region Common MixIn
    public static partial class AMagicEffectArchetypeMixIn
    {
        public static void Clear(this IAMagicEffectArchetype item)
        {
            ((AMagicEffectArchetypeSetterCommon)((IAMagicEffectArchetypeGetter)item).CommonSetterInstance()!).Clear(item: item);
        }

        public static AMagicEffectArchetype.Mask<bool> GetEqualsMask(
            this IAMagicEffectArchetypeGetter item,
            IAMagicEffectArchetypeGetter rhs,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).GetEqualsMask(
                item: item,
                rhs: rhs,
                include: include);
        }

        public static string Print(
            this IAMagicEffectArchetypeGetter item,
            string? name = null,
            AMagicEffectArchetype.Mask<bool>? printMask = null)
        {
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).Print(
                item: item,
                name: name,
                printMask: printMask);
        }

        public static void Print(
            this IAMagicEffectArchetypeGetter item,
            StructuredStringBuilder sb,
            string? name = null,
            AMagicEffectArchetype.Mask<bool>? printMask = null)
        {
            ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).Print(
                item: item,
                sb: sb,
                name: name,
                printMask: printMask);
        }

        public static bool Equals(
            this IAMagicEffectArchetypeGetter item,
            IAMagicEffectArchetypeGetter rhs,
            AMagicEffectArchetype.TranslationMask? equalsMask = null)
        {
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).Equals(
                lhs: item,
                rhs: rhs,
                equalsMask: equalsMask?.GetCrystal());
        }

        public static void DeepCopyIn(
            this IAMagicEffectArchetype lhs,
            IAMagicEffectArchetypeGetter rhs)
        {
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: default,
                copyMask: default,
                deepCopy: false);
        }

        public static void DeepCopyIn(
            this IAMagicEffectArchetype lhs,
            IAMagicEffectArchetypeGetter rhs,
            AMagicEffectArchetype.TranslationMask? copyMask = null)
        {
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: default,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: false);
        }

        public static void DeepCopyIn(
            this IAMagicEffectArchetype lhs,
            IAMagicEffectArchetypeGetter rhs,
            out AMagicEffectArchetype.ErrorMask errorMask,
            AMagicEffectArchetype.TranslationMask? copyMask = null)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: errorMaskBuilder,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: false);
            errorMask = AMagicEffectArchetype.ErrorMask.Factory(errorMaskBuilder);
        }

        public static void DeepCopyIn(
            this IAMagicEffectArchetype lhs,
            IAMagicEffectArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask)
        {
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: false);
        }

        public static AMagicEffectArchetype DeepCopy(
            this IAMagicEffectArchetypeGetter item,
            AMagicEffectArchetype.TranslationMask? copyMask = null)
        {
            return ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask);
        }

        public static AMagicEffectArchetype DeepCopy(
            this IAMagicEffectArchetypeGetter item,
            out AMagicEffectArchetype.ErrorMask errorMask,
            AMagicEffectArchetype.TranslationMask? copyMask = null)
        {
            return ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask,
                errorMask: out errorMask);
        }

        public static AMagicEffectArchetype DeepCopy(
            this IAMagicEffectArchetypeGetter item,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask = null)
        {
            return ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask,
                errorMask: errorMask);
        }

        #region Binary Translation
        public static void CopyInFromBinary(
            this IAMagicEffectArchetype item,
            MutagenFrame frame,
            TypedParseParams translationParams = default)
        {
            ((AMagicEffectArchetypeSetterCommon)((IAMagicEffectArchetypeGetter)item).CommonSetterInstance()!).CopyInFromBinary(
                item: item,
                frame: frame,
                translationParams: translationParams);
        }

        #endregion

    }
    #endregion

}

namespace Mutagen.Bethesda.Skyrim
{
    #region Field Index
    internal enum AMagicEffectArchetype_FieldIndex
    {
        ActorValue = 0,
    }
    #endregion

    #region Registration
    internal partial class AMagicEffectArchetype_Registration : ILoquiRegistration
    {
        public static readonly AMagicEffectArchetype_Registration Instance = new AMagicEffectArchetype_Registration();

        public static ProtocolKey ProtocolKey => ProtocolDefinition_Skyrim.ProtocolKey;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_Skyrim.ProtocolKey,
            msgID: 490,
            version: 0);

        public const string GUID = "f883954a-c2f0-49bc-bbe6-3186688b20ba";

        public const ushort AdditionalFieldCount = 1;

        public const ushort FieldCount = 1;

        public static readonly Type MaskType = typeof(AMagicEffectArchetype.Mask<>);

        public static readonly Type ErrorMaskType = typeof(AMagicEffectArchetype.ErrorMask);

        public static readonly Type ClassType = typeof(AMagicEffectArchetype);

        public static readonly Type GetterType = typeof(IAMagicEffectArchetypeGetter);

        public static readonly Type? InternalGetterType = null;

        public static readonly Type SetterType = typeof(IAMagicEffectArchetype);

        public static readonly Type? InternalSetterType = null;

        public const string FullName = "Mutagen.Bethesda.Skyrim.AMagicEffectArchetype";

        public const string Name = "AMagicEffectArchetype";

        public const string Namespace = "Mutagen.Bethesda.Skyrim";

        public const byte GenericCount = 0;

        public static readonly Type? GenericRegistrationType = null;

        public static readonly Type BinaryWriteTranslation = typeof(AMagicEffectArchetypeBinaryWriteTranslation);
        #region Interface
        ProtocolKey ILoquiRegistration.ProtocolKey => ProtocolKey;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        ushort ILoquiRegistration.FieldCount => FieldCount;
        ushort ILoquiRegistration.AdditionalFieldCount => AdditionalFieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.SetterType => SetterType;
        Type? ILoquiRegistration.InternalSetterType => InternalSetterType;
        Type ILoquiRegistration.GetterType => GetterType;
        Type? ILoquiRegistration.InternalGetterType => InternalGetterType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        string ILoquiRegistration.Namespace => Namespace;
        byte ILoquiRegistration.GenericCount => GenericCount;
        Type? ILoquiRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? ILoquiRegistration.GetNameIndex(StringCaseAgnostic name) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsEnumerable(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsLoqui(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsSingleton(ushort index) => throw new NotImplementedException();
        string ILoquiRegistration.GetNthName(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.IsNthDerivative(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.IsProtected(ushort index) => throw new NotImplementedException();
        Type ILoquiRegistration.GetNthType(ushort index) => throw new NotImplementedException();
        #endregion

    }
    #endregion

    #region Common
    internal partial class AMagicEffectArchetypeSetterCommon
    {
        public static readonly AMagicEffectArchetypeSetterCommon Instance = new AMagicEffectArchetypeSetterCommon();

        partial void ClearPartial();
        
        public virtual void Clear(IAMagicEffectArchetype item)
        {
            ClearPartial();
            item.ActorValue = AMagicEffectArchetype.ActorValueDefault;
        }
        
        #region Mutagen
        public void RemapLinks(IAMagicEffectArchetype obj, IReadOnlyDictionary<FormKey, FormKey> mapping)
        {
        }
        
        #endregion
        
        #region Binary Translation
        public virtual void CopyInFromBinary(
            IAMagicEffectArchetype item,
            MutagenFrame frame,
            TypedParseParams translationParams)
        {
            PluginUtilityTranslation.SubrecordParse(
                record: item,
                frame: frame,
                translationParams: translationParams,
                fillStructs: AMagicEffectArchetypeBinaryCreateTranslation.FillBinaryStructs);
        }
        
        #endregion
        
    }
    internal partial class AMagicEffectArchetypeCommon
    {
        public static readonly AMagicEffectArchetypeCommon Instance = new AMagicEffectArchetypeCommon();

        public AMagicEffectArchetype.Mask<bool> GetEqualsMask(
            IAMagicEffectArchetypeGetter item,
            IAMagicEffectArchetypeGetter rhs,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            var ret = new AMagicEffectArchetype.Mask<bool>(false);
            ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).FillEqualsMask(
                item: item,
                rhs: rhs,
                ret: ret,
                include: include);
            return ret;
        }
        
        public void FillEqualsMask(
            IAMagicEffectArchetypeGetter item,
            IAMagicEffectArchetypeGetter rhs,
            AMagicEffectArchetype.Mask<bool> ret,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            ret.ActorValue = item.ActorValue == rhs.ActorValue;
        }
        
        public string Print(
            IAMagicEffectArchetypeGetter item,
            string? name = null,
            AMagicEffectArchetype.Mask<bool>? printMask = null)
        {
            var sb = new StructuredStringBuilder();
            Print(
                item: item,
                sb: sb,
                name: name,
                printMask: printMask);
            return sb.ToString();
        }
        
        public void Print(
            IAMagicEffectArchetypeGetter item,
            StructuredStringBuilder sb,
            string? name = null,
            AMagicEffectArchetype.Mask<bool>? printMask = null)
        {
            if (name == null)
            {
                sb.AppendLine($"AMagicEffectArchetype =>");
            }
            else
            {
                sb.AppendLine($"{name} (AMagicEffectArchetype) =>");
            }
            using (sb.Brace())
            {
                ToStringFields(
                    item: item,
                    sb: sb,
                    printMask: printMask);
            }
        }
        
        protected static void ToStringFields(
            IAMagicEffectArchetypeGetter item,
            StructuredStringBuilder sb,
            AMagicEffectArchetype.Mask<bool>? printMask = null)
        {
            if (printMask?.ActorValue ?? true)
            {
                sb.AppendItem(item.ActorValue, "ActorValue");
            }
        }
        
        #region Equals and Hash
        public virtual bool Equals(
            IAMagicEffectArchetypeGetter? lhs,
            IAMagicEffectArchetypeGetter? rhs,
            TranslationCrystal? equalsMask)
        {
            if (!EqualsMaskHelper.RefEquality(lhs, rhs, out var isEqual)) return isEqual;
            if ((equalsMask?.GetShouldTranslate((int)AMagicEffectArchetype_FieldIndex.ActorValue) ?? true))
            {
                if (lhs.ActorValue != rhs.ActorValue) return false;
            }
            return true;
        }
        
        public virtual int GetHashCode(IAMagicEffectArchetypeGetter item)
        {
            var hash = new HashCode();
            hash.Add(item.ActorValue);
            return hash.ToHashCode();
        }
        
        #endregion
        
        
        public virtual object GetNew()
        {
            return AMagicEffectArchetype.GetNew();
        }
        
        #region Mutagen
        public IEnumerable<IFormLinkGetter> EnumerateFormLinks(IAMagicEffectArchetypeGetter obj)
        {
            yield break;
        }
        
        #endregion
        
    }
    internal partial class AMagicEffectArchetypeSetterTranslationCommon
    {
        public static readonly AMagicEffectArchetypeSetterTranslationCommon Instance = new AMagicEffectArchetypeSetterTranslationCommon();

        #region DeepCopyIn
        public virtual void DeepCopyIn(
            IAMagicEffectArchetype item,
            IAMagicEffectArchetypeGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask,
            bool deepCopy)
        {
            if ((copyMask?.GetShouldTranslate((int)AMagicEffectArchetype_FieldIndex.ActorValue) ?? true))
            {
                item.ActorValue = rhs.ActorValue;
            }
        }
        
        #endregion
        
        public AMagicEffectArchetype DeepCopy(
            IAMagicEffectArchetypeGetter item,
            AMagicEffectArchetype.TranslationMask? copyMask = null)
        {
            AMagicEffectArchetype ret = (AMagicEffectArchetype)((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).GetNew();
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: ret,
                rhs: item,
                errorMask: null,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: true);
            return ret;
        }
        
        public AMagicEffectArchetype DeepCopy(
            IAMagicEffectArchetypeGetter item,
            out AMagicEffectArchetype.ErrorMask errorMask,
            AMagicEffectArchetype.TranslationMask? copyMask = null)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            AMagicEffectArchetype ret = (AMagicEffectArchetype)((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).GetNew();
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                ret,
                item,
                errorMask: errorMaskBuilder,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: true);
            errorMask = AMagicEffectArchetype.ErrorMask.Factory(errorMaskBuilder);
            return ret;
        }
        
        public AMagicEffectArchetype DeepCopy(
            IAMagicEffectArchetypeGetter item,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask = null)
        {
            AMagicEffectArchetype ret = (AMagicEffectArchetype)((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)item).CommonInstance()!).GetNew();
            ((AMagicEffectArchetypeSetterTranslationCommon)((IAMagicEffectArchetypeGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: ret,
                rhs: item,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: true);
            return ret;
        }
        
    }
    #endregion

}

namespace Mutagen.Bethesda.Skyrim
{
    public partial class AMagicEffectArchetype
    {
        #region Common Routing
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => AMagicEffectArchetype_Registration.Instance;
        public static ILoquiRegistration StaticRegistration => AMagicEffectArchetype_Registration.Instance;
        [DebuggerStepThrough]
        protected virtual object CommonInstance() => AMagicEffectArchetypeCommon.Instance;
        [DebuggerStepThrough]
        protected virtual object CommonSetterInstance()
        {
            return AMagicEffectArchetypeSetterCommon.Instance;
        }
        [DebuggerStepThrough]
        protected virtual object CommonSetterTranslationInstance() => AMagicEffectArchetypeSetterTranslationCommon.Instance;
        [DebuggerStepThrough]
        object IAMagicEffectArchetypeGetter.CommonInstance() => this.CommonInstance();
        [DebuggerStepThrough]
        object IAMagicEffectArchetypeGetter.CommonSetterInstance() => this.CommonSetterInstance();
        [DebuggerStepThrough]
        object IAMagicEffectArchetypeGetter.CommonSetterTranslationInstance() => this.CommonSetterTranslationInstance();

        #endregion

    }
}

#region Modules
#region Binary Translation
namespace Mutagen.Bethesda.Skyrim
{
    public partial class AMagicEffectArchetypeBinaryWriteTranslation : IBinaryWriteTranslator
    {
        public static readonly AMagicEffectArchetypeBinaryWriteTranslation Instance = new();

        public static void WriteEmbedded(
            IAMagicEffectArchetypeGetter item,
            MutagenWriter writer)
        {
        }

        public virtual void Write(
            MutagenWriter writer,
            IAMagicEffectArchetypeGetter item,
            TypedWriteParams translationParams)
        {
            WriteEmbedded(
                item: item,
                writer: writer);
        }

        public virtual void Write(
            MutagenWriter writer,
            object item,
            TypedWriteParams translationParams = default)
        {
            Write(
                item: (IAMagicEffectArchetypeGetter)item,
                writer: writer,
                translationParams: translationParams);
        }

    }

    internal partial class AMagicEffectArchetypeBinaryCreateTranslation
    {
        public static readonly AMagicEffectArchetypeBinaryCreateTranslation Instance = new AMagicEffectArchetypeBinaryCreateTranslation();

        public static void FillBinaryStructs(
            IAMagicEffectArchetype item,
            MutagenFrame frame)
        {
        }

    }

}
namespace Mutagen.Bethesda.Skyrim
{
    #region Binary Write Mixins
    public static class AMagicEffectArchetypeBinaryTranslationMixIn
    {
        public static void WriteToBinary(
            this IAMagicEffectArchetypeGetter item,
            MutagenWriter writer,
            TypedWriteParams translationParams = default)
        {
            ((AMagicEffectArchetypeBinaryWriteTranslation)item.BinaryWriteTranslator).Write(
                item: item,
                writer: writer,
                translationParams: translationParams);
        }

    }
    #endregion


}
namespace Mutagen.Bethesda.Skyrim
{
    internal abstract partial class AMagicEffectArchetypeBinaryOverlay :
        PluginBinaryOverlay,
        IAMagicEffectArchetypeGetter
    {
        #region Common Routing
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => AMagicEffectArchetype_Registration.Instance;
        public static ILoquiRegistration StaticRegistration => AMagicEffectArchetype_Registration.Instance;
        [DebuggerStepThrough]
        protected virtual object CommonInstance() => AMagicEffectArchetypeCommon.Instance;
        [DebuggerStepThrough]
        protected virtual object CommonSetterTranslationInstance() => AMagicEffectArchetypeSetterTranslationCommon.Instance;
        [DebuggerStepThrough]
        object IAMagicEffectArchetypeGetter.CommonInstance() => this.CommonInstance();
        [DebuggerStepThrough]
        object? IAMagicEffectArchetypeGetter.CommonSetterInstance() => null;
        [DebuggerStepThrough]
        object IAMagicEffectArchetypeGetter.CommonSetterTranslationInstance() => this.CommonSetterTranslationInstance();

        #endregion

        void IPrintable.Print(StructuredStringBuilder sb, string? name) => this.Print(sb, name);

        public virtual IEnumerable<IFormLinkGetter> EnumerateFormLinks() => AMagicEffectArchetypeCommon.Instance.EnumerateFormLinks(this);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected virtual object BinaryWriteTranslator => AMagicEffectArchetypeBinaryWriteTranslation.Instance;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        object IBinaryItem.BinaryWriteTranslator => this.BinaryWriteTranslator;
        void IBinaryItem.WriteToBinary(
            MutagenWriter writer,
            TypedWriteParams translationParams = default)
        {
            ((AMagicEffectArchetypeBinaryWriteTranslation)this.BinaryWriteTranslator).Write(
                item: this,
                writer: writer,
                translationParams: translationParams);
        }

        partial void CustomFactoryEnd(
            OverlayStream stream,
            int finalPos,
            int offset);

        partial void CustomCtor();
        protected AMagicEffectArchetypeBinaryOverlay(
            MemoryPair memoryPair,
            BinaryOverlayFactoryPackage package)
            : base(
                memoryPair: memoryPair,
                package: package)
        {
            this.CustomCtor();
        }


        #region To String

        public virtual void Print(
            StructuredStringBuilder sb,
            string? name = null)
        {
            AMagicEffectArchetypeMixIn.Print(
                item: this,
                sb: sb,
                name: name);
        }

        #endregion

        #region Equals and Hash
        public override bool Equals(object? obj)
        {
            if (obj is not IAMagicEffectArchetypeGetter rhs) return false;
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)this).CommonInstance()!).Equals(this, rhs, equalsMask: null);
        }

        public bool Equals(IAMagicEffectArchetypeGetter? obj)
        {
            return ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)this).CommonInstance()!).Equals(this, obj, equalsMask: null);
        }

        public override int GetHashCode() => ((AMagicEffectArchetypeCommon)((IAMagicEffectArchetypeGetter)this).CommonInstance()!).GetHashCode(this);

        #endregion

    }

}
#endregion

#endregion
