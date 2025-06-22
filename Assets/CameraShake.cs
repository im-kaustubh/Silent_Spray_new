using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 0.1f;

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    private System.Collections.IEnumerator ShakeRoutine()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
