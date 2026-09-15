using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;
public class MoveToGoalAgent : Agent 
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float speed = 3f;


    public override void OnEpisodeBegin()
    {
        speed = Academy.Instance.EnvironmentParameters.GetWithDefault(
            "speed", speed
        );

        transform.localPosition = Vector3.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, moveY, 0) * Time.deltaTime * speed;
        //Debug.Log(actions.ContinuousActions[0]);
        //Debug.Log(actions.ContinuousActions[1]);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;


        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -speed;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            horizontal = speed;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            vertical = -speed;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            vertical = speed;
        }
            
        continuousActions[0] = horizontal;
        continuousActions[1] = vertical;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}
