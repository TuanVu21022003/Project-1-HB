using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private LeveItemUI levelItemPrefab;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private LevelOS levelData;
    // Start is called before the first frame update
    void Start()
    {
        SelectLevelList();
    }

    public void SelectLevelItem(string textIndex)
    {
        LeveItemUI levelItem = Instantiate(levelItemPrefab, parentPosition);
        levelItem.OnInit(textIndex);
    }

    public void SelectLevelList()
    {
        for (int i = 0; i < levelData.list.Count; i++)
        {
            SelectLevelItem(levelData.list[i].levelIndex.ToString());
        }
    }
}
