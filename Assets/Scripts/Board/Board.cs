using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Board
{
    public readonly Dictionary<NeighborhoodID, Neighborhood> Neighborhoods;
    public readonly Dictionary<TileID, Tile> Tiles;
    public int TotalDoom => Neighborhoods.Sum(neighborhood => neighborhood.Value.DoomAmount);

    public Board(Dictionary<NeighborhoodID, Neighborhood> neighborhoods, Dictionary<TileID, Tile> tiles)
    {
        Neighborhoods = neighborhoods;
        Tiles = tiles;
    }
}

public class Neighborhood
{
    public readonly NeighborhoodID NeighborhoodID;
    public readonly Dictionary<TileID, Tile> Tiles;
    private int ClueAmount { get; set; }
    public int DoomAmount => Tiles.Sum(tile => tile.Value.doomAmount);
    private bool HasAnomaly { get; set; }

    public Neighborhood(NeighborhoodID id, Dictionary<TileID, Tile> tiles)
    {
        NeighborhoodID = id;
        Tiles = tiles;
        ClueAmount = 0;
        HasAnomaly = false;
    }
}


public readonly struct Location
{
    public Location(NeighborhoodID neighborhoodId, TileID tileId)
    {
        NeighborhoodID = neighborhoodId;
        TileID = tileId;
    }

    public NeighborhoodID NeighborhoodID { get; }
    public TileID TileID { get; }

    public override string ToString() => $"({MapUtils.EnumToString(NeighborhoodID)}, {MapUtils.EnumToString(TileID)})";
}
