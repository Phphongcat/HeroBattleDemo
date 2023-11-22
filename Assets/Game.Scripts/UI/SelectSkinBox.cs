using System.Collections.Generic;
using DG.Tweening;
using QtNameSpace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class SelectSkinBox : QtBox
{
    [SerializeField] private SelectCharacterBoxModel model;
    [SerializeField] private RectTransform selectSkinPanel;
    [SerializeField] private Button backButton;
    [SerializeField] private float moveX; 
    [SerializeField] private float scaleValue; 
    [SerializeField] private List<ABaseElement> views;
    private Vector3 _startPosition;
    private Vector3 _scaleSize;
    private Sequence _sequence;
    

    public override void Open(bool useAmin = true, UnityAction action = null)
    {
        action = () =>
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(selectSkinPanel
                .DOLocalMoveX(_startPosition.x + moveX, durationMotion)
                .SetEase(motionEase));
            _sequence.Join(selectSkinPanel
                .DOScale(Vector3.one * scaleValue, durationMotion)
                .SetEase(motionEase));
        };
        Setup();
        base.Open(false, action);
    }

    public override void Close(bool useAmin = true, UnityAction action = null)
    {
        action = () =>
        {
            _sequence.Kill();
            selectSkinPanel.DOKill();
            selectSkinPanel.localPosition = _startPosition;
            selectSkinPanel.localScale = _scaleSize;
        };
        base.Close(false, action);
    }
    
    private void Awake()
    {
        _startPosition = selectSkinPanel.localPosition;
        _scaleSize = selectSkinPanel.localScale;
        
        backButton.onClick.AddListener(Back);
    }

    private void OnDestroy()
    {
        if(GameModel.Instance != null)
            GameModel.Instance.RemoveModel<SelectCharacterBoxModel>();
    }

    private void Setup()
    {
        model = new SelectCharacterBoxModel
        {
            selectType = SelectHeroType.Skin,
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

    private async void Back()
    {
        var box = await QtUIContainer.Instance.LoadContainer(UIName.SelectCharacterBox);
        Close(false);
        box.Open();
    }
}
