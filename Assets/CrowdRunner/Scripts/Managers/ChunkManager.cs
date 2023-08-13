using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [SerializeField] private LevelSO[] levels;

    private GameObject finishLine;

    private Vector3 chunkPosition;
    private Chunk chunkToCreate;
    private Chunk chunkInstance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        GenrateLevel();

        finishLine = GameObject.FindGameObjectWithTag("Finish");
    }

    private void GenrateLevel()
    {
        int currentLevel = GetLevel();

        // Trick to currentLevel never will be greater than levels length.
        currentLevel = currentLevel % levels.Length;

        LevelSO level = levels[currentLevel];

        CreateChunk(level.chunks);
    }

    private void CreateChunk(Chunk[] chunks)
    {
        chunkPosition = Vector3.zero;

        for (int i = 0; i < chunks.Length; i++)
        {
            chunkToCreate = chunks[i];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLength.z / 2;

            chunkInstance =  Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength.z / 2;
        }
    }

    public float GetFinishZ()
    {
        if (finishLine == null)
            Debug.LogError("You must add finish chunk");

        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level");
    }
}
