using static System.Convert;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Active Menu")]
    public GameObject isActive;
    public GameObject isInactive;
    public TextMeshProUGUI title;
    public string titleAddon;
    
    [Header("Gamemode Description")]
    public TextMeshProUGUI descText;
    public string desc;

    void Start()
    {
        // Initialize
        if (!PlayerPrefs.HasKey("Timer"))
        {
            PlayerPrefs.SetInt("Timer", 0);
        }

        if (descText!=null)
        {
            descText.text = "";
        }

        Cursor.visible = true;
    }

    // Change active menu
    public void change()
    {
        isInactive.SetActive(true);
        isActive.SetActive(false);
        title.text = $"Memory Paint\n{titleAddon}";
    }

    // Toggle timer feature
    public void toggleTimer(TextMeshProUGUI timer)
    {
        bool timerActive = !ToBoolean(PlayerPrefs.GetInt("Timer"));
        PlayerPrefs.SetInt("Timer", ToInt16(timerActive));
        timer.text = timerActive ? "Timer Mode\n<color=green>Active</color>" : "Timer Mode\n<color=red>Inactive</color>";
    }

    // Show a description of the gamemode
    public void toggleGameDescription(bool descActive)
    {
        if (descActive) {
            descText.text = desc;
        } else {
            descText.text = "";
        }
    }

    // Quit application
    public void quit()
    {
        Application.Quit();
    }
}