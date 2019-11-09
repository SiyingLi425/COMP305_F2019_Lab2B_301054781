/*
 * By: Siying Li
 * Student Number: 301054781
 * Date: 2019-11-09
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSettings", menuName = "Scene/Settings")]
[System.Serializable]
public class SceneSettings : ScriptableObject
{

    [Header("Scene Configuration")]
    public Scene scene;
    public SoundClip activeSoundClip;

    [Header("Scoreboard Labels")]
    public bool scoreLabelEnabled;
    public bool livesLabelEnabled;
    public bool highScoreLabelEnabled;

    [Header("Scene Label")]
    public bool startLabelActive;
    public bool endLabelActive;

    [Header("Scene Buttons")]
    public bool restartButtonActive;  
    public bool startButtonActive;

}
