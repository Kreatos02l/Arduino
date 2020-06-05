using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quality : MonoBehaviour {

    public void High()
    {
        QualitySettings.currentLevel = QualityLevel.Fantastic;
    }
    public void Good()
    {
        QualitySettings.currentLevel = QualityLevel.Good;
    }
    public void Medium()
    {
        QualitySettings.currentLevel = QualityLevel.Simple;
    }
    public void Low()
    {
        QualitySettings.currentLevel = QualityLevel.Fastest;
    }

}
