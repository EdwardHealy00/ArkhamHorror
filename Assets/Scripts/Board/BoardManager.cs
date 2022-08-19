using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BoardManager : MonoBehaviour
{
    public Board Board;
    public int numberOfPlayers = 2;
    public List<Investigator> investigators;
    public GameObject startingTile;
    [SerializeField] private GameObject investigatorPrefab;
    [SerializeField] private GameObject actionPanel;

    public static Dictionary<ActionID, Action> ActionTriggers = new Dictionary<ActionID, Action>()
    {
        {ActionID.Move, TriggerMove},
        {ActionID.GatherResources, TriggerGatherResources},
        {ActionID.Focus, TriggerFocus},
        {ActionID.Ward, TriggerWard},
        {ActionID.Attack, TriggerAttack},
        {ActionID.Evade, TriggerEvade},
        {ActionID.Research, TriggerResearch},
        {ActionID.Trade, TriggerTrade}
    };

    // Start is called before the first frame update
    void Start()
    {
        CreateApproachOfAzatothMap();
        CreateInvestigators();
        StartGame();
    }

    private void StartGame()
    {
        DoActionPhase();
    }

    private void DoActionPhase()
    {
        actionPanel.SetActive(true);
        //DoMonsterPhase();
    }

    private void DoMonsterPhase()
    {
        DoEncounterPhase();
    }

    private void DoEncounterPhase()
    {
        DoMythosPhase();
    }

    private void DoMythosPhase()
    {
        DoActionPhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateApproachOfAzatothMap()
    {
        var neighborhoods = new Dictionary<NeighborhoodID, Neighborhood>()
        {
            {NeighborhoodID.Northside, CreateNorthsideNeighborhood() },
            {NeighborhoodID.MerchantDistrict, CreateMerchantsDistrictNeighborhood() },
            {NeighborhoodID.Easttown, CreateEasttownNeighborhood() },
            {NeighborhoodID.Rivertown, CreateRivertownNeighborhood() },
            {NeighborhoodID.Downtown, CreateDowntownNeighborhood() }
        };

        Board = new Board(neighborhoods);
        Board.Neighborhoods[NeighborhoodID.Streets] = CreateStreets();
        startingTile = MapUtils.EnumToGameObject(TileID.TrainStation);
        System.Console.WriteLine("BOARD GENERATED");
    }
    
    private void CreateInvestigators()
    {
        investigators = new List<Investigator>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            CreateInvestigator();
        }
    }

    private void CreateInvestigator()
    {
        Instantiate(investigatorPrefab, startingTile.transform);
    }

    #region Create Neighborhoods

    private Neighborhood CreateNorthsideNeighborhood()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            { TileID.ArkhamAdvertiser, new Tile(new Location(NeighborhoodID.Northside, TileID.ArkhamAdvertiser)) },
            { TileID.TrainStation, new Tile(new Location(NeighborhoodID.Northside, TileID.TrainStation)) },
            { TileID.CuriositieShoppe, new Tile(new Location(NeighborhoodID.Northside, TileID.CuriositieShoppe)) }
        };
        ConnectTilesWithinNeighborhood(ref tiles);
        
        return new Neighborhood(NeighborhoodID.Northside, tiles);
    }
    private Neighborhood CreateMerchantsDistrictNeighborhood()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            { TileID.UnvisitedIsle, new Tile(new Location(NeighborhoodID.MerchantDistrict, TileID.UnvisitedIsle)) },
            { TileID.TickTockClub, new Tile(new Location(NeighborhoodID.MerchantDistrict, TileID.TickTockClub)) },
            { TileID.RiverDocks, new Tile(new Location(NeighborhoodID.MerchantDistrict, TileID.RiverDocks)) }
        };
        ConnectTilesWithinNeighborhood(ref tiles);
        
        return new Neighborhood(NeighborhoodID.MerchantDistrict, tiles);
    }
    private Neighborhood CreateEasttownNeighborhood()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            { TileID.VelmasDiner, new Tile(new Location(NeighborhoodID.Easttown, TileID.VelmasDiner)) },
            { TileID.HibbsRoadhouse, new Tile(new Location(NeighborhoodID.Easttown, TileID.HibbsRoadhouse)) },
            { TileID.PoliceStation, new Tile(new Location(NeighborhoodID.Easttown, TileID.PoliceStation)) }
        };
        ConnectTilesWithinNeighborhood(ref tiles);
        
        return new Neighborhood(NeighborhoodID.Easttown, tiles);
    }
    private Neighborhood CreateRivertownNeighborhood()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            { TileID.BlackCave, new Tile(new Location(NeighborhoodID.Rivertown, TileID.BlackCave)) },
            { TileID.Graveyard, new Tile(new Location(NeighborhoodID.Rivertown, TileID.Graveyard)) },
            { TileID.GeneralStore, new Tile(new Location(NeighborhoodID.Rivertown, TileID.GeneralStore)) }
        };
        ConnectTilesWithinNeighborhood(ref tiles);
        
        return new Neighborhood(NeighborhoodID.Rivertown, tiles);
    }
    private Neighborhood CreateDowntownNeighborhood()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            { TileID.IndependenceSquare, new Tile(new Location(NeighborhoodID.Downtown, TileID.IndependenceSquare)) },
            { TileID.LaBellaLuna, new Tile(new Location(NeighborhoodID.Downtown, TileID.LaBellaLuna)) },
            { TileID.ArkhamAsylum, new Tile(new Location(NeighborhoodID.Downtown, TileID.ArkhamAsylum)) }
        };
        ConnectTilesWithinNeighborhood(ref tiles);
        
        return new Neighborhood(NeighborhoodID.Downtown, tiles);
    }
    
    private Neighborhood CreateStreets()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            { TileID.Urban1, new Tile(new Location(NeighborhoodID.Streets, TileID.Urban1)) },
            { TileID.Urban2, new Tile(new Location(NeighborhoodID.Streets, TileID.Urban2)) },
            { TileID.Urban3, new Tile(new Location(NeighborhoodID.Streets, TileID.Urban3)) },
            { TileID.Forest1, new Tile(new Location(NeighborhoodID.Streets, TileID.Forest1)) },
            { TileID.Forest2, new Tile(new Location(NeighborhoodID.Streets, TileID.Forest2)) },
            { TileID.Bridge1, new Tile(new Location(NeighborhoodID.Streets, TileID.Bridge1)) },
            { TileID.Bridge2, new Tile(new Location(NeighborhoodID.Streets, TileID.Bridge2)) }
        };

        #region Connect Streets to Neighborhoods
        
        tiles[TileID.Urban1].AdjacentTiles.Add(TileID.ArkhamAsylum);
        tiles[TileID.Urban1].AdjacentTiles.Add(TileID.LaBellaLuna);
        tiles[TileID.Urban1].AdjacentTiles.Add(TileID.UnvisitedIsle);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.ArkhamAsylum].AdjacentTiles.Add(TileID.Urban1);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Urban1);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles.Add(TileID.Urban1);

        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.UnvisitedIsle);
        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.TickTockClub);
        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.BlackCave);
        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.GeneralStore);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles.Add(TileID.Urban2);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.TickTockClub].AdjacentTiles.Add(TileID.Urban2);
        Board.Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].AdjacentTiles.Add(TileID.Urban2);
        Board.Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.GeneralStore].AdjacentTiles.Add(TileID.Urban2);
        

        tiles[TileID.Urban3].AdjacentTiles.Add(TileID.LaBellaLuna);
        tiles[TileID.Urban3].AdjacentTiles.Add(TileID.VelmasDiner);
        tiles[TileID.Urban3].AdjacentTiles.Add(TileID.IndependenceSquare);
        tiles[TileID.Urban3].AdjacentTiles.Add(TileID.PoliceStation);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.IndependenceSquare].AdjacentTiles.Add(TileID.Urban3);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Urban3);
        Board.Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.VelmasDiner].AdjacentTiles.Add(TileID.Urban3);
        Board.Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.PoliceStation].AdjacentTiles.Add(TileID.Urban3);
        
        tiles[TileID.Forest1].AdjacentTiles.Add(TileID.ArkhamAdvertiser);
        tiles[TileID.Forest1].AdjacentTiles.Add(TileID.TrainStation);
        tiles[TileID.Forest1].AdjacentTiles.Add(TileID.ArkhamAsylum);
        Board.Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.ArkhamAdvertiser].AdjacentTiles.Add(TileID.Forest1);
        Board.Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.TrainStation].AdjacentTiles.Add(TileID.Forest1);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.ArkhamAsylum].AdjacentTiles.Add(TileID.Forest1);
        
        tiles[TileID.Forest2].AdjacentTiles.Add(TileID.TrainStation);
        tiles[TileID.Forest2].AdjacentTiles.Add(TileID.UnvisitedIsle);
        tiles[TileID.Forest2].AdjacentTiles.Add(TileID.RiverDocks);
        Board.Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.TrainStation].AdjacentTiles.Add(TileID.Forest2);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles.Add(TileID.Forest2);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.RiverDocks].AdjacentTiles.Add(TileID.Forest2);
        
        tiles[TileID.Bridge1].AdjacentTiles.Add(TileID.PoliceStation);
        tiles[TileID.Bridge1].AdjacentTiles.Add(TileID.BlackCave);
        tiles[TileID.Bridge1].AdjacentTiles.Add(TileID.Graveyard);
        Board.Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].AdjacentTiles.Add(TileID.Bridge1);
        Board.Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.Graveyard].AdjacentTiles.Add(TileID.Bridge1);
        Board.Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.PoliceStation].AdjacentTiles.Add(TileID.Bridge1);
        
        tiles[TileID.Bridge2].AdjacentTiles.Add(TileID.LaBellaLuna);
        tiles[TileID.Bridge2].AdjacentTiles.Add(TileID.BlackCave);
        Board.Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].AdjacentTiles.Add(TileID.Bridge2);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Bridge2);

        #endregion
        
        return new Neighborhood(NeighborhoodID.Streets, tiles);
    }

    #endregion
    
    private void ConnectTilesWithinNeighborhood(ref Dictionary<TileID, Tile> tiles)
    {
        foreach (var tile in tiles)
        {
            foreach (var otherTile in tiles.Where(otherTile => otherTile.Key != tile.Key))
            {
                tile.Value.AdjacentTiles.Add(otherTile.Key);
            }
        }
    }

    #region ActionTriggers

    private static void TriggerTrade()
    {
        throw new NotImplementedException();
    }

    private static void TriggerResearch()
    {
        throw new NotImplementedException();
    }

    private static void TriggerEvade()
    {
        throw new NotImplementedException();
    }

    private static void TriggerAttack()
    {
        throw new NotImplementedException();
    }

    private static void TriggerWard()
    {
        throw new NotImplementedException();
    }

    private static void TriggerFocus()
    {
        throw new NotImplementedException();
    }

    private static void TriggerGatherResources()
    {
        throw new NotImplementedException();
    }

    private static void TriggerMove()
    {
        Debug.Log("CA MARCHE");
    }

    #endregion
}
