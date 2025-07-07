using UnityEngine;
using TMPro;

public class SprayValidator : MonoBehaviour
{
    [Header("Graffiti Validation")]
    public GameObject correctGraffitiPrefab;
    public Transform correctSpraySpot;
    public Vector2 spotSize = new Vector2(5f, 5f);

    [Header("UI References")]
    public GameObject monologuePanel;
    public TMP_Text monologueText;

    [Header("Audio Clips")]
    public AudioClip wrongSpotClip;
    public AudioClip wrongGraffitiClip;
    public AudioClip correctSprayClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (monologuePanel != null)
            monologuePanel.SetActive(false);
    }

    public void ValidateSpray(GameObject sprayedGraffiti)
    {
        string sprayedName = sprayedGraffiti.name;
        string correctName = correctGraffitiPrefab.name + "(Clone)";
        Vector2 sprayPos = sprayedGraffiti.transform.position;

        bool isInCorrectZone =
            (sprayPos.x >= correctSpraySpot.position.x - spotSize.x / 2f &&
             sprayPos.x <= correctSpraySpot.position.x + spotSize.x / 2f &&
             sprayPos.y >= correctSpraySpot.position.y - spotSize.y / 2f &&
             sprayPos.y <= correctSpraySpot.position.y + spotSize.y / 2f);

        Debug.Log("Sprayed: " + sprayedName + " | Needed: " + correctName + " | InZone: " + isInCorrectZone);

        if (isInCorrectZone && sprayedName == correctName)
        {
            PlaySound(correctSprayClip);
            ShowMonologue("Congrats! You did it");
        }
        else if (isInCorrectZone && sprayedName != correctName)
        {
            PlaySound(wrongGraffitiClip);
            ShowMonologue("You are at the right spot but used wrong graffiti");
        }
        else
        {
            PlaySound(wrongSpotClip);
            ShowMonologue("You are at the wrong place");
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void ShowMonologue(string message)
    {
        if (monologuePanel != null && monologueText != null)
        {
            monologuePanel.SetActive(true);
            monologueText.text = message;
            Invoke(nameof(HideMonologue), 3f);
        }
        else
        {
            Debug.Log(message); // fallback
        }
    }

    void HideMonologue()
    {
        if (monologuePanel != null)
            monologuePanel.SetActive(false);
    }
}
