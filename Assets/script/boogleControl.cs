using UnityEngine;
using System.Collections;

public class boogleControl : MonoBehaviour {
    public float moveSpeedx = 0.1f;
    public string type = "";

    private float moveSpeedy = 0.0f;
    private bool isDragged = false;

    private float sizeOfBoogle;
    private float airWallPositionX;
	// Use this for initialization
	void Start () {
        sizeOfBoogle = GetComponent<CircleCollider2D>().radius;
        airWallPositionX = GameObject.Find("AirWall").transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDragged)
        {
            transform.Translate(moveSpeedx, moveSpeedy, 0.0f);
            if (transform.position.x > airWallPositionX - sizeOfBoogle)
            {
                moveSpeedx = -moveSpeedx;
                if(moveSpeedy == 0.0f)
                    moveSpeedy = Random.Range(0.08f, 0.12f);
            }
            if(transform.position.x < -9.6f + sizeOfBoogle)
                moveSpeedx = -moveSpeedx;
            if (transform.position.y > 5.4f - sizeOfBoogle)
                moveSpeedy = -moveSpeedy;
            if (transform.position.y < -5.4f + sizeOfBoogle)
                moveSpeedy = -moveSpeedy;
        }

	}

    void OnMouseDrag()
    {
        isDragged = true;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3 (mouse.x, mouse.y, transform.position.z);
    }

    void OnMouseUp()
    {
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        
        //Whether it's above the bowl
        if(aboveBowl())
        {
            GameObject.Find("Game").GetComponent<Game>().CurrentHits++;
            GameObject.Find("Game").GetComponent<Game>().CurrentScores += GameObject.Find("Game").GetComponent<Game>().CurrentHits;
            GameObject.Find("Game").GetComponent<Game>().PlaySuccess();
        }
        else
        {
            GameObject.Find("Game").GetComponent<Game>().CurrentHits = 0;
            GameObject.Find("Game").GetComponent<Game>().PlayFail();
        }


        Destroy(gameObject, ps.duration);
        GameObject.Find("Game").GetComponent<Game>().CurrentNumberOfBoogles --;

    }


    bool aboveBowl()
    {
        RaycastHit hit;

        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.transform.gameObject.name == "bowl" + type)
                return true;
            else
            {
                return false;
            }
        }
        return false;
    }
}
