using System.Collections.Generic;
using System.Linq;


public class Board
{
    public readonly Dictionary<NeighborhoodID, Neighborhood> Neighborhoods;
    public int TotalDoom => Neighborhoods.Sum(neighborhood => neighborhood.Value.DoomAmount);

    public Board(Dictionary<NeighborhoodID, Neighborhood> neighborhoods)
    {
        Neighborhoods = neighborhoods;
    }
}

public class Neighborhood
{
    public readonly NeighborhoodID NeighborhoodID;
    public readonly Dictionary<TileID, Tile> Tiles;
    private int ClueAmount { get; set; }
    public int DoomAmount => Tiles.Sum(tile => tile.Value.DoomAmount);
    private bool HasAnomaly { get; set; }

    public Neighborhood(NeighborhoodID id, Dictionary<TileID, Tile> tiles)
    {
        NeighborhoodID = id;
        Tiles = tiles;
        ClueAmount = 0;
        HasAnomaly = false;
    }
}

public class Tile
{
    //public List<Monster> monsters;
    //public List<Investigator> investigators;
    public int DoomAmount { get; }
    public readonly HashSet<TileID> AdjacentTiles;


    public string Name => MapUtils.EnumToString(_location.TileID);
    public string Neighborhood => MapUtils.EnumToString(_location.NeighborhoodID);
    
    private readonly Location _location;

    public Tile(Location location)
    {
        _location = location;
        DoomAmount = 0;
        AdjacentTiles = new HashSet<TileID>();
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
