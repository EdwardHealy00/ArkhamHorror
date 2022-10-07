using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Board
{
    public class Neighborhood : MonoBehaviour
    {
        public NeighborhoodID NeighborhoodID;
        public Dictionary<TileID, Tile> Tiles;
        private int clueAmount = 0;
        private TMP_Text doomCounter;
        private TMP_Text clueCounter;
        private GameObject anomalyToken;
        private bool hasAnomaly = false;
        public uint DoomAmount => (uint) Tiles.Sum(tile => tile.Value.DoomAmount);

        public bool HasAnomaly
        {
            get => hasAnomaly;
            set
            {
                if (NeighborhoodID == NeighborhoodID.Streets) return;
                hasAnomaly = value;
                anomalyToken.SetActive(hasAnomaly);
            }
        }
        
        public int ClueAmount
        {
            get => clueAmount;
            set
            {
                if (NeighborhoodID == NeighborhoodID.Streets) return;
                clueAmount = value;
                clueCounter.text = clueAmount.ToString();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Initialize(NeighborhoodID id, Dictionary<TileID, Tile> tiles)
        {
            NeighborhoodID = id;
            Tiles = tiles;
            if (NeighborhoodID != NeighborhoodID.Streets)
            {
                doomCounter = gameObject.FindInChildren("NDoom").FindInChildren("DoomCounter").GetComponent<TMP_Text>();
                clueCounter = gameObject.FindInChildren("NClue").FindInChildren("ClueCounter").GetComponent<TMP_Text>();
                anomalyToken = gameObject.FindInChildren("AnomalyToken");
                anomalyToken.SetActive(false);
            }
        }

        public void UpdateDoomCounter()
        {
            doomCounter.text = DoomAmount.ToString();
        }

        public void TriggerAnomaly()
        {
            HasAnomaly = true;
        }
    }
}
