using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheBaiController : MonoBehaviour
{
    public TheBai thebai;
    public Sprite matsau;
    private Sprite icon;
    public Button btn;
    private bool isOpen;
    public Sprite Icon { get => icon; set => icon = value; }
    private GameObject gameController;
    public GameObject TheBaiObject;
    public GameObject Smoke;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        thebai.sprite= matsau;

        btn.GetComponent<Image>().sprite = thebai.sprite;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        btn.GetComponent<Image>().sprite = thebai.sprite;
    }
    public void ChonBai()
    {
        if (isOpen == false)
        {

            thebai.sprite = Icon;
            isOpen = true;
            gameController.GetComponent<GameController>().listBaiLat.Add(TheBaiObject);

        }
        else
        {

            thebai.sprite = matsau;
            isOpen = false;
            gameController.GetComponent<GameController>().listBaiLat.Remove(TheBaiObject);
        }

    }
    public void DongBai()
    {
        StartCoroutine(DongBaiAfter());

    }
    IEnumerator DongBaiAfter()
    {
        yield return new WaitForSeconds(0.5f);
        thebai.sprite = matsau;
        isOpen = false;
        
    }
    public void HuyBai()
    {
        StartCoroutine(HuyBaiAfter());
    }
    IEnumerator HuyBaiAfter()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        btn.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        btn.GetComponent<Button>().enabled = false;
    }

}
