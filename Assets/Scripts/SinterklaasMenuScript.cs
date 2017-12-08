using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinterklaasMenuScript : LevelMenuScript  {

    public override void Start()
    {
        PauseMenu.SetActive(true);
        //DeathMenu.SetActive(false);
        LevelCompleteMenu.SetActive(false);
    }
    

}
