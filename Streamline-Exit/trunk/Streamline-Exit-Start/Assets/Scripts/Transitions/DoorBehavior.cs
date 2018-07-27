using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    public string targetScene;
    public Vector2 targetPosition;

    bool active;
    void Start()
    {
        active = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure collided with a player, this is active, and no SceneTransitions exist
        if (collision.tag == "Player" && active && GameObject.FindGameObjectWithTag("SceneTransition") == null)
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            // can only activate once
            active = false;
            // get direction from door to center of screen
            Vector2 direction = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(0.5f, 0.5f) * screenSize);
            // cardinally normalize the direction vector
            SceneTransitions.Side side = SceneTransitions.ToSide(direction);
            // go to the given scene in the map
            SceneTransition trans = SceneTransitions.Transition<SlideTransition>(new SceneTransitions.Time(1f, 0.25f, 1f), targetScene, targetPosition);
            trans.side = side;
        }
    }
}