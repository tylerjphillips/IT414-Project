using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class LevelControllerScript : MonoBehaviour
{
    #region Variable Initilization
    public Camera GameCamera; //Takes a camera object as a parameter, but this is purely for relitive positioning. Assumes the camera has a FOV of 60;
    public Light BackLight;
    public float gradientresolution = 0.05f;
    public float speed = 0.016f; //Speed of WorldChunk spawns;
    public static List<GameObject> BlockList = new List<GameObject>();
    private List<bool[,]> WorldChunkList = new List<bool[,]>();
    private int RandomNumberHolder;
    private IColorCalculator colorCalculator;
    private IColorCalculator lightColorCalculator;
    #endregion

    void Start()
    {
        colorCalculator = new ColorCalculator(1.0f, 0.0f, 0.0f, true, false, true, gradientresolution);
        lightColorCalculator = new ColorCalculator(1.0f, 0.0f, 0.0f, true, false, true, gradientresolution);
        #region Pre-made WorldChunks
        bool[,] ExampleWorldChunk = new bool[,] { { true, true, true, true, true }, { false, true, true, true, false }, { false, false, true, false, false } };
        WorldChunkList.Add(ExampleWorldChunk);
        bool[,] ExampleWorldChunk2 = new bool[,] { { true, true, true, true, true }, { true, false, true, false, true }, { false, false, false, false, false } };
        WorldChunkList.Add(ExampleWorldChunk2);
        #endregion
    }

    void Update()
    {
        if (Time.frameCount % 45.0f == 0)
        {
            BackLight.color = lightColorCalculator.CalculateNextColor();
        }
        if (Time.frameCount % 300.0f == 0) //Create a world chunk every 3 seconds
        {
            RandomNumberHolder = Random.Range(0, WorldChunkList.Count);
            SpawnWorldChunk(WorldChunkList[RandomNumberHolder]);
        }

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
    }

    #region Helper Methods
    void SpawnWorldChunk(bool[,] worldChunk)
    {
        Vector3 CameraPosition = GameCamera.transform.position;
        for (int column = 0; column < worldChunk.GetLength(1); column++)
        {
            Color blockColor = colorCalculator.CalculateNextColor();
            for (int row = 0; row < worldChunk.GetLength(0); row++)
            {
                if(worldChunk[row,column])
                {
                    Vector3 blockPosition = new Vector3(CameraPosition.x + 10.0f + column, CameraPosition.y - 2.2f + row, CameraPosition.z + 5.0f);
                    BlockData blockData = new BlockData(blockColor, blockPosition);
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
