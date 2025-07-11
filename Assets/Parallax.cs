using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camera;
    public float relativeMovement = 0.3f;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(camera.position.x * relativeMovement, transform.position.y);
    }
}
