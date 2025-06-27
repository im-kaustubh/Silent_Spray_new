using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QTEManager : MonoBehaviour
{
    public Image qteImage;
    public Sprite qteA, qteS, qteD, qteW;

    private KeyCode expectedKey;
    private GraffitiSprayer activeSprayer;

    void Start()
    {
        if (qteImage != null)
            qteImage.gameObject.SetActive(false); // ✅ Hide at game start
    }

    public void BeginQTE(GraffitiSprayer sprayer)
    {
        activeSprayer = sprayer;
        StartCoroutine(QTESequence());
    }

    private IEnumerator QTESequence()
    {
        KeyCode[] keys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };
        expectedKey = keys[Random.Range(0, keys.Length)];

        if (qteImage != null)
        {
            qteImage.sprite = GetSpriteForKey(expectedKey);
            qteImage.gameObject.SetActive(true);
        }

        float timer = 0f;
        bool success = false;

        while (timer < 2f)
        {
            // ❌ Player released 'E' while QTE running
            if (!Input.GetKey(KeyCode.E))
                break;

            if (Input.GetKeyDown(expectedKey))
            {
                success = true;
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (qteImage != null)
            qteImage.gameObject.SetActive(false);

        if (success)
            activeSprayer.OnQTESuccess();
        else
            activeSprayer.OnQTEFail();
    }

    Sprite GetSpriteForKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A: return qteA;
            case KeyCode.S: return qteS;
            case KeyCode.D: return qteD;
            case KeyCode.W: return qteW;
            default: return null;
        }
    }
}
