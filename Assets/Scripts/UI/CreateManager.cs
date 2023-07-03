using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    public enum Vehicle
    {
        Ambulance,
        Delivery,
        DeliveryFlat,
        FireTruck,
        GarbageTruck,
        HatchbackSports,
        Police,
        Race,
        RaceFuture,
        Sedan,
        SedanSports,
        SUV,
        SUVLuxury,
        Taxi,
        Tractor,
        TractorPolice,
        TractorShovel,
        Truck,
        TruckFlat,
        Van
    }
    public Vehicle chosenVehicle;

    public enum Blaster
    {
        BlasterA,
        BlasterB,
        BlasterC,
        BlasterD,
        BlasterE,
        BlasterF,
        BlasterG,
        BlasterH,
        BlasterI,
        BlasterJ,
        BlasterK,
        BlasterL,
        BlasterM,
        BlasterN,
        BlasterO,
        BlasterP,
        BlasterQ,
        BlasterR,
    }
    public Blaster chosenBlaster;

    private Vector3 blasterLocation;

    // Start is called before the first frame update
    void Start()
    {
        chosenVehicle = Vehicle.Ambulance;
        chosenBlaster = Blaster.BlasterA;
        blasterLocation = new Vector3(0, 0.58f, 1.63f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVehicle(Vehicle vehicle)
    {
        switch (vehicle)
        {
            case Vehicle.Ambulance:
                blasterLocation = new Vector3(0, 0.58f, 1.63f);
                break;
            case Vehicle.Delivery:

                break;
            case Vehicle.DeliveryFlat:

                break;
            case Vehicle.FireTruck:

                break;
            case Vehicle.GarbageTruck:

                break;
            case Vehicle.HatchbackSports:

                break;
            case Vehicle.Police:

                break;
            case Vehicle.Race:

                break;
            case Vehicle.RaceFuture:

                break;
            case Vehicle.Sedan:

                break;
            case Vehicle.SedanSports:

                break;
            case Vehicle.SUV:

                break;
            case Vehicle.SUVLuxury:

                break;
            case Vehicle.Taxi:

                break;
            case Vehicle.Tractor:

                break;
            case Vehicle.TractorPolice:

                break;
            case Vehicle.TractorShovel:

                break;
            case Vehicle.Truck:

                break;
            case Vehicle.TruckFlat:

                break;
            case Vehicle.Van:

                break;
        }
        chosenVehicle = vehicle;
    }

    public void ChangeBlaster(Blaster blaster)
    {
        switch (blaster)
        {
            case Blaster.BlasterA:

                break;
            case Blaster.BlasterB:

                break;
            case Blaster.BlasterC:

                break;
            case Blaster.BlasterD:

                break;
            case Blaster.BlasterE:

                break;
            case Blaster.BlasterF:

                break;
            case Blaster.BlasterG:

                break;
            case Blaster.BlasterH:

                break;
            case Blaster.BlasterI:

                break;
            case Blaster.BlasterJ:

                break;
            case Blaster.BlasterK:

                break;
            case Blaster.BlasterL:

                break;
            case Blaster.BlasterM:

                break;
            case Blaster.BlasterN:

                break;
            case Blaster.BlasterO:

                break;
            case Blaster.BlasterP:

                break;
            case Blaster.BlasterQ:

                break;
            case Blaster.BlasterR:

                break;
        }
        chosenBlaster = blaster;
    }
}
