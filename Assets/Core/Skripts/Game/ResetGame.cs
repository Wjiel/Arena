using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetScane()
    {
        SceneManager.LoadScene(0);
    }
}
