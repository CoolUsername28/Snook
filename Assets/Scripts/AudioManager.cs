using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSourcePf;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySoundClip(AudioClip audioClip, Transform t, float volume)
    {
        AudioSource audioSource = Instantiate(audioSourcePf, t.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float lenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, lenght);
    }

}
