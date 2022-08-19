using System;
using System.Collections.Generic;
using UnityEngine;

public enum NeighborhoodID
{
    Northside,
    MerchantDistrict,
    Easttown,
    Rivertown,
    Downtown,
    Streets
}
public enum TileID
{
    ArkhamAdvertiser,
    TrainStation,
    CuriositieShoppe,
    UnvisitedIsle,
    TickTockClub,
    RiverDocks,
    VelmasDiner,
    HibbsRoadhouse,
    PoliceStation,
    BlackCave,
    Graveyard,
    GeneralStore,
    IndependenceSquare,
    LaBellaLuna,
    ArkhamAsylum,
    Urban1,
    Urban2,
    Urban3,
    Forest1,
    Forest2,
    Bridge1,
    Bridge2
}

public enum StreetsLocations
{
    Urban,
    Forest,
    Bridge
}

public class MapUtils
{
    public static string EnumToString(Enum name)
    {
        return name switch
        {
            NeighborhoodID.Northside => "Northside",
            NeighborhoodID.MerchantDistrict => "Merchant District",
            NeighborhoodID.Easttown => "Easttown",
            NeighborhoodID.Rivertown => "Rivertown",
            NeighborhoodID.Downtown => "Downtown",
            
            TileID.ArkhamAdvertiser => "Arkham Advertiser",
            TileID.TrainStation => "Train Station",
            TileID.CuriositieShoppe => "Curiositie Shoppe",
            
            TileID.UnvisitedIsle => "Unvisited Isle",
            TileID.TickTockClub => "Tick-Tock Club",
            TileID.RiverDocks => "River Docks",
            
            TileID.VelmasDiner => "Velma's Diner",
            TileID.HibbsRoadhouse => "Hibb's Roadhouse",
            TileID.PoliceStation => "Police Station",
            
            TileID.BlackCave => "Black Cave",
            TileID.Graveyard => "Graveyard",
            TileID.GeneralStore => "General Store",
            
            TileID.IndependenceSquare => "Independence Square",
            TileID.LaBellaLuna => "La Bella Luna",
            TileID.ArkhamAsylum => "Arkham Asylum",
            _ => "Unknown Location"
        };
    }
    
    public static GameObject EnumToGameObject(Enum name)
    {
        return name switch
        {
            NeighborhoodID.Northside => GameObject.Find("Northside"),
            NeighborhoodID.MerchantDistrict => GameObject.Find("MerchantDistrict"),
            NeighborhoodID.Easttown => GameObject.Find("Easttown"),
            NeighborhoodID.Rivertown => GameObject.Find("Rivertown"),
            NeighborhoodID.Downtown => GameObject.Find("Downtown"),
            
            TileID.ArkhamAdvertiser => GameObject.Find("ArkhamAdvertiser"),
            TileID.TrainStation => GameObject.Find("TrainStation"),
            TileID.CuriositieShoppe => GameObject.Find("CuriositieShoppe"),
            
            TileID.UnvisitedIsle => GameObject.Find("UnvisitedIsle"),
            TileID.TickTockClub => GameObject.Find("TickTockClub"),
            TileID.RiverDocks => GameObject.Find("RiverDocks"),
            
            TileID.VelmasDiner => GameObject.Find("VelmasDiner"),
            TileID.HibbsRoadhouse => GameObject.Find("HibbsRoadhouse"),
            TileID.PoliceStation => GameObject.Find("PoliceStation"),
            
            TileID.BlackCave => GameObject.Find("BlackCave"),
            TileID.Graveyard => GameObject.Find("Graveyard"),
            TileID.GeneralStore => GameObject.Find("GeneralStore"),
            
            TileID.IndependenceSquare => GameObject.Find("IndependenceSquare"),
            TileID.LaBellaLuna => GameObject.Find("LaBellaLuna"),
            TileID.ArkhamAsylum => GameObject.Find("ArkhamAsylum"),
            _ => null
        };
    }
}

public enum ActionID
{
    Move,
    GatherResources,
    Focus,
    Ward,
    Attack,
    Evade,
    Research,
    Trade
}

public class ActionUtils
{
    public static string EnumToString(Enum name)
    {
        return name switch
        {
            ActionID.Move => "Move",
            ActionID.GatherResources => "Gather Resources",
            ActionID.Focus => "Focus",
            ActionID.Ward => "Ward",
            ActionID.Attack => "Attack",
            ActionID.Evade => "Evade",
            ActionID.Research => "Research",
            ActionID.Trade => "Trade",
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };
    }
}
