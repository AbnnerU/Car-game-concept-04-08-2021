using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManeger : MonoBehaviour
{

    [SerializeField] private PointsCreator pointsCreator;

    [SerializeField] private Button playButton;

    [SerializeField] private Button resetButton;

    [SerializeField] private GameObject pointPositonEditor;

    [SerializeField] private GameObject mainButtons;

    void Awake()
    {
        pointsCreator.HaveTargets += HaveTargetEvent;
    }

    private void HaveTargetEvent(bool haveTarget)
    {
        playButton.interactable = haveTarget;

        resetButton.interactable = haveTarget;
    }

    public void EnablePointPositionEditor()
    {
        mainButtons.SetActive(false);

        pointPositonEditor.SetActive(true);

    }
    
}
