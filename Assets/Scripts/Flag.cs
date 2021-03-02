using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>(); //Get the Player component

        if (player == null) { //If player isn't the collider
            return; //Exit early
        }

        //Play flag waving
        var animator = GetComponent<Animator>(); //Get the Animator
        animator.SetTrigger("Raise"); //Trigger the Raise animation

        StartCoroutine(LoadLevelAfterDelay());
        
    }

    IEnumerator LoadLevelAfterDelay()
    {
        PlayerPrefs.SetInt(_sceneName + "Unlocked", 1);
        yield return new WaitForSeconds(1f); //yield return for IEnumerator return syntax
        //Load new level
        SceneManager.LoadScene(_sceneName);
        //Worth noting code beyond this line won't actually function.  Loading a new scene with a new flag (or other code)
    }
}
