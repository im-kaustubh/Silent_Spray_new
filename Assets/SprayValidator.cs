using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void ValidateSpray(string sprayedGraffitiName)
    {
        string correctName = correctGraffitiPrefab.name + "(Clone)";
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(correctSpraySpot.position, spotSize, 0f);

        bool foundCorrectGraffitiInZone = false;

        foreach (Collider2D col in overlaps)
        {
            if (col.gameObject.name == correctName)
            {
                foundCorrectGraffitiInZone = true;
                break;
            }
        }

        Debug.Log("Sprayed: " + sprayedGraffitiName + " | Needed: " + correctName + " | FoundInZone: " + foundCorrectGraffitiInZone);

        if (foundCorrectGraffitiInZone && sprayedGraffitiName == correctName)
        {
            ShowMonologue("Congrats! You did it.");
            // StartCoroutine(CompleteMission());  //Use for CompleteMission()
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

    // This is for the direct scene changes when player spray at right spot and used right graffiti
    /*IEnumerator CompleteMission()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("NewspaperArea");
    }*/
}
