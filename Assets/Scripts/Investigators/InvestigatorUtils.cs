using System;
using System.Collections.Generic;
using UnityEngine;

namespace Investigators
{
    public enum SkillID
        {
            Lore,
            Influence,
            Observation,
            Strength,
            Will,
            None
        }
    
    public enum InvestigatorID
        {
            Dilettante,
            Waitress,
            Entertainer,
            Mobster,
            Urchin,
            Astronomer,
            Reporter,
            Mechanic,
            Haunted,
            Magician,
            Secretary,
            RookieCop
        }

    public static class InvestigatorUtils
    {
        public static string InvestigatorIDtoName(InvestigatorID id)
        {
            return id switch
            {
                InvestigatorID.Dilettante => "Jenny Barnes",
                InvestigatorID.Waitress => "Agnes Baker",
                InvestigatorID.Entertainer => "Marie Lambeau",
                InvestigatorID.Mobster => "Michael McGlen",
                InvestigatorID.Urchin => "Wendy Adams",
                InvestigatorID.Astronomer => "Norman Withers",
                InvestigatorID.Mechanic => "Daniela Reves",
                InvestigatorID.Reporter => "Rex Murphy",
                InvestigatorID.Haunted => "Calvin Wright",
                InvestigatorID.Magician => "Dexter Drake",
                InvestigatorID.Secretary => "Minh Thi Phan",
                InvestigatorID.RookieCop => "Tommy Muldoon",
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
        
        public static string InvestigatorIDtoClass(InvestigatorID id)
        {
            return id switch
            {
                InvestigatorID.Dilettante => "The Dilettante",
                InvestigatorID.Waitress => "The Waitress",
                InvestigatorID.Entertainer => "The Entertainer",
                InvestigatorID.Mobster => "The Mobster",
                InvestigatorID.Urchin => "The Urchin",
                InvestigatorID.Astronomer => "The Astronomer",
                InvestigatorID.Mechanic => "The Mechanic",
                InvestigatorID.Reporter => "The Reporter",
                InvestigatorID.Haunted => "The Haunted",
                InvestigatorID.Magician => "The Magician",
                InvestigatorID.Secretary => "The Secretary",
                InvestigatorID.RookieCop => "The Rookie Cop",
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
        
        public static SkillID SkillNameToID(string name)
        {
            return name switch
            {
                "Lore" => SkillID.Lore,
                "Influence" => SkillID.Influence,
                "Observation" => SkillID.Observation, 
                "Strength" => SkillID.Strength,
                "Will" => SkillID.Will,
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
            };
        }
        
        public static Sprite GetSprite(InvestigatorID id)
        {
            var sprites4 = Resources.LoadAll<Sprite>("Sprites/Characters/4chars");
            var sprites8 = Resources.LoadAll<Sprite>("Sprites/Characters/4chars");
            return id switch
            {
                InvestigatorID.Magician => sprites4[0],
                InvestigatorID.Astronomer => sprites4[1],
                InvestigatorID.Mobster => sprites4[2],
                InvestigatorID.Entertainer => sprites4[3],
                InvestigatorID.Dilettante => sprites8[0],
                InvestigatorID.Mechanic => sprites8[1],
                InvestigatorID.Reporter => sprites8[2],
                InvestigatorID.RookieCop => sprites8[3],
                InvestigatorID.Haunted => sprites8[4],
                InvestigatorID.Waitress => sprites8[5],
                InvestigatorID.Urchin => sprites8[6],
                InvestigatorID.Secretary => sprites8[7],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    
}
