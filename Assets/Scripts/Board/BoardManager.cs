using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Investigators;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {
        public Dictionary<NeighborhoodID, Neighborhood> Neighborhoods;
        public Dictionary<TileID, Tile> Tiles;
        public int TotalDoom => Neighborhoods.Sum(neighborhood => neighborhood.Value.DoomAmount); // TODO

        public Tile startingTile;
        public Func<Tile, List<Tile>> ShortestPath;
        private List<Tile> _highlightedPath = new List<Tile>();
        private List<Tile> _pendingMovePath = new List<Tile>();
        private TMP_Text doomCounter;
    
        [SerializeField] private GameManager game;
        [SerializeField] private GameObject investigatorPrefab;

        public bool IsValidPath => _pendingMovePath.Count() + _highlightedPath.Count() <= game.currentInvestigator.MoveLimit;

        // Start is called before the first frame update
        void Start()
        {
            //doomCounter = gameObject.FindInChildren("TDoom").FindInChildren("DoomCounter").GetComponent<TMP_Text>(); TODO
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
            Neighborhoods = neighborhoods;
            Tiles = tiles;
            Neighborhoods[NeighborhoodID.Streets] = CreateStreets();

            startingTile = MapUtils.EnumToGameObject(TileID.TrainStation).GetComponent<Tile>();
            startingTile.Investigators.AddRange(game.investigators);
            foreach (var investigator in game.investigators)
            {
                investigator.Value.Tile = startingTile;
            }

            DoFinalPreparations(ScenarioID.ApproachOfAzatoth);
        }

        private void DoFinalPreparations(ScenarioID scenario)
        {
            SpawnStartingClues(scenario);
            PlaceStartingDoom(scenario);
            SpreadDoom();
            FinalizeSetup(scenario);
        }

        private void SpawnStartingClues(ScenarioID scenario)
        {
            switch (scenario)
            {
                case ScenarioID.ApproachOfAzatoth:
                    Neighborhoods[NeighborhoodID.Northside].ClueAmount++;
                    Neighborhoods[NeighborhoodID.Northside].ClueAmount++;
                    Neighborhoods[NeighborhoodID.Downtown].ClueAmount++;
                    break;
                case ScenarioID.VeilOfTwilight:
                case ScenarioID.FeastForUmordhoth:
                case ScenarioID.EchoesOfTheDeep:
                default:
                    break;
            }
        }

        private void PlaceStartingDoom(ScenarioID scenario)
        {
            switch (scenario)
            {
                case ScenarioID.ApproachOfAzatoth:
                    Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.ArkhamAdvertiser].DoomAmount++;
                    Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.IndependenceSquare].DoomAmount++;
                    Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.VelmasDiner].DoomAmount++;
                    Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].DoomAmount++;
                    Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].DoomAmount++;
                    break;
                case ScenarioID.VeilOfTwilight:
                case ScenarioID.FeastForUmordhoth:
                case ScenarioID.EchoesOfTheDeep:
                default:
                    break;
            }
        }
    
        private void SpreadDoom()
        {
            //throw new NotImplementedException();
        }
    
        private void FinalizeSetup(ScenarioID scenario)
        {
            //throw new NotImplementedException();
        }

        #region Create Neighborhoods

        private Neighborhood CreateNorthsideNeighborhood()
        {
            var neighborhoodTransform = transform.Find("Northside");
            var tiles = new Dictionary<TileID, Tile>
            {
                {TileID.ArkhamAdvertiser, neighborhoodTransform.Find("ArkhamAdvertiser").GetComponent<Tile>()},
                {TileID.TrainStation, neighborhoodTransform.Find("TrainStation").GetComponent<Tile>()},
                {TileID.CuriositieShoppe, neighborhoodTransform.Find("CuriositieShoppe").GetComponent<Tile>()}
            };
            ConnectTilesWithinNeighborhood(ref tiles);
            var neighborhood = neighborhoodTransform.GetComponent<Neighborhood>();
            neighborhood.Initialize(NeighborhoodID.Northside, tiles);
            return neighborhood;
        }

        private Neighborhood CreateMerchantDistrictNeighborhood()
        {
            var neighborhoodTransform = transform.Find("MerchantDistrict");
            var tiles = new Dictionary<TileID, Tile>
            {
                {TileID.UnvisitedIsle, neighborhoodTransform.Find("UnvisitedIsle").GetComponent<Tile>()},
                {TileID.TickTockClub, neighborhoodTransform.Find("TickTockClub").GetComponent<Tile>()},
                {TileID.RiverDocks, neighborhoodTransform.Find("RiverDocks").GetComponent<Tile>()}
            };
            ConnectTilesWithinNeighborhood(ref tiles);

            var neighborhood = neighborhoodTransform.GetComponent<Neighborhood>();
            neighborhood.Initialize(NeighborhoodID.MerchantDistrict, tiles);
            return neighborhood;
        }

        private Neighborhood CreateEasttownNeighborhood()
        {
            var neighborhoodTransform = transform.Find("Easttown");
            var tiles = new Dictionary<TileID, Tile>
            {
                {TileID.VelmasDiner, neighborhoodTransform.Find("VelmasDiner").GetComponent<Tile>()},
                {TileID.HibbsRoadhouse, neighborhoodTransform.Find("HibbsRoadhouse").GetComponent<Tile>()},
                {TileID.PoliceStation, neighborhoodTransform.Find("PoliceStation").GetComponent<Tile>()}
            };
            ConnectTilesWithinNeighborhood(ref tiles);

            var neighborhood = neighborhoodTransform.GetComponent<Neighborhood>();
            neighborhood.Initialize(NeighborhoodID.Easttown, tiles);
            return neighborhood;
        }

        private Neighborhood CreateRivertownNeighborhood()
        {
            var neighborhoodTransform = transform.Find("Rivertown");
            var tiles = new Dictionary<TileID, Tile>
            {
                {TileID.BlackCave, neighborhoodTransform.Find("BlackCave").GetComponent<Tile>()},
                {TileID.Graveyard, neighborhoodTransform.Find("Graveyard").GetComponent<Tile>()},
                {TileID.GeneralStore, neighborhoodTransform.Find("GeneralStore").GetComponent<Tile>()}
            };
            ConnectTilesWithinNeighborhood(ref tiles);

            var neighborhood = neighborhoodTransform.GetComponent<Neighborhood>();
            neighborhood.Initialize(NeighborhoodID.Rivertown, tiles);
            return neighborhood;
        }

        private Neighborhood CreateDowntownNeighborhood()
        {
            var neighborhoodTransform = transform.Find("Downtown");
            var tiles = new Dictionary<TileID, Tile>
            {
                {TileID.IndependenceSquare, neighborhoodTransform.Find("IndependenceSquare").GetComponent<Tile>()},
                {TileID.LaBellaLuna, neighborhoodTransform.Find("LaBellaLuna").GetComponent<Tile>()},
                {TileID.ArkhamAsylum, neighborhoodTransform.Find("ArkhamAsylum").GetComponent<Tile>()}
            };
            ConnectTilesWithinNeighborhood(ref tiles);

            var neighborhood = neighborhoodTransform.GetComponent<Neighborhood>();
            neighborhood.Initialize(NeighborhoodID.Downtown, tiles);
            return neighborhood;
        }

        private Neighborhood CreateStreets()
        {
            var neighborhoodTransform = transform.Find("Streets");
            var tiles = new Dictionary<TileID, Tile>
            {
                {TileID.Urban1, neighborhoodTransform.Find("Urban1").Find("Urban1").GetComponent<Tile>()},
                {TileID.Urban2, neighborhoodTransform.Find("Urban2").Find("Urban2").GetComponent<Tile>()},
                {TileID.Urban3, neighborhoodTransform.Find("Urban3").Find("Urban3").GetComponent<Tile>()},
                {TileID.Forest1, neighborhoodTransform.Find("Forest1").Find("Forest1").GetComponent<Tile>()},
                {TileID.Forest2, neighborhoodTransform.Find("Forest2").Find("Forest2").GetComponent<Tile>()},
                {TileID.Bridge1, neighborhoodTransform.Find("Bridge1").Find("Bridge1").GetComponent<Tile>()},
                {TileID.Bridge2, neighborhoodTransform.Find("Bridge2").Find("Bridge2").GetComponent<Tile>()}
            };

            Tiles.AddRange(tiles);

            #region Connect Streets to Neighborhoods

            tiles[TileID.Urban1].AdjacentTiles.Add(TileID.ArkhamAsylum);
            tiles[TileID.Urban1].AdjacentTiles.Add(TileID.LaBellaLuna);
            tiles[TileID.Urban1].AdjacentTiles.Add(TileID.UnvisitedIsle);
            Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.ArkhamAsylum].AdjacentTiles.Add(TileID.Urban1);
            Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Urban1);
            Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles
                .Add(TileID.Urban1);

            tiles[TileID.Urban2].AdjacentTiles.Add(TileID.UnvisitedIsle);
            tiles[TileID.Urban2].AdjacentTiles.Add(TileID.TickTockClub);
            tiles[TileID.Urban2].AdjacentTiles.Add(TileID.BlackCave);
            tiles[TileID.Urban2].AdjacentTiles.Add(TileID.GeneralStore);
            Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles
                .Add(TileID.Urban2);
            Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.TickTockClub].AdjacentTiles
                .Add(TileID.Urban2);
            Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].AdjacentTiles.Add(TileID.Urban2);
            Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.GeneralStore].AdjacentTiles.Add(TileID.Urban2);


            tiles[TileID.Urban3].AdjacentTiles.Add(TileID.LaBellaLuna);
            tiles[TileID.Urban3].AdjacentTiles.Add(TileID.VelmasDiner);
            tiles[TileID.Urban3].AdjacentTiles.Add(TileID.IndependenceSquare);
            tiles[TileID.Urban3].AdjacentTiles.Add(TileID.PoliceStation);
            Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.IndependenceSquare].AdjacentTiles.Add(TileID.Urban3);
            Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Urban3);
            Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.VelmasDiner].AdjacentTiles.Add(TileID.Urban3);
            Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.PoliceStation].AdjacentTiles.Add(TileID.Urban3);

            tiles[TileID.Forest1].AdjacentTiles.Add(TileID.ArkhamAdvertiser);
            tiles[TileID.Forest1].AdjacentTiles.Add(TileID.TrainStation);
            tiles[TileID.Forest1].AdjacentTiles.Add(TileID.ArkhamAsylum);
            Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.ArkhamAdvertiser].AdjacentTiles.Add(TileID.Forest1);
            Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.TrainStation].AdjacentTiles.Add(TileID.Forest1);
            Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.ArkhamAsylum].AdjacentTiles.Add(TileID.Forest1);

            tiles[TileID.Forest2].AdjacentTiles.Add(TileID.TrainStation);
            tiles[TileID.Forest2].AdjacentTiles.Add(TileID.UnvisitedIsle);
            tiles[TileID.Forest2].AdjacentTiles.Add(TileID.RiverDocks);
            Neighborhoods[NeighborhoodID.Northside].Tiles[TileID.TrainStation].AdjacentTiles.Add(TileID.Forest2);
            Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.UnvisitedIsle].AdjacentTiles
                .Add(TileID.Forest2);
            Neighborhoods[NeighborhoodID.MerchantDistrict].Tiles[TileID.RiverDocks].AdjacentTiles.Add(TileID.Forest2);

            tiles[TileID.Bridge1].AdjacentTiles.Add(TileID.PoliceStation);
            tiles[TileID.Bridge1].AdjacentTiles.Add(TileID.BlackCave);
            tiles[TileID.Bridge1].AdjacentTiles.Add(TileID.Graveyard);
            Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].AdjacentTiles.Add(TileID.Bridge1);
            Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.Graveyard].AdjacentTiles.Add(TileID.Bridge1);
            Neighborhoods[NeighborhoodID.Easttown].Tiles[TileID.PoliceStation].AdjacentTiles.Add(TileID.Bridge1);

            tiles[TileID.Bridge2].AdjacentTiles.Add(TileID.LaBellaLuna);
            tiles[TileID.Bridge2].AdjacentTiles.Add(TileID.BlackCave);
            Neighborhoods[NeighborhoodID.Rivertown].Tiles[TileID.BlackCave].AdjacentTiles.Add(TileID.Bridge2);
            Neighborhoods[NeighborhoodID.Downtown].Tiles[TileID.LaBellaLuna].AdjacentTiles.Add(TileID.Bridge2);

            #endregion

            var neighborhood = neighborhoodTransform.GetComponent<Neighborhood>();
            neighborhood.Initialize(NeighborhoodID.Streets, tiles);
            return neighborhood;
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
                    queue.Enqueue(Tiles[neighbor]);
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

        public void UpdateDoomCounter()
        {
            //doomCounter.text = TotalDoom.ToString(); TODO
        }
    }
}
