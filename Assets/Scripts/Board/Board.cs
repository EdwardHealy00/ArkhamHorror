using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Board
{
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
}