using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDataManager : MonoBehaviour
{
    [Header("Data Object")] public QuickDataScriptableObject data;
    public List<int> generatedTodaysWinningBlackNumbers;
    public List<int> generatedTodaysWinningRedNumbers;

    public List<int> generatedBlackNumbers;

    public List<int> generatedRedNumbers;

    // Start is called before the first frame update
    private void Awake()
    {
        GenerateTodaysWinningBlackNumbers();
        GenerateTodaysWinningRedNumbers();
        GenerateBlackNumbers();
        GenerateRedNumbers();
    }

    #region GETTING DATA FROM SERVER

    private void GenerateTodaysWinningBlackNumbers()
    {
        while (generatedTodaysWinningBlackNumbers.Count < 5)
        {
            var tempNumber = Random.Range(1, 69);
            if (!generatedTodaysWinningBlackNumbers.Contains(tempNumber))
            {
                generatedTodaysWinningBlackNumbers.Add(tempNumber);
            }
        }
    }

    private void GenerateTodaysWinningRedNumbers()
    {
        while (generatedTodaysWinningRedNumbers.Count < 1)
        {
            var tempNumber = Random.Range(1, 26);
            if (!generatedTodaysWinningRedNumbers.Contains(tempNumber))
            {
                generatedTodaysWinningRedNumbers.Add(tempNumber);
            }
        }
    }

    private void GenerateBlackNumbers()
    {
        while (generatedBlackNumbers.Count < 5)
        {
            var tempNumber = Random.Range(1, 69);
            if (!generatedBlackNumbers.Contains(tempNumber))
            {
                generatedBlackNumbers.Add(tempNumber);
            }
        }
    }

    private void GenerateRedNumbers()
    {
        while (generatedRedNumbers.Count < 1)
        {
            var tempNumber = Random.Range(1, 26);
            if (!generatedRedNumbers.Contains(tempNumber))
            {
                generatedRedNumbers.Add(tempNumber);
            }
        }
    }

    #endregion

    private void ClearExistingData()
    {
        generatedBlackNumbers.Clear();
        generatedRedNumbers.Clear();
    }
}