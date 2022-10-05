using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Investigators;
using UnityEngine;

public class Tile: MonoBehaviour
{
    [HideInInspector] public Transform CenterPos;
    private Material _greenMaterial;
    private Material _greenOpaqueMaterial;
    private Material _redMaterial;
    private Material _transparentMaterial;
    public MeshRenderer _meshRenderer;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameManager gameManager;

    public bool selected = false;
    
    //public List<Monster> monsters;
    public int doomAmount = 0;
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
