using System.Collections;
using UnityEngine;

public class DroneDetector : MonoBehaviour
{
    private float hitCooldown = 2f;
    private float lastHitTime = -10f;
    private AudioSource droneAlarm;

    private void Start()
    {
        droneAlarm = this.GetComponent<AudioSource>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHide playerHide = other.GetComponent<PlayerHide>();
        if (playerHide != null && playerHide.isHiding)
        {
            Debug.Log("Player is hiding — safe from drone");
            return;
        }

        if (Time.time - lastHitTime < hitCooldown) return;
        lastHitTime = Time.time;

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {

            if(droneAlarm == null)
            {
                print("no alarm found");
            }
            else
            {
                StartCoroutine(playAlarm());
            }
                
            Debug.Log("Player caught by drone — Taking damage");
            playerHealth.TakeDamage();
        }
        else
        {
            Debug.LogWarning("PlayerHealth not found!");
        }

        IEnumerator playAlarm(){
            droneAlarm.Play();
            yield return new WaitForSeconds(1);
            droneAlarm.Stop();
        }

    }
}
