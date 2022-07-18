using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Jirko.Unity.VRoidAvatarUtils;
using VRC.SDK3.Dynamics.PhysBone.Components;

namespace Tests
{
    public class PhysBonesTest
    {
        GameObject sourceObject;
        GameObject targetObject;

        [SetUp]
        public void Setup()
        {
            GameObject prefab = (GameObject)Resources.Load("SourceObject");
            sourceObject = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            prefab = (GameObject)Resources.Load("TargetObject");
            targetObject = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(sourceObject);
            GameObject.DestroyImmediate(targetObject);
        }

        [UnityTest]
        public IEnumerator PhysBonesをコピーできる()
        {
            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = false;
            sourceAvatar.physBones = true;
            sourceAvatar.physBones_hair = true;
            sourceAvatar.physBones_skirt = true;
            sourceAvatar.physBones_bust = true;
            sourceAvatar.physBones_sleeve = true;
            sourceAvatar.physBones_other = true;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            VRCPhysBone[] targetObjectPhysBones = targetObject.GetComponentsInChildren<VRCPhysBone>();

            Assert.That(targetObjectPhysBones.Length == 24);
            Assert.That(sourceAvatar.messages[0], Is.EqualTo("Phys Boneをコピー（24件）"));

            yield return null;
        }
        [UnityTest]
        public IEnumerator PhysBoneColliderをコピーできる()
        {
            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = false;
            sourceAvatar.physBones = false;
            sourceAvatar.physBones_hair = false;
            sourceAvatar.physBones_skirt = false;
            sourceAvatar.physBones_bust = false;
            sourceAvatar.physBones_sleeve = false;
            sourceAvatar.physBones_other = false;
            sourceAvatar.physBoneColiders = true;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            VRCPhysBoneCollider[] targetObjectPhysBoneColiders = targetObject.GetComponentsInChildren<VRCPhysBoneCollider>();

            Assert.That(targetObjectPhysBoneColiders.Length, Is.EqualTo(9));
            Assert.That(sourceAvatar.messages[0], Is.EqualTo("Phys Bone Coliderをコピー（9件）"));

            yield return null;
        }
    }
}
