using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public GameObject restartButton;
    // Start is called before the first frame update    
    public GameObject textPrefab;  
    public int numberOfColumns = 5;  
    public int rowsPerColumn = 10;  
    public float columnSpacing = 100f;  
    public float rowSpacing = 30f; 

    void Start()
    {
        restartButton.SetActive(false);
        StartCoroutine(think());
    }

    void Update() 
    {
        
    }

    IEnumerator think() 
    {
        yield return new WaitForSeconds(2);
        restartButton.SetActive(true);
    }
    void CreateColumn(int columnIndex)
    {
        GameObject column = new GameObject("Column_" + columnIndex);
        column.transform.SetParent(this.transform, false); 

        RectTransform columnRect = column.AddComponent<RectTransform>();
        columnRect.anchorMin = new Vector2(0, 1);  
        columnRect.anchorMax = new Vector2(0, 1);  
        columnRect.pivot = new Vector2(0, 1);      

        float xPosition = columnIndex * columnSpacing;
        columnRect.anchoredPosition = new Vector2(xPosition, 0);  
        for (int j = 0; j < rowsPerColumn; j++)
        {
            GameObject textInstance = Instantiate(textPrefab, column.transform);
            TMP_Text tmpText = textInstance.GetComponent<TMP_Text>();

            if (tmpText != null)
            {
                tmpText.text = "you failed us";  

                RectTransform textRect = textInstance.GetComponent<RectTransform>();
                textRect.anchorMin = new Vector2(0, 1);  
                textRect.anchorMax = new Vector2(0, 1);  
                textRect.pivot = new Vector2(0, 1);      

                float yPosition = -j * rowSpacing;  
                textRect.anchoredPosition = new Vector2(0, yPosition);  
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the prefab.");
            }
        }
    }
}
