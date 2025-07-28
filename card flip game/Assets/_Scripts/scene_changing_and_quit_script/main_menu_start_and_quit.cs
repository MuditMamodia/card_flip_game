using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_menu_start_and_quit : MonoBehaviour
{
    [SerializeField] private GameObject loading_canvas;
    [SerializeField] private GameObject level_selection_canvas;
    [SerializeField] private Image loading_slider;
   

    public void chnage_scene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }

    public void Quit_game()
    {
        Application.Quit();
    }

    public void asyncloader_scene(int scene_id)
    {
        level_selection_canvas.SetActive(false);
        loading_canvas.SetActive(true);
        StartCoroutine(loading_level(scene_id));
    }
    IEnumerator loading_level(int scene_id)
    {
        AsyncOperation loading_operaction = SceneManager.LoadSceneAsync(scene_id);
        while (!loading_operaction.isDone)
        {
            float progressvalue = Mathf.Clamp01(loading_operaction.progress / 0.9f);
            loading_slider.fillAmount = progressvalue;
            yield return null;
        }
    }

    
}
