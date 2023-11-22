using System.Collections.Generic;
using DG.Tweening;
using QtNameSpace;
using UnityEngine;
using UnityEngine.Events;

public class SelectCharacterBox : QtBox
{
    [SerializeField] private SelectCharacterBoxModel model;
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private RectTransform panel;
    [SerializeField] private float startMoveX = 1500;
    [SerializeField] private List<ABaseElement> views;
    private float _endMoveX;


    public override void Open(bool useAmin = true, UnityAction action = null)
    {
        selectPanel.SetActive(true);
        Setup();
        base.Open(false, action);
        if (useAmin)
        {
            var localPos = panel.localPosition;
            panel.localPosition = new Vector3(startMoveX, localPos.y, localPos.z);
            panel.DOLocalMoveX(_endMoveX, durationMotion).SetEase(motionEase);
        }
        else
        {
            var localPos = panel.localPosition;
            panel.localPosition = new Vector3(_endMoveX, localPos.y, localPos.z);
        }
    }

    public override void Close(bool useAmin = true, UnityAction action = null)
    {
        selectPanel.SetActive(false);
        if (useAmin)
        {
            var localPos = panel.localPosition;
            panel.localPosition = new Vector3(_endMoveX, localPos.y, localPos.z);
            panel.DOLocalMoveX(startMoveX, durationMotion).SetEase(motionEase).OnComplete(() =>
            {
                base.Close(false, action);
            });
        }
        else
        {
            base.Close(false, action);
            var localPos = panel.localPosition;
            panel.localPosition = new Vector3(startMoveX, localPos.y, localPos.z);
        }
    }

    private void Awake()
    {
        _endMoveX = panel.localPosition.x;
        Setup();
    }

    private void OnDestroy()
    {
        if (GameModel.Instance != null)
            GameModel.Instance.RemoveModel<SelectCharacterBoxModel>();
    }

    private void Setup()
    {
        GameConfig.Instance.characterSkinSoContainer.Restore();
        model = new SelectCharacterBoxModel
        {
            selectType = SelectHeroType.Stat,
        };
        model.UpdateEvent.RemoveAllListeners();
        model.UpdateEvent.AddListener(UpdateView);
        GameModel.Instance.AddOrUpdateModel(model);
        views.ForEach(item => item.Init());
        UpdateView();
    }

    private void UpdateView()
    {
        views.ForEach(item => item.UpdateView());
    }
}