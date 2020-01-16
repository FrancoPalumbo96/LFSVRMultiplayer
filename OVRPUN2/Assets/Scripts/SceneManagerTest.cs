using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        SceneManager.LoadScene("DobleOVR2", LoadSceneMode.Additive);
    }
}
