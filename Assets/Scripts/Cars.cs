using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cars : MonoBehaviour
{


    private bool initilized = false;

    private NeuralNetwork net;
    public Transform refrence;

    private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	private bool isBreaking;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;

	private float currentbreakForce;

    [SerializeField] public float breakForce;

    private Rigidbody rBody;
    private Material[] mats;

    private float time = 0.0f;
    public float intertime = 5f;


    
    void Start()
    {
       Time.timeScale = 10;
        
    
    
    
    }
    void Update ()
    {
        if (initilized == true)
        {
            float distance = Vector3.Distance(transform.position, refrence.position);
            
            if (Input.GetKey(KeyCode.DownArrow)) {
                
                Time.timeScale = Time.timeScale - 0.01f;

            }

            if (Input.GetKey(KeyCode.UpArrow)){

                Time.timeScale = Time.timeScale + 0.01f;
            }

            //if (distance > 200f)
            //    distance = 200f;\

            /*
            //Array.Reverse(mats);
            for (int i = 0; i < mats.Length; i++) {
                float fit = distance/22f;
                mats[i].color = new Color ((fit) / 20f, (1f-(fit / 20f)), (1f - (fit / 20f))); 

            }
            */

            //mats[NewManger.index].color = new Color (1,0,0);

            //Change
            float[] inputs = new float[6];

            float FMDE = RayCast.FMDE;
            float FMLDE = RayCast.FMLDE;
            float FMRDE = RayCast.FMRDE;
            float FLDE = RayCast.FLDE;
            float FRDE = RayCast.FRDE;
            float BMDE = RayCast.BMDE;

            inputs[0] = FMDE;
            inputs[1] = FMLDE;
            inputs[2] = FMRDE;
            inputs[3] = FLDE;
            inputs[4] = FRDE;
            inputs[5] = BMDE;


            float[] output = net.FeedForward(inputs);


            //float g = (float) (1 / (1 + (Math.Pow(2.7182818, (double) output[0]))));

            //float Tanh = (float) ((Math.Pow(Math.E, (double) output[0]) - Math.Pow(Math.E, (double) -output[0]) / (Math.Pow(Math.E, (double) output[0]) + Math.Pow(Math.E, (double) -output[0]))));
            
            //Debug.Log(rearPassengerW.brakeTorque);

            int binarystepOut;

            if (output[2] > 0) {
                binarystepOut = 1;
            }
            else {
                binarystepOut = 0;

            }

            m_steeringAngle = maxSteerAngle * output[0];
            if (m_steeringAngle > maxSteerAngle) 
            {
                m_steeringAngle = maxSteerAngle;
            }
		    frontDriverW.steerAngle = m_steeringAngle;
		    frontPassengerW.steerAngle = m_steeringAngle;

            //Output 2

            frontDriverW.motorTorque = output[1] * motorForce * 100f;
		    frontPassengerW.motorTorque = output[1] * motorForce * 100f;

            //Output 3


            frontDriverW.brakeTorque = breakForce * binarystepOut;
            frontPassengerW.brakeTorque = breakForce * binarystepOut;
            rearDriverW.brakeTorque = breakForce * binarystepOut;
            rearPassengerW.brakeTorque = breakForce * binarystepOut;

            UpdateWheelPoses();

            net.AddFitness(distance/18f);
            
            
            //Debug.Log(Collition);
            if (FMDE < 3 || FMLDE < 3 || FMRDE < 3 || FLDE < 3 || FRDE < 3 || BMDE < 2) {

                float sum = FMDE + FMLDE + FMRDE + FLDE +FRDE + BMDE;
                float avgsum = sum / 6f;
                float addfit = 24f/avgsum;

                time += Time.deltaTime;
                if (time>= intertime) {
                    time = time - intertime;
                    net.AddFitness(-addfit);
                    //Debug.Log("time5");
                }
            }  
            
            

        }
	}
    
    public void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Wall") {

            //Debug.Log("Wall Collition Detected");
            //net.SetFitness(0);
            //gameObject.SetActive(false);
            //net.AddFitness((-10*(NewManger.generationNumber/10)));
           

           
           

        }
    }

    public void Init(NeuralNetwork net, Transform refrence)
    {
        this.net = net;
        this.refrence = refrence;
        initilized = true;
    }

    

    private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

}