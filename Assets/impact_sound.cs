using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impact_sound : MonoBehaviour
{
    public AudioClip[] sounds;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        float volume = Mathf.Min(1f, collision.relativeVelocity.magnitude / 10.0f);
        source.pitch = Random.Range(0.6f, 1.4f);

        int sound_index = Random.Range(0, sounds.Length);
        source.PlayOneShot(sounds[sound_index], volume);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
