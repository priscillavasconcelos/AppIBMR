using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;

public class AppManager : MonoBehaviour
{
    public static AppManager manager;
    private Informations informations;
    public InputField pesquisa;
    public RectTransform rectPesquisa;
    public GameObject contentPesquisa, spawnPesquisa;
    public GameObject main;
    public GameObject ar;
    private EventSystem m_EventSystem;
    public Text Toasttxt;
    FrameController currentFrame;
    private bool iQuit = false;
    private List<FrameController> backList = new List<FrameController>();
    public DataController dataController;
    public GenericFrame genericFrame;

    private void Awake()
    {
        manager = this;
    }
    void Start()
    {
        m_EventSystem = EventSystem.current;
        informations = dataController.LoadInformations();
    }
    public void NextView(RectTransform next)
    {
        if (m_EventSystem.currentSelectedGameObject.transform.GetComponentInParent<ViewController>().gameObject)
        {
            GameObject preview = m_EventSystem.currentSelectedGameObject.transform.GetComponentInParent<ViewController>().gameObject;
            preview.GetComponent<RectTransform>().position = next.position;
        }
        
        next.position = Vector3.zero;
        if (next.GetComponentInChildren<FrameController>())
        {
            currentFrame = next.GetComponentInChildren<FrameController>();
        }
    }
    public void NextFrame(string codigo)
    {
        if (currentFrame)
        {
            if (backList.Count == 0)
            {
                currentFrame.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            
            currentFrame.Deactive();
            backList.Add(currentFrame);
        }

        genericFrame.Active();
        genericFrame.Information = GetInformation(codigo);
        //next.position = Vector3.zero;
        currentFrame = genericFrame.GetComponent<FrameController>();
    }

    public void NextFrame(RectTransform next)
    {
        if (currentFrame)
        {
            if (backList.Count == 0)
            {
                currentFrame.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            
            currentFrame.Deactive();
            backList.Add(currentFrame);
        }

        next.position = Vector3.zero;
        currentFrame = next.GetComponent<FrameController>();
    }

    public void PreviewFrame()
    {
        if (backList.Count > 0)
        {
            if (currentFrame)
            {
                //currentFrame.myRect.position = currentFrame.myInitialPosition;
                currentFrame.Deactive();
            }
            backList.Last().Active();
            if (backList.Count == 1)
            {
                currentFrame.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            currentFrame = backList.Last().GetComponent<FrameController>();
            backList.Remove(backList.Last());
        }
        else
        {
            Application.Quit();
        }
    }
    void Update()
    {
        if (iQuit)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(QuitingTimer("Pressione novamente para sair"));
        }
    }

    IEnumerator QuitingTimer(string msg)
    {
        iQuit = true;
        showToast(msg, 1);
        yield return new WaitForSeconds(1);
        PreviewFrame();
        iQuit = false;
    }
    public void ArCam()
    {
        main.SetActive(false);
        ar.SetActive(true);
    }

    public void TrackFound(string codigo)
    {
        NextFrame(codigo);
        main.SetActive(true);
        ar.SetActive(false);
    }

    public void showToast(string text, int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text, int duration)
    {
        Color orginalColor = Toasttxt.color;

        Toasttxt.text = text;
        Toasttxt.enabled = true;

        //Fade in
        yield return fadeInAndOut(Toasttxt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(Toasttxt, false, 0.5f);

        Toasttxt.enabled = false;
        Toasttxt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

    public Information GetInformation(string codigo)
    {
        foreach (Information info in informations.informations)
        {
            if (info.codigo == codigo)
            {
                return info;
            }
        }
        return null;
    }

    public void BarraPesquisa(string pesquisa)
    {
        bool found = false;
        CleanPesquisa();
        NextFrame(rectPesquisa);
        
        foreach (Information info in informations.informations)
        {
            if (info.titulo.IndexOf(pesquisa, StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                found = true;
                GameObject txt = Instantiate(spawnPesquisa, contentPesquisa.transform);
                txt.GetComponent<ContentList>().titulo.text = info.titulo;
                txt.GetComponent<ContentList>().Information = info;
                txt.GetComponent<ContentList>().manager = this;
                txt.GetComponent<ContentList>().frame = genericFrame;

                Vector2 toAdd = new Vector2(0, 30);
                contentPesquisa.GetComponent<RectTransform>().sizeDelta += toAdd;
            }
        }
        if (!found)
        {
            GameObject txt = Instantiate(spawnPesquisa, contentPesquisa.transform);
            txt.GetComponent<ContentList>().titulo.text = "Não encontrado";
            Destroy(txt.GetComponent<Button>());

            Vector2 toAdd = new Vector2(0, 30);
            contentPesquisa.GetComponent<RectTransform>().sizeDelta += toAdd;
        }
    }
    public void CleanPesquisa()
    {
        foreach (Transform child in contentPesquisa.transform)
        {
            Destroy(child.gameObject);
        }
        contentPesquisa.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 10);
    }

    public void CleanBarra()
    {
        pesquisa.text = "";
    }
}
