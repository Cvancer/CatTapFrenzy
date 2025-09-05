using System.Collections;
using UnityEngine;

public class ShaderWipe : MonoBehaviour
{
    public Material wipeMaterial;
    public float duration = 5f;

    private float timer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Refill());
        }
    }

    private IEnumerator Refill()
    {
        Debug.Log("Before");
        timer = 0f; 
        while (timer < duration)
        {
            timer += Time.deltaTime;
            var progress = Mathf.Clamp01(timer / duration);
            wipeMaterial.SetFloat("_Progress", progress);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("After");
    }
}
