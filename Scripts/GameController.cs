using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject pnlStartGame;
    public GameObject pnlMainGame;
    public GameObject pnlEndGame;
    public GameObject pnlPauseGame;
    public Text txtEndGame;
    public Slider sliderTime;
    private float timeGame;
    private bool isWin;
    public float soLaBai=20;
    public GameObject sanDau;
    public GameObject TheBaiGameObject;
    public List<Sprite> listSpite = new List<Sprite>();
    private List<GameObject> listbtn = new List<GameObject>();
    public List<GameObject> listBaiLat = new List<GameObject>();
    private int soLuotDung = 0;
    public float TimeGame { get => timeGame; set => timeGame = value; }
    public bool IsWin { get => isWin; set => isWin = value; }
    private AudioSource audioRight;
    // Start is called before the first frame update
    void Start()
    {
        audioRight = gameObject.GetComponent<AudioSource>();
        pnlStartGame.SetActive(true);
        pnlMainGame.SetActive(false);
        pnlEndGame.SetActive(false);
        pnlPauseGame.SetActive(false);
        Time.timeScale = 1;
        TimeGame = 60;
        sliderTime.maxValue = TimeGame;
        UpdateSliderTime();
        sliderTime.enabled = false;
        isWin = false;
        RutBai();
        shufferBai();

    }
    void RutBai()
    {
        for(int i = 0; i < soLaBai/2; i++)
        {
            GameObject tb = Instantiate(TheBaiGameObject, Vector3.zero, Quaternion.identity) as GameObject;
            tb.transform.SetParent(sanDau.transform);
            tb.transform.localPosition = Vector3.zero;
            tb.transform.localScale = Vector3.one;
            tb.GetComponent<TheBaiController>().thebai.id = i;
            SetIconLaBai(tb);

            GameObject copy = Instantiate(TheBaiGameObject, Vector3.zero, Quaternion.identity) as GameObject;
            copy.transform.SetParent(sanDau.transform);
            copy.transform.localPosition = Vector3.zero;
            copy.transform.localScale = Vector3.one;
            CopyBai(tb, copy);
            listbtn.Add(tb);
            listbtn.Add(copy);

        }
    }
    void shufferBai()
    {
        for(int i=0;i<listbtn.Count;i++)
        {
            int r = Random.Range(0, listbtn.Count);

            int id = listbtn[i].GetComponent<TheBaiController>().thebai.id;
            Sprite sprite = listbtn[i].GetComponent<TheBaiController>().Icon;

            listbtn[i].GetComponent<TheBaiController>().thebai.id = listbtn[r].GetComponent<TheBaiController>().thebai.id;
            listbtn[i].GetComponent<TheBaiController>().Icon = listbtn[r].GetComponent<TheBaiController>().Icon;


            listbtn[r].GetComponent<TheBaiController>().thebai.id = id;
            listbtn[r].GetComponent<TheBaiController>().Icon = sprite;

        }
    }
    void CopyBai(GameObject tb,GameObject copy)
    {
        copy.GetComponent<TheBaiController>().Icon = tb.GetComponent<TheBaiController>().Icon;
        copy.GetComponent<TheBaiController>().thebai.id =tb.GetComponent<TheBaiController>().thebai.id;
    }
    void SetIconLaBai(GameObject tb)
    {
        int r = Random.Range(0, listSpite.Count);
        tb.GetComponent<TheBaiController>().Icon = listSpite[r];
        listSpite.RemoveAt(r);
    }
    void UpdateSliderTime()
    {
        sliderTime.value = TimeGame;
    }
    // Update is called once per frame
    void Update()
    {
        if(TimeGame<=0)
        {
            isWin = false;
            EndGame();
        }
        if(listBaiLat.Count==2)
        {
            if(listBaiLat[0].GetComponent<TheBaiController>().thebai.id== listBaiLat[1].GetComponent<TheBaiController>().thebai.id)
            {
                audioRight.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
                listBaiLat[0].GetComponent<TheBaiController>().HuyBai();
                listBaiLat[1].GetComponent<TheBaiController>().HuyBai();
                listBaiLat.RemoveAt(0);
                listBaiLat.RemoveAt(0);
                soLuotDung += 2;
            }
            else
            {
                listBaiLat[0].GetComponent<TheBaiController>().DongBai();
                listBaiLat[1].GetComponent<TheBaiController>().DongBai();
                listBaiLat.RemoveAt(0);
                listBaiLat.RemoveAt(0);
            }
        }
        if(soLuotDung==soLaBai)
        {
            isWin = true;
            EndGame();
        }
        

    }
    void EndGame()
    {
        if(isWin==false)
        {

            txtEndGame.text = "Hết thời gian!!! Bạn có muốn thử Lại?";
            
            
        }
        else
        {
            txtEndGame.text = "Chúc Mừng Bạn Đã Chiến Thắng Thử Thách!!! Bạn có muốn thử Lại?";
            
        }
        pnlEndGame.SetActive(true);
        
    }
    public void StartGame()
    {
        pnlStartGame.SetActive(false);
        pnlMainGame.SetActive(true);
        StartCoroutine(CountDownTime());
    }
    public void PauseGame()
    {
        pnlPauseGame.SetActive(true);
        Time.timeScale = 0;
    }
    public void pnlResumeGame()
    {
        pnlPauseGame.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator CountDownTime()
    {
        yield return new WaitForSeconds(1);
        TimeGame--;
        UpdateSliderTime();
        StartCoroutine(CountDownTime());
    }
}
