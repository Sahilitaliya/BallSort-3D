using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllLeavel;
    [SerializeField] List <GameObject> Tube;
    [SerializeField] Color[] TubeColors;
    [SerializeField] List<GameObject> TubeSilect, BollSilected;
    [SerializeField] List <Color> ColorList;
    [SerializeField] GameObject BollPrefb;
    [SerializeField] string[] BallTag;
    [SerializeField] List<int> TagList;
    [SerializeField] TextMeshProUGUI LevelTxt;
    private int Counter = 0;
    [SerializeField] ParticleSystem Particle;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Particle.Pause();
        leavel = PlayerPrefs.GetInt("Skip", 1);
        LevelTxt.text = leavel.ToString();
        Counter = PlayerPrefs.GetInt("Leavel" , Counter);
        for(int i=0; i<AllLeavel.Length; i++)
        {
            if(i == Counter)
            {
                AllLeavel[i].SetActive(true);
            }
            else
            {
                AllLeavel[i].SetActive(false);
            }
        }
        foreach(Transform child in AllLeavel[Counter].transform)
        {
            Tube.Add(child.gameObject);
        }

        TubeChoice();
        BollGanrate();
    }
    public void TubeChoice()
    {
        int tube;
        for (int i = 0; i < Tube.Count - 2; i++)
        {
            do
            {
                tube = Random.Range(0, Tube.Count);
            } while (TubeSilect.Contains(Tube[tube]));
            TubeSilect.Add(Tube[tube]);
        }
    }
    public void BollGanrate()
    {
        for(int i = 0; i < TubeSilect.Count; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                GameObject G = Instantiate(BollPrefb, TubeSilect[i].transform.GetChild(j).position, Quaternion.identity, TubeSilect[i].transform);
                BollSilected.Add(G);
            }
        }
        BollColorChange();
    }
    //public void ColorSet()
    //{
    //    for(int i=0; i<3; i++)
    //    {
    //        int Val;
    //        do
    //        {
    //            Val = Random.Range(0, TubeColors.Length);
    //        } while (ColorList.Contains(TubeColors[Val]));
    //        ColorList.Add(TubeColors[Val]);
    //    }
    //    BollColorChange();
    //}
    public void BollColorChange()
    {
        for (int i = 0; i < BollSilected.Count; i++)
        {
            int Val;
            do
            {
                Val = Random.Range(0, TubeColors.Length);
            } while (ColorList.Contains(TubeColors[Val]));
            ColorList.Add(TubeColors[Val]);
            TagList.Add(Val);
            for(int j = 0; j < 4; j++)
            {
                int RandBoll = Random.Range(0, BollSilected.Count);
                BollSilected[RandBoll].GetComponent<MeshRenderer>().material.color = ColorList[i];
                BollSilected[RandBoll].GetComponent<MeshRenderer>().tag = BallTag[TagList[i]];
                BollSilected.RemoveAt(RandBoll);
            }
        }
    }
    private GameObject FirstClick, SecClick;
    bool click;
    public void BollSetUpPos(GameObject gameObject)
    {
        if (!click)
        {
            if (gameObject.transform.childCount > 5)
            {
                FirstClick = gameObject;
                FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.DOMove(FirstClick.transform.GetChild(4).transform.position, 0.5f).SetEase(Ease.OutBounce);
                FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.DOScale(new Vector3(0.3f, 0f, 0.3f), 0.3f);
                FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.DOScale(new Vector3(0.1111111f , 0.1428571f , 0.1111111f), 0.5f);
                click = true;
            }
        }
        else
        {
            if (gameObject.transform.childCount >= 9)
            {
                //FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.position = FirstClick.transform.GetChild(FirstClick.transform.childCount - 6).transform.position;
                FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.DOMove(FirstClick.transform.GetChild(FirstClick.transform.childCount - 6).transform.position , 0.5f).SetEase(Ease.OutBounce);
                click = false;
            }
            else if (gameObject.transform.childCount == 5)
            {
                SecClick = gameObject;
                FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.parent = SecClick.transform;
                //SecClick.transform.GetChild(SecClick.transform.childCount - 1).transform.position = SecClick.transform.GetChild(0).transform.position;
                Sequence mySequnce = DOTween.Sequence();
                mySequnce.Append(SecClick.transform.GetChild(SecClick.transform.childCount - 1).transform.DOMove(SecClick.transform.GetChild(4).transform.position, 0.5f));
                mySequnce.Append(SecClick.transform.GetChild(SecClick.transform.childCount - 1).transform.DOMove(SecClick.transform.GetChild(0).transform.position , 0.5f).SetEase(Ease.OutBounce));
                click = false;
            }
            else
            {
                SecClick = gameObject;
                if(FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).tag == SecClick.transform.GetChild(SecClick.transform.childCount - 1).tag)
                {
                    FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.parent = SecClick.transform;
                    //SecClick.transform.GetChild(SecClick.transform.childCount - 1).transform.position = SecClick.transform.GetChild(SecClick.transform.childCount - 6).transform.position;
                    Sequence mySequnce = DOTween.Sequence();
                    mySequnce.Append(SecClick.transform.GetChild(SecClick.transform.childCount - 1).transform.DOMove(SecClick.transform.GetChild(4).transform.position, 0.5f));
                    mySequnce.Append(SecClick.transform.GetChild(SecClick.transform.childCount - 1).transform.DOMove(SecClick.transform.GetChild(SecClick.transform.childCount - 6).transform.position, 0.5f).SetEase(Ease.OutBounce));
                    click = false;
                }
                else
                {
                    //FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.position = FirstClick.transform.GetChild(FirstClick.transform.childCount - 6).transform.position;
                    FirstClick.transform.GetChild(FirstClick.transform.childCount - 1).transform.DOMove(FirstClick.transform.GetChild(FirstClick.transform.childCount - 6).transform.position , 0.5f).SetEase(Ease.OutBounce);
                    click = false;
                }
            }
        }
        CheakBall();
    }
    public List<GameObject> WinList;
    private bool win;
    public void CheakBall()
    {
        WinList.Clear();
        win = false;

        foreach (GameObject Ball in Tube)
        {
            if (Ball.transform.childCount >= 9)
            {
                WinList.Add(Ball);
                win = true;
            }
        }

        if (WinList.Count == Tube.Count - 2)
        {
            foreach (GameObject selectedTube in WinList)
            {
                win = true;

                for (int i = 5; i <= 8; i++)
                {
                    if (selectedTube.transform.GetChild(i).tag != selectedTube.transform.GetChild(5).tag)
                    {
                        win = false;
                        break;
                    }
                }

            }
            if (win)
            {
                Particle.Play();
                Invoke("ChangeLeavel" , 3f);
            }
            else
            {
                win = true;
            }
        }
    }
    int leavel;
    void ChangeLeavel()
    {
        leavel = PlayerPrefs.GetInt("Skip", 1);
        leavel++;
        PlayerPrefs.SetInt("Skip", leavel);
        SceneManager.LoadScene(0);

        if (leavel == 6)
        {
            Counter++;
            PlayerPrefs.SetInt("Leavel", Counter);
            SceneManager.LoadScene(0);
        }
        else if (leavel == 11)
        {
            Counter++;
            PlayerPrefs.SetInt("Leavel", Counter);
            SceneManager.LoadScene(0);
        }
        else if (leavel == 16)
        {
            Counter++;
            PlayerPrefs.SetInt("Leavel", Counter);
            SceneManager.LoadScene(0);
        }
        else if (leavel == 21)
        {
            Counter++;
            PlayerPrefs.SetInt("Leavel", Counter);
            SceneManager.LoadScene(0);
        }
        else if (leavel == 26)
        {
            Counter++;
            PlayerPrefs.SetInt("Leavel", Counter);
            SceneManager.LoadScene(0);
        }
        else if (leavel == 31)
        {
            Counter++;
            PlayerPrefs.SetInt("Leavel", Counter);
            SceneManager.LoadScene(0);
        }
    }
    public void HomePanelActive()
    {
        SceneManager.LoadScene(1);
    }
}