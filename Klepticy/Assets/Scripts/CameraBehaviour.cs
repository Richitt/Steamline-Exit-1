using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject line;

    float ySpeed = 0;
    public List<GameObject> walls = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCameraSmooth();
        UpdateWalls();
        TrackHighest();
    }

    // track the highest object
    private void TrackHighest()
    {
        // track the highest
        float maxYPos = -100;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Trash"))
        {
            TrashBehaviour trash = gameObject.GetComponent<TrashBehaviour>();
            if (trash.landed)
            {
                Bounds bounds = gameObject.GetComponent<Collider2D>().bounds;
                float yPos = (bounds.center + bounds.extents).y;
                if (maxYPos < yPos)
                {
                    maxYPos = yPos;
                }
            }
        }
        line.transform.position = new Vector3(line.transform.position.x, maxYPos, line.transform.position.z);
    }

    // if the camera bounds exceed the walls
    private void UpdateWalls()
    {
        // get camera bounds
        Bounds cameraBounds = OrthographicBounds();
        float cameraTop = (cameraBounds.center + cameraBounds.extents).y;
        // wall bounds to compare to camera bounds
        for (int i = 0; i < walls.Count; i++)
        {
            GameObject wall = walls[i];
            Bounds wallBounds = wall.GetComponent<BoxCollider2D>().bounds;
            float wallTop = (wallBounds.center + wallBounds.extents).y;
            // add more walls if necessary
            if (cameraTop > wallTop)
            {
                walls[i] = Instantiate(wall,
                    new Vector3(wall.transform.position.x,
                    wall.transform.position.y + (wallBounds.extents.y * 2),
                    wall.transform.position.z), Quaternion.identity);
            }
        }
    }

    // gets camera bounds
    private Bounds OrthographicBounds()
    {
        float screenAspect = Screen.width / Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(
            Camera.main.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    private void MoveCameraSmooth()
    {
        // follow the specified object
        // to smooth the following, use a differential equation
        // where acceleration makes spd approach targetSpd = k1 * distanceToTravel
        // acceleration is k2 * spdDifference
        float targetPos = Mathf.Max(0, line.transform.position.y);
        float currentPos = transform.position.y;
        float k1 = 0.1f;
        float targetSpd = k1 * (targetPos - currentPos);
        float k2 = 0.1f;
        float acc = k2 * (targetSpd - ySpeed);
        // move the y position of the camera to the calculated value
        ySpeed += acc;
        currentPos += ySpeed;
        transform.position = new Vector3(transform.position.x,
            currentPos, transform.position.z);
    }
}
