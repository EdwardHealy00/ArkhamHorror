using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Game;
using Investigators;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BoardManager : MonoBehaviour
{
    public Board Board;
    public Tile startingTile;
    public Func<Tile, List<Tile>> ShortestPath;
    private List<Tile> _highlightedPath = new List<Tile>();
    private List<Tile> _pendingMovePath = new List<Tile>();
    
    [SerializeField] private GameManager game;
    [SerializeField] private GameObject investigatorPrefab;

    public bool IsValidPath => _pendingMovePath.Count() + _highlightedPath.Count() <= game.currentInvestigator.MoveLimit;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnInvestigators(Dictionary<InvestigatorID, Investigator> investigators)
    {
        foreach (var (_, investigator) in investigators)
        {
            var pawn = Instantiate(investigatorPrefab, startingTile.CenterPos);
            pawn.GetComponent<SpriteRenderer>().sprite = investigator.GetSprite();
            investigator.Pawn = pawn;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void CreateApproachOfAzatothMap()
    {
        var neighborhoods = new Dictionary<NeighborhoodID, Neighborhood>()
        {
            {NeighborhoodID.Northside, CreateNorthsideNeighborhood()},
            {NeighborhoodID.MerchantDistrict, CreateMerchantDistrictNeighborhood()},
            {NeighborhoodID.Easttown, CreateEasttownNeighborhood()},
            {NeighborhoodID.Rivertown, CreateRivertownNeighborhood()},
            {NeighborhoodID.Downtown, CreateDowntownNeighborhood()}
        };
        var tiles = GetAllTiles(neighborhoods);
        Board = new Board(neighborhoods, tiles);
        Board.Neighborhoods[NeighborhoodID.Streets] = CreateStreets();

        startingTile = MapUtils.EnumToGameObject(TileID.TrainStation).GetComponent<Tile>();
        startingTile.Investigators.AddRange(game.investigators);
        foreach (var investigator in game.investigators)
        {
            investigator.Value.Tile = startingTile;
        }
    }

    #region Create Neighborhoods

    private Neighborhood CreateNorthsideNeighborhood()
    {
        var neighborhood = transform.Find("Northside");
        var tiles = new Dictionary<TileID, Tile>
        {
            {TileID.ArkhamAdvertiser, neighborhood.Find("ArkhamAdvertiser").GetComponent<Tile>()},
            {TileID.TrainStation, neighborhood.Find("TrainStation").GetComponent<Tile>()},
            {TileID.CuriositieShoppe, neighborhood.Find("CuriositieShoppe").GetComponent<Tile>()}
        };
        ConnectTilesWithinNeighborhood(ref tiles);

        return new Neighborhood(NeighborhoodID.Northside, tiles);
    }

    private Neighborhood CreateMerchantDistrictNeighborhood()
    {
        var neighborhood = transform.Find("MerchantDistrict");
        var tiles = new Dictionary<TileID, Tile>
        {
            {TileID.UnvisitedIsle, neighborhood.Find("UnvisitedIsle").GetComponent<Tile>()},
            {TileID.TickTockClub, neighborhood.Find("TickTockClub").GetComponent<Tile>()},
            {TileID.RiverDocks, neighborhood.Find("RiverDocks").GetComponent<Tile>()}
        };
        ConnectTilesWithinNeighborhood(ref tiles);

        return new Neighborhood(NeighborhoodID.MerchantDistrict, tiles);
    }

    private Neighborhood CreateEasttownNeighborhood()
    {
        var neighborhood = transform.Find("Easttown");
        var tiles = new Dictionary<TileID, Tile>
        {
            {TileID.VelmasDiner, neighborhood.Find("VelmasDiner").GetComponent<Tile>()},
            {TileID.HibbsRoadhouse, neighborhood.Find("HibbsRoadhouse").GetComponent<Tile>()},
            {TileID.PoliceStation, neighborhood.Find("PoliceStation").GetComponent<Tile>()}
        };
        ConnectTilesWithinNeighborhood(ref tiles);

        return new Neighborhood(NeighborhoodID.Easttown, tiles);
    }

    private Neighborhood CreateRivertownNeighborhood()
    {
        var neighborhood = transform.Find("Rivertown");
        var tiles = new Dictionary<TileID, Tile>
        {
            {TileID.BlackCave, neighborhood.Find("BlackCave").GetComponent<Tile>()},
            {TileID.Graveyard, neighborhood.Find("Graveyard").GetComponent<Tile>()},
            {TileID.GeneralStore, neighborhood.Find("GeneralStore").GetComponent<Tile>()}
        };
        ConnectTilesWithinNeighborhood(ref tiles);

        return new Neighborhood(NeighborhoodID.Rivertown, tiles);
    }

    private Neighborhood CreateDowntownNeighborhood()
    {
        var neighborhood = transform.Find("Downtown");
        var tiles = new Dictionary<TileID, Tile>
        {
            {TileID.IndependenceSquare, neighborhood.Find("IndependenceSquare").GetComponent<Tile>()},
            {TileID.LaBellaLuna, neighborhood.Find("LaBellaLuna").GetComponent<Tile>()},
            {TileID.ArkhamAsylum, neighborhood.Find("ArkhamAsylum").GetComponent<Tile>()}
        };
        ConnectTilesWithinNeighborhood(ref tiles);

        return new Neighborhood(NeighborhoodID.Downtown, tiles);
    }

    private Neighborhood CreateStreets()
    {
        var tiles = new Dictionary<TileID, Tile>
        {
            {TileID.Urban1, transform.Find("Urban1").Find("Urban1").GetComponent<Tile>()},
            {TileID.Urban2, transform.Find("Urban2").Find("Urban2").GetComponent<Tile>()},
            {TileID.Urban3, transform.Find("Urban3").Find("Urban3").GetComponent<Tile>()},
            {TileID.Forest1, transform.Find("Forest1").Find("Forest1").GetComponent<Tile>()},
            {TileID.Forest2, transform.Find("Forest2").Find("Forest2").GetComponent<Tile>()},
            {TileID.Bridge1, transform.Find("Bridge1").Find("Bridge1").GetComponent<Tile>()},
            {TileID.Bridge2, transform.Find("Bridge2").Find("Bridge2").GetComponent<Tile>()}
        };

        Board.Tiles.AddRange(tiles);

        #region Connect Streets to Neighborhoods

        tiles[TileID.Urban1].AdjacentTiles.Add(TileID.ArkhamAsylum);
        tiles[TileID.Urban1].AdjacentTiles.Add(TileID.LaBellaLuna);
        tiles[TileID.Urban1].AdjacentTiles.Add(TileID.UnvisitedIsle);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.ArkhamAsylum].AdjacentTiles.Add(TileID.Urban1);
        Board.Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Urban1);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles
            .Add(TileID.Urban1);

        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.UnvisitedIsle);
        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.TickTockClub);
        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.BlackCave);
        tiles[TileID.Urban2].AdjacentTiles.Add(TileID.GeneralStore);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles
            .Add(TileID.Urban2);
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.TickTockClub].AdjacentTiles
            .Add(TileID.Urban2);
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
        Board.Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles
            .Add(TileID.Forest2);
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

    private Dictionary<TileID, Tile> GetAllTiles(Dictionary<NeighborhoodID, Neighborhood> neighborhoods)
    {
        var tiles = new Dictionary<TileID, Tile>();
        foreach (var neighborhood in neighborhoods)
        {
            tiles.AddRange(neighborhood.Value.Tiles);
        }

        return tiles;
    }

    public void ShowHoverPath(Tile destTile)
    {
        foreach (var tile in _highlightedPath) tile.UnhoverTile();
        _highlightedPath = ShortestPath.Invoke(destTile);
        foreach (var tile in _highlightedPath) tile.HoverTile();
    }

    public Func<Tile, List<Tile>> FindShortestPath(Tile start)
    {
        var previous = new Dictionary<TileID, Tile>();

        var queue = new Queue<Tile>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var tile = queue.Dequeue();
            foreach (var neighbor in tile.AdjacentTiles)
            {
                if (previous.ContainsKey(neighbor))
                    continue;

                previous[neighbor] = tile;
                queue.Enqueue(Board.Tiles[neighbor]);
            }
        }

        List<Tile> Path(Tile v)
        {
            var path = new List<Tile> { };

            var current = v;
            while (!current.Equals(start))
            {
                path.Add(current);
                current = previous[current.ID];
            }

            path.Reverse();

            return path;
        }

        return Path;
    }

    public void RemoveHoverPath()
    {
        foreach (var tile in _highlightedPath)
        {
            if (!tile.selected)
            {
                tile.UnhoverTile();
            }
        }
        _highlightedPath.Clear();
    }

    public void MoveActionInvestigator()
    {
        if (!_pendingMovePath.Any()) return;
        
        var investigator = game.currentInvestigator;
        var tile = _pendingMovePath.Last();
        if (tile.ID == investigator.Tile.ID) return;

        if (ShortestPath(tile).Count <= investigator.MoveLimit)
        {
            Move(investigator, tile);
            RemoveTilesToMoveAction(tile);
        }

        game.currentInvestigator.ActionsDoneThisTurn[ActionID.Move] = true; // TODO Bind with radio buttons
    }
    
    public void AddTilesToMoveAction(Tile destTile)
    {
        if (!IsValidPath) return;
        
        _pendingMovePath.AddRange(_highlightedPath);
        _highlightedPath.Clear();
        foreach (var tile in _pendingMovePath)
        {
            tile.SelectTile();
        }
        ShortestPath = FindShortestPath(destTile);
    }
    
    public void RemoveTilesToMoveAction(Tile destTile)
    {
        foreach (var tile in _pendingMovePath)
        {
            tile.UnselectTile();
        }
        _pendingMovePath.Clear();
        ShortestPath = FindShortestPath(game.currentInvestigator.Tile);
    }

    private void Move(Investigator investigator, Tile tile)
    {
        investigator.Tile.Investigators.Remove(investigator.ID);
        var i = 0;
        foreach (var inv in investigator.Tile.Investigators.Values)
        {
            inv.Pawn.transform.position = investigator.Tile.CenterPos.position + new Vector3(1 * i, 0, 0);
            i++;
        }
        
        tile.Investigators[investigator.ID] = investigator;
        investigator.Tile = tile;
        investigator.Pawn.transform.position = tile.CenterPos.position + new Vector3(1 * (tile.Investigators.Count - 1), 0, 0);
    }
}
