using Game;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{

    [SerializeField] private List<MenuWithId> menusWithId;
    [SerializeField] private DataSource<GameManager> gameManagerDataSource;
    [SerializeField] private List<DataSource<string>> SourceOfIdsToTellGameManager;
    [SerializeField] private List<string> idsToTellGameManager;
    [SerializeField] private VoidEventChannel WinEvent;
    [SerializeField] private VoidEventChannel LoseEvent;


    [SerializeField] private DataSource<string> WinID;
    [SerializeField] private DataSource<string> LoseID;
    private int _currentMenuIndex = 0;
    private void OnEnable()
    {
        WinEvent.OnEventRaised += ShowWinMenu;
        LoseEvent.OnEventRaised += ShowLoseMenu;
       
    }
    private void OnDisable()
    {
        WinEvent.OnEventRaised -= ShowWinMenu;
        LoseEvent.OnEventRaised -= ShowLoseMenu;
    }
    private void Start()
    {
        foreach (var source in SourceOfIdsToTellGameManager)
        {
            idsToTellGameManager.Add(source.Value);
        }
        foreach (var menu in menusWithId)
        {
            menu.Menu.Setup();
            menu.Menu.OnChangeMenu += HandleChangeMenu;
            menu.Menu.gameObject.SetActive(false);
        }

        if (menusWithId.Count > 0)
        {
            menusWithId[_currentMenuIndex].Menu.gameObject.SetActive(true);
        }
    }
    private void HandleChangeMenu(string id)
    {
        if (idsToTellGameManager.Contains(id) && gameManagerDataSource != null && gameManagerDataSource.Value != null)
        {
            menusWithId[_currentMenuIndex].Menu.gameObject.SetActive(false);
            gameManagerDataSource.Value.HandleSpecialEvents(id);
        }
        for (var i = 0; i < menusWithId.Count; i++)
        {
            var menuWithId = menusWithId[i];
            if (menuWithId.ID.Value == id)
            {
                menusWithId[_currentMenuIndex].Menu.gameObject.SetActive(false);
                menuWithId.Menu.gameObject.SetActive(true);
                _currentMenuIndex = i;
                break;
            }
        }
    }

    public void ShowWinMenu()
    {
        HandleChangeMenu(WinID.Value);
    }
    public void ShowLoseMenu()
    {
        HandleChangeMenu(LoseID.Value);
    }

    [Serializable]
    public struct MenuWithId
    {
        [field: SerializeField] public DataSource<string> ID { get; set; }
        [field: SerializeField] public Menu Menu { get; set; }
    }
}


