using Photon.Pun;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviourPun
{
    public GameObject gameover_popup;
    public Tile createtile;
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    public TetrisData[] tetrises;
    public NextBlocks[] NextBlocks;

    public TMP_Text WinOrLose;
    public TMP_Text score;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10,20);
    List<int> Blocks = new List<int>();

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
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrises.Length; i++)
        {
            this.tetrises[i].Initialize();
        }
    }

    private void Update()
    {

    }

    private void setRandomBlock()
    {
        int random = UnityEngine.Random.Range(0, this.tetrises.Length);
        Blocks.Add(random);
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            setRandomBlock();
        }
        BlocksListSet();
    }
    public void BlocksListSet()
    {
        SpawnPiece(Blocks[0]);
        Blocks.RemoveAt(0);
        setRandomBlock();
        for (int i = 0; i < 5; i++)
        {
            NextBlocks[i].set_active(Blocks[i]);
        }
    }

    private void SpawnPiece(int blocknum)
    {
        TetrisData data = this.tetrises[blocknum];

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (SceneManager.GetActiveScene().name == "Multi_GameScene")
        {
            photonView.RPC("GameEnd", RpcTarget.All, PhotonNetwork.NickName);
        }
        else
        {

        }
    }

    [PunRPC]
    public void GameEnd(string player)
    {
        if (player == PhotonNetwork.NickName)
            WinOrLose.text = "YOU LOSE";
        gameover_popup.SetActive(true);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        int clearnums = 0;
        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                clearnums++;
                LineClear(row);
                Shake.instance.Shaking();
                score.text = (Int32.Parse(score.text) + 100).ToString("D8");
            }
            else
            {
                row++;
            }
        }
        if (clearnums >= 2)
        {
            photonView.RPC("addLine", RpcTarget.Others, clearnums - 1);
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    [PunRPC]
    public void addLine(int num)
    {
        int upsize = num + Bounds.yMin;

        int random = UnityEngine.Random.Range(0, 10);
        int a = random + Bounds.xMin;
        RectInt bounds = this.Bounds;
        int b = bounds.yMax;
        Debug.Log("b : " + b);
        Debug.Log("num : " + num);
        while (b >= upsize)
        {
            bool ispiece = false;
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, b - num, 0);
                TileBase below = this.tilemap.GetTile(position);
                for (int i = 0; i < activePiece.cells.Length; i++)
                {
                    Vector3Int tilePosition = activePiece.cells[i] + activePiece.position;
                    if (tilePosition == position)
                    {
                        ispiece = true;
                        break;
                    }
                }
                if (ispiece == false)
                {
                    position = new Vector3Int(col, b, 0);
                    this.tilemap.SetTile(position, below);
                }
            }

            b--;
        }

        for (int row = bounds.yMin; row < upsize; row++)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, null);
                if (col != a)
                {
                    this.tilemap.SetTile(position, createtile);
                }
            }
        }

    }
    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }

            row++;
        }
    }
}