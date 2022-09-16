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
    private Material _redMaterial;
    private Material _transparentMaterial;
    public MeshRenderer _meshRenderer;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameManager gameManager;
    
    //public List<Monster> monsters;
    //public List<Investigator> investigators;
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
        _redMaterial = Resources.Load("Materials/Error", typeof(Material)) as Material;      
        _transparentMaterial = Resources.Load("Materials/InvisibleFace", typeof(Material)) as Material;
    }

    public void OnMouseEnter()
    {
        if (gameManager.currentAction == ActionID.Move)
        {
            boardManager.ShowPath(this);
        }
    }

    public void OnMouseExit()
    {
        if (gameManager.currentAction == ActionID.Move)
        {
            boardManager.RemoveHighlightedPath();
        }
    }
    
    public void OnMouseDown()
    {
        if (gameManager.currentAction == ActionID.Move)
        {
            boardManager.MoveActionInvestigator(this);
        }
    }

    public void SelectTile()
    {
        _meshRenderer.material = boardManager.IsValidPath ? _greenMaterial : _redMaterial;
    }
    
    public void UnselectTile()
    {
        _meshRenderer.material = _transparentMaterial;
    }
}
