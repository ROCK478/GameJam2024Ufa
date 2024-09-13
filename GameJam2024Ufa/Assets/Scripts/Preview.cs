using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preview : MonoBehaviour
{
    public AudioClip[] _audio = new AudioClip[9];
    public Sprite[] pictures = new Sprite[10];
    public AudioSource AudioSource;
    public SpriteRenderer _sprite;
    private bool Play = true;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Play)
        {
            Play = false;
            StartCoroutine(Music());
        }
    }

    private IEnumerator Music()
    {
        AudioSource.Play();
        yield return new WaitForSeconds(6.4f);
        _sprite.sprite = pictures[1];
        AudioSource.clip = _audio[1];
        AudioSource.Play();
        yield return new WaitForSeconds(13f);
        _sprite.sprite = pictures[2];
        AudioSource.clip = _audio[2];
        AudioSource.Play();
        yield return new WaitForSeconds(2f);
        _sprite.sprite = pictures[3];
        AudioSource.clip = _audio[3];
        AudioSource.Play();
        yield return new WaitForSeconds(6.2f);
        AudioSource.clip = _audio[4];
        _sprite.sprite = pictures[4];
        AudioSource.Play();
        yield return new WaitForSeconds(4.1f);
        AudioSource.clip = _audio[5];
        AudioSource.Play();
        yield return new WaitForSeconds(14f);
        AudioSource.clip = _audio[6];
        _sprite.sprite = pictures[6];
        AudioSource.Play();
        yield return new WaitForSeconds(5.8f);
        AudioSource.clip = _audio[7];
        _sprite.sprite = pictures[7];
        AudioSource.Play();
        yield return new WaitForSeconds(11.2f);
        AudioSource.clip = _audio[8];
        _sprite.sprite = pictures[8];
        AudioSource.Play();
        yield return new WaitForSeconds(6.6f);
        _sprite.sprite = pictures[9];
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Lvl 3");

    }

}
