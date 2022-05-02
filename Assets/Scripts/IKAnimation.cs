using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    [SerializeField] private Transform handObject;
    [SerializeField] private Transform lookObject;

    [SerializeField] private Animator animator;

    [SerializeField] private float  rightHandWeight;
    [SerializeField] private float  leftFootWeight;

    [SerializeField] private float leftHandWeight;

    public Transform leftLowerLeg;
    public Transform leftFood;
    public LayerMask Mask;

    public Vector3 leftFoodPosition;
    public Quaternion leftFoodRotation;

    private int leftHash;


    void Start()
    {
        animator = GetComponent<Animator>();

        leftHash = Animator.StringToHash("left_foot");

        leftLowerLeg = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        leftFood = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        RaycastHit hit;
        leftFootWeight = animator.GetFloat(leftHash);

        if(Physics.Raycast(leftLowerLeg.position, Vector3.down, out hit, 1.5f, Mask))
        {
            leftFoodPosition = Vector3.Lerp(leftFood.position, hit.point + Vector3.up * 0.3f, Time.deltaTime * 10f);

            leftFoodRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFoodPosition);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFoodRotation);


        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);


        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        animator.SetLookAtWeight(1);

        if (handObject)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, handObject.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, handObject.rotation);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, handObject.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, handObject.rotation);
        }

        if (lookObject)
        {
            animator.SetLookAtPosition(lookObject.position);
        }

    }
}
