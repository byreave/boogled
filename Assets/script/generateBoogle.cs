using UnityEngine;
using System.Collections;

public class generateBoogle : MonoBehaviour {

    public GameObject red;
    public GameObject blue;
    public GameObject yellow;
    public float redLane = 3.33f; //Based on the postion of bowl
    public float blueLane = -3.33f;
    public float yellowLane = 0.0f;

    public float x = -9.6f; //X-axis position

    public int numberOfBoogles = 12; //In the same scene
    private int currentNumOfBoogles = 0;
    private float sizeOfBoogles = 0.0f;
    private int numPerSec = 2; //how many boogles to generate in one sec
    private int count = 0;
	// Use this for initialization
	void Start () {
        sizeOfBoogles = 0.65f;
        numberOfBoogles = GameObject.Find("Game").GetComponent<Game>().TotalNumberOfBoogles;
        x = x + sizeOfBoogles;
        while(numberOfBoogles%3 != 0)
        {
            numberOfBoogles++;
        }
        GameObject.Find("Game").GetComponent<Game>().TotalNumberOfBoogles = numberOfBoogles;
	}
	
	// Update is called once per frame
	void Update () {
        currentNumOfBoogles = GameObject.Find("Game").GetComponent<Game>().CurrentNumberOfBoogles;
        if (currentNumOfBoogles < numberOfBoogles)
            count++;
        if(count == Application.targetFrameRate / numPerSec)
        {
            int type = Random.Range(0, 2); //two kinds on one lane
            int lane = Random.Range(0, 3); //three lanes

            if(lane == 0)
            {
                if (type == 0)
                    CreateBoogle("Yellow", redLane);
                else
                    CreateBoogle("Blue", redLane);
            }
            else if(lane == 1)
            {
                if (type == 0)
                    CreateBoogle("Red", yellowLane);
                else
                    CreateBoogle("Blue", yellowLane);
            }
            else
            {
                if (type == 0)
                    CreateBoogle("Red", blueLane);
                else
                    CreateBoogle("Yellow", blueLane);
            }

            count = 0;

            currentNumOfBoogles++;

            GameObject.Find("Game").GetComponent<Game>().CurrentNumberOfBoogles = currentNumOfBoogles;
        }
	}

    void CreateBoogle(string type, float lane)
    {
        switch(type)
        {
            case "Red":
                Instantiate(red, new Vector3(x, lane, -1.0f), Quaternion.identity);
                break;
            case "Blue":
                Instantiate(blue, new Vector3(x, lane, -1.0f), Quaternion.identity);
                break;
            case "Yellow":
                Instantiate(yellow, new Vector3(x, lane, -1.0f), Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
