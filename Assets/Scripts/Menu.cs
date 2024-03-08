using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void batdau()
    {
        SceneManager.LoadScene(2);
    }
    public void huongdan()
    {
        SceneManager.LoadScene(1);
    }
    public void thoat()
    {
        Application.Quit();
    }
    public void back()
    {
        SceneManager.LoadScene(0);
    }
}
