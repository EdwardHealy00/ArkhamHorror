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
            
            TileID.Urban1 => "Urban Street",
            TileID.Urban2 => "Urban Street",
            TileID.Urban3 => "Urban Street",
            TileID.Forest1 => "Forest Street",
            TileID.Forest2 => "Forest Street",
            TileID.Bridge1 => "Bridge Street",
            TileID.Bridge2 => "Bridge Street",
            _ => "Unknown Location"
        };
    }
    
    public static NeighborhoodID GetNeighborhoodForTile(TileID tile)
    {
        switch(tile)
        {
            case TileID.ArkhamAdvertiser:
            case TileID.TrainStation: 
            case TileID.CuriositieShoppe: 
                return NeighborhoodID.Northside;
            case TileID.IndependenceSquare:
            case TileID.ArkhamAsylum: 
            case TileID.LaBellaLuna: 
                return NeighborhoodID.Downtown;
            case TileID.UnvisitedIsle:
            case TileID.TickTockClub: 
            case TileID.RiverDocks: 
                return NeighborhoodID.MerchantDistrict;
            case TileID.BlackCave:
            case TileID.GeneralStore: 
            case TileID.Graveyard: 
                return NeighborhoodID.Rivertown;
            case TileID.HibbsRoadhouse:
            case TileID.VelmasDiner: 
            case TileID.PoliceStation: 
                return NeighborhoodID.Easttown;
            case TileID.Urban1:
            case TileID.Urban2:
            case TileID.Urban3:
            case TileID.Forest1:
            case TileID.Forest2:
            case TileID.Bridge1:
            case TileID.Bridge2:
                return NeighborhoodID.Streets;
            default:
                throw new ArgumentOutOfRangeException(nameof(tile), tile, null);
        };
    }
    
    public static TileID StringToEnum(String name)
    {
        return name switch
        {
            "ArkhamAdvertiser" => TileID.ArkhamAdvertiser,
            "TrainStation" => TileID.TrainStation,
            "CuriositieShoppe" => TileID.CuriositieShoppe,
            
            "UnvisitedIsle" => TileID.UnvisitedIsle,
            "TickTockClub" => TileID.TickTockClub,
            "RiverDocks" => TileID.RiverDocks,
            
            "VelmasDiner" => TileID.VelmasDiner,
            "HibbsRoadhouse" => TileID.HibbsRoadhouse,
            "PoliceStation" => TileID.PoliceStation,
            
            "BlackCave" => TileID.BlackCave,
            "Graveyard" => TileID.Graveyard,
            "GeneralStore" => TileID.GeneralStore,
            
            "IndependenceSquare" => TileID.IndependenceSquare,
            "LaBellaLuna" => TileID.LaBellaLuna,
            "ArkhamAsylum" => TileID.ArkhamAsylum,
            
            "Urban1" => TileID.Urban1,
            "Urban2" => TileID.Urban2,
            "Urban3" => TileID.Urban3,
            
            "Forest1" => TileID.Forest1,
            "Forest2" => TileID.Forest2,
            
            "Bridge1" => TileID.Bridge1,
            "Bridge2" => TileID.Bridge2,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
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
    None,
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
            ActionID.None => "None",
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
