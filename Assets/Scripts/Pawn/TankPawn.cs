using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankPawn : Pawn
{
    [Header("Prefab Children")]
    private GameManager gameManager;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject specialFirePoint;
    [SerializeField] private GameObject minePoint;
    private GameObject createManager;
    private CreateManager tankPrefabs;

    private GameObject vehicle;
    [SerializeField] private GameObject blaster;

    private float shootTimer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject createManager = GameObject.Find("CreateManager");
        if (createManager != null)
        {
            tankPrefabs = createManager.GetComponent<CreateManager>();
        }

        // Set the vehicle data from the vehicle menu
        if (tankPrefabs != null)
        {
            switch (tankPrefabs.chosenVehicle)
            {
                case CreateManager.Vehicle.Ambulance:
                    vehicle = Instantiate(gameManager.ambulance, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Delivery:
                    vehicle = Instantiate(gameManager.delivery, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.DeliveryFlat:
                    vehicle = Instantiate(gameManager.deliveryFlat, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.FireTruck:
                    vehicle = Instantiate(gameManager.fireTruck, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.GarbageTruck:
                    vehicle = Instantiate(gameManager.garbageTruck, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.HatchbackSports:
                    vehicle = Instantiate(gameManager.hatchbackSports, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Police:
                    vehicle = Instantiate(gameManager.police, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Race:
                    vehicle = Instantiate(gameManager.race, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.RaceFuture:
                    vehicle = Instantiate(gameManager.raceFuture, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Sedan:
                    vehicle = Instantiate(gameManager.sedan, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.SedanSports:
                    vehicle = Instantiate(gameManager.sedanSports, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.SUV:
                    vehicle = Instantiate(gameManager.suv, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.SUVLuxury:
                    vehicle = Instantiate(gameManager.suvLuxury, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Taxi:
                    vehicle = Instantiate(gameManager.taxi, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Tractor:
                    vehicle = Instantiate(gameManager.tractor, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.TractorPolice:
                    vehicle = Instantiate(gameManager.tractorPolice, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.TractorShovel:
                    vehicle = Instantiate(gameManager.tractorShovel, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Truck:
                    vehicle = Instantiate(gameManager.truck, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.TruckFlat:
                    vehicle = Instantiate(gameManager.truckFlat, transform.position, transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Vehicle.Van:
                    vehicle = Instantiate(gameManager.van, transform.position, transform.rotation, gameObject.transform);
                    break;
            }
            blaster.transform.localPosition = vehicle.GetComponent<VehicleData>().blasterLocation;
            firePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().firePoint;
            specialFirePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().specialFirePoint;
            minePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().minePoint;
            health.maxHealth = vehicle.GetComponent<VehicleData>().health;
            health.currentHealth = vehicle.GetComponent<VehicleData>().health;
            moveSpeed = vehicle.GetComponent<VehicleData>().speed;
            turnSpeed = vehicle.GetComponent<VehicleData>().turnSpeed;

            switch (tankPrefabs.chosenBlaster)
            {
                case CreateManager.Blaster.BlasterA:
                    blaster = Instantiate(gameManager.blasterA, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterB:
                    blaster = Instantiate(gameManager.blasterB, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterC:
                    blaster = Instantiate(gameManager.blasterC, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterD:
                    blaster = Instantiate(gameManager.blasterD, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterE:
                    blaster = Instantiate(gameManager.blasterE, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterF:
                    blaster = Instantiate(gameManager.blasterF, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterG:
                    blaster = Instantiate(gameManager.blasterG, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterH:
                    blaster = Instantiate(gameManager.blasterH, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterI:
                    blaster = Instantiate(gameManager.blasterI, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterJ:
                    blaster = Instantiate(gameManager.blasterJ, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterK:
                    blaster = Instantiate(gameManager.blasterK, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterL:
                    blaster = Instantiate(gameManager.blasterL, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterM:
                    blaster = Instantiate(gameManager.blasterM, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterN:
                    blaster = Instantiate(gameManager.blasterN, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterO:
                    blaster = Instantiate(gameManager.blasterO, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterP:
                    blaster = Instantiate(gameManager.blasterP, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterQ:
                    blaster = Instantiate(gameManager.blasterQ, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case CreateManager.Blaster.BlasterR:
                    blaster = Instantiate(gameManager.blasterR, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
            }
            fireForce = blaster.GetComponent<BlasterData>().bulletSpeed;
            shotsPerSecond = blaster.GetComponent<BlasterData>().fireRate;
            fireRate = 1 / shotsPerSecond;
            specialShotType = blaster.GetComponent<BlasterData>().specialShot;
        }
        // Set random data
        else
        {
            int random = Random.Range(0, 20);
            switch (random)
            {
                case 0:
                    vehicle = Instantiate(gameManager.ambulance, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 1:
                    vehicle = Instantiate(gameManager.delivery, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 2:
                    vehicle = Instantiate(gameManager.deliveryFlat, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 3:
                    vehicle = Instantiate(gameManager.fireTruck, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 4:
                    vehicle = Instantiate(gameManager.garbageTruck, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 5:
                    vehicle = Instantiate(gameManager.hatchbackSports, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 6:
                    vehicle = Instantiate(gameManager.police, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 7:
                    vehicle = Instantiate(gameManager.race, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 8:
                    vehicle = Instantiate(gameManager.raceFuture, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 9:
                    vehicle = Instantiate(gameManager.sedan, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 10:
                    vehicle = Instantiate(gameManager.sedanSports, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 11:
                    vehicle = Instantiate(gameManager.suv, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 12:
                    vehicle = Instantiate(gameManager.suvLuxury, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 13:
                    vehicle = Instantiate(gameManager.taxi, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 14:
                    vehicle = Instantiate(gameManager.tractor, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 15:
                    vehicle = Instantiate(gameManager.tractorPolice, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 16:
                    vehicle = Instantiate(gameManager.tractorShovel, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 17:
                    vehicle = Instantiate(gameManager.truck, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 18:
                    vehicle = Instantiate(gameManager.truckFlat, transform.position, transform.rotation, gameObject.transform);
                    break;
                case 19:
                    vehicle = Instantiate(gameManager.van, transform.position, transform.rotation, gameObject.transform);
                    break;
            }
            blaster.transform.localPosition = vehicle.GetComponent<VehicleData>().blasterLocation;
            firePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().firePoint;
            specialFirePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().specialFirePoint;
            minePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().minePoint;
            health.maxHealth = vehicle.GetComponent<VehicleData>().health;
            health.currentHealth = vehicle.GetComponent<VehicleData>().health;
            moveSpeed = vehicle.GetComponent<VehicleData>().speed;
            turnSpeed = vehicle.GetComponent<VehicleData>().turnSpeed;

            random = Random.Range(0, 18);
            switch (random)
            {
                case 0:
                    blaster = Instantiate(gameManager.blasterA, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 1:
                    blaster = Instantiate(gameManager.blasterB, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 2:
                    blaster = Instantiate(gameManager.blasterC, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 3:
                    blaster = Instantiate(gameManager.blasterD, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 4:
                    blaster = Instantiate(gameManager.blasterE, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 5:
                    blaster = Instantiate(gameManager.blasterF, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 6:
                    blaster = Instantiate(gameManager.blasterG, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 7:
                    blaster = Instantiate(gameManager.blasterH, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 8:
                    blaster = Instantiate(gameManager.blasterI, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 9:
                    blaster = Instantiate(gameManager.blasterJ, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 10:
                    blaster = Instantiate(gameManager.blasterK, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 11:
                    blaster = Instantiate(gameManager.blasterL, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 12:
                    blaster = Instantiate(gameManager.blasterM, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 13:
                    blaster = Instantiate(gameManager.blasterN, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 14:
                    blaster = Instantiate(gameManager.blasterO, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 15:
                    blaster = Instantiate(gameManager.blasterP, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 16:
                    blaster = Instantiate(gameManager.blasterQ, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
                case 17:
                    blaster = Instantiate(gameManager.blasterR, vehicle.transform.position, vehicle.transform.rotation, gameObject.transform);
                    break;
            }
            fireForce = blaster.GetComponent<BlasterData>().bulletSpeed;
            shotsPerSecond = blaster.GetComponent<BlasterData>().fireRate;
            fireRate = 1 / shotsPerSecond;
            specialShotType = blaster.GetComponent<BlasterData>().specialShot;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        shootTimer += Time.deltaTime;
        specialShotTimer += Time.deltaTime;
        specialShotTimer = Mathf.Clamp(specialShotTimer, 0, specialChargeTime);

        blaster.transform.localPosition = vehicle.GetComponent<VehicleData>().blasterLocation;
        firePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().firePoint;
        specialFirePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().specialFirePoint;
        minePoint.transform.localPosition = vehicle.GetComponent<VehicleData>().minePoint;

        base.Update();
    }

    // Calls Mover to move the tank forward
    public override void MoveForward()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, moveSpeed);
            if (noiseMaker != null)
            {
                noiseMaker.volumeDistance = moveNoise;
            }
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }
    }

    // Calls Mover to move the tank backward
    public override void MoveBackward()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, -moveSpeed);
            if (noiseMaker != null)
            {
                noiseMaker.volumeDistance = moveNoise;
            }
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveBackward()!");
        }
    }

    // Calls Mover to rotate the tank clockwise
    public override void Rotate(float setTurnSpeed)
    {
        if (mover != null)
        {
            mover.Rotate(setTurnSpeed);
            if (noiseMaker != null)
            {
                noiseMaker.volumeDistance = moveNoise;
            }
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.Rotate()!");
        }
    }

    public override void Shoot()
    {
        if (shootTimer >= fireRate)
        {
            shooter.Shoot(shellPrefab, firepointTransform, fireForce, damageDone, shellLifespan);
            shootTimer = 0;
            specialShotTimer -= 2;
            if (noiseMaker != null)
            {
                noiseMaker.volumeDistance = shootNoise;
            }
        }
    }

    public override void SpecialShoot()
    {
        if (specialShotTimer >= specialChargeTime)
        {
            switch (specialShotType)
            {
                case SpecialShotType.BouncyShot:
                    shooter.BouncyShot(specialShotPrefab, specialFirepointTransform, fireForce);
                    break;
                case SpecialShotType.LaserBeam:
                    //shooter.LaserBeam(specialShotPrefab, specialFirepointTransform, specialLifespan);
                    break;
                case SpecialShotType.Mine:
                    //shooter.Mine(specialShotPrefab, specialFirepointTransform, specialLifespan);
                    break;
            }
            specialShotTimer = 0;
            if (noiseMaker != null)
            {
                noiseMaker.volumeDistance = specialShotNoise;
            }
        }
    }

    public override void RotateTowards(Vector3 targetPosition, float avoidanceSpeed = 0)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        // Rotate the vehicle manually if near an obstacle
        if (avoidanceSpeed != 0)
        {
            if (avoidanceSpeed > 0) mover.Rotate(turnSpeed + avoidanceSpeed);
            else mover.Rotate(-turnSpeed + avoidanceSpeed);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = moveNoise;
        }
    }

    public override void StayStill()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 0;
        }
    }
}
