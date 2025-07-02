using UnityEngine;

public class SprayController : MonoBehaviour
{
    public KeyCode sprayKey = KeyCode.E;
    [SerializeField] private AudioSource spraySound;

    private bool isSpraying = false;

    void Update()
    {
        if (Input.GetKey(sprayKey))
        {
            if (!isSpraying)
            {
                isSpraying = true;
                spraySound?.Play();
            }
        }
        else
        {
            if (isSpraying)
            {
                isSpraying = false;
                spraySound?.Stop();
            }
        }
    }
}
