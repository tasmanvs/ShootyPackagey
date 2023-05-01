using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class break_collide_sounds : MonoBehaviour
{
    public AudioClip[] small_sounds;
    public AudioClip[] medium_sounds;
    public AudioClip[] big_sounds;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("package"))
        {
            return;
        }


        float impact = collision.relativeVelocity.magnitude;

        Debug.Log("Impact: " + impact);


        source.pitch = Random.Range(0.6f, 1.4f);
        float volume = Random.Range(0.5f, 1.0f);

        if (impact < 19)
        {
            int sound_index = Random.Range(0, small_sounds.Length);
            source.PlayOneShot(small_sounds[sound_index], volume);
        }
        else if (impact < 50)
        {
            int sound_index = Random.Range(0, medium_sounds.Length);
            source.PlayOneShot(medium_sounds[sound_index], volume);
        }
        else
        {
            int sound_index = Random.Range(0, big_sounds.Length);
            source.PlayOneShot(big_sounds[sound_index], volume);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
