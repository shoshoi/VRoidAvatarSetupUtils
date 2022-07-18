using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Jirko.Unity.VRoidAvatarUtils;

namespace Tests
{
    public class ConstraintsTest
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
        public IEnumerator Constraintsをコピーできる()
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
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = true;
            sourceAvatar.lookAtConstraint = true;
            sourceAvatar.parentConstraint = true;
            sourceAvatar.positionConstraint = true;
            sourceAvatar.rotationConstraint = true;
            sourceAvatar.scaleConstraint = true;
            sourceAvatar.CopyToTarget(targetObject);

            int constraintsCount = TestUtil.GetConstraintsCount(targetObject);

            Assert.That(constraintsCount, Is.EqualTo(1));
            Assert.That(sourceAvatar.messages[0], Is.EqualTo("Constraintsをコピー（1件）"));

            yield return null;
        }
    }
}
