using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator SharedInstance;

    
    /// <summary>
    /// Input list of blocks from an Inspector
    /// </summary>
    [SerializeField] private List<GameObject> levelBlocksList;
    [SerializeField] private int levelLength;

    private int cb = 0;
    private int currentBlock
    {
        get
        {
            return cb;
        }
        set
        {
            cb = value % middlewareBlocks.Count;
        }
    }

    private List<Block> startBlocks;
    private List<Block> middlewareBlocks;
    private List<Block> endBlocks;
    private Queue<Block> levelQueue;

    private Transform nextBlockPos;

    private void Awake()
    {
        SharedInstance = this;
        startBlocks = new();
        middlewareBlocks = new();
        endBlocks = new();
        levelQueue = new();
        
        nextBlockPos = new GameObject().transform;
    }

    private void Start()
    {
        LoadBlocksFromInspector();

        GenerateNextBlock(GetRandomItem(startBlocks));
        for (int i = 0; i < 2; i++)
            GenerateNextBlock(middlewareBlocks[currentBlock++]);
        
    }

    public void BlockStep()
    {
        GenerateNextBlock(middlewareBlocks[currentBlock++]);
        DeactivatePrevBlock();
    }

    private void GenerateNextBlock(Block block)
     {
        if (block.Enter is null)
            block.blockPrefab.transform.position = Vector3.zero;
        else
            block.blockPrefab.transform.position = nextBlockPos.position - block.Enter.position;

        if (block.Exit is null)
            nextBlockPos.position = Vector3.zero;
        else
            nextBlockPos.position = block.Exit.position;
        
        block.blockPrefab.SetActive(true);
        levelQueue.Enqueue(block);
    }

    private void DeactivatePrevBlock()
    {
        var block = levelQueue.Dequeue();
        block.blockPrefab.SetActive(false);
    }

    private T GetRandomItem<T>(List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
    
    private void LoadBlocksFromInspector()
    {
        GameObject tmp;
        foreach (GameObject blockList in levelBlocksList)
        {
            tmp = Instantiate(blockList, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            tmp.SetActive(false);

            Block block = new()
            {
                blockPrefab = tmp
            };
            
            if (!block.Exit)
                endBlocks.Add(block);
            else if (block.Enter is null)
                startBlocks.Add(block);
            else
                middlewareBlocks.Add(block);
        }
    }

    private class Block
    {
        public GameObject blockPrefab;
        public Transform Enter => blockPrefab.transform.Find("Enter");
        public Transform Exit => blockPrefab.transform.Find("Exit");
    }
}
