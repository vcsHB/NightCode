using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Basement;
using Basement.Training;

public class UIManager : MonoSingleton<UIManager>
{
    public Dictionary<UIType, IUIPanel> uiPanels;
    public TrainingExplainUI trainingUI;

    protected override void Awake()
    {
        base.Awake();

        uiPanels = new Dictionary<UIType, IUIPanel>();

        IUIPanel techTreePanel = FindFirstObjectByType<SkillTreePanel>().GetComponent<IUIPanel>();
        IUIPanel furniturePanel = FindFirstObjectByType<FurnitureUI>().GetComponent<IUIPanel>();
        IUIPanel floorSelectPanel = FindFirstObjectByType<FloorSelectUI>().GetComponent<IUIPanel>();
        IUIPanel characterSelectPanel = FindFirstObjectByType<CharacterSelectPanel>().GetComponent<IUIPanel>();

        techTreePanel.Close();
        furniturePanel.Close();
        floorSelectPanel.Close(); 
        characterSelectPanel.Close();

        uiPanels.Add(UIType.FurnitureUI, furniturePanel);
        uiPanels.Add(UIType.SkillTreePanel, techTreePanel);
        uiPanels.Add(UIType.FloorSelectPanel, floorSelectPanel);
        uiPanels.Add(UIType.CharacterSelectPanel, characterSelectPanel);
    }

    public IUIPanel GetUIPanel(UIType uiType) => uiPanels[uiType];

    //디버깅용 코드

    private bool _trainingPanelOpen = false;
    private bool _skillTreePanelOpen = false;
    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (!_trainingPanelOpen)
            {
                // uiPanels[UIType.TrainingPanel].Open(Vector2.zero);
                _trainingPanelOpen = true;
            }
            else
            {
                //uiPanels[UIType.TrainingPanel].Close();
                _trainingPanelOpen = false;
            }
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (!_skillTreePanelOpen)
            {
                uiPanels[UIType.SkillTreePanel].Open(Vector2.zero);
                _skillTreePanelOpen = true;
            }
            else
            {
                uiPanels[UIType.SkillTreePanel].Close();
                _skillTreePanelOpen = false;
            }
        }
    }
}

public enum UIType
{
    SkillTreePanel,
    FloorSelectPanel,
    BuildUIPanel,
    CharacterSelectPanel,
    FurnitureUI
    //TrainingPanel
}
