using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAutoDestroyer : MonoBehaviour
{
    private AudioClip clip;

    // Start is called before the first frame update
   private void Start()
    {
        clip = GetComponent<AudioSource>().clip;
        Invoke(nameof(DestroySelf), clip.length);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}