using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class FirestoreGet : MonoBehaviour
{

    public Renderer rndr;

    public TempAndHumidity Data;
    //setting up variables dtatabase and listener
    public float Humid;

    public float Temp;

    FirebaseFirestore db;

    ListenerRegistration listenerRegistration;

    // Start is called before the first frame update
    void Start()
    {
        //setting up database for firestore
        db = FirebaseFirestore.DefaultInstance;
        //settinh up listener with callback
        listenerRegistration = db.Collection("SensorData").Document("TempAndHumidity").Listen(snapshot =>
        {
             Data = snapshot.ConvertTo<TempAndHumidity>();
            Debug.Log(Data.Humidity.ToString());
            Humid = float.Parse(Data.Humidity.ToString());
            Temp = float.Parse(Data.Temperature.ToString());

            Color clr = new Color(Humid, Temp, 0);
            rndr.material.SetColor("_Color", clr);
        });
      
    }

    public void GetData()
    {
        db.Collection("SensorData").Document("TempAndHumidity").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Data = task.Result.ConvertTo<TempAndHumidity>();
            Debug.Log( float.Parse(Data.Humidity.ToString()));
    
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
