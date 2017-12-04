using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControlScript : MonoBehaviour {


    public float baseThrust = 10000;
    public float inputThrust = 6000;
    public float inputPitch = 40F;
    public float inputRoll = 50F;
    public float inputYaw = 30F;

    public float forwardDrag = 40;
    public float upwardDrag = 50;
    public float sidewaysDrag = 40;

    public float pitchDrag = 40F;
    public float rollDrag = 50F;
    public float yawDrag = 35F;

    public float pitchRecovery = 8F;
    public float rollRecovery = 40F;

    public float forwardSpeedBoostFraction = 3F;

    public GameObject Wreck;
    private GameObject wreckInstance;
    private float wreckTime = -1;
    private bool isWrecked = false;

    private Rigidbody rb;
	private HelicopterControls controls;


    private RotorCollisions[] rotors;
    private bool isImmortal = false;

    public GameObject HookAssembly;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
		controls = GetComponent<HelicopterControls> ();

        rotors = GetComponentsInChildren<RotorCollisions>();

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isWrecked)
        {
            float currentThrust = baseThrust + controls.Thrust * inputThrust;

            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            Vector3 localAngularVelocity = transform.InverseTransformDirection(rb.angularVelocity);



            rb.AddRelativeTorque(new Vector3(
                controls.Pitch * inputPitch
                    - localAngularVelocity.x * pitchDrag
                    - Mathf.Tan(rb.transform.localEulerAngles.x * Mathf.PI / 180) * pitchRecovery,
                controls.Yaw * inputYaw
                    - localAngularVelocity.y * yawDrag,
                controls.Roll * inputRoll
                    - localAngularVelocity.z * rollDrag
                    - Mathf.Tan(rb.transform.localEulerAngles.z * Mathf.PI / 180) * rollRecovery
                ), ForceMode.Force);

            rb.AddRelativeForce(
                new Vector3(
                    -SignedSqrt(localVelocity.x) * sidewaysDrag,
                    currentThrust - SignedSqrt(localVelocity.y) * upwardDrag,
                    Mathf.Max(new float[] { localVelocity.magnitude - 10, 0 }) * forwardSpeedBoostFraction - SignedSqrt(localVelocity.z) * forwardDrag
                    ),
                ForceMode.Force);


        }
        else
        {
            if (wreckTime > 0)
            {
                wreckTime -= Time.deltaTime;
            }
            else
            {
                GameObject.Destroy(wreckInstance);
                FindObjectOfType<GameController>().BroadcastMessage("OnRestart");

                //SetWrecked(false);
            }
        }
    }

    public void SetImmortal(bool immortal)
    {
        isImmortal = immortal;
    }

    void OnRotorCollision()
    {
        if (!isImmortal)
        {
            wreckInstance = Instantiate(Wreck, transform.position, transform.rotation);
            wreckTime = 2;
            SetWrecked(true);
			var explosionSound = transform.Find ("explosion").GetComponent<AudioSource> ();
			explosionSound.Play();
			FindObjectOfType<GameController>().BroadcastMessage("OnDeath");
        }
    }

    //public void Reset(GameObject spawn)
    //{
    //    transform.position = spawn.transform.position;
    //    transform.rotation = spawn.transform.rotation;
    //    rb.velocity = Vector3.zero;
    //    rb.angularVelocity = Vector3.zero;
    //    SetImmortal(false);
    //}

    private float SignedSqrt(float t)
    {
        return Mathf.Sign(t) * Mathf.Sqrt(Mathf.Abs(t));
    }

    private void SetWrecked(bool wrecked)
    {
        isWrecked = wrecked;
        foreach (var rotor in rotors)
        {
            rotor.gameObject.SetActive(!wrecked);
        }
    }


}
