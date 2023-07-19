using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameHandler : MonoBehaviour {

    [SerializeField] private Text timerText;
    [SerializeField] private Text targetCode;
    [SerializeField] private Text firstPinPosText;
    [SerializeField] private Text secondPinPosText;
    [SerializeField] private Text thirdPinPosText;
    [SerializeField] private Text warningText;
    [SerializeField] private Text gameResultText;
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private Text drillStatText;
    [SerializeField] private Text hammerStatText;
    [SerializeField] private Text lockpickStatText;

    private int[] pinsPosition = new int[3];
    private int[] lockCode = new int[3];
    private int[] drillStats = new int[] { 1, -1, -1 };
    private int[] hardDrillStats = new int[3];
    private int[] hammerStats = new int[] { 2, -1, 2 };
    private int[] hardHammerStats = new int[3];
    private int[] lockpickStats = new int[] { -1, 1, 2 };
    private int[] hardLockpickStats = new int[3];

    private static float timerTime = 60F;
    private float currentTime;

    private bool isHardMode;
    private bool gameWin;

    public void setHardMode(bool mode) {
        isHardMode = mode;
    }

    private void OnEnable() {
        currentTime = timerTime;

        for (int i = 0; i < lockCode.Length; i++) {
            lockCode[i] = UnityEngine.Random.Range(0, 11);
        }

        targetCode.text = $"ƒл€ взлома вы должны поставить пины в положение:";
        for (int i = 0; i < lockCode.Length; i++) {
            targetCode.text += " " + lockCode[i];
        }

        for (int i = 0; i < pinsPosition.Length; i++) {
            pinsPosition[i] = UnityEngine.Random.Range(2, 9);
        }

        UpdatePinPositions();
        updateInstDesc();

    }

    private void Update() {
        if (currentTime >= 0F) {
            currentTime -= Time.deltaTime;
            timerText.text = Mathf.Round(currentTime).ToString();
        } else {
            gameWin = false;
            StopGame(gameWin);
        }

        if (pinsPosition.SequenceEqual(lockCode)) {
            gameWin = true;
            StopGame(gameWin);
        }
    }

    private void UpdatePinPositions() {
        firstPinPosText.text = pinsPosition[0].ToString();
        secondPinPosText.text = pinsPosition[1].ToString();
        thirdPinPosText.text = pinsPosition[2].ToString();
        if(isHardMode) {
            RandomizeInstrument("drill");
            RandomizeInstrument("hammer");
            RandomizeInstrument("lockpick");
        }
    }

    private void updateInstDesc() {
        if (!isHardMode) {
            drillStatText.text = "";
            for (int i = 0; i < drillStats.Length; i++) {
                if (drillStats[i] >= 0) {
                    drillStatText.text += (" +" + drillStats[i]).ToString();
                } else {
                    drillStatText.text += (" " + drillStats[i]).ToString();
                }
            }
            hammerStatText.text = "";
            for (int i = 0; i < hammerStats.Length; i++) {
                if (hammerStats[i] >= 0) {
                    hammerStatText.text += (" +" + hammerStats[i]).ToString();
                } else {
                    hammerStatText.text += (" " + hammerStats[i]).ToString();
                }
            }
            lockpickStatText.text = "";
            for (int i = 0; i < lockpickStats.Length; i++) {
                if (lockpickStats[i] >= 0) {
                    lockpickStatText.text += (" +" + lockpickStats[i]).ToString();
                } else {
                    lockpickStatText.text += (" " + lockpickStats[i]).ToString();
                }
            }
        }
    }

    public void DrillOnClick() {
        if (!isHardMode) {
            if (CheckIfInstApllicable(drillStats)) {
                for (int i = 0; i < pinsPosition.Length; i++) {
                    pinsPosition[i] += drillStats[i];
                    warningText.text = "¬ы успешно применили инструмент дрель";
                }
            } else {
                warningText.text = "Ќевозможно применить дрель, значение выходит за пределы допустимого диапозона";
            }
        } else {
            if (CheckIfInstApllicable(hardDrillStats)) {
                for (int i = 0; i < pinsPosition.Length; i++) {
                    pinsPosition[i] += hardDrillStats[i];
                    warningText.text = "¬ы успешно применили инструмент дрель";
                }
            } else {
                warningText.text = "Ќевозможно применить дрель, значение выходит за пределы допустимого диапозона";
            }
        }
        UpdatePinPositions();
    }

    public void HammerOnClick() {
        if (!isHardMode) {
            if (CheckIfInstApllicable(hammerStats)) {
                for (int i = 0; i < pinsPosition.Length; i++) {
                    pinsPosition[i] += hammerStats[i];
                    warningText.text = "¬ы успешно применили инструмент молоток";
                }
            } else {
                warningText.text = "Ќевозможно применить молоток, значение выходит за пределы допустимого диапозона";
            }
        } else {
            if (CheckIfInstApllicable(hardHammerStats)) {
                for (int i = 0; i < pinsPosition.Length; i++) {
                    pinsPosition[i] += hardHammerStats[i];
                    warningText.text = "¬ы успешно применили инструмент молоток";
                }
            } else {
                warningText.text = "Ќевозможно применить молоток, значение выходит за пределы допустимого диапозона";
            }
        }
        UpdatePinPositions();
    }

    public void LockpickOnClick() {
        if (!isHardMode) {
            if (CheckIfInstApllicable(lockpickStats)) {
                for (int i = 0; i < pinsPosition.Length; i++) {
                    pinsPosition[i] += lockpickStats[i];
                    warningText.text = "¬ы успешно применили инструмент отмычка";
                }
            } else {
                warningText.text = "Ќевозможно применить отмычка, значение выходит за пределы допустимого диапозона";
            }
        } else {
            if (CheckIfInstApllicable(hardLockpickStats)) {
                for (int i = 0; i < pinsPosition.Length; i++) {
                    pinsPosition[i] += hardLockpickStats[i];
                    warningText.text = "¬ы успешно применили инструмент отмычка";
                }
            } else {
                warningText.text = "Ќевозможно применить отмычка, значение выходит за пределы допустимого диапозона";
            }
        }
        UpdatePinPositions();
    }

    public bool CheckIfInstApllicable(int[] instrument) {
        bool isInstApplicable = true;
        for (int i = 0; i < pinsPosition.Length; i++) {
            if (pinsPosition[i] + instrument[i] < 0 || pinsPosition[i] + instrument[i] > 10) {
                isInstApplicable = false;
            }
        }
        return isInstApplicable;
    }

    public void RandomizeInstrument(string instrument) {
        if (instrument == "drill") {
            drillStatText.text = "";
            for (int i = 0; i < hardDrillStats.Length; i++) {
                hardDrillStats[i] = UnityEngine.Random.Range(-2, 3);
                if (hardDrillStats[i] >= 0) {
                    drillStatText.text += (" +" + hardDrillStats[i]).ToString();
                } else {
                    drillStatText.text += (" " + hardDrillStats[i]).ToString();
                }
            }
        } else if (instrument == "hammer") {
            hammerStatText.text = "";
            for (int i = 0; i < hardHammerStats.Length; i++) {
                hardHammerStats[i] = UnityEngine.Random.Range(-3, 4);
                if (hardHammerStats[i] >= 0) {
                    hammerStatText.text += (" +" + hardHammerStats[i]).ToString();
                } else {
                    hammerStatText.text += (" " + hardHammerStats[i]).ToString();
                }
            }
        } else if (instrument == "lockpick") {
            lockpickStatText.text = "";
            for (int i = 0; i < hardLockpickStats.Length; i++) {
                hardLockpickStats[i] = UnityEngine.Random.Range(-1, 2);
                if (hardLockpickStats[i] >= 0) {
                    lockpickStatText.text += (" +" + hardLockpickStats[i]).ToString();
                } else {
                    lockpickStatText.text += (" " + hardLockpickStats[i]).ToString();
                }
            }
        }
    }

    private void StopGame(bool result) {
        if (result) {
            gameResultText.text = "ѕобеда";
        } else {
            gameResultText.text = "ѕоражение";
        }
        gameCanvas.SetActive(false);
        endGameCanvas.SetActive(true);
    }

}
