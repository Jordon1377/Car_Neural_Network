using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    //Gameobject Refrences
    public GameObject FMCast;
    public GameObject FMLCast;
    public GameObject FMRCast;
    public GameObject FLCast;
    public GameObject FRCast;
    public GameObject BMCast;
    
    //Distances
    public float FMDistance;
    public float FMLDistance;
    public float FMRDistance;
    public float FLDistance;
    public float FRDistance;
    public float BMDistance;

    //DistanceError From Target Variables
    private float distanceErrorFM;
    private float distanceErrorFML;
    private float distanceErrorFMR;
    private float distanceErrorFL;
    private float distanceErrorFR;
    private float distanceErrorBM;

    public static float FMDE;
    public static float FMLDE;
    public static float FMRDE;
    public static float FLDE;
    public static float FRDE;
    public static float BMDE;

    public LayerMask IgnorePlayer;

    public RaycastHit hit;

    // Update is called once per frame
    void FixedUpdate()
    {

        //Initializing Ray Variables
        Vector3 frontMiddlePos = FMCast.transform.position;
        Vector3 frontMiddleLeftPos = FMLCast.transform.position;
        Vector3 frontMiddleRightPos = FMRCast.transform.position;
        Vector3 frontLeftPos = FLCast.transform.position;
        Vector3 frontRightPos = FRCast.transform.position;
        Vector3 backMiddlePos = BMCast.transform.position;

        Vector3 dirFM = FMCast.transform.TransformDirection(new Vector3(0,0,1));
        Vector3 dirFML = FMLCast.transform.TransformDirection(new Vector3(-0.5f,0,1));
        Vector3 dirFMR = FMRCast.transform.TransformDirection(new Vector3(0.5f,0,1));
        Vector3 dirFL = FLCast.transform.TransformDirection(new Vector3(-1.5f,0,1));
        Vector3 dirFR = FRCast.transform.TransformDirection(new Vector3(1.5f,0,1));
        Vector3 dirBM = BMCast.transform.TransformDirection(new Vector3(0,0,-1));

        
        Ray FMRay = new Ray(frontMiddlePos, dirFM);
        Ray FMLRay = new Ray(frontMiddleLeftPos, dirFML);
        Ray FMRRay = new Ray(frontMiddleRightPos, dirFMR);
        Ray FLRay = new Ray(frontLeftPos, dirFL);
        Ray FRRay = new Ray(frontRightPos, dirFR);
        Ray BMRay = new Ray(backMiddlePos, dirBM);


        Debug.DrawRay(frontMiddlePos, dirFM * FMDistance, Color.red);
        Debug.DrawRay(frontMiddleLeftPos, dirFML * FMLDistance, Color.red);
        Debug.DrawRay(frontMiddleRightPos, dirFMR * FMRDistance, Color.red);
        Debug.DrawRay(frontLeftPos, dirFL * FLDistance, Color.red);
        Debug.DrawRay(frontRightPos, dirFR * FRDistance, Color.red);
        Debug.DrawRay(backMiddlePos, dirBM * BMDistance, Color.red);


        
        FMDE = resultRay(FMRay, FMDistance, distanceErrorFM, hit);
        FMLDE = resultRay(FMLRay, FMLDistance, distanceErrorFML, hit);
        FMRDE = resultRay(FMRRay, FMRDistance, distanceErrorFMR, hit);
        FLDE = resultRay(FLRay, FLDistance, distanceErrorFL, hit);
        FRDE = resultRay(FRRay, FRDistance, distanceErrorFR, hit);
        BMDE = resultRay(BMRay, BMDistance, distanceErrorBM, hit);

        //Debug.Log(FMDE);

        

        /*
        if (Physics.Raycast(FMRay, out hit, FMDistance)){
            distanceErrorFM = hit.distance;
            Debug.Log(distanceErrorFM);
        }
        else {
            distanceErrorFM = FMDistance;
        }



        if (Physics.Raycast(FMLRay, out hit, FMLDistance)){
            distanceErrorFML = hit.distance;
        }
        else {
            distanceErrorFML = FMLDistance;
        }



        if (Physics.Raycast(FMRRay, out hit, FMRDistance)){
            distanceErrorFMR = hit.distance;
        }
        else {
            distanceErrorFMR = FMRDistance;
        }



        if (Physics.Raycast(FLRay, out hit, FLDistance)){
            distanceErrorFL = hit.distance;
        }
        else {
            distanceErrorFL = FLDistance;
        }



        if (Physics.Raycast(FRRay, out hit, FRDistance)){
            distanceErrorFR = hit.distance;
        }
        else {
            distanceErrorFR = FRDistance;
        }



        if (Physics.Raycast(BMRay, out hit, BMDistance)){
            distanceErrorBM = hit.distance;
        }
        else {
            distanceErrorBM = BMDistance;
        }



        //Debug.Log("Raycast hit:" + distanceError);
        */
    }

    public float resultRay(Ray ray, float distance, float distanceError, RaycastHit h){

        if (Physics.Raycast(ray, out h, distance, ~IgnorePlayer)){
            distanceError = h.distance;
            
        }
        else {
            distanceError = distance;
        }

        return distanceError;
        
    }
    
        
    

}
