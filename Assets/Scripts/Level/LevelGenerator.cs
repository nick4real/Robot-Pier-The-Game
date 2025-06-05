using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator SharedInstance;

    [SerializeField] private List<GameObject> levelBlocksList;

    private List<Block> blocks;
    private Queue<Block> queue; // TODO

    private void Awake()
    {
        SharedInstance = this;
        blocks = new();
    }

    private void Start()
    {
        GameObject tmp;

        foreach (GameObject blockList in levelBlocksList)
        {
            tmp = Instantiate(blockList, new Vector3(), Quaternion.Euler(0,0,0));
            tmp.SetActive(false);

            Transform enter = blockList.transform.Find("Enter");
            Transform exit = blockList.transform.Find("Exit");

            Block block = new()
            {
                blockPrefab = tmp,
                enter = enter,
                exit = exit
            };

            blocks.Add(block);
        }


    }

    public void GenerateNextBlock()
    {

    }

    private class Block
    {
        internal GameObject blockPrefab;
        internal Transform enter;
        internal Transform exit;
    }
}
