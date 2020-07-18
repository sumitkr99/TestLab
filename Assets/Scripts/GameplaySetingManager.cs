using System;
using System.Collections;
using System.Collections.Generic;
using DG.DemiLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameplaySetingManager : MonoBehaviour
{
    [Header("Data Object")] public QuickDataScriptableObject data;
    public GameplayManager gameplayManager;
    public SoundManager soundManager;
    [Header("Panels")] public RectTransform settingPanel;
    public List<RectTransform> settingSubPanels;
    public PanelState panelState = PanelState.None;
    public Button backButton;
    [Header("Game Type SubPanel")] public List<Button> typeButtons;
    public Sprite checkTypeSprite;
    public Sprite uncheckTypeSprite;
    [Header("Game Price SubPanel")] public List<Button> priceButtons;
    public Sprite checkPriceSprite;
    public Sprite uncheckPriceSprite;
    [Header("Game Turn SubPanel")] public List<Button> turnButtons;
    public Sprite checkTurnSprite;
    public Sprite uncheckTurnSprite;
    public Sprite checkBoardTurnSprite;
    public Sprite uncheckBoardTurnSprite;

    public enum PanelState
    {
        None,
        TypePanel,
        PricePanel,
        TurnPanel
    }


    // public Button replayButton;

    // Start is called before the first frame update
    private void Start()
    {
        typeButtons[0].onClick.AddListener(delegate { OnGameType(QuickDataScriptableObject.GameplayType.Seen, 0); });
        typeButtons[1].onClick.AddListener(delegate { OnGameType(QuickDataScriptableObject.GameplayType.Blind, 1); });

        priceButtons[0].onClick.AddListener(delegate { OnGamePrice(QuickDataScriptableObject.GameplayPrice.Rs20, 0); });
        priceButtons[1].onClick
            .AddListener(delegate { OnGamePrice(QuickDataScriptableObject.GameplayPrice.Rs20x2, 1); });
        priceButtons[2].onClick.AddListener(delegate
        {
            OnGamePrice(QuickDataScriptableObject.GameplayPrice.Rs100, 2);
        });
        priceButtons[3].onClick.AddListener(delegate
        {
            OnGamePrice(QuickDataScriptableObject.GameplayPrice.Rs100x2, 3);
        });

        turnButtons[0].onClick.AddListener(delegate
        {
            OnGameTurn(QuickDataScriptableObject.GameplayTurn.MultiClick, 0);
        });
        turnButtons[1].onClick.AddListener(delegate
        {
            OnGameTurn(QuickDataScriptableObject.GameplayTurn.SingleClick, 1);
        });
        turnButtons[2].onClick.AddListener(delegate
        {
            OnGameTurn(QuickDataScriptableObject.GameplayTurn.TouchPad, 2);
        });

        backButton.onClick.AddListener(OnBack);
        ResetAll();
    }

    /// <summary>
    /// Gameplay Type Panel...
    /// </summary>
    private void OnGameType(QuickDataScriptableObject.GameplayType gameType, int buttonIndex)
    {
        data.gameplayType = gameType;
        for (var i = 0; i < typeButtons.Count; i++)
        {
            //typeButtons[i].enabled = i == buttonIndex;
            typeButtons[i].GetComponentInChildren<TMP_Text>().color =
                i == buttonIndex ? data.checkColor : data.uncheckColor;
            typeButtons[i].GetComponent<Image>().sprite =
                i == buttonIndex ? checkTypeSprite : uncheckTypeSprite;
        }

        turnButtons[2].interactable = data.gameplayType == QuickDataScriptableObject.GameplayType.Blind;
        turnButtons[2].GetComponentInChildren<TMP_Text>().color =
            data.gameplayType != QuickDataScriptableObject.GameplayType.Blind
                ? data.disabledColor
                : Color.white;
        ActivePanel(settingSubPanels[1], true);
        panelState = PanelState.PricePanel;
    }

    /// <summary>
    /// Gameplay Price Panel...
    /// </summary>
    private void OnGamePrice(QuickDataScriptableObject.GameplayPrice gamePrice, int buttonIndex)
    {
        data.gameplayPrice = gamePrice;
        for (var i = 0; i < priceButtons.Count; i++)
        {
            //priceButtons[i].enabled = i == buttonIndex;
            priceButtons[i].GetComponentInChildren<TMP_Text>().color =
                i == buttonIndex ? data.checkColor : data.uncheckColor;
            priceButtons[i].GetComponent<Image>().sprite =
                i == buttonIndex ? checkPriceSprite : uncheckPriceSprite;
        }

        ActivePanel(settingSubPanels[2], true);
        panelState = PanelState.TurnPanel;
    }

    /// <summary>
    /// Gameplay Turn Panel...
    /// </summary>
    private void OnGameTurn(QuickDataScriptableObject.GameplayTurn gameTurn, int buttonIndex)
    {
        data.gameplayTurn = gameTurn;
        for (var i = 0; i < turnButtons.Count; i++)
        {
            if (!turnButtons[i].interactable) continue;
            turnButtons[i].GetComponentInChildren<TMP_Text>().color =
                i == buttonIndex ? data.checkColor : data.uncheckColor;
            if (i < 2)
            {
                turnButtons[i].GetComponent<Image>().sprite =
                    i == buttonIndex ? checkTurnSprite : uncheckTurnSprite;
            }
            else
            {
                turnButtons[i].GetComponent<Image>().sprite =
                    i == buttonIndex ? checkBoardTurnSprite : uncheckBoardTurnSprite;
            }
        }

        gameplayManager.continueButton.interactable = true;
    }

    private void OnBack()
    {
        switch (panelState)
        {
            case PanelState.None:
                break;
            case PanelState.TypePanel:
                ActivePanel(settingPanel, false);
                Invoke(nameof(ResetAll), data.duration);
                panelState = PanelState.None;
                ResetGameTypeSetting();
                Instantiate(soundManager.backClickSound);
                break;
            case PanelState.PricePanel:
                ActivePanel(settingSubPanels[1], false);
                panelState = PanelState.TypePanel;
                ResetGamePriceSetting();
                Instantiate(soundManager.backClickSound);
                break;
            case PanelState.TurnPanel:
                ActivePanel(settingSubPanels[2], false);
                panelState = PanelState.PricePanel;
                ResetGameTurnSetting();
                Instantiate(soundManager.backClickSound);
                break;
        }


        // ActivePanel(GetComponent<RectTransform>(), false);
    }


    private void ActivePanel(RectTransform panel, bool status)
    {
        panel.DOAnchorPos(status ? data.inPos : data.outPos, data.duration);
    }

    private void ResetGameTypeSetting()
    {
        foreach (var t in typeButtons)
        {
            t.GetComponentInChildren<TMP_Text>().color = data.uncheckColor;
            t.GetComponent<Image>().sprite = uncheckTypeSprite;
        }
    }

    private void ResetGamePriceSetting()
    {
        foreach (var t in priceButtons)
        {
            t.GetComponentInChildren<TMP_Text>().color = data.uncheckColor;
            t.GetComponent<Image>().sprite = uncheckPriceSprite;
        }
    }

    private void ResetGameTurnSetting()
    {
        for (var i = 0; i < turnButtons.Count; i++)
        {
            if (!turnButtons[i].interactable) continue;
            turnButtons[i].GetComponentInChildren<TMP_Text>().color = data.uncheckColor;
            turnButtons[i].GetComponent<Image>().sprite = i < 2 ? uncheckTurnSprite : uncheckBoardTurnSprite;
        }
    }


    private void ResetAll()
    {
        print("Inside Reset");
        //  ActivePanel(settingSubPanels, 0);
        data.gameplayType = QuickDataScriptableObject.GameplayType.None;
        data.gameplayPrice = QuickDataScriptableObject.GameplayPrice.None;
        data.gameplayTurn = QuickDataScriptableObject.GameplayTurn.None;

        // foreach (var lable in typeToggleLabels)
        // {
        //     lable.color = Color.white;
        // }
        //
        // foreach (var lable in coinToggleLables)
        // {
        //     lable.color = Color.white;
        // }
        //
        // foreach (var lable in turnToggleLables)
        // {
        //     lable.color = Color.white;
        // }
    }
}