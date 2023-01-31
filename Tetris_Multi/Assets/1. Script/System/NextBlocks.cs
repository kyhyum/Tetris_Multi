using System.Collections;
using System;
using UnityEngine;

public class NextBlocks : MonoBehaviour
{

    public GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        set_unactive();
    }
    private void set_unactive()
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            blocks[i].SetActive(false);
        }
    }

    public void set_active(int blocksnum)
    {
        set_unactive();
        blocks[blocksnum].SetActive(true);
    }
}
