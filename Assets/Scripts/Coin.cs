using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected; //Static = only one instance of this variable across all game objects
    public AudioClip[] _audioClips;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player == null) {
            return;
        }

        CoinsCollected++; //Add to coin counter
        ScoreSystem.Add(100); //Add 100 points

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        int _randomNum = Random.Range(1, 10);
        GetComponent<AudioSource>().PlayOneShot(RandomAudioClip());
    }

    private AudioClip RandomAudioClip()
    {
        return _audioClips[Random.Range(0, _audioClips.Length)];
    }
}
