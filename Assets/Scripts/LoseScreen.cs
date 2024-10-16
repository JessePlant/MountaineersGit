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
    public GameObject textPrefab;  // Assign the "you failed us" Text prefab
    public int numberOfColumns = 5;  // How many columns across the screen
    public int rowsPerColumn = 10;  // How many rows per column
    public float columnSpacing = 100f;  // Spacing between columns
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
        // Create a parent GameObject to hold the column
        GameObject column = new GameObject("Column_" + columnIndex);
        column.transform.SetParent(this.transform, false);  // Set the parent to the Canvas

        // Add and configure RectTransform for the column
        RectTransform columnRect = column.AddComponent<RectTransform>();
        columnRect.anchorMin = new Vector2(0, 1);  // Top-left anchor
        columnRect.anchorMax = new Vector2(0, 1);  // Top-left anchor
        columnRect.pivot = new Vector2(0, 1);      // Pivot at the top-left

        // Calculate position for the column
        float xPosition = columnIndex * columnSpacing;
        columnRect.anchoredPosition = new Vector2(xPosition, 0);  // Set anchored position relative to the top-left of the canvas

        // Create rows within this column
        for (int j = 0; j < rowsPerColumn; j++)
        {
            // Instantiate the TextMeshProUGUI prefab
            GameObject textInstance = Instantiate(textPrefab, column.transform);
            TMP_Text tmpText = textInstance.GetComponent<TMP_Text>();

            if (tmpText != null)
            {
                tmpText.text = "you failed us";  // Set the text

                // Set RectTransform for the text instance
                RectTransform textRect = textInstance.GetComponent<RectTransform>();
                textRect.anchorMin = new Vector2(0, 1);  // Top-left anchor
                textRect.anchorMax = new Vector2(0, 1);  // Top-left anchor
                textRect.pivot = new Vector2(0, 1);      // Pivot at the top-left

                // Calculate and set position for each row
                float yPosition = -j * rowSpacing;  // Negative to position below the previous row
                textRect.anchoredPosition = new Vector2(0, yPosition);  // Set position relative to the column
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the prefab.");
            }
        }
    }
}
