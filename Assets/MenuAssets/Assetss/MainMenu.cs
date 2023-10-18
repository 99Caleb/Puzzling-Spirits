using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
public void playGame ()
 {
    SceneManager.LoadScene("Level1");
 }
 public void playLevel1 ()
 {
    SceneManager.LoadScene("Level1");
 }
 public void playLevel2 ()
 {
    SceneManager.LoadScene("Level2");
 }
 public void playLevel3 ()
 {
    SceneManager.LoadScene("Level3");
 }
 public void playLevel4 ()
 {
    SceneManager.LoadScene("Level4");
 }
 public void playLevel5 ()
 {
    SceneManager.LoadScene("Level5");
 }
public void QuitGame()
{
    Application.Quit();
}
}
