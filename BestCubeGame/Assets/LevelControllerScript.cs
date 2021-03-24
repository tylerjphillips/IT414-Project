using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using Assets.Assets;

public class LevelControllerScript : MonoBehaviour
{
    #region Variable Initilization
    public Camera GameCamera; //Takes a camera object as a parameter, but this is purely for relitive positioning. Assumes the camera has a FOV of 60;
    public Light BackLight;
    public Light LeftLight;
    public Light RightLight;
    public float gradientresolution = 0.05f;
    public float speed = 0.016f; //Speed of WorldChunk spawns;
    public static List<GameObject> BlockList = new List<GameObject>();
    private List<bool[,]> WorldChunkList = new List<bool[,]>();
    private int RandomNumberHolder;
    private IColorCalculator leftcolorCalculator;
    private IColorCalculator rightcolorCalculator;
    private IColorCalculator lightColorCalculator;
    private IBlockDataFactory blockFactory = new BlockDataFactory();
    #endregion

    void Start()
    {
        leftcolorCalculator = new ColorCalculator(1.0f, 0.0f, 0.0f, true, true, false, gradientresolution);
        rightcolorCalculator = new ColorCalculator(1.0f, 0.0f, 0.0f, true, true, false, gradientresolution);
        lightColorCalculator = new ColorCalculator(1.0f, 0.0f, 0.0f, true, false, true, gradientresolution);
        #region Pre-made WorldChunks
        bool[,] WorldChunk1 = new bool[,] { { true, true, true, true, true }, { false, true, true, true, false }, { false, false, true, false, false } };
        WorldChunkList.Add(WorldChunk1);
        bool[,] WorldChunk2 = new bool[,] { { true, true, true, true, true }, { true, false, true, false, true }, { false, false, false, false, false } };
        WorldChunkList.Add(WorldChunk2);
        bool[,] WorldChunk3 = new bool[,] { { true, true, true, true, true }, { false, true, true, true, true }, { false, false, true, true, true }, { false, false, false, true, true }, { false, false, false, false, true } };
        WorldChunkList.Add(WorldChunk3);

        bool[,] SpawnPlatform = new bool[,] { { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true } };
        SpawnWorldChunk(SpawnPlatform, 16);
        #endregion
    }

    void Update()
    {
        if (Time.frameCount % 45.0f == 0)
        {
            BackLight.color = lightColorCalculator.CalculateNextColor();
            RightLight.color = rightcolorCalculator.CalculateNextColor();
            LeftLight.color = leftcolorCalculator.CalculateNextColor();
        }
        if (Time.frameCount % 300.0f == 0) //Create a world chunk every 3 seconds
        {
            RandomNumberHolder = Random.Range(0, WorldChunkList.Count);
            SpawnWorldChunk(WorldChunkList[RandomNumberHolder]);
        }

        #region Block Culling
        for (int i = 0; i < BlockList.Count; i++) //Keep track of the blocks
        {
            GameObject block = BlockList[i];
            if (block.transform.position.x <= GameCamera.transform.position.x - 10) //Check if any cubes need to be removed
            {
                DestroyBlock(block, i);
            }
            else
            {
                Vector3 currentPosition = block.transform.position;
                block.transform.position = new Vector3(currentPosition.x-speed, currentPosition.y, currentPosition.z); //Move each cube to the left
            }
        }
        #endregion
    }

    #region Helper Methods
    void SpawnWorldChunk(bool[,] worldChunk, float xOffset = 0)
    {
        Vector3 CameraPosition = GameCamera.transform.position;
        for (int column = 0; column < worldChunk.GetLength(1); column++)
        {
            for (int row = 0; row < worldChunk.GetLength(0); row++)
            {
                if(worldChunk[row,column])
                {
                    Vector3 blockPosition = new Vector3(CameraPosition.x + 10.0f + column - xOffset, CameraPosition.y - 2.7f + row, CameraPosition.z + 5.0f);
                    BlockData blockData = blockFactory.CreateBlockData(Color.black, blockPosition);
                    CreateBlock(blockData);
                }
            }
        }
    }

    void CreateBlock(BlockData blockData)
    {
        //Setup the cube object
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Material matertial = block.GetComponent<Renderer>().material;

        //Set its color
        matertial.color = blockData.BlockColor;

        //Set its position and velocity
        block.transform.position = blockData.BlockPosition;

        //Add the block to the master list
        BlockList.Add(block);
    }

    void DestroyBlock(GameObject block, int blockIndex)
    {
        BlockList.RemoveAt(blockIndex);
        Destroy(block);
    }
    #endregion
}
