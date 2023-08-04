using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerStageController : MonoBehaviour
{
    [SerializeField] string _sceneName = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(_sceneName);
    }
}