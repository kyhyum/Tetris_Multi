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
        // this.remote_tilemap.color = Color.black;
    }
    private void Update()
    {
        hastile();
    }

    public void hastile()
    {
        /*if (!PhotonNetwork.IsConnected)
             return;*/
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row, 0);

                if (tilemap.HasTile(pos))
                {
                    Debug.Log("aa");
                    TileBase above = this.tilemap.GetTile(pos);
                    photonView.RPC("createblock", RpcTarget.Others, (Vector3)pos);
                }
                else
                    this.remote_tile.SetTile(pos, null);
            }
            row++;

        }
    }

    [PunRPC]
    void createblock(Vector3 pos)
    {
        Vector3Int position = new Vector3Int((int)pos.x, (int)pos.y, 0);
        TileBase above = this.tilemap.GetTile(position);
        this.remote_tile.SetTile(position, above);
        remote_tile.color = Color.black;
    }

    /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
         if (stream.IsWriting)
         {
             stream.SendNext(_123.text);

         }
         else
         {
             remote_123.text = (string)stream.ReceiveNext();
         }
     }
     public void aa()
     {
         _123.text = Random.Range(1, 10000).ToString();
     }*/
}
