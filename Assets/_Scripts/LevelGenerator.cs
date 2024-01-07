using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingRoomPrefab;
    [SerializeField] private GameObject endingRoomPrefab;
    [SerializeField, Tooltip("Do not include starting room and ending room")] private List<GameObject> roomPrefabs;
    [SerializeField, Tooltip("1 based counting")] private int level = 1;
    [SerializeField] private int gridWidth = 4;
    [SerializeField] private int gridHeight = 4;
    [SerializeField] private int roomBaseDimension = 30;
    private int baseRoomCount;
    private int additionalRoomCountPerLevel;

    private Vector3 startingPos;
    // Start is called before the first frame update
    private void Awake()
    {
        this.baseRoomCount = 6;
        this.additionalRoomCountPerLevel = 2;
    }

    void Start()
    {
        List<Vector2> rooms = this.GenerateRoomPositions();
        if (this.startingRoomPrefab != null && this.endingRoomPrefab != null && this.roomPrefabs.Count > 0)
        {
            this.InstantiateRoomPrefabs(rooms);
        }

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = startingPos;
        player.GetComponent<CharacterController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generates room positions based on this generators grid size, and level. Positions are 0 based.
    /// </summary>
    /// <returns>A list of positions</returns>
    public List<Vector2> GenerateRoomPositions()
    {
        int totalRoomCount = this.baseRoomCount + ((this.level - 1) * this.additionalRoomCountPerLevel);

        List<Vector2> availableRooms = new List<Vector2>();
        List<Vector2> resultRooms = new List<Vector2>();

        //initial position
        availableRooms.Add(new Vector2(1, 2));

        while (resultRooms.Count < totalRoomCount)
        {
            int choice = Random.Range(0, availableRooms.Count);
            Vector2 pos = availableRooms[choice];
            resultRooms.Add(pos);
            availableRooms.RemoveAt(choice);
            // add adjacent cardinal positions
            Vector2 up = new Vector2(pos.x, Mathf.Max(pos.y - 1, 0));
            Vector2 down = new Vector2(pos.x, Mathf.Min(pos.y + 1, gridHeight - 1));
            Vector2 left = new Vector2(Mathf.Max(pos.x - 1, 0), pos.y);
            Vector2 right = new Vector2(Mathf.Min(pos.x + 1, gridWidth - 1), pos.y);
            List<Vector2> adjacentCardinals = new List<Vector2>();
            adjacentCardinals.Add(up);
            adjacentCardinals.Add(down);
            adjacentCardinals.Add(left);
            adjacentCardinals.Add(right);
            foreach (var cardinal in adjacentCardinals)
            {
                // make sure we are not adding duplicates
                if (!availableRooms.Contains(cardinal) && !resultRooms.Contains(cardinal))
                {
                    availableRooms.Add(cardinal);
                }
            }
        }

        return resultRooms;
    }

    public void InstantiateRoomPrefabs(List<Vector2> roomPositions)
    {
        Vector3 startingRoomPosition = Vector3.zero;

        int totalRoomCount = roomPositions.Count;
        List<GameObject> roomPrefabsDupe = new List<GameObject>(this.roomPrefabs);

        for (int i = 0; i < totalRoomCount; i += 1)
        {
            int choice = Random.Range(0, roomPositions.Count);
            Vector2 pos = roomPositions[choice];
            roomPositions.RemoveAt(choice);

            GameObject roomPrefab;

            if (i == 0)
            {
                roomPrefab = this.startingRoomPrefab;
                //player.transform.position = new Vector3(pos.x * this.roomBaseDimension, player.transform.position.y, pos.y * this.roomBaseDimension);
                startingPos = new Vector3(pos.x * this.roomBaseDimension, player.transform.position.y, pos.y * this.roomBaseDimension);
            }
            else if (i == totalRoomCount - 1)
            {
                roomPrefab = this.endingRoomPrefab;
            }
            else
            {
                choice = Random.Range(0, roomPrefabsDupe.Count);
                roomPrefab = roomPrefabsDupe[choice];
                roomPrefabsDupe.RemoveAt(choice);
            }

            Instantiate(roomPrefab, new Vector3(pos.x * this.roomBaseDimension, 0, pos.y * this.roomBaseDimension), Quaternion.identity);
        }
    }
}
