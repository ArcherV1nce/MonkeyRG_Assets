using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelStart : MonoBehaviour
{
    Collider2D trigger;

    private void Awake()
    {
        trigger = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerTag")
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(scene);
    }

}
