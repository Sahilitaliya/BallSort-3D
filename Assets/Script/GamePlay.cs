using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    private bool Flag;
    private float Speed = 0.5f;
    [SerializeField] GameObject HomePanel , LoadingPanel , SettingPanel;
    [SerializeField] Image LoadingBar;
    [SerializeField] Sprite MusicOn, MusicOff, SoundOn, SoundOff;
    [SerializeField] Button MusicBtn, SoundBtn;
    [SerializeField] AudioSource MusicSource, SoundSource;

    private void Start()
    {
        Flag = true;

        if (AudioManager.instance.IsMusic)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOn;
            MusicSource.mute = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOff;
            MusicSource.mute = true;
        }
        if (AudioManager.instance.IsSound)
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOn;
            SoundSource.mute = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOff;
            SoundSource.mute = true;
        }
    }
    private void Update()
    {
        if(Flag)
        {
            if(LoadingBar.fillAmount < 1)
            {
                LoadingBar.fillAmount += Speed * Time.deltaTime; ;
            }
            else
            {
                HomePanel.SetActive(true);
            }
        }
    }
    public void GameScenLoad()
    {
        SceneManager.LoadScene(0);
    }
    public void MusicManagement()
    {
        if (AudioManager.instance.IsMusic)
        {

            MusicBtn.GetComponent<Image>().sprite = MusicOff;
            AudioManager.instance.IsMusic = false;
            MusicSource.mute = true;

        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOn;
            AudioManager.instance.IsMusic = true;
            MusicSource.mute = false;
        }
    }
    public void SoundManagement()
    {
        if (AudioManager.instance.IsSound)
        {

            SoundBtn.GetComponent<Image>().sprite = SoundOff;
            AudioManager.instance.IsSound = false;
            SoundSource.mute = true;

        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOn;
            AudioManager.instance.IsSound = true;
            SoundSource.mute = false;
        }
    }
    public void SettingPanelOpen()
    {
        SettingPanel.SetActive(true);
    }
    public void SetingPanelClose()
    {
        SettingPanel.SetActive(false);
        HomePanel.SetActive(true);
    }
}