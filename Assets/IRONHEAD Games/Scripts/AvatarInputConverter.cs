using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    public Transform mainAvatarTransform;
    public Transform avatarHead;
    public Transform avatarBody;

    public Transform leftHandTransform;
    public Transform rightHandTransform;

    public Transform xrHead;
    public Transform xrLeft;
    public Transform xrRight;

    public Vector3 headPositionOffset;
    public Vector3 handRotationOffset;

    // Update is called once per frame
    void Update()
    {
        mainAvatarTransform.position = Vector3.Lerp(mainAvatarTransform.position, xrHead.position + headPositionOffset, 0.5f);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, xrHead.rotation, 0.5f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(0, avatarHead.rotation.eulerAngles.y, 0), 0.05f);

        leftHandTransform.position = Vector3.Lerp(leftHandTransform.position, xrLeft.position, 0.5f);
        leftHandTransform.rotation = Quaternion.Lerp(leftHandTransform.rotation, xrLeft.rotation, 0.5f)*Quaternion.Euler(handRotationOffset);

        rightHandTransform.position = Vector3.Lerp(rightHandTransform.position, xrRight.position, 0.5f);
        rightHandTransform.rotation = Quaternion.Lerp(rightHandTransform.rotation, xrRight.rotation, 0.5f)*Quaternion.Euler(handRotationOffset);

    }
}
