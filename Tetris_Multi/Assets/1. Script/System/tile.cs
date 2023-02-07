using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Realtime;
using TMPro;

public class tile : MonoBehaviourPun
{
    //public TextMeshProUGUI _123;
    //public TextMeshProUGUI remote_123;

    public Tile createtile;
    public GameObject board;
    public Tilemap tilemap { get; private set; }
    private Tilemap remote_tile;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.remote_tile = GetComponentInChildren<Tilemap>();
        tilemap = board.GetComponentInChildren<Tilemap>();
        InvokeRepeating("hastile", 0f, 0.1f);
        // this.remote_tilemap.color = Color.black;
    }
    private void Update()
    {
    }

    public void hastile()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        int clearrow = bounds.yMin;

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row, 0);

                if (tilemap.HasTile(pos))
                {
                    TileBase above = this.tilemap.GetTile(pos);
                    photonView.RPC("createblock", RpcTarget.Others, col, row, true);
                }
                else
                    photonView.RPC("createblock", RpcTarget.Others, col, row, false);
            }
            row++;
        }
    }

    [PunRPC]
    void createblock(int col, int row, bool a)
    {
        if (a)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            this.remote_tile.SetTile(pos, createtile);
            this.remote_tile.color = Color.grey;
            Color color = this.remote_tile.color;
            color.a = 0.1f;
        }
        else
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            this.remote_tile.SetTile(pos, null);
        }
    }
}