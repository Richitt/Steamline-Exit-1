using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject line;

    float ySpeed = 0;
    public List<GameObject> walls = new List<GameObject>();

    public static Vector3 zipperTarget = Vector3.zero;

    List<Checkpoint> checkpoints = new List<Checkpoint>();

    // Use this for initialization
    void Start ()
    {
        checkpoints = new List<Checkpoint>()
        {
            new Checkpoint(-4, "This place really isn't up my alley, let's get out of here!"),
            new Checkpoint(5, "Up and up!"),
            new Checkpoint(14, "Look at all this glorious trash!"),
            new Checkpoint(23, "I can almost see the sun!"),
            new Checkpoint(32, "I'm flying!"),
            new Checkpoint(41, "Thanks for playing! This is the end of the Madhacks Demo!"),
        };
    }

    // Update is called once per frame
    void Update ()
    {
        MoveCameraSmooth();
        UpdateWalls();
        TrackHighest();
        HandleCheckpoints();
    }

    // say dialogue every x distance up
    private void HandleCheckpoints()
    {
        GameObject zipper = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < checkpoints.Count; i++)
        {
            // for every checkpoint
            // display the checkpoint's dialogue if you haven't seen it and dialogue is free.
            if (zipper.transform.position.y >= checkpoints[i].height && !checkpoints[i].activated)
            {
                if (!UIBehavior.CheckDialogue())
                {
                    UIBehavior.DisplayDialogue(checkpoints[i].display);
                    checkpoints.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    // it's called a checkpoint, but there are none.
    // you just get a little bit of dialogue instead.
    // have fun talking to a raccoon you silly little gray frienderino.
    struct Checkpoint
    {
        public int height;
        public string display;
        public bool activated;
        public Checkpoint(int height, string display)
        {
            this.height = height;
            this.display = display;
            activated = false;
        }
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
                Collider2D trashCollider = gameObject.GetComponent<Collider2D>();
                Bounds bounds = trashCollider.bounds;
                float yPos = (bounds.center + bounds.extents).y;
                if (maxYPos < yPos)
                {
                    maxYPos = yPos;
                    // if it's the highest so far, find x and y
                    PolygonCollider2D polygon = trashCollider as PolygonCollider2D;
                    if (polygon != null)
                    {
                        foreach (Vector3 localPoint in polygon.points)
                        {
                            Vector3 point = polygon.transform.TransformPoint(localPoint);
                            // if you find the highest y position, then save its x position also
                            if (Mathf.Abs(point.y - maxYPos) <= 0.001)
                            {
                                zipperTarget = point;
                                zipperTarget.z = -10;
                            }
                        }
                    }
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
