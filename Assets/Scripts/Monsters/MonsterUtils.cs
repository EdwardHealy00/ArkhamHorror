using System;
using UnityEngine;

namespace Monsters
{
    
    public enum MonsterID
    {
        AbyssalServant,
        EyelessWatcher,
        HighPriest,
        HoodedStalker1,
        HoodedStalker2,
        OccultRitualist1,
        OccultRitualist2,
        RobedFigure1,
        RobedFigure2,
        RobedFigure3,
        SwiftByakhee,
        KeeningHound,
        RavenousPredator,
        TindalosAlpha
    }
    
    public enum MonsterState
    {
        Ready,
        Exhausted,
        Engaged
    }
    
    public static class MonsterUtils
    {
        public static string MonsterIDToName(MonsterID id)
        {
            return id switch
            {
                MonsterID.AbyssalServant => "Abyssal Servant",
                MonsterID.EyelessWatcher => throw new ArgumentOutOfRangeException(nameof(id), id, null),
                MonsterID.HighPriest => "High Priest",
                MonsterID.HoodedStalker1 => "Hooded Stalker",
                MonsterID.HoodedStalker2 => "Hooded Stalker",
                MonsterID.OccultRitualist1 => "Occult Ritualist",
                MonsterID.OccultRitualist2 => "Occult Ritualist",
                MonsterID.RobedFigure1 => "Robed Figure",
                MonsterID.RobedFigure2 => "Robed Figure",
                MonsterID.RobedFigure3 => "Robed Figure",
                MonsterID.SwiftByakhee => "Swift Byakhee",
                MonsterID.KeeningHound => "Keening Hound",
                MonsterID.RavenousPredator => "Ravenous Predator",
                MonsterID.TindalosAlpha => "Tindalos Alpha",
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
        
        public static Sprite GetReadySprite(MonsterID id)
        {
            var sprites = Resources.LoadAll<Sprite>("Sprites/MonsterCards/AoA_Monsters_Ready.png");
            return id switch
            {
                MonsterID.AbyssalServant => sprites[5],
                MonsterID.EyelessWatcher => throw new ArgumentOutOfRangeException(nameof(id), id, null),
                MonsterID.HighPriest => sprites[7],
                MonsterID.HoodedStalker1 => sprites[8],
                MonsterID.HoodedStalker2 => sprites[8],
                MonsterID.OccultRitualist1 => sprites[9],
                MonsterID.OccultRitualist2 => sprites[9],
                MonsterID.RobedFigure1 => sprites[1],
                MonsterID.RobedFigure2 => sprites[1],
                MonsterID.RobedFigure3 => sprites[1],
                MonsterID.SwiftByakhee => sprites[6],
                MonsterID.KeeningHound => sprites[2],
                MonsterID.RavenousPredator => sprites[3],
                MonsterID.TindalosAlpha => sprites[4],
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
        
        public static Sprite GetExhaustedSprite(MonsterID id)
        {
            var sprites = Resources.LoadAll<Sprite>("Sprites/MonsterCards/AoA_Monsters_Exausted.png");
            return id switch
            {
                MonsterID.AbyssalServant => sprites[5],
                MonsterID.EyelessWatcher => throw new ArgumentOutOfRangeException(nameof(id), id, null),
                MonsterID.HighPriest => sprites[7],
                MonsterID.HoodedStalker1 => sprites[9],
                MonsterID.HoodedStalker2 => sprites[9],
                MonsterID.OccultRitualist1 => sprites[8],
                MonsterID.OccultRitualist2 => sprites[8],
                MonsterID.RobedFigure1 => sprites[0],
                MonsterID.RobedFigure2 => sprites[0],
                MonsterID.RobedFigure3 => sprites[0],
                MonsterID.SwiftByakhee => sprites[6],
                MonsterID.KeeningHound => sprites[1],
                MonsterID.RavenousPredator => sprites[2],
                MonsterID.TindalosAlpha => sprites[3],
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}