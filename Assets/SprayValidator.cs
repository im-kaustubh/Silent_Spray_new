using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SprayValidator : MonoBehaviour
{
    public GameObject correctGraffitiPrefab;    // Assign like "Graffiti_3(Clone)"
    public GameObject monologuePanel;
    public TMP_Text monologueText;

    private bool isInCorrectZone = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CorrectSpot"))
        {
            isInCorrectZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CorrectSpot"))
        {
            isInCorrectZone = false;
        }
    }

    public void ValidateSpray(string sprayedGraffitiName)
    {
        string correctName = correctGraffitiPrefab.name + "(Clone)";
        Debug.Log("Sprayed: " + sprayedGraffitiName + " | Needed: " + correctName + " | CorrectZone: " + isInCorrectZone);

        if (isInCorrectZone && sprayedGraffitiName == correctName)
        {
            ShowMonologue("Congrats! You did it.");
            StartCoroutine(CompleteMission());
        }
        else
        {
            ShowMonologue("You are at the wrong place or using wrong graffiti.");
        }

    }

    void ShowMonologue(string message)
    {
        if (monologuePanel != null && monologueText != null)
        {
            monologuePanel.SetActive(true);
            monologueText.text = message;
            Invoke("HideMonologue", 3f);
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

    IEnumerator CompleteMission()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("NewspaperArea");
    }
}
