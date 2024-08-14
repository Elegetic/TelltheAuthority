using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 moveDirection = Vector3.forward;
    public float maxPosition = 33f;
    public float resetPosition = -33f;

    void Update()
    {
        transform.position += moveDirection.normalized * speed * Time.deltaTime;

        if (CheckResetCondition())
        {
            ResetPosition();
        }
    }

    private bool CheckResetCondition()
    {
        if (moveDirection.x != 0 && Mathf.Abs(transform.position.x) >= Mathf.Abs(maxPosition))
        {
            return true;
        }
        if (moveDirection.z != 0 && Mathf.Abs(transform.position.z) >= Mathf.Abs(maxPosition))
        {
            return true;
        }
        return false;
    }

    private void ResetPosition()
    {
        Vector3 newPosition = transform.position;
        if (moveDirection.x != 0)
        {
            newPosition.x = resetPosition;
        }
        if (moveDirection.z != 0)
        {
            newPosition.z = resetPosition;
        }
        transform.position = newPosition;
    }
}