using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : MonoBehaviour
{

    HingeJoint joint;
    public float minWindForce;
    public float maxWindForce;
    //l'interval de temps abans de que canvii la v del vent
    public float updateInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
      
        joint = GetComponent<HingeJoint>();
        // Cridem el mètode UpdateWindForce() repetidament
        // cada "updateInterval" segons
        InvokeRepeating("UpdateWindForce", 0f, updateInterval);
    }

    
    void UpdateWindForce()
    {
        //executem les funcions cada frame, primer calculem la velocitat del vent 
        //i després apliquem la força al motor del HingeJoint utilitzant-la
        float windSpeed = CalculateWindSpeed();
        ApplyWindForce(windSpeed);
    }
    float CalculateWindSpeed()
    {
        //calculem la velocitat del vent. Fem que sigui aleatoria entre dos valors
        float windForce = Random.Range(minWindForce, maxWindForce);
        return windForce;
        
    }
    void ApplyWindForce(float windSpeed)
    {
        //apliquem la força del vent al molí girant el hingejoint en funció 
        //de la velocitat del vent calculada anteriorment
        var motor = joint.motor;
        motor.targetVelocity = windSpeed;
        joint.motor = motor;
    }
}

