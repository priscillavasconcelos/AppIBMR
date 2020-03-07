using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Information
{
    public string codigo;
    public string titulo;
    public string comoChegarImg;
    public string comoChegarTxt;
    public string oQueFazer;
    public List<string> disciplinas = new List<string>();
    public List<string> responsaveis = new List<string>();
    public List<string> contato = new List<string>();
}
[Serializable]
public class Informations
{
    public List<Information> informations;
}

