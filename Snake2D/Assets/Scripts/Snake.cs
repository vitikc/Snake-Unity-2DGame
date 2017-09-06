using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    LEFT,RIGHT,UP,DOWN
}

public class Snake : MonoBehaviour {
    public FoodGenerator fg;

    public int length = 3;
    public int x = 0;
    public int y = 0;
    public GameObject gameOver;
    public GameObject snake;
    private bool cooldown = false;
    private bool isPlaying = false;
    private Direction direction = Direction.RIGHT;
    private Direction currentDirection = Direction.RIGHT;
    private ArrayList snakeParts = new ArrayList();
    // Use this for initialization
	void Start () {
        x = Constants.OffsetWidth + length-1;
        y = Constants.OffsetHeigth;
		for(int i = 0; i < length; i++)
        {
            GameObject part = Instantiate(snake, new Vector3(x - length + 1 + i, y), Quaternion.identity, this.transform);
            part.name = "snake_"+(length -1 - i);
            snakeParts.Add(part);
        }
        isPlaying = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            KeyboardInput();
            length = snakeParts.Count;
            if (!cooldown)
            {
                StartCoroutine(Move());
            }
            if (isOnFood())
            {
                Destroy(fg.generatedFood);
                fg.isNeedToGenerate = true;
                AddPart();
            }
            if (isOnOwnPart())
            {
                gameOver.SetActive(true);
                isPlaying = false;
            }
        }
    }

    void KeyboardInput()
    {
        
        if (Input.GetKeyUp(KeyCode.RightArrow)&& currentDirection!=Direction.LEFT)
        {
            direction = Direction.RIGHT;
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow) && currentDirection != Direction.RIGHT)
        {
            direction = Direction.LEFT;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && currentDirection != Direction.DOWN)
        {
            direction = Direction.UP;
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow)&& currentDirection != Direction.UP)
        {
            direction = Direction.DOWN;
        }
        
    }

    void MoveAllParts()
    {
        for (int i = 0; i < length-1; i++)
        {
            ((GameObject)snakeParts[i]).transform.position = ((GameObject)snakeParts[i+1]).transform.position;
        }
    }
    IEnumerator Move()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.1f);
        switch (direction)
        {
            case Direction.RIGHT:
                currentDirection = Direction.RIGHT;
                Right();
                break;
            case Direction.LEFT:
                currentDirection = Direction.LEFT;
                Left();
                break;
            case Direction.UP:
                currentDirection = Direction.UP;
                Up();
                break;
            case Direction.DOWN:
                currentDirection = Direction.DOWN;
                Down();
                break;
        }
        if (isOnOwnPart())
        {
            Debug.Log("TRUE");
        }
        cooldown = false;
 
    }
    void Right()
    {
        x++;
        if (x == Constants.OffsetWidth + Constants.Width)
        {
            x = Constants.OffsetWidth;
        }
        MoveAllParts();
        Vector3 moveTo = ((GameObject)snakeParts[length-1]).transform.position;
        moveTo.x = x;
        ((GameObject)snakeParts[length-1]).transform.position = moveTo;
    }
    void Left()
    {   x--;
        if (x == Constants.OffsetWidth - 1)
        {
            x = Constants.OffsetWidth + Constants.Width-1;
        }
        MoveAllParts();
        Vector3 moveTo = ((GameObject)snakeParts[length - 1]).transform.position;
        moveTo.x = x;
        ((GameObject)snakeParts[length - 1]).transform.position = moveTo;
    }
    void Up()
    {
        y++;
        if (y == Constants.OffsetHeigth + 1)
        {
            y = Constants.OffsetHeigth - Constants.Heigth + 1;
        }
        MoveAllParts();
        Vector3 moveTo = ((GameObject)snakeParts[length - 1]).transform.position;
        moveTo.y = y;
        ((GameObject)snakeParts[length - 1]).transform.position = moveTo;
    }
    void Down()
    {
        y--;
        if (y == Constants.OffsetHeigth - Constants.Heigth)
        {
            y = Constants.OffsetHeigth;
        }
        MoveAllParts();
        Vector3 moveTo = ((GameObject)snakeParts[length - 1]).transform.position;
        moveTo.y = y;
        ((GameObject)snakeParts[length - 1]).transform.position = moveTo;   
    }
    void AddPart()
    {
        Vector3 position = ((GameObject)snakeParts[0]).transform.position;
        GameObject part = Instantiate(snake, position, Quaternion.identity, this.transform);
        part.name = "snake_" + length;
        snakeParts.Insert(0, part);
        length++;
    }
    bool isOnFood()
    {
        if(fg.generatedFoodPosition.x == x && fg.generatedFoodPosition.y == y)
        {
            return true;
        }
        return false;
    }
    bool isOnOwnPart()
    {
        foreach(GameObject part in snakeParts)
        {
            if (part.name.Equals("snake_0")) continue;
            if (part.transform.position.x == x && part.transform.position.y == y)
            {
                return true;
            }
        }
        return false;
    }
}