using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_start_and_quit : MonoBehaviour
{
    public void chnage_scene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }

    public void Quit_game()
    {
        Application.Quit();
    }
}
