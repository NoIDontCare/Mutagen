/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins.Records.Internals;
using Loqui;

namespace Mutagen.Bethesda.Oblivion.Internals
{
    public class LinkInterfaceMapping : ILinkInterfaceMapping
    {
        public IReadOnlyDictionary<Type, InterfaceMappingResult> InterfaceToObjectTypes { get; }

        public GameCategory GameCategory => GameCategory.Oblivion;

        public LinkInterfaceMapping()
        {
            var dict = new Dictionary<Type, InterfaceMappingResult>();
            dict[typeof(IItem)] = new InterfaceMappingResult(true, new ILoquiRegistration[]
            {
                AlchemicalApparatus_Registration.Instance,
                Ammunition_Registration.Instance,
                Armor_Registration.Instance,
                Book_Registration.Instance,
                Clothing_Registration.Instance,
                Ingredient_Registration.Instance,
                Key_Registration.Instance,
                LeveledItem_Registration.Instance,
                Light_Registration.Instance,
                Miscellaneous_Registration.Instance,
                Potion_Registration.Instance,
                SigilStone_Registration.Instance,
                SoulGem_Registration.Instance,
                Weapon_Registration.Instance,
            });
            dict[typeof(IItemGetter)] = dict[typeof(IItem)] with { Setter = false };
            dict[typeof(INpcSpawn)] = new InterfaceMappingResult(true, new ILoquiRegistration[]
            {
                Creature_Registration.Instance,
                LeveledCreature_Registration.Instance,
                Npc_Registration.Instance,
            });
            dict[typeof(INpcSpawnGetter)] = dict[typeof(INpcSpawn)] with { Setter = false };
            dict[typeof(INpcRecord)] = new InterfaceMappingResult(true, new ILoquiRegistration[]
            {
                Creature_Registration.Instance,
                Npc_Registration.Instance,
            });
            dict[typeof(INpcRecordGetter)] = dict[typeof(INpcRecord)] with { Setter = false };
            dict[typeof(IOwner)] = new InterfaceMappingResult(true, new ILoquiRegistration[]
            {
                Faction_Registration.Instance,
                Npc_Registration.Instance,
            });
            dict[typeof(IOwnerGetter)] = dict[typeof(IOwner)] with { Setter = false };
            dict[typeof(IPlaced)] = new InterfaceMappingResult(true, new ILoquiRegistration[]
            {
                Landscape_Registration.Instance,
                PlacedCreature_Registration.Instance,
                PlacedNpc_Registration.Instance,
                PlacedObject_Registration.Instance,
            });
            dict[typeof(IPlacedGetter)] = dict[typeof(IPlaced)] with { Setter = false };
            dict[typeof(ISpellRecord)] = new InterfaceMappingResult(true, new ILoquiRegistration[]
            {
                LeveledSpell_Registration.Instance,
                Spell_Registration.Instance,
            });
            dict[typeof(ISpellRecordGetter)] = dict[typeof(ISpellRecord)] with { Setter = false };
            InterfaceToObjectTypes = dict;
        }
    }
}
