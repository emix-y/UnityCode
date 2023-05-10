using Firebase.Firestore;
using System;
using Unity.VisualScripting;

[Serializable]
[FirestoreData()]
public struct TempAndHumidity
{
    [FirestoreProperty]
    public string Humidity { get; set; }

    [FirestoreProperty]
    public string Temperature { get; set; }

}
