using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fadeout : MonoBehaviour
{
   
        [SerializeField] private RawImage fadeImage;
        [SerializeField] private float delay = 3f;
        [SerializeField] private float fadeDuration = 1f;

        void Start()
        {
            StartCoroutine(FadeRoutine());
        }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Clamp01(1f - (t / fadeDuration)); // ahora baja de 1 a 0
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;

        gameObject.SetActive(false);
    }

}
