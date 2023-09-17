using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction{ up, down, left, right};
    public Direction direction;

    [Header("房間訊息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject longwayRoom;

    [Header("位置控制")]
    public Transform genratorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public List<Room> rooms = new List<Room>();
    void Start()
    {
        for (int i = 0; i < roomNumber-1; i++)
        {
            rooms.Add(Instantiate(roomPrefab, genratorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改變Point位置
            ChangePointPos();
        }

        longwayRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {
            if (room.transform.position.sqrMagnitude > longwayRoom.transform.position.sqrMagnitude)
            {
                longwayRoom = room.gameObject;
            }
            SetupRoom(room, room.transform.position);
        }
        GenratorlongwayRoom();
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        rooms[roomNumber - 1].GetComponent<SpriteRenderer>().color = endColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePointPos()
    {
        do{
            direction = (Direction)Random.Range(0, 4);

            switch(direction)
            {
                case Direction.up:
                    genratorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    genratorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    genratorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    genratorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        }while (Physics2D.OverlapCircle(genratorPoint.position, 0.2f, roomLayer));
    }

    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
    }

    public void GenratorlongwayRoom()
    {
        Room room = longwayRoom.GetComponent<Room>();
        List<bool> doorDeractions = new List<bool>{room.roomUp, room.roomDown, room.roomLeft, room.roomRight};
        genratorPoint.position = longwayRoom.transform.position;
        for (int i = 0; i < 4; i++)
        {
            if(!doorDeractions[i])
            {
                direction = (Direction)i;
                Debug.Log(i);
                break;
            }
        }
        switch(direction)
            {
                case Direction.up:
                    genratorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    genratorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    genratorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    genratorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        
        rooms.Add(Instantiate(roomPrefab, genratorPoint.position, Quaternion.identity).GetComponent<Room>());
        rooms[roomNumber-1].SetDoor((int)direction, true);
        SetupRoom(longwayRoom.GetComponent<Room>(), longwayRoom.transform.position);
    }
}
