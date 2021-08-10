using UnityEngine;

public class AudioPlayerManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    private int lastAudioIndex , currentAudioIndex;

    private void audioQueueRandomizer()
    {
        int[] a = {0, 1, 2, 3 };

        Shuffle(a);
        foreach (int value in a)
        {
            Debug.Log(value);
        }

        AudioClip[] arr = new AudioClip[audioClipArray.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = audioClipArray[a[i]];
        }

        audioClipArray = arr;
        currentAudioIndex = 0;
    }

    private static AudioPlayerManager instance = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        lastAudioIndex = Random.Range(1, audioClipArray.Length);
        audioQueueRandomizer();
        audioSource.clip = audioClipArray[currentAudioIndex];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if(currentAudioIndex < audioClipArray.Length)
            {
                currentAudioIndex++;
                audioSource.clip = audioClipArray[currentAudioIndex];
                audioSource.Play();
            }
            else
            {
                audioQueueRandomizer();
            }

        }

    }

    void Shuffle(int[] array)
    {
        int r;
        r = Random.Range(1, audioClipArray.Length);
        while (r == lastAudioIndex)
        {
            r = Random.Range(1, audioClipArray.Length);
        }

        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            r = Random.Range(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }

        if (Random.Range(0, 4) != 2)
        {
            int t2 = array[0];
            array[0] = array[p - 1];
            array[p - 1] = t2;
        }

        lastAudioIndex = r;
    }
}