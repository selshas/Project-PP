using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXEmitter : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }

    private void OnEnable()
    {
        audioSource.Play();
        StartCoroutine(ReserveRetrievation());
    }

    IEnumerator ReserveRetrievation()
    {
        yield return new WaitForSecondsRealtime(audioSource.clip.length);

        GetComponent<PoolItem>().Retrieve();
    }
}
