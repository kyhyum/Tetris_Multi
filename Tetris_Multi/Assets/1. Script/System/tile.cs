using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tile : MonoBehaviourPun
{
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (!photonView.IsMine)
        {
            tilemap.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
