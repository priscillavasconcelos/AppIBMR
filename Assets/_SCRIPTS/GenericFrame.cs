using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GenericFrame : FrameController
{
    private Information information;

    [SerializeField]
    private Text titulo, detailComoChegar, detailPossoFazer, detailDisciplinas, detailResponsaveis, detailContato;
    [SerializeField]
    private Image imgComoChegar;
    [SerializeField]
    private CanvasGroup panelChegar;

    public override void Active()
    {
        base.Active();
    }

    public override void Deactive()
    {
        HideComoChegar();
        CleanFields();
        base.Deactive();
    }
    public Information Information
    {
        get
        {
            return (information);
        }
        set
        {
            information = value;
            SetFrame();
        }
    }
    private void SetFrame()
    {
        titulo.text = information.titulo;
        LoadImage(information.comoChegarImg);
        
        detailPossoFazer.text = information.oQueFazer;
        
        foreach (string disciplina in information.disciplinas)
        {
            detailDisciplinas.text += disciplina;
            if (disciplina != information.disciplinas.Last<string>())
            {
                detailDisciplinas.text += "\n";
            }
        }

        foreach (string responsavel in information.responsaveis)
        {
            detailResponsaveis.text += responsavel;
            if (responsavel != information.responsaveis.Last<string>())
            {
                detailResponsaveis.text += "\n";
            }
        }

        foreach (string contato in information.contato)
        {
            detailContato.text += contato;
            if (contato != information.contato.Last<string>())
            {
                detailContato.text += "\n";
            }
        }
    }
    public void ShowComoChegar()
    {
        panelChegar.alpha = 1f;
        panelChegar.interactable = true;
        panelChegar.blocksRaycasts = true;
    }
    public void HideComoChegar()
    {
        panelChegar.alpha = 0f;
        panelChegar.interactable = false;
        panelChegar.blocksRaycasts = false;
    }
    private void LoadImage(string fileName)
    {
        Sprite sprite = Resources.Load<Sprite>(fileName);
        imgComoChegar.sprite = sprite;
    }

    private void CleanFields()
    {
        titulo.text = "";
        
        detailPossoFazer.text = "";
        detailDisciplinas.text = "";
        detailResponsaveis.text = "";
        detailContato.text = "";
    }
}
