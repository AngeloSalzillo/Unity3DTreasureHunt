using System.Collections;
using UnityEngine;

public class DestroyBreakable : MonoBehaviour
{
    [Header("Character")]
    public Transform player;
    [Header("Camera")]
    public Transform playerCamera;
    [Header("Whole Crate")]
    public GameObject wholeCrate;
    [Header("Fractured Create")]
    public GameObject fracturedCrate;
    [Header("Audio")]
    public AudioSource crashAudioClip;
    public float delayBeforeFade;
    public float fadeTime;
    Renderer[] fracturedCrateRenderers;
    float t = 0f;


    private void OnTriggerEnter(Collider other)
    {   
        if(other.CompareTag("Attack"))
        {
                fracturedCrate.SetActive(true);
                wholeCrate.SetActive(false);
                crashAudioClip.Play();
                StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        fracturedCrateRenderers = fracturedCrate.GetComponentsInChildren<Renderer>();
        yield return new WaitForSeconds(delayBeforeFade);

        while (t < fadeTime)
        {
            
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            foreach (Renderer renderer in fracturedCrateRenderers)
            {
                renderer.material.color = new Color(renderer.material.color.r,
                                                    renderer.material.color.g, 
                                                    renderer.material.color.b, 
                                                    alpha);
            }
            yield return null;
        }
        fracturedCrate.SetActive(false);
    }
}