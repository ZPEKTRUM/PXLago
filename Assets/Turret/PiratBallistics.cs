using UnityEngine;

public class PiratBallistics : MonoBehaviour
{
    // Drags
    public Transform targetObj;
    public Transform Barrel;
    public Transform gunObj;

    // The bullet's initial speed in m/s
    public static float bulletSpeed = 20f;

    // The step size for numerical integration
    static float h;

    // For debugging
    private LineRenderer lineRenderer;

    void Awake()
    {
        // Set step size for numerical integration
        h = Time.fixedDeltaTime * 1f;

        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RotateGun();
        // Uncomment the line below to draw the trajectory path
        // DrawTrajectoryPath();
    }

    // Rotate the gun and the turret
    void RotateGun()
    {
        // Get the angles needed to hit the target
        float? highAngle = 0f;
        float? lowAngle = 0f;

        CalculateAngleToHitTarget(out highAngle, out lowAngle);

        // Use the high angle for artillery shots
        float angle = highAngle ?? 0f;

        // If we are within range
        if (angle != 0f)
        {
            // Rotate the gun
            gunObj.localEulerAngles = new Vector3(360f - angle, 0f, 0f);

            // Rotate the turret towards the target
            transform.LookAt(targetObj);
            transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }

    // Calculate the angles needed to hit the target
    void CalculateAngleToHitTarget(out float? theta1, out float? theta2)
    {
        // Initial speed
        float v = bulletSpeed;

        Vector3 targetVec = targetObj.position - gunObj.position;

        // Vertical distance
        float y = targetVec.y;

        // Reset y to get the horizontal distance x
        targetVec.y = 0f;

        // Horizontal distance
        float x = targetVec.magnitude;

        // Gravity
        float g = 9.81f;

        // Calculate the angles
        float vSqr = v * v;
        float underTheRoot = (vSqr * vSqr) - g * (g * x * x + 2 * y * vSqr);

        // Check if we are within range
        if (underTheRoot >= 0f)
        {
            float rightSide = Mathf.Sqrt(underTheRoot);
            float bottom = g * x;

            theta1 = Mathf.Atan2(vSqr + rightSide, bottom) * Mathf.Rad2Deg;
            theta2 = Mathf.Atan2(vSqr - rightSide, bottom) * Mathf.Rad2Deg;
        }
        else
        {
            theta1 = null;
            theta2 = null;
        }
    }

    // Display the trajectory path with a line renderer
    void DrawTrajectoryPath()
    {
        // Calculate the time to hit the target
        float timeToHitTarget = CalculateTimeToHitTarget();

        // Number of segments in the line renderer
        int maxIndex = Mathf.RoundToInt(timeToHitTarget / h);
        lineRenderer.positionCount = maxIndex;

        // Start values
        Vector3 currentVelocity = gunObj.transform.forward * bulletSpeed;
        Vector3 currentPosition = gunObj.transform.position;

        // Build the trajectory line
        for (int index = 0; index < maxIndex; index++)
        {
            lineRenderer.SetPosition(index, currentPosition);

            // Calculate the new position of the bullet
            CurrentIntegrationMethod(h, currentPosition, currentVelocity, out currentPosition, out currentVelocity);
        }
    }

    // Calculate the time to hit the target (assuming flat trajectory for simplicity)
    float CalculateTimeToHitTarget()
    {
        Vector3 targetVec = targetObj.position - gunObj.position;
        targetVec.y = 0f; // Consider horizontal distance only
        float distance = targetVec.magnitude;

        return distance / bulletSpeed;
    }

    // Use the current integration method for updating position and velocity
    public static void CurrentIntegrationMethod(float h, Vector3 currentPosition, Vector3 currentVelocity, out Vector3 newPosition, out Vector3 newVelocity)
    {
        // Use one of the integration methods here
        IntegrationMethods.BackwardEuler(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
    }
}
