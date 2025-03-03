using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarChange : MonoBehaviour
{
    [Header("Progress bar")]
    [SerializeField] private Image ProgressBar1;
    [SerializeField] private Image ProgressBar2;
    [SerializeField] private Image ProgressBar3;
    [SerializeField] private Image ProgressBar4;
    [SerializeField] private Image SecretBar;
    private Image[] ArrayOfProgressBar;

    [Header("Progress Fill Bar")]
    [SerializeField] private Image ProgressFillBar1;
    [SerializeField] private Image ProgressFillBar2;
    [SerializeField] private Image ProgressFillBar3;
    [SerializeField] private Image ProgressFillBar4;
    private Image[] ArrayOfProgressFillBar;
    
    [Header("POSITION")]
    [SerializeField] private RectTransform POSBar1;
    [SerializeField] private RectTransform POSBar2;
    [SerializeField] private RectTransform POSBar3;
    [SerializeField] private RectTransform POSBar4;
    [SerializeField] private RectTransform POSBarSecret;
    private RectTransform[] ArrayOfPosBar;
    
    [Header("Locomotiv")]
    [SerializeField] private Image Locomotive;
    
    [Header("Zone Manager")] 
    [SerializeField] private ZoneManager ZoneManager;
    
    //private Value

    private void Start()
    {
        ZoneManager.ZoneChangeUI += UpdateProgressBar4Show;
        ArrayOfProgressBar = new[] { ProgressBar1, ProgressBar2, ProgressBar3, ProgressBar4 };
        ArrayOfProgressFillBar = new[] { ProgressFillBar1, ProgressFillBar2, ProgressFillBar3, ProgressFillBar4 };
        ArrayOfPosBar = new[] { POSBar1, POSBar2, POSBar3, POSBar4 , POSBarSecret};
        UpdateProgressBar4Show();
    }

    private void Update()
    {
        if(ZoneManager.CurrentZoneOfSpeedID < ArrayOfProgressFillBar.Length)
            ArrayOfProgressFillBar[ZoneManager.CurrentZoneOfSpeedID].fillAmount = Mathf.Clamp(ZoneManager.CurrentZoneOfSpeed.currentProgression / 100f, 0f, 1f);
        
        if (ZoneManager.CurrentZoneOfSpeedID + 1 < ArrayOfPosBar.Length)
        {
            Locomotive.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(
                ArrayOfPosBar[ZoneManager.CurrentZoneOfSpeedID].anchoredPosition.x,
                ArrayOfPosBar[ZoneManager.CurrentZoneOfSpeedID + 1].anchoredPosition.x,
                Mathf.Clamp(ZoneManager.CurrentZoneOfSpeed.currentProgression / 100f, 0f, 1f)
            ), Locomotive.rectTransform.anchoredPosition.y);
        }
    }

    private void OnDestroy()
    {
        ZoneManager.ZoneChangeUI -= UpdateProgressBar4Show;
    }

    void UpdateProgressBar4Show()
    {
        for (int i = 0; i < ZoneManager.ZoneOfSpeedList.Count; i++)
        {
            switch (ZoneManager.ZoneOfSpeedList[i].zoneHeat)
            {
                case StateOfHeat.LOW:
                    ArrayOfProgressBar[i].color = new Color(1f, 0.773f, 0.208f); //Light
                    ArrayOfProgressFillBar[i].color = new Color(1f, 0.773f, 0.208f);
                    ArrayOfProgressFillBar[i].fillAmount = 0f;
                    break;
                case StateOfHeat.MEDIUM:
                    ArrayOfProgressBar[i].color = new Color(1f, 0.506f, 0.404f); //Medium
                    ArrayOfProgressFillBar[i].color = new Color(1f, 0.506f, 0.404f);
                    ArrayOfProgressFillBar[i].fillAmount = 0f;
                    break;
                case StateOfHeat.HIGH:
                    ArrayOfProgressBar[i].color = new Color(0.996f, 0.224f, 0.612f); //Hot
                    ArrayOfProgressFillBar[i].color = new Color(0.996f, 0.224f, 0.612f);
                    ArrayOfProgressFillBar[i].fillAmount = 0f;
                    break;
            }
            
        }
    }
}
