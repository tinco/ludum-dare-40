using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinterklaasController : GameController {

    public override void OnRestart()
    {
        if (!isWrecked)
        {
            base.OnRestart();
            var chimnies = (FinishBehaviorV2[])FindObjectsOfType<FinishBehaviorV2>();
            foreach (var chimney in chimnies)
            {
                chimney.Reset();
            }
        }
        else
        {
            isComplete = false;
            isWrecked = false;
            //foundPickups = 0;

            var spawn = GameObject.FindGameObjectWithTag("Respawn");

            //helicopterInstance.Reset(spawn);
            Destroy(helicopterInstance.gameObject);
            var playerInstance = Instantiate(helicopterPrefab, spawn.transform.position, spawn.transform.rotation);
            helicopterInstance = playerInstance.GetComponent<HelicopterControlScript>();

            var camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<CameraScript>().Helicopter = playerInstance.GetComponent<Rigidbody>();
            camera.GetComponent<CameraScript>().SetMobile(true);

            var pickups = (Pickupable[])FindObjectsOfType(typeof(Pickupable));

            foreach (var pickup in pickups)
            {
                if (pickup.gameObject.activeSelf)
                {
                    pickup.Reset();
                }
            }
            Time.timeScale = 1;
            elapsedTime = 0;
            isTimerActive = true;
        }
    }

    public override void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 250, 100), GetFormattedTime(), guiStyle);


        GUI.Label(new Rect(Screen.width - 180, 10, 250, 100),
            string.Format("Deliveries: {0} / {1}", foundPickups, requiredPickups),
            guiStyle);


        string hint="";
        switch (Deaths/3)
        {
            case 0:
                hint = "?? ??? ??????????????";
                break;
            case 1:
                hint = "?n ??e ??s??n??a??i??";
                break;
            case 2:
                hint = "?n t?e w?sh?ng?ac?in?";
                break;
            default:
                hint = "In the washingmachine";
                break;

        }
        GUI.Label(new Rect(Screen.width/2 - 125, 10, 250, 100), hint, guiStyle);
    }


}
