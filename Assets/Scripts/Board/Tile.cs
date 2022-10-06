using System.Collections.Generic;
using Game;
using Investigators;
using TMPro;
using UnityEngine;

namespace Board
{
    public class Tile: MonoBehaviour
    {
        private Material _greenMaterial;
        private Material _greenOpaqueMaterial;
        private Material _redMaterial;
        private Material _transparentMaterial;
        public MeshRenderer _meshRenderer;
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private GameManager gameManager;
        private TMP_Text doomCounter;

        private int doomAmount = 0;
        //public List<Monster> monsters;
        public int DoomAmount
        {
            get => doomAmount;
            set
            {
                if (Location.NeighborhoodID == NeighborhoodID.Streets) return;
                var neighborhood = boardManager.Neighborhoods[Location.NeighborhoodID];
                if (neighborhood.HasAnomaly)
                {
                    int difference = DoomAmount - doomAmount;
                    // TODO
                }
                
                doomAmount = value;
                doomCounter.text = value.ToString();
                
                neighborhood.UpdateDoomCounter();
                if (doomAmount >= 3)
                {
                    neighborhood.TriggerAnomaly();
                }
                boardManager.UpdateDoomCounter();
                
            }
        }
    
        public bool selected = false;
        [HideInInspector] public Transform CenterPos;
        public HashSet<TileID> AdjacentTiles = new HashSet<TileID>();
        public string Name => MapUtils.EnumToString(Location.TileID);
        public string Neighborhood => MapUtils.EnumToString(Location.NeighborhoodID);
        public TileID ID => Location.TileID;
        public Location Location;
        public Dictionary<InvestigatorID, Investigator> Investigators = new Dictionary<InvestigatorID, Investigator>();

        void Awake()
        {
            var tileID = MapUtils.StringToEnum(name);
            Location = new Location(MapUtils.GetNeighborhoodForTile(tileID), tileID);
            CenterPos = transform.Find("Center");
            _meshRenderer = GetComponent<MeshRenderer>();
            _greenMaterial = Resources.Load("Materials/Selected", typeof(Material)) as Material;
            _greenOpaqueMaterial = Resources.Load("Materials/OpaqueSelected", typeof(Material)) as Material;
            _redMaterial = Resources.Load("Materials/Error", typeof(Material)) as Material;      
            _transparentMaterial = Resources.Load("Materials/InvisibleFace", typeof(Material)) as Material;

            if (Location.NeighborhoodID != NeighborhoodID.Streets)
            {
                doomCounter = gameObject.FindInChildren("Doom").FindInChildren("DoomCounter").GetComponent<TMP_Text>();
            }
        }

        public void OnMouseEnter()
        {
            if (gameManager.currentAction == ActionID.Move)
            {
                boardManager.ShowHoverPath(this);
            }
        }

        public void OnMouseExit()
        {
            if (gameManager.currentAction == ActionID.Move)
            {
                boardManager.RemoveHoverPath();
            }
        }
    
        public void OnMouseDown()
        {
            if (gameManager.currentAction == ActionID.Move)
            {
                if (selected)
                {
                    boardManager.RemoveTilesToMoveAction(this);
                    boardManager.ShowHoverPath(this);
                }
                else { boardManager.AddTilesToMoveAction(this);}
            }
        }

        public void HoverTile()
        {
            if (selected) return;
            _meshRenderer.material = boardManager.IsValidPath ? _greenMaterial : _redMaterial;
        }

        public void UnhoverTile()
        {
            if (selected) return;
            _meshRenderer.material = _transparentMaterial;
        }
    
        public void SelectTile()
        {
            selected = true;
            _meshRenderer.material = _greenOpaqueMaterial;
        }
    
        public void UnselectTile()
        {
            selected = false;
            UnhoverTile();
        }
    }
}
