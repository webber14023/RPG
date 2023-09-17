using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private enum Direction{ up, down, left, right};
    private Direction direction;

    public GameObject doorLeft, doorRight, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;
    
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }


    void Update()
    {
        
    }

    public void SetDoor(int longwayRoomDirection, bool Flip)
    {
        if (Flip)
        {
            direction = (Direction)longwayRoomDirection;
            switch(direction)
            {
                case Direction.up:
                    roomDown = true;
                    break;
                case Direction.down:
                    roomUp = true;
                    break;
                case Direction.left:
                    roomRight = true;
                    break;
                case Direction.right:
                    roomLeft = true;
                    break;
            }
        }
    }
}
